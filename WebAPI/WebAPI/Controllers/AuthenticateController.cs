using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.UnitOfWork;
using Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IUnitOfWork _repository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthenticateController(
            IUnitOfWork repository,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<IActionResult> Authenticate(AuthenticateModel model)
        {
            var response = await _repository.UserRepository.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User model)
        {
            await _repository.UserRepository.CreateUser(model);
            return Ok(new { message = "Registration successful" });
        }
    }
}
