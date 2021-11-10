﻿using FluentValidation.TestHelper;
using SmartRestaurant.Application.Illness.Queries;
using System;
using Xunit;

namespace SmartRestaurant.Application.Tests.CommandValidatorTests.Illnesses.Queries
{
    public class GetIllnessByIdQueryValidatorTests
    {
        private readonly GetIllnessByIdQueryValidator _validator;

        public GetIllnessByIdQueryValidatorTests()
        {
            _validator = new GetIllnessByIdQueryValidator();
        }

        [Fact]
        public void Given_IdIsNull_WhenValidating_ShouldGetAnError()
        {
            string _null = null;
            _validator.ShouldHaveValidationErrorFor(x => x.Id, _null);
        }

        [Fact]
        public void Given_IdIsEmpty_WhenValidating_ShouldGetAnError()
        {
            var empty = "";
            _validator.ShouldHaveValidationErrorFor(x => x.Id, empty);
        }

        [Fact]
        public void Given_IdIsEmptyGuid_WhenValidating_ShouldGetAnError()
        {
            var emptyGuid = Guid.Empty.ToString();
            _validator.ShouldHaveValidationErrorFor(x => x.Id, emptyGuid);
        }

        [Fact]
        public void Given_IdIsInvalidGuid_WhenValidating_ShouldGetAnError()
        {
            var invalidGuid = "3cbf3570-4444-4673-8746-2";
            _validator.ShouldHaveValidationErrorFor(x => x.Id, invalidGuid);
        }

      
    }
}
