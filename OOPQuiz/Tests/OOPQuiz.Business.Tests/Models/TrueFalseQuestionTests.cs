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
            var trueFalseQuestion = new TrueFalseQuestion(input, true, "");

            var actual = trueFalseQuestion.Question;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("True", true)]
        [InlineData("False", false)]
        public void TestAnswer(string expected, bool input)
        {
            // Should be the same as what was specified upon instantiation.
            var trueFalseQuestion = new TrueFalseQuestion("", input, "");

            var actual = trueFalseQuestion.Answer;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(@"test\uri\image.png", @"test\uri\image.png")]
        public void TestImageURI(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var trueFalseQuestion = new TrueFalseQuestion("", true, input);

            var actual = trueFalseQuestion.ImageURI;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFeedbackDefaultBehaviour()
        {
            // Should return an empty string.
            var trueFalseQuestion = new TrueFalseQuestion("", true, "");

            var actual = trueFalseQuestion.Feedback;

            Assert.Equal(string.Empty, actual);
        }

        [Theory]
        [InlineData("Feedback", "Feedback")]
        public void TestFeedbackSetUponInstantiation(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var trueFalseQuestion = new TrueFalseQuestion("", true, "", input);

            var actual = trueFalseQuestion.Feedback;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestChoices()
        {
            // Should always return a list containing "True" and "False".
            Dictionary<string, string> expected = new() { { "True", "" }, { "False", "" } };

            var trueFalseQuestion = new TrueFalseQuestion("", true, "");

            var actual = trueFalseQuestion.ChoicesWithFeedback;

            Assert.Equal(expected, actual);
        }
    }
}
