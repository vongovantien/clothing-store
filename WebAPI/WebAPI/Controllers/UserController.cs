using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Helpers;
using WebAPI.Services.Excels;
using WebAPI.Utils;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _repository.UserRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _repository.UserRepository.GetUserById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User model)
        {
            var hs = _repository.UserRepository.UpdateUser(id, model);
            return Ok(hs);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.UserRepository.DeleteUser(id);
            return Ok(new { message = "User deleted successfully" });
        }

        [ProducesResponseType(typeof(FileContentResult), 200)]
        [HttpGet("exportUsers")]
        public async Task<IActionResult> GetExportUserFile()
        {
            DateTime dtNow = DateTime.Now;
            var allUser = await _repository.UserRepository.GetAllAsync();
            List<object> lstObject = new List<object>();
            int index = 1;
            foreach (var item in allUser)
            {
                lstObject.Add(new
                {
                    order = index++,
                    full_name = CommonUtils.getFullName(item),
                });
            }

            var byteData = ExcelTemplateFactory.ExcelUsersTemplate(lstObject).Export();
            return File(byteData, ".xlsx", "ExcelUsersTemplate.xlsx");
        }


        [AllowAnonymous]
        [HttpPost("users/forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                var hs = new HandleState();
                var userFound = await _repository.UserRepository.GetUserByEmail(email);
                if (userFound != null)
                {
                    string dataEncrypt = userFound.Email + "|" + DateTime.Now.AddMinutes(120).ToString();
                    //var mail = EmailTemplateFactory.EmailForgotPassword("Khôi phục mật khẩu", ConfigEnvironment.FRONTEND_HOST + "/auth/set-password?secretkey=" + CryptoUtils.EncryptData(dataEncrypt), userFound.email, userFound.email);
                    //await mail.SendAsync();
                }
                return Ok(hs);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        //[HttpPost("users/changePassword")]
        //public async Task<IActionResult> ChangePassword(string username, string oldPassword, string newPassword)
        //{
        //    try
        //    {
        //        var userFound = await _repository.UserRepository.GetUserByEmail(email);
        //        if (userFound == null)
        //        {
        //            return new JsonResult(new { success = false, message = "Không tìm thấy người dùng" });
        //        }
        //        var result = await _userProvider.changePassword(userFound.id, newPassword, oldPassword);
        //        if (result)
        //        {
        //            return new JsonResult(new { success = true, message = "Thay đổi mật khẩu thành công" });
        //        }
        //        else
        //        {
        //            return new JsonResult(new { success = false, message = "Thay đổi mật khẩu thất bại" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerFactory.ErrorLog(ex.ToString(), "Exception");
        //        return new JsonResult(new { success = false, message = "Unexpected error " + ex.ToString() });
        //    }

        //}

        [HttpPut("{id}/uploadAvatar")]
        public async Task<IActionResult> UloadAvatar(int id, IFormFile file)
        {
            if (!ReflectionHelper.isImage(file))
            {
                return BadRequest(new HandleState("Đã có lỗi xảy ra, vui lòng thử lại!"));
            }
            var result = await _repository.UserRepository.UploadAvatar(id, file);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}/uploadCoverImage")]
        public async Task<IActionResult> UploadCoverImage(int id, IFormFile file)
        {
            if (!ReflectionHelper.isImage(file))
            {
                return BadRequest(new HandleState("Đã có lỗi xảy ra, vui lòng thử lại!"));
            }
            var result = await _repository.UserRepository.UploadCoverImage(id, file);
            if (result == null)
            {
                return BadRequest(new HandleState("Đã có lỗi xảy ra, vui lòng thử lại!"));
            }
            return Ok(new HandleState());
        }
    }
}
