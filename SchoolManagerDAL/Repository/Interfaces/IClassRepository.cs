
namespace SchoolManagerDAL
{
    public interface IClassRepository : IRepository<Class>
    {
        IdNameTuple[] GetSchoolClassesByName(int schoolId, string name, int count);
    }
}
