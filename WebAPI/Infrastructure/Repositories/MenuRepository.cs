using Domain.Models;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(myDBContext context) : base(context)
        {
        }

    }
}
