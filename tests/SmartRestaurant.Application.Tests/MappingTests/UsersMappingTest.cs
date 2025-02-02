﻿using System;
using AutoMapper;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Tests.Configuration;
using SmartRestaurant.Application.Users.Commands;
using SmartRestaurant.Domain.Identity.Entities;
using Xunit;

namespace SmartRestaurant.Application.Tests.MappingTests
{
    public class UsersMappingTest : IClassFixture<MappingTestsFixture>
    {
        private readonly IMapper _mapper;

        public UsersMappingTest(MappingTestsFixture fixture)
        {
            _mapper = fixture.Mapper;
        }


        [Fact]
        public void Map_ApplicationUser_To_foodBusinessEmployeesDto_Valide_Test()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "aissa@gmail.com",
                FullName = "aissa",
                UserName = "aissa_user",
                PhoneNumber = "07788991123",
                IsActive = true
            };

            var foodBusinessEmployeesDto = _mapper.Map<FoodBusinessEmployeesDtos>(user);

            Assert.Equal(foodBusinessEmployeesDto.Id, user.Id);
            Assert.Equal(foodBusinessEmployeesDto.Email, user.Email);
            Assert.Equal(foodBusinessEmployeesDto.FullName, user.FullName);
            Assert.Equal(foodBusinessEmployeesDto.UserName, user.UserName);
            Assert.Equal(foodBusinessEmployeesDto.PhoneNumber, user.PhoneNumber);
            Assert.Equal(foodBusinessEmployeesDto.IsActive, user.IsActive);
        }

        [Fact]
        public void Map__To_ApplicationUser_Valide_Test()
        {
            var id = Guid.NewGuid().ToString();

            var updateUserCommand = new UpdateUserCommand
            {
                Id = id,
                FullName = "bilal",
                PhoneNumber = "888888888",
                Roles = null
            };

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FullName = "aissa",
                PhoneNumber = "07788991123",
            };

            Assert.NotEqual(user.Id, updateUserCommand.Id);
            Assert.NotEqual(user.FullName, updateUserCommand.FullName);
            Assert.NotEqual(user.PhoneNumber, updateUserCommand.PhoneNumber);

            _mapper.Map(updateUserCommand, user);

            Assert.Equal(user.Id, updateUserCommand.Id);
            Assert.Equal(user.FullName, updateUserCommand.FullName);
            Assert.Equal(user.PhoneNumber, updateUserCommand.PhoneNumber);
        }
        
        public void Map_ApplicationUser_To_ApplicationUserDto_Valide_Test()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "aissa@gmail.com",
                FullName = "aissa",
                UserName = "aissa_user",
                PhoneNumber = "07788991123",
                IsActive = true
            };

            var applicationUserDto = _mapper.Map<ApplicationUserDto>(user);

            Assert.Equal(applicationUserDto.Id, user.Id);
            Assert.Equal(applicationUserDto.Email, user.Email);
            Assert.Equal(applicationUserDto.FullName, user.FullName);
            Assert.Equal(applicationUserDto.UserName, user.UserName);
            Assert.Equal(applicationUserDto.PhoneNumber, user.PhoneNumber);
            Assert.Equal(applicationUserDto.IsActive, user.IsActive);
        }
    }
}