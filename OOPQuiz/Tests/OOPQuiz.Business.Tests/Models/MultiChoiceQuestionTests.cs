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
            var multiChoiceQuestion = new MultiChoiceQuestion(input, "Choice 1", "", new() { { "Choice 1", "" } , { "Choice 2", "" } });

            var actual = multiChoiceQuestion.Question;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Answer", "Answer")]
        public void TestValidAnswer(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var multiChoiceQuestion = new MultiChoiceQuestion("", input, "", new() { { input, "" }, { "", "" } });

            var actual = multiChoiceQuestion.Answer;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Answer")]
        public void TestInvalidAnswer(string input)
        {
            // An exception should be raised if an answer that is not among the choices is provided.
            Assert.Throws<ArgumentException>(() => new MultiChoiceQuestion("", input, "", new() { { "Choice 1", "" }, { "Choice 2", "" } }));
        }

        [Theory]
        [InlineData(@"test\uri\image.png", @"test\uri\image.png")]
        public void TestImageURI(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var multiChoiceQuestion = new MultiChoiceQuestion("", "Choice 1", input, new() { { "Choice 1", "" }, { "Choice 2", "" } });

            var actual = multiChoiceQuestion.ImageURI;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGeneralFeedbackDefaultBehaviour()
        {
            // Should return an empty string.
            var multiChoiceQuestion = new MultiChoiceQuestion("", "Choice 1", "", new() { { "Choice 1", "" }, { "Choice 2", "" } });

            var actual = multiChoiceQuestion.Feedback;

            Assert.Equal(string.Empty, actual);
        }

        [Theory]
        [InlineData("Feedback", "Feedback")]
        public void TestGeneralFeedbackSetUponInstantiation(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var multiChoiceQuestion = new MultiChoiceQuestion("", "Choice 1", "", new() { { "Choice 1", "" }, { "Choice 2", "" } }, input);

            var actual = multiChoiceQuestion.Feedback;

            Assert.Equal(expected, actual);
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

            Dictionary<string, string> choicesDictionary = new();

            foreach (string choice in input)
            {
                choicesDictionary[choice] = "";
            }

            var multiChoiceQuestion = new MultiChoiceQuestion("", "Option 1", "", choicesDictionary);

            var actual = multiChoiceQuestion.ChoicesWithFeedback.Keys.ToList();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Option 1")]
        [InlineData("Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7")]
        public void TestInvalidChoices(params string[] input)
        {
            // Shouldn't be able to set less than two or more than 6 options for the question.
            Dictionary<string, string> choicesDictionary = new();

            foreach (string choice in input)
            {
                choicesDictionary[choice] = "";
            }

            Assert.Throws<ArgumentException>(() => new MultiChoiceQuestion("", "Option 1", "", choicesDictionary));
        }

        [Theory]
        [InlineData("Feedback 1", new string[] { "Option 1", "Feedback 1" })]
        public void TestChoicesFeedbackSetUponInstantiation(string expected, string[] input)
        {
            // Feedback for specific options should be equal to that specified upon instantiation.
            Dictionary<string, string> choicesDictionary = new()
            {
                { input[0], input[1] },
                { "", "" }
            };

            var multiChoiceQuestion = new MultiChoiceQuestion("", "", "", choicesDictionary);

            var actual = multiChoiceQuestion.ChoicesWithFeedback[input[0]];

            Assert.Equal(expected, actual);
        }
    }
}
