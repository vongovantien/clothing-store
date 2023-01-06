using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WebAPI;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(myDBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<HandleState> AddNew(ProductModel model)
        {
            var newItem = _mapper.Map<Product>(model);
            newItem.CreatedAt = newItem.UpdatedAt = newItem.PublishedAt = DateTime.Now;
            await _dbContext.Products.AddAsync(newItem);
            _dbContext.SaveChanges();
            var hs = new HandleState();
            return hs;
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

        public async Task<List<ProductImportModel>> UploadFileImport(IFormFile file)
        {
            var listProduct = new List<ProductImportModel>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        var categoryString = worksheet.Cells[row, 5].Value?.ToString().Trim();
                        var category = await _dbContext.Categories.FirstAsync(x => x.Name.Contains(categoryString));

                        listProduct.Add(new ProductImportModel
                        {
                            Code = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                            Title = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                            Price = int.Parse(worksheet.Cells[row, 4].Value?.ToString().Trim()),
                            CategoryId = category.Id,
                            Description = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                            Discount = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                            Quantity = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                            CreatedAt = DateTime.Now,
                            PublishedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        });
                    }
                }

                var data = await CheckValidImport(listProduct);
                var totalValidRows = data.Count(x => x.IsValid == true);
                var results = new { data, totalValidRows };
                //return results
                //await _dbContext.AddRangeAsync(listItem);
                //await _dbContext.SaveChangesAsync();
                return listProduct;
            }
        }

        public async Task<HandleState> ProductImport(UploadFileModel model)
        {
            var hs = new HandleState();
            return hs;
        }

        private async Task<List<ProductImportModel>> CheckValidImport(List<ProductImportModel> listModel)
        {
            var listProduct = new List<ProductImportModel>();
            return listProduct;
        }

        public Task<HandleState> Uploadfile(ProductCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public Task<HandleState> ImportProduct(UploadFileModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ProductViewModel>> ExportProduct()
        {
            var listProduct = _dbContext.Products.AsNoTracking();

            var listItem = _mapper.Map<IQueryable<ProductViewModel>>(listProduct);
            return listItem;
        }

        Task<HandleState> IProductRepository.ExportProduct()
        {
            throw new NotImplementedException();
        }
    }
}
