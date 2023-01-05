using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;
        public UserRepository(myDBContext dbContext, IMapper mapper, IConfiguration iconfiguration) : base(dbContext)
        {
            _mapper = mapper;
            _iconfiguration = iconfiguration;
        }

        public IQueryable<User> GetAllUser()
        {
            return _dbContext.Users.AsQueryable();
        }

        public async Task<UserViewModel> GetUserById(int Id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> Authenticate(AuthenticateModel model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                throw new ApplicationException("Username or password is incorrect");

            var response = _mapper.Map<UserViewModel>(user);
            //response.Token = tken  
            return response;
        }

        public async Task<HandleState> CreateUser(User model)
        {
            var hs = new HandleState();
            // validate
            if (_dbContext.Users.Any(x => x.Email == model.Email))
            {
                throw new ApplicationException("Username '" + model.Email + "' is already taken");
            }

            // map model to new user object
            var user = _mapper.Map<UserViewModel>(model);

            // hash password
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.RegisteredAt = DateTime.Now;
            // save user
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return hs;
        }

        public async Task<HandleState> DeleteUser(int id)
        {
            var hs = new HandleState();
            var existedItem = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Users.Remove(existedItem);
            _dbContext.SaveChanges();
            return hs;
        }
        public Task<List<UserViewModel>> UpdateUser(int id, User model)
        {
            throw new NotImplementedException();
        }

        public Task<HandleState> UploadAvatar(int id, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<HandleState> UploadCoverImage(int id, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
