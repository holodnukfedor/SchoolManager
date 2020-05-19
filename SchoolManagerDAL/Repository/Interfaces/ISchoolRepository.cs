
namespace SchoolManagerDAL
{
    public interface ISchoolRepository : IRepository<School>
    {
        IdNameTuple[] GetSchoolsByNumber(string number, int count);
    }
}
