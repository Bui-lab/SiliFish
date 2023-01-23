﻿using Microsoft.Web.WebView2.WinForms;
using SiliFish.Definitions;

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
                    tempFile = Path.GetFullPath(System.Guid.NewGuid().ToString() + ".html", tempFolder);
                    GlobalSettings.TempFiles.Add(tempFile);
                    File.WriteAllText(tempFile, html.ToString());
                    htmlContainer.CoreWebView2.Navigate(tempFile);
                }
                else
                    htmlContainer.CoreWebView2.NavigateToString(html);
            }
            catch { }
        }

    }
}
