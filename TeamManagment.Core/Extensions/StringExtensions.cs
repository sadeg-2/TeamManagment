using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace TeamManagment.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsHtmlOrCssOrScript(this string input)
        {
            string htmlPattern = @"<[^>]*>";
            string cssPattern = @"<style[^>]*>.*<\/style>";
            string scriptPattern = @"<script[^>]*>.*<\/script>";
            
            string combinedPattern = $"{htmlPattern}|{cssPattern}|{scriptPattern}";
            
            return Regex.IsMatch(input, combinedPattern, RegexOptions.IgnoreCase);
        }
        public static string HtmlEncode(this string input)
        {
             return HttpUtility.HtmlEncode(input);
        }
    
    }
}
