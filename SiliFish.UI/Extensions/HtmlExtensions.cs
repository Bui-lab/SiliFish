using Microsoft.Web.WebView2.WinForms;
using SiliFish.Definitions;
using SiliFish.Helpers;

namespace SiliFish.UI.Extensions
{
    public static class HtmlExtensions
    {
        public static void NavigateTo(this WebView2 htmlContainer, string html, string tempFolder, ref string tempFile)
        {
            try
            {
                tempFile = "";
                if (html.Length > 1000000 && !string.IsNullOrEmpty(tempFolder))
                {
                    tempFile = System.Guid.NewGuid().ToString() + ".html";
                    tempFile = FileUtil.SaveToTempFolder(tempFile, html.ToString());
                    GlobalSettings.TempFiles.Add(tempFile);
                    htmlContainer.CoreWebView2.Navigate(tempFile);
                }
                else
                    htmlContainer.CoreWebView2.NavigateToString(html);
            }
            catch { }
        }

    }
}
