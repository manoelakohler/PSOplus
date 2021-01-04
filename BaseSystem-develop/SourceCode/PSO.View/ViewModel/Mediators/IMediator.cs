using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO.View.ViewModel.Mediators
{
    public interface IMediator
    {
        /// <summary>
        /// Registra um colleague na lista de colleagues. Caso o colleague já esteja na lista, gera uma excessão do tipo MediatorPatternException.
        /// Este método é thread-safe.
        /// </summary>
        /// <param name="colleague"></param>
        void Register(IColleague colleague);

        /// <summary>
        /// Des-registra um colleague na lista de colleagues. Caso o colleague não esteja na lista, gera uma excessão do tipo 
        /// MediatorPatternException. Este método é thread-safe.
        /// </summary>
        /// <param name="colleague"></param>
        void Unregister(IColleague colleague);

        /// <summary>
        /// Envia a mensagem e os anexos a todos os IColleague registrados via método Register. Este método é thread-safe.
        /// </summary>
        /// <param name="message">Um enum que representa o mensagem.</param>
        /// <param name="attachments">Uma lista de objetos que podem ser anexados à mensagem.</param>
        void SendMessage(Enum message, params object[] attachments);
    }
}
