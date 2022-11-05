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

        public Task<ProductViewModel> GetAllPaging(PagingParameters param)
        {
            throw new NotImplementedException();
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
