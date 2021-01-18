using TicTacToe.Interfaces;
using TicTacToe.Managers;
using Xunit;

namespace TicTacToeTests
{
    public class ValidatorUnitTests
    {
        private readonly IValidator _validator;
        
        public ValidatorUnitTests()
        {
            _validator = new Validator();
        }
        
        [Theory]
        [InlineData("00", true)]
        [InlineData("01", true)]
        [InlineData("02", true)]
        [InlineData("10", true)]
        [InlineData("11", true)]
        [InlineData("12", true)]
        [InlineData("20", true)]
        [InlineData("21", true)]
        [InlineData("22", true)]
        [InlineData("0", false)]
        [InlineData("1", false)]
        [InlineData("8", false)]
        [InlineData("13", false)]
        [InlineData("31", false)]
        [InlineData("aa", false)]
        [InlineData("aaa", false)]
        [InlineData("!@", false)]
        [InlineData("!@#@#!", false)]
        [InlineData("1a", false)]
        [InlineData("2a", false)]
        [InlineData("XX", false)]
        [InlineData("Xx", false)]
        [InlineData("XxXXxXXxXXxXXxX!@#", false)]
        public void ReturnExpectedVResultForDifferentInputInstructions(string input, bool expectedResult)
        {
            var actual = _validator.ValidateInput(input);
            Assert.Equal(expectedResult, actual);
        }
    }
}