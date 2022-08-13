using System.Collections.Generic;

namespace OOPQuiz.Business.Tests.Models
{
    public class MultiChoiceQuestionTests
    {
        [Theory]
        [InlineData("Lorem ipsum dolor sit amet?", "Lorem ipsum dolor sit amet?")]
        public void TestQuestion(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var multiChoiceQuestion = new MultiChoiceQuestion(input, "", new() { "" , "" });

            var actual = multiChoiceQuestion.Question;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Answer", "Answer")]
        public void TestValidAnswer(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var multiChoiceQuestion = new MultiChoiceQuestion("", input, new() { "Answer", "" });

            var actual = multiChoiceQuestion.Answer;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Answer")]
        public void TestInvalidAnswer(string input)
        {
            // An exception should be raised if an answer that is not among the choices is provided.
            Assert.Throws<ArgumentException>(() => new MultiChoiceQuestion("", input, new() { "", "" }));
        }

        [Theory]
        [InlineData("Option 1", "Option 2")]
        [InlineData("Option 1", "Option 2", "Option 3")]
        [InlineData("Option 1", "Option 2", "Option 3", "Option 4")]
        [InlineData("Option 1", "Option 2", "Option 3", "Option 4", "Option 5")]
        [InlineData("Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6")]
        public void TestValidChoices(params string[] input)
        {
            // Should be the same as what was specified upon instantiation.
            List<string> expected = input.ToList();

            var multiChoiceQuestion = new MultiChoiceQuestion("", "Option 1", input.ToList());

            var actual = multiChoiceQuestion.Choices;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Option 1")]
        [InlineData("Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7")]
        public void TestInvalidChoices(params string[] input)
        {
            Assert.Throws<ArgumentException>(() => new MultiChoiceQuestion("", "Option 1", input.ToList()));
        }
    }
}
