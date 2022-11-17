using AutoMapper;
using Domain;
using Domain.Models;
using Domain.UnitOfWork;
using Domain.ViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;
using WebAPI.Services.CloudStorageService;

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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.ProductRepository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("GetProductPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingParameters param)
        {
            var data = await _repository.ProductRepository.GetAllPaging(param);
            return Ok(data);
        }

        [HttpGet("GetHotProduct")]
        public async Task<IActionResult> GetHotProduct([FromQuery] PagingParameters param)
        {
            var data = await _repository.ProductRepository.GetAllPaging(param);
            return Ok(data);
        }

        [HttpGet("GetProductByCategory/{cateId}")]
        public async Task<IActionResult> GetProductByCategoryId(int cateId)
        {
            var products = _repository.ProductRepository.GetAll(x => x.CategoryId == cateId);
            return Ok(products);
        }

        [HttpGet("GetProductByCriteria")]
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
            var existItem = await _repository.ProductRepository.GetAsync(x => x.Id == id);
            if (existItem == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> UploadFile(int id, [FromForm]UploadFileModel model)
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

        [HttpPost("import")]
        public async Task<IActionResult> ImportProduct([FromForm] IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return NotFound();
            }
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }
    
            HandleState hs = await _repository.ProductRepository.ImportProduct(formFile);

            return Ok(hs);
        }
        [HttpPost("export")]
        public async Task<IActionResult> ExportProduct([FromForm] IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return NotFound();
            }
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }

            HandleState hs = await _repository.ProductRepository.ImportProduct(formFile);

            return Ok(hs);
        }
    }
}
    