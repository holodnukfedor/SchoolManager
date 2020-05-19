using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagerDAL;
using SchoolManagerBLL;

namespace SchoolManagerBLLTests
{
    [TestClass]
    public class StudentServiceEditTest
    {
        private IStudentService _studentService;
        private StudentDTO _testStudent;
        private string _51LengthStr;

        private String GetStringMoreThan(int strLength)
        {
            const char testChar = 'a';
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < strLength + 1; i++)
            {
                builder.Append(testChar);
            }

            return builder.ToString();
        }

        public StudentServiceEditTest()
        {
            _studentService = DependencyInjector.ServiceProvider.Value.GetService<IStudentService>();
            _51LengthStr = GetStringMoreThan(50);
        }

        [TestInitialize]
        public void Setup()
        {
            StudentDTO newStudent = new StudentDTO(0, "Федор", "Холоднюк", "Ярославович", "1234567890", 1);
            _testStudent = _studentService.Create(newStudent);
        }

        [TestMethod]
        public void EditStudent()
        {
            _testStudent.FirstName = "Павел";
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
            Assert.AreEqual(_testStudent.FirstName, editedStudent.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoFirstName()
        {
            _testStudent.FirstName = String.Empty;
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooBigFirstName()
        {
            _testStudent.FirstName = _51LengthStr;
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoLastName()
        {
            _testStudent.LastName = null;
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
            Assert.AreEqual(_testStudent.FirstName, editedStudent.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooBigLastName()
        {
            _testStudent.LastName = _51LengthStr;
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoPatronymic()
        {
            _testStudent.Patronymic = String.Empty;
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
            Assert.AreEqual(_testStudent.FirstName, editedStudent.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooBigPatronymic()
        {
            _testStudent.Patronymic = _51LengthStr;
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoPhone()
        {
            _testStudent.Phone = String.Empty;
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PhoneNotNumbers()
        {
            _testStudent.Phone = "abcabcabcd";
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PhoneTooManyNumbers()
        {
            _testStudent.Phone = "1234567890111";
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PhoneLittleNumbers()
        {
            _testStudent.Phone = "12345";
            _studentService.Edit(_testStudent);
            StudentWithSchool editedStudent = _studentService.GetStudent(_testStudent.Id);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _studentService.Delete(_testStudent.Id);
        }
    }
}
