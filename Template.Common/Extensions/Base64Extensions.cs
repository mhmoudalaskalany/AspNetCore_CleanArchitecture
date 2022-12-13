using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Template.Common.Helpers.MediaUploader;

namespace Template.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class Base64Extensions
    {
        public static (string extension, string data) GetBase64StringContents(this string input)
        {
            input = input.Replace("data:", "");
            var parts = input.Split(';').ToList();
            return (MimeTypeMap.GetExtension(parts[0]), parts[1].Replace("base64,", ""));
        }
    }
}