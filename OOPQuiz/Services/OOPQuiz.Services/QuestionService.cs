using OOPQuiz.Business.Models;
using OOPQuiz.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOPQuiz.Services
{
    public class QuestionService : IQuestionService
    {
        private const string imagePath = @"..\..\..\..\Services\OOPQuiz.Services\Images\";

        private static readonly List<IQuestion> pythonQuestions = new()
        {
            new MultiChoiceQuestion(
                question: "What should be at the beginning of every file using OOP?",
                answer: "from dataclasses import dataclass",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question1.png"),
                choicesWithFeedback: new()
                {
                    { "from dataclasses import dataclass", "" },
                    { "from dataclasses import *", "This would work, but it is bad practice" },
                    { "import dataclasses", "This would work, but make for cumbersome programming" },
                    { "import sys", "" }
                },
                feedback: "Note that this step is actually optional - you can do OOP in Python without the dataclasses module."),
            new MultiChoiceQuestion(
                question: "What is 'age' in the following code snippet?",
                answer: "A member",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question2.png"),
                choicesWithFeedback: new()
                {
                    { "A member", "" },
                    { "An integer", "Yes, but also no" },
                    { "A property", "Members store information, and properties dictate what has access to members" },
                    { "An attribute", "Yes, it is an attribute of the Dog class, but more specifically it is a member" }
                }),
            new MultiChoiceQuestion(
                question: "What is 'age' in the following code snippet?",
                answer: "A property",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question3.png"),
                choicesWithFeedback: new()
                {
                    { "A property", "" },
                    { "A member", "Members store information, and properties dictate what has access to members" },
                    { "A function", "Functions are separate to classes" },
                    { "A variable", "" }
                }),
            new OpenEndedQuestion(
                question: "What is missing from the following code snippet?",
                answer: "self",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question4.png"),
                feedback: "Strictly speaking the use of self as a parameter is just a convention – you can use any parameter as long as you are consistent. But use self – this is a good convention."),
            new TrueFalseQuestion(
                question: "It is possible to set the age of a Dog object to 5.",
                answer: false,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question5.png")),
            new MultiChoiceQuestion(
                question: "What is the purpose of the ' -> int' in the following code snippet?",
                answer: "It is effectively a form of documentation",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question6.png"),
                choicesWithFeedback: new()
                {
                    { "It is effectively a form of documentation", "" },
                    { "It causes the property to raise an error if an integer is not returned", "" },
                    { "It specifies the parameters of the property must be integers", "" },
                    { "It doesn't really meant anything", "" }
                },
                feedback: "It provides an indication that the property should return an integer."),
            new MultiChoiceQuestion(
                question: "What is the purpose of preceding protected members with an underscore?",
                answer: "It denotes that a member is protected by properties",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question7.png"),
                choicesWithFeedback: new()
                {
                    { "It denotes that a member is protected by properties", "" },
                    { "It prevents the member from being accessed directly", "" },
                    { "It has literally no purpose", "Functionally, yes" },
                    { "All integer variables are preceded by an underscore", "" }
                },
                feedback: "But because Python doesn't protect them properly, you can access them anyway."),
            new MultiChoiceQuestion(
                question: "What is 'generate_dog_tag' in the following code snippet?",
                answer: "A method",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question8.png"),
                choicesWithFeedback: new()
                {
                    { "A method", "" },
                    { "A function", "This is a method not a function because it is defined inside a class" },
                    { "A property", "" },
                    { "An attribute", "Yes, it is an attribute of the Dog class, but more specifically it is a method" }
                }),
            new MultiChoiceQuestion(
                question: "What is the purpose of a protocol?",
                answer: "It provides a template for classes, guaranteeing certain members",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question9.png"),
                choicesWithFeedback: new()
                {
                    { "It provides a template for classes, guaranteeing certain members", "" },
                    { "It provides a way for data to be shared between classes", "" },
                    { "They have no real function", "" },
                    { "It's a special type of overridable class", "" }
                },
                feedback: "Only there is no guarantee because Python doesn't enforce them."),
            new TrueFalseQuestion(
                question: "Subclasses can override members defined by base classes.",
                answer: true,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question10.png"))
        };

        private static readonly List<IQuestion> cSharpQuestions = new()
        {
            new MultiChoiceQuestion(
                question: "What best describes '_age' in the following code snippet?",
                answer: "A field",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question1.png"),
                choicesWithFeedback: new()
                {
                    { "A field", "" },
                    { "An integer", "Yes, but also no" },
                    { "A property", "Fields store information, and properties dictate what has access to fields" },
                    { "A member", "Yes, it is a member of the Dog class, but more specifically it is a field" }
                }),
            new MultiChoiceQuestion(
                question: "What is 'Age' in the following code snippet?",
                answer: "A property",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question2.png"),
                choicesWithFeedback: new()
                {
                    { "A property", "" },
                    { "A field", "Fields store information, and properties dictate what has access to fields" },
                    { "A function", "Functions are separate to classes" },
                    { "An variable", "" }
                }),
            new MultiChoiceQuestion(
                question: "What needs to be at the beginning of every file using OOP?",
                answer: "A namespace declaration",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question3.png"),
                choicesWithFeedback: new()
                {
                    { "A namespace declaration", "" },
                    { "using Dataclasses", "" },
                    { "using System.Dataclasses", "" },
                    { "using System", "" }
                }),
            new TrueFalseQuestion(
                question: "It is possible to set the age of a Dog object to 5.",
                answer: false,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question4.png")),
            new TrueFalseQuestion(
                question: "Age can be set.",
                answer: false,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question5.png"),
                feedback: "No setter is defined for the protected field _age."),
            new MultiChoiceQuestion(
                question: "What is the function of '=>' in C#?",
                answer: "It is shorthand for a getter",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question6.png"),
                choicesWithFeedback: new()
                {
                    { "It is shorthand for a getter", "" },
                    { "It is a comparison operator", "" },
                    { "It increments age by 1", "" },
                    { "It sets the accessible property Age to the protected field age", "" }
                },
                feedback: "Defining getters in this way precludes the option of defining a setter."),
            new MultiChoiceQuestion(
                question: "Which of these is not a protection modifier?",
                answer: "const",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question7.png"),
                choicesWithFeedback: new()
                {
                    { "const", "" },
                    { "private", "" },
                    { "public", "Functionally, yes" },
                    { "protected", "" }
                },
                feedback: "Const doesn't specify what has access to a variable, whereas protection modifiers do."),
            new MultiChoiceQuestion(
                question: "What best describes 'GenerateDogTag' in the following code snippet?",
                answer: "It is a method",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question8.png"),
                choicesWithFeedback: new()
                {
                    { "It is a method", "" },
                    { "It is a function", "This is a method not a function because it is defined inside a class" },
                    { "It is a property", "" },
                    { "It is a member", "Yes, it is an member of the Dog class, but more specifically it is a method" }
                }),
            new TrueFalseQuestion(
                question: "Private members can't be accessed by subclasses.",
                answer: true,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question9.png")),
            new MultiChoiceQuestion(
                question: "What is the purpose of an interface?",
                answer: "It provides a template for classes, guaranteeing certain members",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question10.png"),
                choicesWithFeedback: new()
                {
                    { "It provides a template for classes, guaranteeing certain members", "" },
                    { "It provides a way for components to interact with the user", "" },
                    { "They have no real function", "" },
                    { "It's a special type of overridable class", "" }
                })
        };

        private static readonly List<IQuestion> OOPHistoryQuestions = new()
        {
            new MultiChoiceQuestion(
                question: "Who is considered to be the pioneer ('inventor') of OOP?",
                answer: "Alan Kay",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question1.png"),
                choicesWithFeedback: new()
                {
                    { "Alan Kay", "" },
                    { "Claude Shannon", "" },
                    { "Alan Turing", "" },
                    { "Cecilia Berdichevsky", "" }
                }),
            new TrueFalseQuestion(
                question: "The big idea in OOP is messaging.",
                answer: true,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question2.png"),
                feedback: "\"I'm sorry that I long ago coined the term 'objects' for this topic because it gets many people to focus on the lesser idea. The big idea is 'messaging'.\" - Alan Kay"),
            new MultiChoiceQuestion(
                question: "Which programming language had a strong influence on the development of OOP?",
                answer: "Lisp",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question3.png"),
                choicesWithFeedback: new()
                {
                    { "Lisp", "" },
                    { "C", "" },
                    { "Fortran", "" },
                    { "Simula", "" }
                }),
            new MultiChoiceQuestion(
                question: "Which program was the first to utilise a GUI (Graphical User Interface)?",
                answer: "Sketchpad",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question4.png"),
                choicesWithFeedback: new()
                {
                    { "Sketchpad", "" },
                    { "Visi On", "" },
                    { "GEM (Graphical Environment Manager)", "" },
                    { "DeskMate", "" }
                }),
            new MultiChoiceQuestion(
                question: "Who is considered to be the world's first computer programmer?",
                answer: "Ada Lovelace",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question5.png"),
                choicesWithFeedback: new()
                {
                    { "Ada Lovelace", "" },
                    { "Charles Babbage", "" },
                    { "Claude Shannon", "" },
                    { "Konrad Zuse", "" }
                }),
            new MultiChoiceQuestion(
                question: "When was the concept of OOP first mooted at MIT (Massachussetts Institute of Technology)?",
                answer: "1950's",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question6.png"),
                choicesWithFeedback: new()
                {
                    { "1950's", "" },
                    { "1870's", "" },
                    { "1970's", "" },
                    { "1990's", "" }
                }),
            new MultiChoiceQuestion(
                question: "Which programming language is considered to be the first object-oriented language?",
                answer: "Simula",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question7.png"),
                choicesWithFeedback: new()
                {
                    { "Simula", "" },
                    { "Smalltalk", "" },
                    { "Java", "" },
                    { "Pascal", "" }
                }),
            new MultiChoiceQuestion(
                question: "When did OOP become the dominant programming paradigm?",
                answer: "1990's",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question8.png"),
                choicesWithFeedback: new()
                {
                    { "1990's", "" },
                    { "2000's", "" },
                    { "1980's", "" },
                    { "1970's", "" }
                }),
            new MultiChoiceQuestion(
                question: "Roughly how many programming languages are available for use as of 2022?",
                answer: "700",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question9.png"),
                choicesWithFeedback: new()
                {
                    { "700", "" },
                    { "10000", "" },
                    { "3000", "" },
                    { "1500", "" }
                }),
            new MultiChoiceQuestion(
                question: "Which of these is not amongst the 10 most popular programming languages as of August 2022 (by the TIOBE Index)?",
                answer: "Swift",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question10.png"),
                choicesWithFeedback: new()
                {
                    { "Swift", "" },
                    { "Assembly Language", "" },
                    { "C#", "" },
                    { "PHP", "" }
                })
        };

        private static readonly Dictionary<string, List<IQuestion>> allQuestions = new()
        {
            { "Python", pythonQuestions },
            { "C#", cSharpQuestions },
            { "History of OOP", OOPHistoryQuestions }
        };

        /// <summary>
        /// The question categories in this program.
        /// </summary>
        /// <returns>A list of question categories.</returns>
        public List<string> GetQuestionCategories()
        {
            return allQuestions.Keys.ToList();
        }

        /// <summary>
        /// The questions in this program.
        /// </summary>
        /// <returns>A dictionary with the key as the question category and the value as a list of questions for that category.</returns>
        public Dictionary<string, List<IQuestion>> GetQuestions()
        {
            return allQuestions;
        }
    }
}
