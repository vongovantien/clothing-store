using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(myDBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<Product>>> GetAllPaging(PagingParameters param)
        {
            var pagedData = await _dbContext.Products.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync();
            var totalCount = await _dbContext.Products.CountAsync();
            return new PagedResponse<List<Product>>(pagedData, param.PageNumber, param.PageSize, totalCount);
        }

        public async Task<PagedResponse<List<Product>>> GetHotProduct(PagingParameters param)
        {
            var pagedData = await _dbContext.Products.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync();
            var totalCount = await _dbContext.Products.CountAsync();
            return new PagedResponse<List<Product>>(pagedData, param.PageNumber, param.PageSize, totalCount);
        }

        public async Task<ProductViewModel> GetById(int id)
        {
            Product data = await _dbContext.Products.FirstAsync(x => x.Id == id);
            return _mapper.Map<ProductViewModel>(data);
        }

        public Task<IQueryable<ProductViewModel>> GetProductByCriteria(ProductCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
