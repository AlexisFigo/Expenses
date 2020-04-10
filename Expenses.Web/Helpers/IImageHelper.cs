using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Helpers
{
    public interface IImageHelper
    {
        string UploadImage(byte[] pictureArray, string folder);
    }
}
