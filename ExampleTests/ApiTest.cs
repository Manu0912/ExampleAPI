using Xunit;
using System;
using FluentValidation.TestHelper;
using ExampleAPI.Models;

namespace ExampleTests
{
    public class UnitTest1
    {
        private readonly ExampleItemValidator _validator = new ExampleItemValidator();

        [Fact]
        public void TestNameValidatorPass()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = 1, Name = "sjdfbdskj", IsCompleted = true });
            result.ShouldNotHaveValidationErrorFor(item => item.Name);
        }

        [Fact]
        public void TestNameValidatorFail()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = 1, Name = "sjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskjsjdfbdskj", IsCompleted = true });
            result.ShouldHaveValidationErrorFor(item => item.Name);
        }

        [Fact]
        public void TestIdValidatorPass()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = 1, Name = "sjdfbdskj", IsCompleted = true });
            result.ShouldNotHaveValidationErrorFor(item => item.Id);
        }

        [Fact]
        public void TestIdValidatorFail()
        {
            var result = _validator.TestValidate(new ExampleItem() { Id = -1, Name = "sjdfbdskj", IsCompleted = true });
            result.ShouldHaveValidationErrorFor(item => item.Id);
        }
    }
}
