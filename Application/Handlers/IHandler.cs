using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public interface IHandler<in TIn, TOut>
    {
        Task<TOut> HandleAsync(TIn request);
    }
}
