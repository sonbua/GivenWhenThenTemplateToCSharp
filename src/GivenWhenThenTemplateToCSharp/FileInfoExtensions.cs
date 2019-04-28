using System.IO;

namespace GivenWhenThenTemplateToCSharp
{
    internal static class FileInfoExtensions
    {
        public static string NameWithoutExtension(this FileInfo fileInfo)
        {
            var name = fileInfo.Name;

            return name.Remove(name.Length - fileInfo.Extension.Length);
        }
    }
}