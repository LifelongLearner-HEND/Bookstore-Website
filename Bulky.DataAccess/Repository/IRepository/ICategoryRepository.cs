using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    // will implement the functionalities in the IRepository + the two below methods
    void Update(Category obj);
    void Save();
}