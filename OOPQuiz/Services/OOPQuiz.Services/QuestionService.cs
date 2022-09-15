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
                question: "Which of the following is typically at the beginning of every file using OOP?",
                answer: "from dataclasses import dataclass",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question1.png"),
                choices: new()
                {
                    new("from dataclasses import dataclass"),
                    new("from dataclasses import *", "This would work, but it is very bad practice."),
                    new("import dataclasses", "This would work, but make for cumbersome programming. Why make life hard for yourself?"),
                    new("import sys", "")
                },
                feedback: "Note that this step is actually optional - you can do OOP in Python without the dataclasses module."),
            new MultiChoiceQuestion(
                question: "What best describes 'age' in the following code snippet?",
                answer: "It is a member",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question2.png"),
                choices: new()
                {
                    new("It is a member"),
                    new("It is an integer", "Yes, but also no."),
                    new("It is a property", "Not quite - members store information, and properties dictate what has access to members."),
                    new("It is an attribute", "Yes, it is an attribute of the Dog class, but more specifically it is a member.")
                }),
            new MultiChoiceQuestion(
                question: "What best describes 'age' in the following code snippet?",
                answer: "It is a property",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question3.png"),
                choices: new()
                {
                    new("It is a property"),
                    new("It is a member", "Not quite - members store information, and properties dictate what has access to members."),
                    new("It is a function", "Functions are defined outside of classes (a function defined inside a class is called a method)."),
                    new("It is a variable", "")
                }),
            new OpenEndedQuestion(
                question: "What is missing from the following code snippet?",
                answer: "self",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question4.png"),
                feedback: "A correct answer was: self. You may have noticed it was missing as a parameter from the property. Strictly speaking the use of self as a parameter is just a convention – you can use any parameter you like as long as you are consistent. But please don't be that guy; use self – this is a good convention."),
            new TrueFalseQuestion(
                question: "It is possible to set the age of a Dog object to 5.",
                answer: false,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question5.png")),
            new MultiChoiceQuestion(
                question: "What is the purpose of the ' -> int' in the following code snippet?",
                answer: "It is effectively a form of documentation",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question6.png"),
                choices: new()
                {
                    new("It is effectively a form of documentation"),
                    new("It causes the property to raise an error if an integer is not returned", "In many other languages this would be true."),
                    new("It specifies the parameters of the property must be integers"),
                    new("It doesn't really mean anything", "You are absolutely right; it doesn't really mean anything (thanks Python).")
                },
                feedback: "In Python it provides an *indication* that the property should return an integer."),
            new MultiChoiceQuestion(
                question: "What is the purpose of preceding protected members with an underscore?",
                answer: "It denotes that a member is protected by the class",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question7.png"),
                choices: new()
                {
                    new("It denotes that a member is protected by the class"),
                    new("It prevents the member from being accessed directly"),
                    new("It has literally no purpose", "Functionally, yes."),
                    new("All integer variables are preceded by an underscore")
                },
                feedback: "Because Python doesn't protect attributes properly, you can access anything in a class from anywhere anyway."),
            new MultiChoiceQuestion(
                question: "What best describes 'generate_dog_tag' in the following code snippet?",
                answer: "It is a method",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question8.png"),
                choices: new()
                {
                    new("It is a method"),
                    new("It is a function", "Almost - this is a method not a function because it is defined inside a class."),
                    new("It is a property"),
                    new("It is an attribute", "Yes, it is an attribute of the Dog class, but more specifically it is a method.")
                }),
            new MultiChoiceQuestion(
                question: "What is the purpose of a protocol?",
                answer: "It provides a template for classes, guaranteeing certain members",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question9.png"),
                choices: new()
                {
                    new("It provides a template for classes, guaranteeing certain members"),
                    new("It provides a way for data to be shared between classes"),
                    new("They have no real function", "Very tempted to mark you correct, considering this is Python."),
                    new("It's a special type of overridable class", "You aren't wrong, but we can be more specific.")
                },
                feedback: "In reality there is no guarantee because Python doesn't enforce them. This also makes them hella painful to use."),
            new TrueFalseQuestion(
                question: "Subclasses can override members defined by base classes.",
                answer: true,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}python_question10.png"))
        };

        private static readonly List<IQuestion> cSharpQuestions = new()
        {
            new MultiChoiceQuestion(
                question: "What best describes '_age' in the following code snippet?",
                answer: "It is a field",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question1.png"),
                choices: new()
                {
                    new("It is a field"),
                    new("It is an integer", "Yes, but also no."),
                    new("It is a property", "Not quite - fields store information, and properties dictate what has access to fields."),
                    new("It is a member", "Yes, it is a member of the Dog class, but more specifically it is a field.")
                }),
            new MultiChoiceQuestion(
                question: "What best describes 'Age' in the following code snippet?",
                answer: "It is a property",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question2.png"),
                choices: new()
                {
                    new("It is a property"),
                    new("It is a field", "Not quite - fields store information, and properties dictate what has access to fields."),
                    new("It is a function", "Functions are defined outside of classes (a function defined inside a class is called a method)."),
                    new("It is a variable")
                }),
            new MultiChoiceQuestion(
                question: "What needs to be at the beginning of every file using OOP?",
                answer: "A namespace declaration",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question3.png"),
                choices: new()
                {
                    new("A namespace declaration"),
                    new("using Dataclasses;"),
                    new("using System.Dataclasses;"),
                    new("using System;")
                },
                feedback: "This isn't specific to OOP - every C# file needs this."),
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
                choices: new()
                {
                    new("It is shorthand for a getter"),
                    new("It is a comparison operator", "If it were a comparison operator it would be the other way round (>=)!"),
                    new("It increments age by 1", "You are thinking of ++."),
                    new("It sets the accessible property Age to the protected field age")
                },
                feedback: "Defining getters in this way precludes the option of defining a setter."),
            new MultiChoiceQuestion(
                question: "Which of these is not a protection modifier?",
                answer: "const",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question7.png"),
                choices: new()
                {
                    new("const"),
                    new("private"),
                    new("public", "Public is the default (or implicit) protection modifier for C#."),
                    new("protected")
                },
                feedback: "Const doesn't specify what has access to a variable, whereas protection modifiers do."),
            new MultiChoiceQuestion(
                question: "What best describes 'GenerateDogTag' in the following code snippet?",
                answer: "It is a method",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question8.png"),
                choices: new()
                {
                    new("It is a method"),
                    new("It is a function", "This is a method not a function because it is defined inside a class."),
                    new("It is a property"),
                    new("It is a member", "Yes, it is a member of the Dog class, but more specifically it is a method.")
                }),
            new TrueFalseQuestion(
                question: "Private members can't be accessed by subclasses.",
                answer: true,
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question9.png")),
            new MultiChoiceQuestion(
                question: "What is the purpose of an interface?",
                answer: "It provides a template for classes, guaranteeing certain members",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}csharp_question10.png"),
                choices: new()
                {
                    new("It provides a template for classes, guaranteeing certain members"),
                    new("It provides a way for components to interact with the user"),
                    new("They have no real function"),
                    new("It's a special type of overridable class")
                },
                feedback: "In C# interfaces are enforced and are immensely useful - this very question was constructed with them!")
        };

        private static readonly List<IQuestion> OOPHistoryQuestions = new()
        {
            new MultiChoiceQuestion(
                question: "Who is considered to be the pioneer ('inventor') of OOP?",
                answer: "Alan Kay",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question1.png"),
                choices: new()
                {
                    new("Alan Kay"),
                    new("Claude Shannon"),
                    new("Alan Turing"),
                    new("Cecilia Berdichevsky")
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
                choices: new()
                {
                    new("Lisp"),
                    new("C"),
                    new("Fortran"),
                    new("Simula")
                }),
            new MultiChoiceQuestion(
                question: "Which program was the first to utilise a GUI (Graphical User Interface)?",
                answer: "Sketchpad",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question4.png"),
                choices: new()
                {
                    new("Sketchpad"),
                    new("Visi On"),
                    new("GEM (Graphical Environment Manager)"),
                    new("DeskMate")
                }),
            new MultiChoiceQuestion(
                question: "Who is considered to be the world's first computer programmer?",
                answer: "Ada Lovelace",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question5.png"),
                choices: new()
                {
                    new("Ada Lovelace"),
                    new("Charles Babbage"),
                    new("Claude Shannon"),
                    new("Konrad Zuse")
                }),
            new MultiChoiceQuestion(
                question: "When was the concept of OOP first mooted at MIT (Massachussetts Institute of Technology)?",
                answer: "1950's",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question6.png"),
                choices: new()
                {
                    new("1950's"),
                    new("1870's"),
                    new("1970's"),
                    new("1990's")
                }),
            new MultiChoiceQuestion(
                question: "Which programming language is considered to be the first object-oriented language?",
                answer: "Simula",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question7.png"),
                choices: new()
                {
                    new("Simula"),
                    new("Smalltalk"),
                    new("Java"),
                    new("Pascal")
                }),
            new MultiChoiceQuestion(
                question: "When did OOP become the dominant programming paradigm?",
                answer: "1990's",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question8.png"),
                choices: new()
                {
                    new("1990's"),
                    new("2000's"),
                    new("1980's"),
                    new("1970's")
                }),
            new MultiChoiceQuestion(
                question: "Roughly how many programming languages are available for use as of 2022?",
                answer: "700",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question9.png"),
                choices: new()
                {
                    new("700"),
                    new("10000"),
                    new("3000"),
                    new("1500")
                }),
            new MultiChoiceQuestion(
                question: "Which of these is not amongst the 10 most popular programming languages as of August 2022 (by the TIOBE Index)?",
                answer: "Swift",
                imageURI: Path.Combine(Directory.GetCurrentDirectory(), $@"{imagePath}oophistory_question10.png"),
                choices: new()
                {
                    new("Swift"),
                    new("Assembly Language"),
                    new("C#"),
                    new("PHP")
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
