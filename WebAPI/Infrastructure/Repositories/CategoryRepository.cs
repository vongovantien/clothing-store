using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(myDBContext context) : base(context)
        {
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
