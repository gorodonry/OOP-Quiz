using System.Collections.Generic;

namespace OOPQuiz.Business.Tests.Models
{
    public class TrueFalseQuestionTests
    {
        [Theory]
        [InlineData("Lorem ipsum dolor sit amet?", "Lorem ipsum dolor sit amet?")]
        public void TestQuestion(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var trueFalseQuestion = new TrueFalseQuestion(input, true);

            var actual = trueFalseQuestion.Question;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("True", true)]
        [InlineData("False", false)]
        public void TestAnswer(string expected, bool input)
        {
            // Should be the same as what was specified upon instantiation.
            var trueFalseQuestion = new TrueFalseQuestion("", input);

            var actual = trueFalseQuestion.Answer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestChoices()
        {
            // Should always return a list containing "True" and "False".
            List<string> expected = new() { "True", "False" };

            var trueFalseQuestion = new TrueFalseQuestion("", true);

            var actual = trueFalseQuestion.Choices;

            Assert.Equal(expected, actual);
        }
    }
}
