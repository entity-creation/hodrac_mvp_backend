using System.ComponentModel.DataAnnotations;
using Hodrac_MVP_Backend.DTOs.UserInfo;
using Hodrac_MVP_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hodrac_MVP_Backend.Controllers
{
    [ApiController]
    [Route("api/user_info")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoRepository _userInfoRepo;

        public UserInfoController(IUserInfoRepository userInfoRepo)
        {
            _userInfoRepo = userInfoRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInfoDto dto)
        {
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            var isValidEmail = Validator.TryValidateObject(dto, context, results, true);

            if (!isValidEmail)
                return BadRequest();
            var userInfo = await _userInfoRepo.CreateNewInfo(dto);
            return Ok(userInfo);
        }
    }
}
