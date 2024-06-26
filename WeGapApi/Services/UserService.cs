﻿using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class UserService : IUserService
	{
		private readonly IRepositoryManager _repositoryManager;
		private readonly IMapper _mapper;
		public UserService(IRepositoryManager repositoryManager, IMapper mapper)
		{
			_repositoryManager = repositoryManager;
			_mapper = mapper;
		}

        public async Task<UserDto> BlockUnblock(string id)
        {

            var user =  _repositoryManager.User.BlockUnblock(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
           
        }

        public async Task<UserDto> DeleteUser(string id)
        {
            var user = _repositoryManager.User.DeleteUser(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public  async Task<List<UserDto>> GetAllUsers()
        {


            var user =  _repositoryManager.User.GetUsers();
            var userDto = _mapper.Map<List<UserDto>>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var user = _repositoryManager.User.GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;

        }

        public async Task<UserDto> UpdateUser(string id, UpdateUserDto updateUserDto)
        {
            var userDomain = _mapper.Map<ApplicationUser>(updateUserDto);
            var user = _repositoryManager.User.UpdateUser(id,userDomain);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}

