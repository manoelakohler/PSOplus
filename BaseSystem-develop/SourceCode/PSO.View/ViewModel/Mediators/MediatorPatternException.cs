using System;

namespace PSO.View.ViewModel.Mediators
{
    internal class MediatorPatternException : Exception
    {
        private string _message;

        protected MediatorPatternException(string message)
            : base(message)
        {
            _message = message;
        }

        public static MediatorPatternException ColleagueAlreadyRegistered(IColleague colleague)
        {
            return new MediatorPatternException(string.Format("O colleague {0} já está registrado.", colleague));
        }

        public static MediatorPatternException ColleagueAlreadyUnregistered(IColleague colleague)
        {
            return new MediatorPatternException(string.Format("O colleague {0} já foi removido.", colleague));
        }
    }
}
