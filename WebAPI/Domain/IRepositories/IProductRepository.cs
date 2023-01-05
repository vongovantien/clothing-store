using Domain.Models;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using WebAPI;

namespace Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ProductViewModel> GetById(int id);
        Task<HandleState> AddNew(ProductModel model);
        Task<PagedResponse<List<Product>>> GetAllPaging(PagingParameters param);
        Task<PagedResponse<List<Product>>> GetHotProduct(PagingParameters param);
        Task<IQueryable<ProductViewModel>> GetProductByCriteria(ProductCriteria criteria);
        Task<HandleState> UploadFile(int id, UploadFileModel file);
        Task<List<ProductImportModel>> UploadFileImport(IFormFile file);
        Task<HandleState> ImportProduct(UploadFileModel model);
        Task<HandleState> ExportProduct();
    }
}
