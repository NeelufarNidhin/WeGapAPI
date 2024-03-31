using System;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface IUserService
	{
       
        Task<List<UserDto>> GetAllUsers(int pageNumber, int pageSize);
        Task<UserDto> BlockUnblock(string id);
        Task<UserDto> UpdateUser(string id, UpdateUserDto updateUserDto);
        Task<UserDto> DeleteUser(string id);
        Task<List<UserDto>> GetSearchQuery(string searchString);
    }
}

