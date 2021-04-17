using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain
{
    public class GenericSpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; protected set; }
    }
}
