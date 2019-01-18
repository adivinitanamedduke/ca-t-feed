using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.Validation
{
    public interface ILengthRule : IRule
    {
        object GetMinLength();

        object GetMaxLength();
    }
}
