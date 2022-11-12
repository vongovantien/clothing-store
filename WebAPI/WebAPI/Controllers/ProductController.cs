using AutoMapper;
using Domain.Models;
using Domain.UnitOfWork;
using Domain.ViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IUnitOfWork _repository;
        private IMapper _mapper;
        public ProductController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
