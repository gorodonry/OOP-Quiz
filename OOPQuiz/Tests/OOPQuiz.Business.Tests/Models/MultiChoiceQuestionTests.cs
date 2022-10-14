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
            var multiChoiceQuestion = new MultiChoiceQuestion(input, "Choice 1", "", new() { new("Choice 1"), new("Choice 2") });

            var actual = multiChoiceQuestion.Question;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Answer", "Answer")]
        public void TestValidAnswer(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var multiChoiceQuestion = new MultiChoiceQuestion("", input, "", new() { new(input), new("") });

            var actual = multiChoiceQuestion.Answer;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Answer")]
        public void TestInvalidAnswer(string input)
        {
            // An exception should be raised if an answer that is not among the choices is provided.
            Assert.Throws<ArgumentException>(() => new MultiChoiceQuestion("", input, "", new() { new("Choice 1"), new("Choice 2") }));
        }

        [Theory]
        [InlineData(@"test\uri\image.png", @"test\uri\image.png")]
        public void TestImageURI(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var multiChoiceQuestion = new MultiChoiceQuestion("", "Choice 1", input, new() { new("Choice 1"), new("Choice 2") });

            var actual = multiChoiceQuestion.ImageURI;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGeneralFeedbackDefaultBehaviour()
        {
            // Should return an empty string.
            var multiChoiceQuestion = new MultiChoiceQuestion("", "Choice 1", "", new() { new("Choice 1"), new("Choice 2") });

            var actual = multiChoiceQuestion.Feedback;

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData("Feedback", "Feedback")]
        public void TestGeneralFeedbackSetUponInstantiation(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var multiChoiceQuestion = new MultiChoiceQuestion("", "Choice 1", "", new() { new("Choice 1"), new("Choice 2") }, input);

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

            List<Choice> choicesList = new();

            foreach (string choice in input)
            {
                choicesList.Add(new(choice));
            }

            var multiChoiceQuestion = new MultiChoiceQuestion("", "Option 1", "", choicesList);

            var actualChoices = multiChoiceQuestion.Choices;

            // Note that the constructor for the multichoice question randomises the order the choices occur in.
            foreach (var actual in actualChoices)
            {
                Assert.Contains(actual.PotentialAnswer, expected);

                // Ensure each element occurs once only.
                expected.Remove(actual.PotentialAnswer);
            }
        }

        [Theory]
        [InlineData("Option 1")]
        [InlineData("Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7")]
        public void TestInvalidChoices(params string[] input)
        {
            // Shouldn't be able to set less than two or more than 6 options for the question.
            List<Choice> choicesList = new();

            foreach (string choice in input)
            {
                choicesList.Add(new(choice));
            }

            Assert.Throws<ArgumentException>(() => new MultiChoiceQuestion("", "Option 1", "", choicesList));
        }

        [Theory]
        [InlineData("Feedback 1", new string[] { "Option 1", "Feedback 1" })]
        public void TestChoicesFeedbackSetUponInstantiation(string expected, string[] input)
        {
            // Feedback for specific options should be equal to that specified upon instantiation.
            List<Choice> choicesList = new()
            {
                new(input[0], input[1]),
                new("")
            };

            var multiChoiceQuestion = new MultiChoiceQuestion("", "", "", choicesList);

            var actual = new List<string>()
            {
                multiChoiceQuestion.Choices[0].FeedbackForAnswer,
                multiChoiceQuestion.Choices[1].FeedbackForAnswer
            };

            Assert.Contains(expected, actual);
        }

        [Fact]
        public void TestClone()
        {
            // Clone should not be the same instance but should have all the same data.
            var multiChoiceQuestion = new MultiChoiceQuestion("", "Choice 1", "", new() { new("Choice 1"), new("Choice 2") });

            var clone = multiChoiceQuestion.Clone();

            Assert.IsType<MultiChoiceQuestion>(clone);
            Assert.NotSame(multiChoiceQuestion, clone);
        }
    }
}
