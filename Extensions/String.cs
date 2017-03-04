using MiscUtil;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RevStack.Mvc
{
    public static partial class Extensions
    {
        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tail_length"></param>
        /// <returns></returns>
        public static string LastChars(this string source, int tailLength)
        {
            if (tailLength >= source.Length)
                return source;
            return source.Substring(source.Length - tailLength);
        }

        public static string FirstChars(this string source, int startLength)
        {
            return source.Substring(0, startLength);
        }

        public static string TrimFirstChar(this string src)
        {
            return src.Substring(1);
        }

        public static string TrimLastChar(this string src)
        {
            if (string.IsNullOrEmpty(src))
            {
                return src;
            }
            else {
                return src.TrimEnd(src[src.Length - 1]);
            }
        }

        public static bool InString(this string source, string value)
        {
            var index = source.IndexOf(value);
            return (index > -1);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Concatenate(this string source, string str, string separator)
        {
            if (source.Length > 0)
            {
                source = source + separator + str;
            }
            else
            {
                source = str;
            }

            return source;
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string source)
        {
            var myTI = new CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(source);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToProperCase(this string source)
        {
            const string pattern = @"(?<=\w)(?=[A-Z])";
            var result = Regex.Replace(source, pattern,
                " ", RegexOptions.None);
            return result.Substring(0, 1).ToUpper() + result.Substring(1);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string source)
        {
            source = source.ToTitleCase();
            return source.Substring(0, 1).ToLower() + source.Substring(1);
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToWrappedInQuotes(this string value)
        {
            return "\"" + value + "\"";
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToWrappedInSingleQuotes(this string value)
        {
            return "'" + value + "'";
        }

        /// <summary>
        ///     Returns the first part of a split string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="strSeparator"></param>
        /// <returns></returns>
        public static string ToStringFirstPart(this string source, char chrSeparator)
        {
            var parts = source.Split(chrSeparator);
            return parts[0];
        }

        /// <summary>
        ///     Returns the last part of a split string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="chrSeparator"></param>
        /// <returns></returns>
        public static string ToStringLastPart(this string source, char chrSeparator)
        {
            var parts = source.Split(chrSeparator);
            var length = parts.Length;
            return parts[length - 1];
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToPhraseCase(this string source)
        {
            return Regex.Replace(source, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDefaultSelectValue(this string source)
        {
            if (source.ToLower() == "select")
            {
                return null;
            }
            return source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ToPossessiveCase(this string src)
        {
            if (src.LastChars(1).ToLower() == "s") return src + "'";
            else return src + "'s";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ToHtmlLink(this string src)
        {
            return "<a href =\"" + src +"\">" + src + "</a>";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static string ToHtmlLink(this string src, string label)
        {
            return "<a href =\"" + src + "\">" + label + "</a>";
        }

        public static decimal ToRatioValue(this decimal src)
        {
            if (src > 1)
            {
                src = src / 100;
            }
            return src;
        }

        public static decimal ToPercentageValue(this decimal src)
        {
            if (src < 1)
            {
                src = src * 100;
            }
            return src;
        }

        public static string ToHtmlLineBreak(this string src)
        {
            src = src.Replace(Environment.NewLine, "<br>");
            return src;
        }

        public static string ToMonthName(this int src)
        {
            string month = "";
            switch (src)
            {
                case 1:
                    month = "Jan";
                    break;
                case 2:
                    month = "Feb";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "Aug";
                    break;
                case 9:
                    month = "Sep";
                    break;
                case 10:
                    month = "Oct";
                    break;
                case 11:
                    month = "Nov";
                    break;
                case 12:
                    month = "Dec";
                    break;
            }

            return month;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string StripNewLineChars(string msg)
        {
            msg = msg.Replace('\r', ' ');
            msg = msg.Replace('\n', ' ');
            return msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            //var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[StaticRandom.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

    }
}
