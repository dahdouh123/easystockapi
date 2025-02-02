﻿using AutoMapper;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Dtos.ValueObjects;
using SmartRestaurant.Application.FoodBusinessClient.Commands;
using SmartRestaurant.Application.Tests.Configuration;
using SmartRestaurant.Domain.ValueObjects;
using System;
using Xunit;

namespace SmartRestaurant.Application.Tests.MappingTests
{
    public class FoodBusinessClientMappingTest : IClassFixture<MappingTestsFixture>
    {
        private readonly CreateFoodBusinessClientCommandValidator _createFoodBusinessClientValidator ;
        private readonly IMapper _mapper;

        public FoodBusinessClientMappingTest(MappingTestsFixture fixture)
        {
            _mapper = fixture.Mapper;
            _createFoodBusinessClientValidator = new CreateFoodBusinessClientCommandValidator();
        }

        [Fact]
        public void Map_CreateFoodBusinessClientCommand_To_FoodBusinessClient_Valide_Test()
        {
            var foodBusinessId = Guid.NewGuid();
            
            var createFoodBusinessClientCommand = new CreateFoodBusinessClientCommand
            {
                Address = new AddressDto
                {
                    City = "Algiers",
                    Country = "Algeria",
                    GeoPosition = new GeoPositionDto
                    {
                        Latitude = "0",
                        Longitude = "0"
                    },
                    StreetAddress = "Didouche Mourad"
                },
                Description = "",
                Name = "G22 REI ",
                PhoneNumber = new PhoneNumberDto { CountryCode = 213, Number = 670217536 },
                Email = "test@g22.com",
                Website = "",
                FoodBusinessId = foodBusinessId.ToString()
            };

            var validationResult = _createFoodBusinessClientValidator.Validate(createFoodBusinessClientCommand);
            Assert.True(validationResult.IsValid);

            var foodBusinessClient = _mapper.Map<Domain.Entities.FoodBusinessClient>(createFoodBusinessClientCommand);
            Assert.Equal(foodBusinessClient.FoodBusinessClientId, createFoodBusinessClientCommand.Id);
            Assert.Equal(foodBusinessClient.Name, createFoodBusinessClientCommand.Name);
            Assert.Equal(foodBusinessClient.Address.GeoPosition.Latitude, createFoodBusinessClientCommand.Address.GeoPosition.Latitude);
            Assert.Equal(foodBusinessClient.Address.GeoPosition.Longitude, createFoodBusinessClientCommand.Address.GeoPosition.Longitude);
            Assert.Equal(foodBusinessClient.Address.City, createFoodBusinessClientCommand.Address.City);
            Assert.Equal(foodBusinessClient.Address.Country, createFoodBusinessClientCommand.Address.Country);
            Assert.Equal(foodBusinessClient.Address.StreetAddress, createFoodBusinessClientCommand.Address.StreetAddress);
            Assert.Equal(foodBusinessClient.Description, createFoodBusinessClientCommand.Description);
            Assert.Equal(foodBusinessClient.PhoneNumber.CountryCode, createFoodBusinessClientCommand.PhoneNumber.CountryCode);
            Assert.Equal(foodBusinessClient.PhoneNumber.Number, createFoodBusinessClientCommand.PhoneNumber.Number);
            Assert.Equal(foodBusinessClient.Email, createFoodBusinessClientCommand.Email);
            Assert.Equal(foodBusinessClient.Website, createFoodBusinessClientCommand.Website);
            Assert.Equal(foodBusinessClient.FoodBusinessId.ToString(), createFoodBusinessClientCommand.FoodBusinessId);
        }

