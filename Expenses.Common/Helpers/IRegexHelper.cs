using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Common.Helpers
{
    public interface IRegexHelper
    {
        bool IsValidEmail(string emailaddress);
    }
}
