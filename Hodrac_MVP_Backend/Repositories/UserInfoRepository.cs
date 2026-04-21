using Hodrac_MVP_Backend.Data;
using Hodrac_MVP_Backend.DTOs.UserInfo;
using Hodrac_MVP_Backend.Interfaces;
using Hodrac_MVP_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Hodrac_MVP_Backend.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly CustomDbContext _context;

        public UserInfoRepository(CustomDbContext context)
        {
            _context = context;
        }

        public async Task<UserInfoDto> CreateNewInfo(UserInfoDto dto)
        {
            var exitingUser = await _context.UserInfos.FirstOrDefaultAsync(u =>
                u.UserEmail == dto.UserEmail
            );
            if (exitingUser == null)
            {
                var newUser = new UserInfo { UserEmail = dto.UserEmail };
                await _context.UserInfos.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            return dto;
        }
    }
}