        [Fact]
        public void Map_UpdateFoodBusinessClientCommand_To_FoodBusinessClient_Valide_Test()
        {
            var foodBusinessClientId = Guid.NewGuid();
            var foodBusinessClient = new Domain.Entities.FoodBusinessClient
            {
                Address = new Address
                {
                    City = "Algiers",
                    Country = "Algeria",
                    GeoPosition = new GeoPosition
                    {
                        Latitude = "0",
                        Longitude = "0"
                    },
                    StreetAddress = "Didouche Mourad"
                },
                Description = "",
                Name = "G22 REI ",
                PhoneNumber = new PhoneNumber { CountryCode = 213, Number = 670217536 },
                Email = "test@g22.com",
                Website = ""
            };
            var updateFoodBusinessClientCommand = new UpdateFoodBusinessClientCommand
            {
                Id = foodBusinessClientId.ToString(),
                Address = new AddressDto
                {
                    City = "Alger",
                    Country = "Algerie",
                    GeoPosition = new GeoPositionDto
                    {
                        Latitude = "0",
                        Longitude = "0"
                    },
                    StreetAddress = "Didouche Mourad"
                },
                Description = "Updated Description",
                Name = "G22 REI Updated test",
                PhoneNumber = new PhoneNumberDto { CountryCode = 213, Number = 672217426 },
                Email = "testUpdate@g22.com",
                Website = "http://g22rei.com"
                
            };
             _mapper.Map(updateFoodBusinessClientCommand,foodBusinessClient);
            Assert.Equal(foodBusinessClient.FoodBusinessClientId, foodBusinessClientId);
            Assert.Equal(foodBusinessClient.Name, updateFoodBusinessClientCommand.Name);
            Assert.Equal(foodBusinessClient.Address.GeoPosition.Latitude, updateFoodBusinessClientCommand.Address.GeoPosition.Latitude);
            Assert.Equal(foodBusinessClient.Address.GeoPosition.Longitude, updateFoodBusinessClientCommand.Address.GeoPosition.Longitude);
            Assert.Equal(foodBusinessClient.Address.City, updateFoodBusinessClientCommand.Address.City);
            Assert.Equal(foodBusinessClient.Address.Country, updateFoodBusinessClientCommand.Address.Country);
            Assert.Equal(foodBusinessClient.Address.StreetAddress, updateFoodBusinessClientCommand.Address.StreetAddress);
            Assert.Equal(foodBusinessClient.Description, updateFoodBusinessClientCommand.Description);
            Assert.Equal(foodBusinessClient.PhoneNumber.CountryCode, updateFoodBusinessClientCommand.PhoneNumber.CountryCode);
            Assert.Equal(foodBusinessClient.PhoneNumber.Number, updateFoodBusinessClientCommand.PhoneNumber.Number);
            Assert.Equal(foodBusinessClient.Email, updateFoodBusinessClientCommand.Email);
            Assert.Equal(foodBusinessClient.Website, updateFoodBusinessClientCommand.Website);
        }
        [Fact]
        public void Map_FoodBusinessClientDto_To_FoodBusinessClient_Valide_Test()
        {
            var foodBusinessClientDto = new FoodBusinessClientDto
            {
                FoodBusinessClientId = Guid.NewGuid(),
                Address = new AddressDto
                {
                    City = "Alger",
                    Country = "Algerie",
                    GeoPosition = new GeoPositionDto
                    {
                        Latitude = "0",
                        Longitude = "0"
                    },
                    StreetAddress = "Didouche Mourad"
                },
                Description = "Updated Description",
                Name = "G22 REI Updated test",
                PhoneNumber = new PhoneNumberDto { CountryCode = 213, Number = 672217426 },
                Email = "testUpdate@g22.com",
                Website = "http://g22rei.com",

            };
            var foodBusinessClient = _mapper.Map<Domain.Entities.FoodBusinessClient>(foodBusinessClientDto);

            Assert.Equal(foodBusinessClientDto.FoodBusinessClientId, foodBusinessClient.FoodBusinessClientId);
            Assert.Equal(foodBusinessClientDto.Name, foodBusinessClient.Name);
            Assert.Equal(foodBusinessClientDto.Address.City, foodBusinessClient.Address.City);
            Assert.Equal(foodBusinessClientDto.Address.Country, foodBusinessClient.Address.Country);
            Assert.Equal(foodBusinessClientDto.Address.GeoPosition.Latitude, foodBusinessClient.Address.GeoPosition.Latitude);
            Assert.Equal(foodBusinessClientDto.Address.GeoPosition.Longitude, foodBusinessClient.Address.GeoPosition.Longitude);
            Assert.Equal(foodBusinessClientDto.Address.StreetAddress, foodBusinessClient.Address.StreetAddress);
            Assert.Equal(foodBusinessClientDto.Description, foodBusinessClient.Description);
            Assert.Equal(foodBusinessClientDto.PhoneNumber.CountryCode, foodBusinessClient.PhoneNumber.CountryCode);
            Assert.Equal(foodBusinessClientDto.PhoneNumber.Number, foodBusinessClient.PhoneNumber.Number);
            Assert.Equal(foodBusinessClientDto.Email, foodBusinessClient.Email);
            Assert.Equal(foodBusinessClientDto.Website, foodBusinessClient.Website);

        }

        [Fact]
        public void Map_FoodBusinessClientDto_GeoLocationDto_to_FoodBusinessClient_Geolocation_Valide_Test()
        {
            var foodBusinessClientDto = new FoodBusinessClientDto
            {
                Address = new AddressDto
                {
                    City = "Oran",
                    StreetAddress = "12 rue exemple",
                    Country = "Algeria",
                    GeoPosition = new GeoPositionDto
                    {
                        Latitude = "+12.5",
                        Longitude = "-24.3"
                    }
                }
            };
            var foodBusinessClient = _mapper.Map<Domain.Entities.FoodBusinessClient>(foodBusinessClientDto);

            Assert.Equal("+12.5", foodBusinessClient.Address.GeoPosition.Latitude);
        }

        [Fact]
        public void Map_FoodBusinessClientDto_AddressDto_to_FoodBusinessClient_Address_Valide_Test()
        {
            var foodBusinessClientDto = new FoodBusinessClientDto
            {
                Address = new AddressDto
                {
                    StreetAddress = "12 rue exemple",
                    City = "Oran",
                    Country = "Algeria"
                }
            };
            var foodBusinessClient = _mapper.Map<Domain.Entities.FoodBusinessClient>(foodBusinessClientDto);

            Assert.Equal("12 rue exemple", foodBusinessClient.Address.StreetAddress);
        }

        [Fact]
        public void Map_FoodBusinessClientDto_PhoneNumberDto_to_FoodBusinessClient_PhoneNumber_Valide_Test()
        {
            var foodBusinessClientDto = new FoodBusinessClientDto
            {
                PhoneNumber = new PhoneNumberDto
                {
                    CountryCode = 213,
                    Number = 798924059
                }
            };
            var foodBusinessClient = _mapper.Map<Domain.Entities.FoodBusinessClient>(foodBusinessClientDto);

            Assert.Equal("798924059", foodBusinessClient.PhoneNumber.Number.ToString());
        }

    }
}
