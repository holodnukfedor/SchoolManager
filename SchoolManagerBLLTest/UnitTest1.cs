using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagerBLL;
using SchoolManagerDAL;

namespace SchoolManagerBLLTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var db = new SchoolManagerDb("Data Source=DESKTOP-UUK4JM4\\SQLEXPRESS;Initial Catalog=SchoolManager;Integrated Security=True");
            var studentsService = new StudentService(db);
            var students = studentsService.GetStudents(new PaginationParameters(1, 10));
        }
    }
}
