using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagerBLL;
using SchoolManagerDAL;

namespace SchoolManagerBLLTests
{
    [TestClass]
    public class StudentServicePaginationTest
    {
        private IStudentService _studentService;
        private ISchoolManagerDb _schoolManagerDb;
        private PaginationInfo _defaultPagination;

        private void PaginationInfoFirstPageEquals(PaginationInfo pagination)
        {
            Assert.AreEqual(_defaultPagination.PageNumber, pagination.PageNumber);
            Assert.AreEqual(_defaultPagination.PageSize, pagination.PageSize);
        }

        public StudentServicePaginationTest()
        {
            _studentService = DependencyInjector.ServiceProvider.Value.GetService<IStudentService>();
            _defaultPagination = new PaginationInfo(1, 10, 1);
        }

        [TestMethod]
        public void HasStudents()
        {
            PaginationParameters pagination = new PaginationParameters();
            StudentWithSchoolList studentsWithPagination = _studentService.GetStudents(pagination);
            PaginationInfoFirstPageEquals(studentsWithPagination.PaginationInfo);
            Assert.IsTrue(studentsWithPagination.Students.Length > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroPageNumber()
        {
            PaginationParameters pagination = new PaginationParameters(0, PaginationParameters.MinPageSize);
            StudentWithSchoolList studentsWithPagination = _studentService.GetStudents(pagination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativePageNumber()
        {
            PaginationParameters pagination = new PaginationParameters(-1, PaginationParameters.MinPageSize);
            StudentWithSchoolList studentsWithPagination = _studentService.GetStudents(pagination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NinePageSize()
        {
            PaginationParameters pagination = new PaginationParameters(1, 9);
            StudentWithSchoolList studentsWithPagination = _studentService.GetStudents(pagination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroPageSize()
        {
            PaginationParameters pagination = new PaginationParameters(1, 0);
            StudentWithSchoolList studentsWithPagination = _studentService.GetStudents(pagination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativePageSize()
        {
            PaginationParameters pagination = new PaginationParameters(1, -9);
            StudentWithSchoolList studentsWithPagination = _studentService.GetStudents(pagination);
        }

        [TestMethod]
        public void PageNumberGreaterThanPageCount()
        {
            PaginationParameters pagination = new PaginationParameters(1, 10);
            StudentWithSchoolList studentsWithPagination = _studentService.GetStudents(pagination);
            PaginationParameters pageNumberGreaterThanPageCountPP = new PaginationParameters(studentsWithPagination.PaginationInfo.PageCount + 5, PaginationParameters.MinPageSize);
            StudentWithSchoolList studentsWithPagination2 = _studentService.GetStudents(pageNumberGreaterThanPageCountPP);
            Assert.AreEqual(studentsWithPagination.PaginationInfo.PageCount, studentsWithPagination2.PaginationInfo.PageCount);
        }
    }
}
