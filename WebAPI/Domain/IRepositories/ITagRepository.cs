using Domain.Entities;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        public Task<Tag> UpdateTagAsync(int id, Tag model);
    }
}
