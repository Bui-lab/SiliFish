﻿using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.Services;

namespace SiliFish.UI.Extensions
{
    public static class HtmlExtensions
    {
        public static void NavigateTo(this WebView2 htmlContainer, string html, string tempFolder, ref string tempFile, ref bool navigated)
        {
            try
            {
                tempFile = "";
                if (html.Length > 1024 * 1024 * GlobalSettings.FileSizeWarningLimit && !string.IsNullOrEmpty(tempFolder))
                {
                    string target = "plot.html";                    
                    tempFile = FileUtil.SaveToOutputFolder(target, html);
                    navigated = false;
                }
                else if (html.Length > 1000000 && !string.IsNullOrEmpty(tempFolder))
                {
                    tempFile = System.Guid.NewGuid().ToString() + ".html";
                    tempFile = FileUtil.SaveToTempFolder(tempFile, html);
                    GlobalSettings.TempFiles.Add(tempFile);
                    htmlContainer.CoreWebView2.Navigate(tempFile);
                    navigated = true;
                }
                else
                {
                    htmlContainer.CoreWebView2.NavigateToString(html);
                    navigated = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                navigated = false;
            }
        }

    }
}
