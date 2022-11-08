using Domain.Models;
using Domain.ViewModel;

namespace Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ProductViewModel> GetById(int id);
        Task<PagedResponse<List<Product>>> GetAllPaging(PagingParameters param);
        Task<PagedResponse<List<Product>>> GetHotProduct(PagingParameters param);
        Task<IQueryable<ProductViewModel>> GetProductByCriteria(ProductCriteria criteria);
    }
}
