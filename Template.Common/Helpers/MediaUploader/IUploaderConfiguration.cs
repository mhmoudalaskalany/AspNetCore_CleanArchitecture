using System.IO;

namespace Template.Common.Helpers.MediaUploader
{
    public interface IUploaderConfiguration
    {
        string SaveBase64(string fileBase64, string fileName, string folderName, string oldFileName = null);
        string ConvertToBase64String(string fileName, string folderName);
        Stream ConvertToStream(string fileName, string folderName);
        void RemoveFile(string fileName, string folderName);
    }
}