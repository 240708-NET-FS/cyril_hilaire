using System;
using ContactBook;
using Xunit;

namespace ContactBook.Tests.EmailValidatorTests;

public class EmailValidityTest
{
    [Theory]
    [InlineData("no-reply@email.do", true)]
    [InlineData("hello@world.revature", true)]
    [InlineData("email@XY.z", true)]
    [InlineData("gmail.com", false)]
    [InlineData("hello world", false)]
    [InlineData("!@.#", false)]
    [InlineData("@email.com", false)]
    [InlineData("", true)]
    public void ValidateEmail(string password, bool expectedResult)
    {
        // Arrange
        // InputValidation inputValidation;

        // Act
        bool isValid = InputValidation.CheckEmail(password);
        
        // Assert
        Assert.Equal(expectedResult, isValid);
    }
}