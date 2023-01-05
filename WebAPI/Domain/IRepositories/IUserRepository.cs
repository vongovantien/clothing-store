using Domain.Models;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using WebAPI;

namespace Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserViewModel> Authenticate(AuthenticateModel model);
        Task<HandleState> CreateUser(User user);
        Task<UserViewModel> GetUserById(int Id);
        Task<UserViewModel> GetUserByEmail(string email);
        IQueryable<User> GetAllUser();
        Task<List<UserViewModel>> UpdateUser(int id, User model);
        Task<HandleState> DeleteUser(int id);
        Task<HandleState> UploadAvatar(int id, IFormFile file);
        Task<HandleState> UploadCoverImage(int id, IFormFile file);
    }
}
