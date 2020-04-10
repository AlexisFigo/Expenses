using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public string UploadImage(byte[] pictureArray, string folder)
        {
            MemoryStream stream = new MemoryStream(pictureArray);
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.jpg";

            try
            {
                stream.Position = 0;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folder}", file);
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch
            {
                return string.Empty;
            }

            return $"~/images/{folder}/{file}";
        }
    }
}
