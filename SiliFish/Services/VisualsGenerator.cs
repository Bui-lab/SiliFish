using System.IO;
using System.Reflection;

namespace SiliFish.Services
{
    internal class VisualsGenerator
    {
        public string ReadEmbeddedResource(string resource)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

    }
}
