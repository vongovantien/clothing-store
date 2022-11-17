using AutoMapper;
using Domain;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

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

        public async Task<HandleState> UploadFile(int id, UploadFileModel model)
        {
            var hs = new HandleState();
            var existItem = await _dbContext.Products.FirstAsync(x => x.Id == id);

            var newImage = new ProductImage();
            newImage.Url = model.SavedUrl;
            newImage.ProductId = existItem.Id;
            await _dbContext.ProductImages.AddAsync(newImage);
            await _dbContext.SaveChangesAsync();
            return hs;
        }
        public async Task<HandleState> ImportProduct(IFormFile file)
        {
            var hs = new HandleState();
            var listItem = new List<Product>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        listItem.Add(new Product
                        {
                            Title = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Price = int.Parse(worksheet.Cells[row, 2].Value.ToString().Trim()),
                        });
                    }
                }
            }
            return hs;
        }
    }
}
