using System.Linq;
using BackendCore.Common.MediaUploader;

namespace BackendCore.Common.Extensions
{
    public static class Base64Extensions
    {
        public static (string extension, string data) GetBase64StringContents(this string input)
        {
            input = input.Replace("data:", "");
            var parts = input.Split(';').ToList<string>();
            return (MimeTypeMap.GetExtension(parts[0]), parts[1].Replace("base64,", ""));
        }
    }
}