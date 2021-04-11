using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureFunctionExample
{
    public static class Extensions
    {
        public static IDisposable BeginScopeWith<T>(this ILogger logger, T state)
        {
            return logger.BeginScope(state.ToDictionary());
        }

        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            return obj.GetType()
                .GetProperties()
                .ToDictionary(x => x.Name, x => x.GetValue(obj));
        }
    }
}
