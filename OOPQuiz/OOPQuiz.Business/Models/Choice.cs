using Prism.Mvvm;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// A potential answer for a multichoice question.
    /// </summary>
    public class Choice : BindableBase
    {
        protected readonly string _potentialAnswer;
        protected readonly string _feedbackForAnswer;

        protected string _backGroundHexCode;

        /// <summary>
        /// A potential answer for a multichoice question.
        /// </summary>
        /// <param name="potentialAnswer">The answer that can be chosen.</param>
        /// <param name="feedbackForAnswer">The feedback given if this answer is chosen.</param>
        /// <param name="backgroundHexCode">The background for this potential answer in the GUI.</param>
        public Choice(string potentialAnswer, string feedbackForAnswer="", string backgroundHexCode= "#F2F2F2")
        {
            _potentialAnswer = potentialAnswer;
            _feedbackForAnswer = feedbackForAnswer;
            _backGroundHexCode = backgroundHexCode;
        }

        /// <summary>
        /// An answer the user can choose.
        /// </summary>
        public string PotentialAnswer
        {
            get { return _potentialAnswer; }
        }

        /// <summary>
        /// The feedback given if the user chooses this answer.
        /// </summary>
        /// <remarks>
        /// Set to an empty string if no feedback is provided.
        /// </remarks>
        public string FeedbackForAnswer
        {
            get { return _feedbackForAnswer; }
        }

        /// <summary>
        /// The background colour for the choice when displayed in the GUI.
        /// </summary>
        /// <remarks>
        /// Can be set; for example to green to indicate this is the correct answer.
        /// </remarks>
        public string BackgroundHexCode
        {
            get { return _backGroundHexCode; }
            set
            {
                _backGroundHexCode = value;

                RaisePropertyChanged(nameof(BackgroundHexCode));
            }
        }
    }
}
