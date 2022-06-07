using System.IO;
using System.Reflection;

namespace SiliFish.Services
{
    public class VisualsGenerator
    {
        public string ReadEmbeddedResource(string resource)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

    }
}
