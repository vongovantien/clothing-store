using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(myDBContext dbContext) : base(dbContext)
        {
           
        }
        public async Task<Tag> UpdateTagAsync(int id, Tag model)
        {
            try
            {
                var existItem = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
                await _dbContext.SaveChangesAsync();
                return existItem;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
