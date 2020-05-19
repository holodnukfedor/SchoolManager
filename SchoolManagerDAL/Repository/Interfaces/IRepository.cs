
namespace SchoolManagerDAL
{
    public interface IRepository<T>
    {
        public void Edit(T model);
        public int Create(T model);
        public void Delete(int id);
    }
}
