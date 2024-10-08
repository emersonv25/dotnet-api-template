﻿using Template.API.Models.Dtos;
using Template.API.Models.Entities;
using Template.API.Repositories;
using Template.API.Services.Interface;

namespace Template.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User?> GetUserByFirebaseUid(string firebaseUid)
        {
            return await _userRepository.GetUserByFirebaseUid(firebaseUid);
        }

        public async Task<User> CreateUser(UserDto userDto, string firebaseUid)
        {
            var existUser = GetUserByFirebaseUid(firebaseUid);
            if(existUser != null)
                throw new Exception("User already exists");

            var user = new User(userDto, firebaseUid);
            return await _userRepository.CreateUser(user);
        }

        public async Task<User?> UpdateUser(UserUpdateDto userDto, string firebaseUid)
        {
            var user = await _userRepository.GetUserByFirebaseUid(firebaseUid);
            if (user == null)
            {
                return null;
            }

            user.Name = userDto.Name;

            return await _userRepository.UpdateUser(user);
        }
    }
}
