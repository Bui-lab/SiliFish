using System.IO;
using System.Reflection;

namespace SiliFish.Services
{
    public class EmbeddedResourceReader
    {
        public static string ReadEmbeddedText(string resource) => ReadEmbeddedText(Assembly.GetExecutingAssembly(), resource);
        public static string ReadEmbeddedText(Assembly assembly, string resource)
        {
            using var stream = assembly.GetManifestResourceStream(resource);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static byte[] ReadEmbeddedBinary(string resource) => ReadEmbeddedBinary(Assembly.GetExecutingAssembly(), resource);
        public static byte[] ReadEmbeddedBinary(Assembly assembly, string resource)
        {
            using var stream = assembly.GetManifestResourceStream(resource);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
