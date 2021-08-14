using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TemperatureRetreiver.Validations;

namespace UnitTests.Validations
{
    [TestClass]
    public class DateValidationTests
    {
        private IDateValidation _dateValidation;

        private readonly string _validDate = "2021/12/12 12:12";
        private readonly string _unValidDate = "2021";

        [TestInitialize]
        public void TestInit()
        {
            _dateValidation = new DateValidation();
        }

        [TestMethod]
        public void DateValidation_CorrectDate_Valid()
        {
            //Arrange

            //Act
            var result = _dateValidation.IsValid(_validDate);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DateValidation_NotCorrectDate_UnValid()
        {
            //Arrange

            //Act
            var result = _dateValidation.IsValid(_unValidDate);

            //Assert
            Assert.AreEqual(false, result);
        }
    }
}
