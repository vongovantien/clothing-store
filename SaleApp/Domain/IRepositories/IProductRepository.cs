using Domain.Models;
using Domain.ViewModel;

namespace Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ProductViewModel> GetById(int id);
        Task<ProductViewModel> GetAllPaging(PagingParameters param);
        Task<IQueryable<ProductViewModel>> GetProductByCriteria(ProductCriteria criteria);
    }
}
