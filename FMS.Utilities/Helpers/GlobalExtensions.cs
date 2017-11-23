using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using FMS.Utilities.StringKeys;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace FMS.Utilities.Helpers
{
    public static class GlobalExtensions
    {
        public static string Shorten(this string str, int toLength, string cutOffReplacement = " ...")
        {
            if (string.IsNullOrEmpty(str) || str.Length <= toLength)
                return str;
            return str.Remove(toLength) + cutOffReplacement;
        }

        public static IEnumerable<T> TakePage<T>(this IEnumerable<T> items, int page, int pageSize = 10) where T : class => items.Skip(pageSize * (page - 1)).Take(pageSize);

        public static HtmlString ToHtmlString(this string value) => new HtmlString(value);

        public static string SeperateWords(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            string output = "";
            char[] chars = str.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == chars.Length - 1 || i == 0 || Char.IsWhiteSpace(chars[i]))
                {
                    output += chars[i];
                    continue;
                }

                if (char.IsUpper(chars[i]) && Char.IsLower(chars[i - 1]))
                    output += " " + chars[i];
                else
                    output += chars[i];
            }

            return output;
        }
        public static string JoinWordsWith(this string str, string delimeter)
        {
            var wordsToJoin = str.MakeOneWord().SeperateWords().Split(' ');
            return string.Join(delimeter, wordsToJoin);
        }

        public static string MakeOneWord(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            string output = "";
            char[] chars = str.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == chars.Length - 1 || i == 0)
                {
                    output += chars[i];
                    continue;
                }

                if (char.IsWhiteSpace(chars[i]))
                    continue;

                if (char.IsWhiteSpace(chars[i - 1]))
                    output += chars[i].ToString().ToUpper();
                else
                    output += chars[i];
            }


            return output;
        }

        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var output = "";
            char[] chars = str.ToLower().ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 0)
                {
                    output += chars[i].ToString().ToUpper();
                    continue;
                }

                if (char.IsWhiteSpace(chars[i - 1]))
                    output += chars[i].ToString().ToUpper();
                else
                    output += chars[i];
            }

            return output;
        }

        public static IList<FieldInfo> GetConstant(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static |
                            BindingFlags.FlattenHierarchy)
                 .Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }

        public static double ToTimeline(this DateTime date)
        {

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            ts = ts.Add(TimeSpan.FromHours(0));
            var delta = ts.TotalSeconds;

            return delta;
        }

        public static double ToTimeline(this DateTime? date)
        {
            if (date.HasValue)
            {
                var convertedDate = date.Value;
                return convertedDate.ToTimeline();
            }
            return Convert.ToDouble(0);
        }
        public static TimeSpan ToTimeSpan(this DateTime startDate, DateTime endDate, int offSet = 0)
        {

            var ts = new TimeSpan(endDate.Ticks - startDate.Ticks);
            ts = ts.Add(TimeSpan.FromHours(offSet));

            return ts;
        }

        public static string ToDateString(this DateTime date, string format = "")
        {

            if (string.IsNullOrEmpty(format))
            {
                format = "dd-MMM-yyyy";
            }
            var toReturn = date.ToString(format);

            return toReturn;
        }

        public static string ToDateString(this DateTime? date, string format = "")
        {
            if (date.HasValue)
            {
                var convertedDate = date.Value;
                return convertedDate.ToDateString(format);
            }
            return "";
        }

        public static string ToTimeString(this DateTime date)
        {
            var toReturn = date.ToString("hh:mm tt");

            return toReturn;
        }

        public static string ToTimeString(this DateTime? date)
        {
            if (date.HasValue)
            {
                var convertedDate = date.Value;
                return convertedDate.ToTimeString();
            }
            return "";
        }

        public static string ToMonthString(this DateTime date)
        {
            var formatedDate = date.ToString("dd-MMM-yyyy");
            var splittedDate = formatedDate.Split('-');
            var toReturn = splittedDate[1];

            return toReturn;
        }

        public static string ToMonthString(this DateTime? date)
        {
            if (date.HasValue)
            {
                var convertedDate = date.Value;
                return convertedDate.ToMonthString();
            }
            return "";
        }

        public static string ArrayToCommaSeparatedString(this string[] array)
        {
            var newArray = new string[array.Length];
            var i = 0;

            foreach (var s in array)
            {
                newArray.SetValue(s.SeperateWords(), i);
                i++;
            }
            return string.Join(",", newArray);
        }

        public static string NigerianMobile(this string str)
        {
            const string naijaPrefix = "234";
            if (string.IsNullOrEmpty(str) || str.Length <= 10)
                return str;

            str = str.TrimStart('+');
            var prefix = str.Remove(3);

            if (prefix.Equals(naijaPrefix))
            {
                return str;
            }
            str = str.TrimStart('0');
            str = naijaPrefix + str;
            return str;
        }

        public static bool IsNigerianMobile(this string str)
        {
            var naijaRegex = new Regex(@"^(0)[0-9]{10}$");
            return naijaRegex.IsMatch(str);
        }

        public static T[] CombineArray<T>(this T[] firstArray, T[] secondArray)
        {
            var newArray = new T[firstArray.Length + secondArray.Length];
            Array.Copy(firstArray, newArray, firstArray.Length);
            Array.Copy(secondArray, 0, newArray, firstArray.Length, secondArray.Length);
            return newArray;
        }

        public static string ControllerStringName(this Controller controller)
        {
            if (controller == null)
            {
                return "";
            }
            var controllerString = nameof(controller);
            return controllerString.Replace(nameof(Controller), "");
        }

        /// <summary>
        /// Formats this value as a currency amount.
        /// </summary>
        /// <param name="value">The Decimal value to be formatted.</param>
        /// <param name="currency"> The currency symbol (defaults to the HTML entity for Naira).</param>
        /// <returns></returns>
        public static IHtmlContent ToCurrency(this decimal? value, string currency = CurrencyKeys.NAIRA)
        {
            if (!value.HasValue) value = Convert.ToDecimal(0);// return MvcHtmlString.Create("");
            return ToCurrency(value.Value, currency);
        }

        /// <summary>
        /// Formats this value as a currency amount.
        /// </summary>
        /// <param name="value">The Decimal value to be formatted.</param>
        /// <param name="currency"> The currency symbol (defaults to the HTML entity for Naira).</param>
        /// <returns></returns>
        public static IHtmlContent ToCurrency(this decimal value, string currency = CurrencyKeys.NAIRA)
        {
            //var symbol = currency.GetCurrencyCode();
            return new HtmlString($"{currency}{value:N}");
        }

        //var symbol = currency.GetCurrencyCode();
        public static IHtmlContent ToPercentage(this double value) => new HtmlString($"{value:P}");
        public static IHtmlContent ToPercentage(this double? value)
        {
            if (!value.HasValue) value = Convert.ToDouble(0);
            return ToPercentage(value);
        }

        public static TimeSpan? To24Hour(this string hoursIn12Format)
        {
            var regex = new Regex(RegexKeys.TIME_FORMAT);
            if (!regex.IsMatch(hoursIn12Format))
            {
                return null;
            }
            var splitedTime = hoursIn12Format.Split(' ');

            if (splitedTime.Any())
            {
                var timecomponent = splitedTime[0];
                var timeOfTheDaycomponent = splitedTime[1];
                int timeOfTheDay;

                var splittedTimeComponent = timecomponent.Split(':');
                var hour = Convert.ToInt32(splittedTimeComponent[0]);
                var minute = splittedTimeComponent[1];

                if (timeOfTheDaycomponent.ToLower() == "am")
                {
                    timeOfTheDay = 0;
                }
                else
                {
                    if (timeOfTheDaycomponent.ToLower() == "pm" && hour != 12)
                    {
                        timeOfTheDay = 12;
                    }
                    else
                    {
                        timeOfTheDay = 0;
                    }
                }

                var totalHour = hour + timeOfTheDay;
                var parsedTime = $"{totalHour}:{minute}";

                var toReturn = TimeSpan.Parse(parsedTime);
                return toReturn;
            }
            return null;
        }

        public static string[] SequentialNumberStringArray(int total, string[] exclude = null)
        {
            var excludeLength = exclude?.Where(s => !string.IsNullOrEmpty(s)).ToArray().Length ?? 0;
            var dataLength = total - excludeLength;
            var newArray = new string[dataLength];
            var excludedCount = 0;

            for (var i = 0; i < total; i++)
            {
                var value = i + 1;
                var valueString = Convert.ToString(value);
                var toBeExcluded = exclude?.Contains(valueString);
                if (!toBeExcluded.GetValueOrDefault())
                {
                    newArray.SetValue(valueString, i - excludedCount);
                }
                else
                {
                    excludedCount += 1;
                }
            }
            return newArray;
        }

        public static void SequentialNumber(this int oldNumber, out int newNumber, int step = 1)
        {
            newNumber = oldNumber + step;
        }
        public static void SequentialNumber(this long oldNumber, out long newNumber, int step = 1)
        {
            newNumber = oldNumber + step;
        }

        public static bool IsEven(this int value)
        {
            if (value.Equals(0))
            {
                return true;
            }
            var rem = value % 2;
            if (rem.Equals(0))
            {
                return true;
            }
            return false;
        }

        public static string RemoveAllWhiteSpace(this string value) => Regex.Replace(value, @"\s+", "");

        public static string UrlEncode(this string message) => WebUtility.UrlEncode(message);
        public static string UrlDecode(this string message) => WebUtility.UrlDecode(message);
    }
}
