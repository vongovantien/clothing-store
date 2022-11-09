using AutoMapper;
using Domain.Models;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        ////private readonly IStringLocalizer<PostsController> stringLocalizer;
        private readonly IStringLocalizer<SharedResource> sharedResourceLocalizer;
        public TagController(IUnitOfWork repository, IMapper mapper, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            // this.stringLocalizer = postsControllerLocalizer;
            this.sharedResourceLocalizer = sharedResourceLocalizer;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var article = sharedResourceLocalizer["Hello"];
            var postName = sharedResourceLocalizer.GetString("Welcome").Value ?? "";

            return Ok(new { PostType = article.Value, PostName = postName });
            //return Ok(await _repository.Tags.GetAllAsync());
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _repository.TagRepository.GetAsync(x => x.Id == id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> PutTag(int id, Tag model)
        {
            try
            {
                var updateItem = await _repository.TagRepository.UpdateTagAsync(id, model);
                return updateItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            _repository.TagRepository.Add(tag);
            await _repository.CommitAsync();

            return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _repository.TagRepository.GetAsync(x => x.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            _repository.TagRepository.Remove(tag);
            await _repository.CommitAsync();

            return NoContent();
        }
    }
}
