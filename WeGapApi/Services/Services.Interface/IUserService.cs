using System;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface IUserService
	{
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(string id);
        Task<UserDto> BlockUnblock(string id);
        Task<UserDto> UpdateUser(string id, UpdateUserDto updateUserDto);
        Task<UserDto> DeleteUser(string id);
    }
}

