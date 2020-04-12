using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Expenses.Common.Helpers
{
    public interface IFilesHelper
    {
        byte[] ReadFully(Stream input);
    }
}
