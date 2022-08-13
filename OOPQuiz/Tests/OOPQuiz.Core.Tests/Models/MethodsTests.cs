namespace OOPQuiz.Core.Tests.Models
{
    public class MethodsTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("T", "t")]
        [InlineData("Te", "te")]
        [InlineData("Test", "test")]
        [InlineData("Test string", "test string")]
        public void TestCapitaliseString(string expected, string input)
        {
            // Method should capitalise the string passed as a parameter
            var capitalisedString = Methods.Capitalise(input);

            Assert.Equal(expected, capitalisedString);
        }
    }
}
