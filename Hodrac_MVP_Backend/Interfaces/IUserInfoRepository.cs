using Hodrac_MVP_Backend.DTOs.UserInfo;

namespace Hodrac_MVP_Backend.Interfaces
{
    public interface IUserInfoRepository
    {
        Task<UserInfoDto> CreateNewInfo(UserInfoDto dto);
    }
}
