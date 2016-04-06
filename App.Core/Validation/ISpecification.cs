using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Exceptions;

namespace App.Core.Validation
{
    public interface ISpecification<TData>
    {
        bool IsSatisfyBy(TData data, IList<ErrorExtraInfo> violations = null);
        ISpecification<TData> Not(ISpecification<TData> specification);
        ISpecification<TData> And(ISpecification<TData> right);
        ISpecification<TData> Or(ISpecification<TData> right);
    }
}
