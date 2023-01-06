using AutoMapper;
using Domain;
using Domain.Models;
using Domain.UnitOfWork;
using Domain.ViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using WebAPI.Extensions;
using WebAPI.Services.CloudStorageService;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using Domain.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ICloudStorageService cloudStorageService;
        private IUnitOfWork _repository;
        private IMapper _mapper;
        public ProductController(IUnitOfWork repository, IMapper mapper, ICloudStorageService cloudStorage)
        {
            _repository = repository;
            _mapper = mapper;
            cloudStorageService = cloudStorage;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.ProductRepository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("getProductPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingParameters param)
        {
            var data = await _repository.ProductRepository.GetAllPaging(param);
            return Ok(data);
        }

        [HttpGet("getHotProduct")]
        public async Task<IActionResult> GetHotProduct([FromQuery] PagingParameters param)
        {
            var data = await _repository.ProductRepository.GetAllPaging(param);
            return Ok(data);
        }

        [HttpGet("getProductByCategory/{cateId}")]
        public async Task<IActionResult> GetProductByCategoryId(int cateId)
        {
            var products = _repository.ProductRepository.GetAll(x => x.CategoryId == cateId);
            return Ok(products);
        }

        [HttpGet("getProductByCriteria")]
        public async Task<IActionResult> GetProductByCriteria(ProductCriteria criteria)
        {
            var products = _repository.ProductRepository.GetProductByCriteria(criteria);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product model)
        {
            HandleState hs = new HandleState();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newItem = _mapper.Map<Product>(model);
            try
            {
                newItem.CreatedAt = DateTime.UtcNow;
                newItem.UpdatedAt = DateTime.UtcNow;
                await _repository.ProductRepository.AddAsync(newItem);
                await _repository.CommitAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(hs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _repository.ProductRepository.GetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var hs = new HandleState();
            var existItem = await _repository.ProductRepository.GetAsync(x => x.Id == id);
            if (existItem == null)
            {
                return NotFound();
            }
            
            //_repository.ProductRepository.Remove(existItem);
            _repository.ProductRepository.Remove(existItem);
            await _repository.CommitAsync();
            return Ok();
        }

        private string GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] UploadFileModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.file != null)
                {
                    model.SavedFileName = GenerateFileNameToSave(model.file.FileName);
                    model.SavedUrl = await cloudStorageService.UploadFileAsync(model.file, model.SavedFileName);
                }
            }
            HandleState hs = await _repository.ProductRepository.UploadFile(id, model);

            return Ok(hs);
        }

        [HttpPost("uploadFileImport")]
        public async Task<IActionResult> UploadFileImport(IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return NotFound();
            }
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }
    
            var listItem = await _repository.ProductRepository.UploadFileImport(formFile);

            return Ok(listItem);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportProduct()
        {
            //if (formFile == null || formFile.Length <= 0)
            //{
            //    return NotFound();
            //}
            //if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            //{
            //    return BadRequest();
            //}
            var products = await _repository.ProductRepository.GetAllAsync();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table  
            //  
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = "S.No";
            workSheet.Cells[1, 2].Value = "Id";
            workSheet.Cells[1, 3].Value = "Name";
            workSheet.Cells[1, 4].Value = "Address";
            //Body of table  
            //  
            int recordIndex = 2;
            foreach (var product in products)
            {
                workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                workSheet.Cells[recordIndex, 2].Value = product;
                workSheet.Cells[recordIndex, 3].Value = product.Title;
                workSheet.Cells[recordIndex, 4].Value = product.Description;
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            using (var memoryStream = new MemoryStream())
            {
                excel.SaveAs(memoryStream);
                var content = memoryStream.ToArray();

                return File(
                    content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "productExport.xlsx");
            }
        }
    }
}
    