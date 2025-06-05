using SiliFish.Services;
using System.Text;


namespace SiliFish.Helpers
{
    public static class HTMLUtil
    {
        public static string CreateScriptHTML(string embeddedScript)
        {
            StringBuilder scriptHTML = new();
            scriptHTML.AppendLine("<script>");
            scriptHTML.AppendLine(EmbeddedResourceReader.ReadEmbeddedText(embeddedScript));
            scriptHTML.AppendLine("</script>");
            return scriptHTML.ToString();
        }
    }
}
