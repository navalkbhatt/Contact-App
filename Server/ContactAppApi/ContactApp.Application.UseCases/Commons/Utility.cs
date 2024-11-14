using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactApp.Application.UseCases.Commons
{
    public static class FileUtility
    {
        public static string GetFilePath(string fileName)
        {
            // Get the directory where the executing assembly is located
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string directoryPath = Path.GetDirectoryName(assemblyLocation);

            // Combine the directory path with the file name
            return Path.Combine(directoryPath ?? string.Empty, fileName);
        }
    }

    public static class StringExtensions
    {
        private static readonly Regex LowercaseUppercase = new Regex("(\\p{Ll})(\\P{Ll})", RegexOptions.Compiled);

        private static readonly Regex UppercaseUppercaseLowercase = new Regex("(\\P{Ll})(\\P{Ll}\\p{Ll})", RegexOptions.Compiled);

        public static bool Contains(this string @string, string value, StringComparison comparisonType)
        {
            return @string.IndexOf(value, comparisonType) >= 0;
        }

        public static bool StartsOrEndsWith(this string @string, string value, StringComparison comparisonType)
        {
            if (!@string.StartsWith(value, comparisonType))
            {
                return @string.EndsWith(value, comparisonType);
            }

            return true;
        }

        public static bool StartsWithAny(this string @string, params char[] characters)
        {
            return characters.Any((char c) => @string.StartsWith(c.ToString(), StringComparison.Ordinal));
        }

        public static string SplitCamelCase(this string @string, bool capitalize = true)
        {
            string text = LowercaseUppercase.Replace(UppercaseUppercaseLowercase.Replace(@string, "$1 $2"), "$1 $2");
            if (capitalize)
            {
                return text.Capitalize();
            }

            return text;
        }

        public static string Capitalize(this string @string)
        {
            if (@string.Length > 1)
            {
                return char.ToUpper(@string.First()) + @string.Substring(1);
            }

            return @string.ToUpper();
        }

        [Obsolete("Use .Left(...) instead")]
        public static string Truncate(this string @string, int maxLength)
        {
            return @string.Left(maxLength);
        }

        public static string Left(this string value, int length)
        {
            if (value.Length <= length)
            {
                return value;
            }

            return value.Substring(0, length);
        }

        public static string Right(this string value, int length)
        {
            if (value.Length <= length)
            {
                return value;
            }

            return value.Substring(value.Length - length);
        }

        public static string RemoveAll(this string value, params char[] removeCharacters)
        {
            if (removeCharacters != null)
            {
                foreach (char c in removeCharacters)
                {
                    value = value.Replace(c.ToString(), string.Empty);
                }
            }

            return value;
        }

        public static string SubstringUntil(this string input, string target, StringComparison comparisonType)
        {
            int num = input.IndexOf(target, comparisonType);
            if (num != -1)
            {
                return input.Substring(0, num);
            }

            return input;
        }

        public static string ToCommaSeparatedEnglishList(this IEnumerable<string> source, string conjunction)
        {
            IList<string> list3;
            if (!(source is IList<string> list))
            {
                IList<string> list2 = source.ToList();
                list3 = list2;
            }
            else
            {
                list3 = list;
            }

            IList<string> list4 = list3;
            if (list4.Count > 2)
            {
                return string.Join(", ", list4.Take(list4.Count - 1)) + ", " + conjunction + " " + list4.Last();
            }

            if (list4.Count == 2)
            {
                return list4[0] + " " + conjunction + " " + list4[1];
            }

            if (list4.Count == 1)
            {
                return list4[0] ?? "";
            }

            return string.Empty;
        }

        public static string Replace(this string input, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.Length == 0)
            {
                return input;
            }

            if (oldValue == null)
            {
                throw new ArgumentNullException("oldValue");
            }

            if (oldValue.Length == 0)
            {
                throw new ArgumentException("String cannot be of zero length.");
            }

            StringBuilder stringBuilder = new StringBuilder(input.Length);
            bool flag = string.IsNullOrEmpty(newValue);
            int num = 0;
            int num2;
            while ((num2 = input.IndexOf(oldValue, num, comparisonType)) != -1)
            {
                int num3 = num2 - num;
                if (num3 != 0)
                {
                    stringBuilder.Append(input, num, num3);
                }

                if (!flag)
                {
                    stringBuilder.Append(newValue);
                }

                num = num2 + oldValue.Length;
                if (num == input.Length)
                {
                    return stringBuilder.ToString();
                }
            }

            int count = input.Length - num;
            stringBuilder.Append(input, num, count);
            return stringBuilder.ToString();
        }

        public static string GetTextSandwichedBy(this string value, string startToken, string endToken)
        {
            int num = value.IndexOf(startToken, StringComparison.Ordinal);
            if (num == -1)
            {
                throw new ArgumentOutOfRangeException("startToken");
            }

            num += startToken.Length;
            int num2 = value.IndexOf(endToken, num, StringComparison.Ordinal);
            if (num2 == -1)
            {
                throw new ArgumentOutOfRangeException("endToken");
            }

            return value.Substring(num, num2 - num);
        }

        public static string TrimQuotes(this string value)
        {
            return value.Trim('"', '\'', '‘', '’', '“', '”');
        }

        public static Stream WriteToStream(this string value)
        {
            MemoryStream memoryStream = new MemoryStream();
            value.WriteToStream(memoryStream);
            return memoryStream;
        }

        public static void WriteToStream(this string value, Stream stream)
        {
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(value);
            streamWriter.Flush();
            stream.Position = 0L;
        }

        public static async Task<Stream> WriteToStreamAsync(this string value)
        {
            MemoryStream stream = new MemoryStream();
            await value.WriteToStreamAsync(stream);
            return stream;
        }

        public static async Task WriteToStreamAsync(this string value, Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            await writer.WriteAsync(value);
            await writer.FlushAsync();
            stream.Position = 0L;
        }
    }
}
