using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using System.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MISA.Web04.Core.Resources.Employee;
using AutoMapper;
using System.Text.RegularExpressions;
using MISA.Web04.Core.Dto.Employee;

namespace MISA.Web04.Core.Services
{
    public class EmployeeSevice : BaseService<Employee, EmployeeDto, EmployeeCreatedDto, EmployeeUpdatedDto>, IEmployeeService
    {

        IEmployeeRepository _employeeRepository;
        IDepartmentRepository _departmentRepository;
        IEmployeeExcel _employeeExcel;

        #region Constructor
        public EmployeeSevice(IEmployeeRepository employeeRepository, IMapper mapper, IDepartmentRepository departmentRepository, IEmployeeExcel employeeExcel) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _employeeExcel = employeeExcel;
        }
        #endregion

        #region Methods

        public override string TestMethod()
        {
            return "Employee";
        }

        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>số lượng bị xóa</returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created by: ttanh (30/06/2023)
        public override async Task<int> DeleteAsync(Guid employeeId)
        {
            bool isExisted = await _employeeRepository.IsExistedIdAsync(employeeId);
            if (!isExisted)
            {
                throw new NotFoundException(new List<string> { EmployeeVN.INVALID_ID });
            }
            int result = await base.DeleteAsync(employeeId);
            return result;
        }

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">danh sách id cần xóa</param>
        /// <returns>số lượng bị xóa</returns>
        /// Created by: ttanh (30/06/2023)
        public override async Task<List<Guid>> DeleteMultipleAsync(List<Guid> ids)
        {
            List<Guid> invalidIds = new List<Guid>();

            // kiểm tra mã tồn tại
            foreach (var id in ids)
            {
                bool isExisted = await _employeeRepository.IsExistedIdAsync(id);
                if (!isExisted)
                {
                    invalidIds.Add(id);
                }
            }

            await _employeeRepository.DeleteMultipleAsync(ids);

            // kiểm tra những mã tồn tại mà không bị xóa
            foreach (var id in ids)
            {
                if (!invalidIds.Contains(id))
                {
                    bool isExisted = await _employeeRepository.IsExistedIdAsync(id);

                    if (isExisted)
                    {
                        invalidIds.Add(id);
                    }
                }
            }


            return invalidIds;
        }

        /// <summary>
        /// lấy danh sách nhân viên
        /// </summary>
        /// <param name="querySearch">từ cần tìm kiếm</param>
        /// <param name="recordsPerPage">số lượng bản ghi 1 trang</param>
        /// <param name="page">trang thứ mấy</param>
        /// <returns>danh sách bản ghi</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<(int, IEnumerable<EmployeeDto>)> GetListAsync(string? querySearch, int? recordsPerPage, int? page)
        {
            IEnumerable<Employee> employees = new List<Employee>();
            int totalRecord = 0;
            (totalRecord, employees) = await _employeeRepository.GetListAsync(querySearch, recordsPerPage, page);
            IEnumerable<EmployeeDto> employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return (totalRecord, employeeDtos);
        }

        /// <summary>
        /// thêm mới nhân viên
        /// </summary>
        /// <param name="employeeDto">dto nhân viên</param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        /// Created by: ttanh (30/06/2023)
        public override async Task<int> InsertAsync(EmployeeCreatedDto employeeDto)
        {
            var errorsList = await ListErrors(employeeDto);

            if (errorsList.Count > 0)
            {
                throw new ValidateException(errorsList);
            }

            // insert dữ liệu

            var result = await base.InsertAsync(employeeDto);

            return result;

        }

        /// <summary>
        /// cập nhật bản ghi
        /// </summary>
        /// <param name="employeeDto">dto nhân viên</param>
        /// <param name="employeeId">id nhân viên</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">không tìm thấy</exception>
        /// <exception cref="ValidateException">dữ liệu không hợp lệ</exception>
        /// Created by: ttanh (30/06/2023)
        public override async Task<int> UpdateAsync(EmployeeUpdatedDto employeeDto, Guid employeeId)
        {
            //validate dữ liệu

            // kiểm tra id tồn tại
            bool isExisted = await _employeeRepository.IsExistedIdAsync(employeeId);
            if (!isExisted)
            {
                throw new NotFoundException(EmployeeVN.INVALID_ID);
            }
            var errorsList = await ListErrors(employeeDto, employeeId);

            if (errorsList.Count > 0)
            {
                throw new ValidateException(errorsList);
            }


            // update dữ liệu
            var result = await base.UpdateAsync(employeeDto, employeeId);
            return result;
        }


        

