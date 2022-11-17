using Domain.Models;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ProductViewModel> GetById(int id);
        Task<PagedResponse<List<Product>>> GetAllPaging(PagingParameters param);
        Task<PagedResponse<List<Product>>> GetHotProduct(PagingParameters param);
        Task<IQueryable<ProductViewModel>> GetProductByCriteria(ProductCriteria criteria);
        Task<HandleState> UploadFile(int id, UploadFileModel model);

        Task<HandleState> ImportProduct(IFormFile file);
    }
}
