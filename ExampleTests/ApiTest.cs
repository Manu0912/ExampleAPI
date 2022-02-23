using Xunit;
using System;
using FluentValidation.TestHelper;
using ExampleAPI.Models;

namespace ExampleTests
{
    public class ApiTest
    {
        private readonly ExampleItemValidator _validator = new();

        [Fact]
        public void TestNameValidator_Pass()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = 1, Name = "sjdfbdskj", IsCompleted = true });
            result.ShouldNotHaveValidationErrorFor(item => item.Name);
        }

        [Fact]
        public void TestNameValidator_IsLongerThanMaxLength_Fail()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = 1, Name = "sjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskj", IsCompleted = true });
            result.ShouldHaveValidationErrorFor(item => item.Name);
        }

        [Fact]
        public void TestNameValidator_IsNull_Fail()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = 1, Name = null, IsCompleted = true });
            result.ShouldHaveValidationErrorFor(item => item.Name);
        }

        [Fact]
        public void TestNameValidator_IsEmpty_Fail()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = 1, Name = "", IsCompleted = true });
            result.ShouldHaveValidationErrorFor(item => item.Name);
        }

        [Fact]
        public void TestIdValidator_Pass()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = 1, Name = "sjdfbdskj", IsCompleted = true });
            result.ShouldNotHaveValidationErrorFor(item => item.Id);
        }

        [Fact]
        public void TestIdValidator_IsLessThanZero_Fail()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = -1, Name = "sjdfbdskj", IsCompleted = true });
            result.ShouldNotHaveValidationErrorFor(item => item.Id);
        }

    }
}