        /// <summary>
        /// validate dữ liệu
        /// </summary>
        /// <param name="employeeCreatedDto">dto nhân viên thêm mới</param>
        /// <returns>list các lỗi</returns>
        /// Created by: ttanh (30/06/2023)
        private async Task<Dictionary<string, List<string>>> ListErrors(EmployeeCreatedDto employeeCreatedDto)
        {
            Dictionary<string, List<string>> errorsList = new Dictionary<string, List<string>>();
            //validate dữ liệu
            var isDuplicateCode = await _employeeRepository.IsDuplicateCodeAsync(employeeCreatedDto.EmployeeCode);

            // kiểm tra trùng mã
            if (isDuplicateCode)
            {
                errorsList.Add("EmployeeCode", new List<string>() { string.Format(EmployeeVN.INVALID_CODE, employeeCreatedDto.EmployeeCode) });
            }

            // kiểm tra mã phòng ban
            var department = await _departmentRepository.GetByIdAsync(employeeCreatedDto.DepartmentId);
            if (department == null)
            {
                errorsList.Add("DepartmentId", new List<string>() { EmployeeVN.INVALID_DEPARTMENT });

            }

          

            return errorsList;
        }

        /// <summary>
        /// validate dữ liệu
        /// </summary>
        /// <param name="employeeUpdatedDto"></param>
        /// <returns>list các lỗi</returns>
        /// Created by: ttanh (30/06/2023)
        private async Task<Dictionary<string, List<string>>> ListErrors(EmployeeUpdatedDto employeeUpdatedDto, Guid employeeId)
        {
            Dictionary<string, List<string>> errorsList = new Dictionary<string, List<string>>();
            //validate dữ liệu
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            bool isDuplicateCode = false;
            if (employee.EmployeeCode != employeeUpdatedDto.EmployeeCode)
            {
                isDuplicateCode = await _employeeRepository.IsDuplicateCodeAsync(employeeUpdatedDto.EmployeeCode);
            }

            // kiểm tra trùng mã
            if (isDuplicateCode)
            {
                errorsList.Add("EmployeeCode", new List<string>() { string.Format(EmployeeVN.INVALID_CODE,employeeUpdatedDto.EmployeeCode) });
            }

            var department = await _departmentRepository.GetByIdAsync(employeeUpdatedDto.DepartmentId);
            if (department == null)
            {
                errorsList.Add("DepartmentId", new List<string>() { EmployeeVN.INVALID_DEPARTMENT });

            }


            return errorsList;
        }

        /// <summary>
        /// tạo mã mới
        /// </summary>
        /// <returns>mã mới</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<string> GenerateCode()
        {
            /*
             * 1. cắt bỏ NV-
             * 2. loại bỏ số 0 ở đầu
             * 3.cộng thêm 1
             */

            string maxCode = await _employeeRepository.GetMaxCode();
            if (maxCode == null)
            {
                return "NV-1";
            }

            // cắt bỏ NV-
            string numStr = maxCode.Substring(3);

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

            int num = Int32.Parse(numStr);

            ++num;
            numStr = num.ToString();

            // Thêm lại số 0 đã bị xóa
            string newCode = "";
            for (int i = 0; i < countFirstZero; i++)
            {
                newCode += '0';
            }

            newCode = "NV-" + newCode + numStr;

            return newCode;


        }

        /// <summary>
        /// xuất file excel
        /// </summary>
        /// <returns>dữ liệu excel</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<MemoryStream> GetEmployeeExcelFile()
        {

            var employees = await _employeeRepository.GetAllAsync();

            var employeeExcel = _mapper.Map<IEnumerable<EmployeeExcelDto>>(employees);

            var excelData = _employeeExcel.GetEmployeeExcel(employeeExcel);

            return excelData;
        } 
        #endregion
    }
}
