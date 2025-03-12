using Bulky.Models.Models;
using BulkyWeb.Models;

namespace Bulky.DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    // will implement the functionalities in the IRepository + the two below methods
    void Update(Product obj);
    void Save();
}