using AutoMapper;
using MISA.Web04.Core.Dto.Receipts;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Helpers;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using MISA.Web04.Core.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Services
{
    public class ReceiptService : BaseService<Receipt, ReceiptDto, ReceiptCreatedDto, ReceiptUpdatedDto>, IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IUnitOfWork _uow;
        private readonly IReceiptValidation _receiptValidation;
        private readonly IAccountantRepository _accountantRepository;
        private readonly IReceiptExcel _receiptExcel;

        public ReceiptService(IUnitOfWork uow,IReceiptExcel receiptExcel ,IReceiptValidation receiptValidation, IReceiptRepository receiptRepository, IAccountantRepository accountantRepository, IMapper mapper) : base(receiptRepository, mapper)
        {
            _uow = uow;
            _receiptValidation = receiptValidation;
            _receiptRepository = receiptRepository;
            _accountantRepository = accountantRepository;
            _receiptExcel = receiptExcel;
        }

        public async Task<(int, IEnumerable<ReceiptDto>)> GetFilter(int pageSize, int pageIndex, string? querySearch, bool? type)
        {
            var (total, receipts) = await _receiptRepository.GetFilter(pageSize, pageIndex, querySearch, type);

            var receiptsDto = _mapper.Map<IEnumerable<ReceiptDto>>(receipts);
            return (total, receiptsDto);
        }

        public new async Task<Guid> InsertAsync(ReceiptCreatedDto receiptCreatedDto)
        {
            bool isValidConditionToNote = true;
            var conditionException = new ValidateException();
            try
            {
                await _uow.BeginTransactionAsync();

                var accountants = _mapper.Map<List<Accountant>>(receiptCreatedDto.Accountants);
                _receiptValidation.CheckEmtpyAccountants(accountants);

                await _receiptValidation.CheckDuplicatedCodeAsync(receiptCreatedDto.ReceiptCode, null);
                _receiptValidation.CheckValidDate(receiptCreatedDto.DateAccounting, receiptCreatedDto.ReceiptDate);

                var receipt = _mapper.Map<Receipt>(receiptCreatedDto);

                Helper.ConvertPropertiesToJson(receipt);


                if (receiptCreatedDto.IsNoted == true)
                {
                    try
                    {
                        await _receiptValidation.CheckConditionToNoteAsync(receipt);

                    }
                    catch (ValidateException ex)
                    {
                        conditionException.ErrorMsgs.Add(ex.ErrorMsgs.Keys.ElementAt(0), ex.ErrorMsgs.Values.ElementAt(0));
                        receipt.IsNoted = false;
                        isValidConditionToNote = false;

                    }

                }

                var properties = receipt.GetType().GetProperties();


                foreach (var property in properties)
                {
                    var name = property.Name;
                    if (name == $"ReceiptId")
                    {
                        property.SetValue(receipt, Guid.NewGuid());
                    }
                    else if (name == $"CreatedDate")
                    {
                        property.SetValue(receipt, DateTime.Now);
                    }
                    else if (name == $"CreatedBy")
                    {
                        property.SetValue(receipt, null);
                    }
                }

                receipt.Accountants = null;

                receipt.Employee = null;

                await _receiptRepository.InsertAsync(receipt);

                if (accountants != null && accountants.Count > 0)
                {
                    foreach (var accountant in accountants)
                    {
                        accountant.ReceiptId = receipt.ReceiptId;
                        accountant.AccountantId = Guid.NewGuid();

                    }

                    await _accountantRepository.InsertListAsync(accountants);
                }

                await _uow.CommitAsync();
                if (isValidConditionToNote == false)
                {

                    conditionException.Data = receipt.ReceiptId.ToString();
                }
                return receipt.ReceiptId;

            }
            catch (Exception ex)
            {

                await _uow.RollbackAsync();
                throw ex;
            }
            finally
            {
                if (isValidConditionToNote == false)
                {
                    throw conditionException;
                }
            }
        }

        public override async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                await _uow.BeginTransactionAsync();

                await _receiptValidation.CheckExistIdAsync(id);

                await _accountantRepository.DeleteByParentIdAsync(id);

                var result = await _receiptRepository.DeleteAsync(id);

                await _uow.CommitAsync();

                return result;
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw ex;
            }
        }


        public override async Task<int> UpdateAsync(ReceiptUpdatedDto receiptUpdatedDto, Guid id)
        {
            bool isValidConditionToNote = true;
            var conditionException = new ValidateException();
            try
            {
                await _uow.BeginTransactionAsync();
                // validate
                _receiptValidation.CheckValidDate(receiptUpdatedDto.DateAccounting, receiptDate: receiptUpdatedDto.ReceiptDate);

                var accountants = _mapper.Map<List<Accountant>>(receiptUpdatedDto.Accountants);
                _receiptValidation.CheckEmtpyAccountants(accountants);
                await _receiptValidation.CheckExistIdAsync(id);
                await _receiptValidation.CheckDuplicatedCodeAsync(receiptUpdatedDto.ReceiptCode, id);

                var listRemoveAccountant = new List<Accountant>();
                var listNewAccountant = new List<Accountant>();
                var listUpdateAccountant = new List<Accountant>();

                foreach (var accountant in receiptUpdatedDto.Accountants)
                {
                    accountant.ReceiptId = id;
                    if (accountant.Flag == -1)
                    {
                        listRemoveAccountant.Add(_mapper.Map<Accountant>(accountant));
                    }
                    else if (accountant.Flag == 1)
                    {
                        listNewAccountant.Add(_mapper.Map<Accountant>(accountant));
                    }
                    else if (accountant.Flag == 0)
                    {
                        listUpdateAccountant.Add(_mapper.Map<Accountant>(accountant));
                    }

                }

                // xử lý hạch toán
                if (listRemoveAccountant.Count > 0)
                {
                    var listRemoveIds = listRemoveAccountant.Select(a => a.AccountantId).ToList();
                    await _accountantRepository.DeleteMultipleAsync(listRemoveIds);

                }
                if (listNewAccountant.Count > 0)
                {
                    await _accountantRepository.InsertListAsync(listNewAccountant);

                }
                if (listUpdateAccountant.Count > 0)
                {

                    await _accountantRepository.UpdateListAsync(listUpdateAccountant);
                }


                //  receipt
                var receipt = _mapper.Map<Receipt>(receiptUpdatedDto);
                int result = 0;
                if (receiptUpdatedDto.IsNoted == true)
                {
                    try
                    {
                        await _receiptValidation.CheckConditionToNoteAsync(receipt);

                    }
                    catch (ValidateException ex)
                    {
                        conditionException.ErrorMsgs.Add(ex.ErrorMsgs.Keys.ElementAt(0), ex.ErrorMsgs.Values.ElementAt(0));
                        receipt.IsNoted = false;
                        isValidConditionToNote = false;

                    }

                }

                //await _accountantRepository.DeleteByParentIdAsync(id);

                //foreach (var accountant in receiptUpdatedDto.Accountants)
                //{
                //    accountant.AccountBalanceCode = null;
                //    accountant.AccountDebtCode = null;
                //    accountant.AccountantId = Guid.NewGuid();
                //    accountant.ReceiptId = id;

                //}

                //if (receiptUpdatedDto.Accountants != null && receiptUpdatedDto.Accountants.Count > 0)
                //{
                //    await _accountantRepository.InsertListAsync(receiptUpdatedDto.Accountants);

                //}

                receipt.Accountants = null;

                receipt.Employee = null;
                result = await _receiptRepository.UpdateAsync(receipt, id);



                await _uow.CommitAsync();

                return result;

            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw ex;
            }
            finally
            {
                if (isValidConditionToNote == false)
                {
                    throw conditionException;
                }
            }




        }

        public override async Task<List<Guid>> DeleteMultipleAsync(List<Guid> ids)
        {
            try
            {
                await _uow.BeginTransactionAsync();
                var deleteIds = await _receiptRepository.GetByListId(ids, false);
                var noDeleteIds = ids.Where(id => !deleteIds.Contains(id));

                await _accountantRepository.DeleteMultipleByParentIdsAsync(deleteIds.ToList());
                var result = await _receiptRepository.DeleteMultipleAsync(deleteIds.ToList());

                await _uow.CommitAsync();
                return noDeleteIds.ToList();

            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw ex;
            }
        }

        /// <summary>
        /// tạo mã mới
        /// </summary>
        /// <returns>mã mới</returns>
        /// Created by: ttanh (07/08/2023)
        public async Task<string> GenerateCode()
        {
            /*
             * 1. cắt bỏ NV-
             * 2. loại bỏ số 0 ở đầu
             * 3.cộng thêm 1
             */

            string maxCode = await _receiptRepository.GetMaxCode();
            if (maxCode == null)
            {
                return "PC00001";
            }

            // cắt bỏ NV-
            string numStr = maxCode.Substring(2);

            int countFirstZero = 0;

            // loại bỏ 0 ở đầu
            for (int i = 0; i < numStr.Length - 1; i++)
            {
                if (numStr[i] == '0')
                {
                    ++countFirstZero;
                }
                else
                {
                    break;
                }
            }

            if (countFirstZero > 0)
            {
                numStr = numStr.Substring(countFirstZero);
            }

            long num = Int64.Parse(numStr);

            ++num;
            numStr = num.ToString();

            // Thêm lại số 0 đã bị xóa
            string newCode = "";
            for (int i = 0; i < countFirstZero; i++)
            {
                newCode += '0';
            }

            newCode = "PC" + newCode + numStr;

            return newCode;


        }

        public async Task<MemoryStream> GetReceiptExcel(string? querySearch)
        {
            var receipt = await _receiptRepository.GetListByKeySearchAsync(querySearch);

            var receitpExcel = _mapper.Map<IEnumerable<ReceiptExcelDto>>(receipt);

           var memoryStream =   _receiptExcel.GetReceiptExcel(receitpExcel);

            return memoryStream;
        }
    }
}
