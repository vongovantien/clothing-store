using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.UnitOfWork;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IUnitOfWork _repository;
        public MenuController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            var menu = await _repository.MenuRepository.GetAllAsync();
            return Ok(menu);
        }
    }
}
