using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Service
{
    public interface IConversaService
    {
        Task<IEnumerable<ConversaResult>> BuscarConversasAsync(DateTime? dataInicio, DateTime? dataFim, string status, int? idAtendente);
    }

}
