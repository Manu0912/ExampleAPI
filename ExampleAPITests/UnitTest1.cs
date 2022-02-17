using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentValidation.TestHelper;
using ExampleAPI.Models;

namespace ExampleAPITests
{
    [TestClass]
    public class ValidatorsTest
    {
        [SetUp]
        private readonly ExampleItemValidator _validator = new ExampleItemValidator();

        [TestMethod]
        public void Create_WhenCalledConstructor_NameValidatorPass()
            => _validator.ShouldHaveValidationErrorFor(model => model.Name, "asjbdhds");

        public void Create_WhenCalledConstructor_NameValidatorFail()
        {
        }

        public void Create_WhenCalledConstructor_IdValidatorPass()
        {
        }

        public void Create_WhenCalledConstructor_IdValidatorFail()
        {
        }
    }
}
