using AutoMapper;
using DocumentFormat.OpenXml.Presentation;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Resources.Employee;
using MISA.Web04.Core.Services;
using NSubstitute;
using NSubstitute.Extensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.UnitTests.Core
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        //[Test]
        //public async Task GetByIdAsync_NullEntity_ThrowNotFoundException()
        //{
        //    // Arrange
        //    var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
        //    var employeeRepository = new FakeEmployeeRepository();
        //    var departmentRepository = new FakeDepartmentRepository();
        //    var mapper = new FakeMapper();

        //    var employeeService = new EmployeeSevice(employeeRepository, mapper, departmentRepository);

        //    // Act & Assert  
           
        //    Assert.ThrowsAsync<NotFoundException>(async () => await employeeService.GetByIdAsync(id));

        //}

        //[Test]
        //public async Task GetByIdAsync_ValidEntity_ValidDto()
        //{
        //    // Arrange
        //    var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
        //    var employeeRepository = new FakeEmployeeRepositoryValid();
        //    var departmentRepository = new FakeDepartmentRepository();
        //    var mapper = new FakeMapper();
        //    var employeeService = new EmployeeSevice(employeeRepository, mapper, departmentRepository);
        //    var employee = employeeService.GetByIdAsync(id);
        //    // Act & Assert
        //    Assert.That(employeeRepository.ActualId, Is.EqualTo(id));
        //}

        [Test]
        public async Task DeleteAsync_NullEntity_ThrowNotFoundException()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var mapper = Substitute.For<IMapper>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var employeeExcel = Substitute.For<IEmployeeExcel>();

            employeeRepository.GetByIdAsync(id).ReturnsNull();
            employeeRepository.DeleteAsync(id).Returns(1);

            var employeeSerive = new EmployeeSevice(employeeRepository, mapper, departmentRepository, employeeExcel);
            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await employeeSerive.DeleteAsync(id));
        }

        [Test]
        public async Task DeleteAsync_ValidEntity_DeletedNum()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var mapper = Substitute.For<IMapper>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var employeeExcel = Substitute.For<IEmployeeExcel>();


            var employee = new Employee() { EmployeeId = id };
            employeeRepository.IsExistedIdAsync(id).Returns(true);
            employeeRepository.DeleteAsync(id).Returns(1);

            var employeeSerive = new EmployeeSevice(employeeRepository, mapper, departmentRepository, employeeExcel);
            // Act & Assert
            var actualResult = await employeeSerive.DeleteAsync(id);
            Assert.That(actualResult, Is.EqualTo(1));
            await employeeRepository.Received(1).DeleteAsync(id);
        }

        [Test]
        public async Task InsertAsync_ValidEntity_CreatedNum()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            var employeeCreatedDto = new EmployeeCreatedDto();
            var employee = new Employee()
            {
                EmployeeId = id
            };

            employeeRepository.IsDuplicateCodeAsync(employeeCreatedDto.EmployeeCode).Returns(false);
            departmentRepository.GetByIdAsync(employeeCreatedDto.DepartmentId).Returns(new Department());
            mapper.Map<Employee>(Arg.Any<EmployeeCreatedDto>()).Returns(employee);
            employeeRepository.InsertAsync(employee).Returns(1);
            var employeeExcel = Substitute.For<IEmployeeExcel>();


            var employeeService = new EmployeeSevice(employeeRepository, mapper, departmentRepository, employeeExcel);

            // Act & Assert
            var actualResult = await employeeService.InsertAsync(employeeCreatedDto);
            Assert.That(actualResult, Is.EqualTo(1));
            await employeeRepository.Received(1).InsertAsync(employee);
        }

        [Test]
        public async Task UpdateAsync_ValidEntity_UpdatedNum()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
            var departmentId = Guid.Parse("1e5ce342-2eec-79a4-5380-7e19d1ea16ea");

            var employeeUpdatedDto = new EmployeeUpdatedDto();
            var employee = new Employee()
            {
                EmployeeId = id
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var mapper = Substitute.For<IMapper>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();

            employeeRepository.IsExistedIdAsync(id).Returns(true);
            employeeRepository.IsDuplicateCodeAsync(employeeUpdatedDto.EmployeeCode).Returns(false);
            departmentRepository.GetByIdAsync(employeeUpdatedDto.DepartmentId).Returns(new Department());
            mapper.Map<Employee>(Arg.Any<EmployeeUpdatedDto>()).Returns(employee);
            employeeRepository.UpdateAsync(employee,id).Returns(1);
            var employeeExcel = Substitute.For<IEmployeeExcel>();


            var employeeService = new EmployeeSevice(employeeRepository, mapper, departmentRepository, employeeExcel);

            var actualResult = await employeeService.UpdateAsync(employeeUpdatedDto, id);
            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(1));
            await employeeRepository.Received(1).UpdateAsync(employee, id);
        }

        [Test]
        public async Task UpdateAsync_InValidDepartment_ThrowNotFoundException()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
            var departmentId = Guid.Parse("1e5ce342-2eec-79a4-5380-7e19d1ea16ea");

            var employeeUpdatedDto = new EmployeeUpdatedDto();
            var employee = new Employee()
            {
                EmployeeId = id
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var mapper = Substitute.For<IMapper>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();

            employeeRepository.IsExistedIdAsync(id).Returns(false);
            employeeRepository.IsDuplicateCodeAsync(employeeUpdatedDto.EmployeeCode).Returns(false);
            departmentRepository.GetByIdAsync(employeeUpdatedDto.DepartmentId).Returns(new Department());
            var employeeExcel = Substitute.For<IEmployeeExcel>();

            mapper.Map<Employee>(Arg.Any<EmployeeUpdatedDto>()).Returns(employee);
            employeeRepository.UpdateAsync(employee, id).Returns(1);

            var employeeService = new EmployeeSevice(employeeRepository, mapper, departmentRepository, employeeExcel);

     
            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await employeeService.UpdateAsync(employeeUpdatedDto, id), "id không tồn tại");
        }


    }
}
