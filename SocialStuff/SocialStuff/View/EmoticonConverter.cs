using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialStuff.View
{
    public static class EmoticonConverter
    {
        private static readonly Dictionary<string, string> EmoticonToEmojiMap = new Dictionary<string, string>
    {
        { ":)", "😊" },
        { ":-)", "😊" },
        { ":]", "😊" },
        { ":D", "😃" },
        { ":-D", "😃" },
        { ":p", "😛" },
        { ":-p", "😛" },
        { ":P", "😛" },
        { ":-P", "😛" },
        { ";)", "😉" },
        { ";-)", "😉" },
        { ":(", "😞" },
        { ":-(", "😞" },
        { ":'(", "😢" },
        { ":O", "😮" },
        { ":-O", "😮" },
        { ":o", "😮" },
        { ":-o", "😮" },
        { "<3", "❤️" },
        { ":*", "😘" },
        { ":-*", "😘" },
        { ":|", "😐" },
        { ":-|", "😐" },
        { ":/", "😕" },
        { ":-/", "😕" },
        { ":\\", "😕" },
        { ":-\\", "😕" },
        { ":$", "😳" },
        { ":-$", "😳" },
        { ":@", "😠" },
        { ":-@", "😠" },
        { ":S", "😖" },
        { ":-S", "😖" },
        { ":s", "😖" },
        { ":-s", "😖" },
        { "XD", "😆" },
        { "xD", "😆" },
        { "=D", "😁" },
        { "=)", "🙂" },
        { "B)", "😎" },
        { "B-)", "😎" },
        { "^_^", "😄" },
        { "^.^", "😄" },
        { "o.O", "😳" },
        { "O.o", "😳" },
        { "O_O", "😳" },
        { "o_o", "😳" },
        { "-_-", "😑" },
        { "T_T", "😭" },
        { "T.T", "😭" },
        { ">:(", "😠" },
        { ">:-(", "😠" },
        { ">:)", "😈" },
        { ">:-)", "😈" },
        { "¯\\_(ツ)_/¯", "🤷" }
    };

        public static string ConvertEmoticonsToEmojis(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string result = input;

            // Process each emoticon in the dictionary
            foreach (var emoticon in EmoticonToEmojiMap)
            {
                // Replace emoticons with corresponding emojis
                // Use word boundaries to prevent replacing parts of words
                result = Regex.Replace(
                    result,
                    $@"(?<!\w){Regex.Escape(emoticon.Key)}(?!\w)",
                    emoticon.Value
                );
            }

            return result;
        }
    }
}
