using System.IO;
using System.Reflection;

namespace SiliFish.Services
{
    public class VisualsGenerator
    {
        public static string ReadEmbeddedText(string resource)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static byte[] ReadEmbeddedBinary(string resource)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
