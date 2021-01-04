using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO.View.ViewModel.Mediators
{
    public interface IColleague
    {
        /// <summary>
        /// Repassa a esta instância do colleague uma nova mensagem recentemente enviada ao mediador.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="attachments"></param>
        void NotifyNewMessage(Enum message, params object[] attachments);
    }
}
