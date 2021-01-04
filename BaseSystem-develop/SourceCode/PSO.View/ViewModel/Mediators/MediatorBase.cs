using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO.View.ViewModel.Mediators
{
    public abstract class MediatorBase : IMediator
    {
        protected internal readonly HashSet<IColleague> RegisteredColleagues = new HashSet<IColleague>();

        protected internal readonly object LockObject = new object();

        public bool IsRegistered(IColleague colleague)
        {
            return RegisteredColleagues.Contains(colleague);
        }

        public virtual void Register(IColleague colleague)
        {
            lock (LockObject)
            {
                if (!RegisteredColleagues.Contains(colleague))
                    RegisteredColleagues.Add(colleague);
                else
                    throw MediatorPatternException.ColleagueAlreadyRegistered(colleague);
            }
        }

        public virtual void Unregister(IColleague colleague)
        {
            lock (LockObject)
            {
                if (RegisteredColleagues.Contains(colleague))
                {
                    RegisteredColleagues.Remove(colleague);
                }
                else
                    throw MediatorPatternException.ColleagueAlreadyUnregistered(colleague);
            }
        }

        public virtual void SendMessage(Enum message, params object[] attachments)
        {
            lock (LockObject)
            {
                foreach (var registeredColleague in RegisteredColleagues)
                {
                    registeredColleague.NotifyNewMessage(message, attachments);
                }
            }
        }
    }
}
