using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.UnitOfWork;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var result = await _repository.CategoryRepository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _repository.CategoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        //// PUT: api/Category/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryViewModel model)
        {
            var data = await _repository.CategoryRepository.GetAsync(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            var newItem = _mapper.Map<Category>(model);
            _repository.CategoryRepository.Update(newItem);

            try
            {
                await _repository.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryViewModel model)
        {
            var newtem = _mapper.Map<Category>(model);
            await _repository.CategoryRepository.AddAsync(newtem);
            await _repository.CommitAsync();
            return CreatedAtAction("GetCategory", new { id = newtem.Id }, model);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _repository.CategoryRepository.GetAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _repository.CategoryRepository.Remove(category);
            await _repository.CommitAsync();

            return NoContent();
        }
    }
}
