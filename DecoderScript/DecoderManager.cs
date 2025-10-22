using System.Text;

namespace DecoderScript;

public static class DecoderManager
{
    public static string DecodeString(string utf8String)
    {
        // Constants
        int subtractionValue = 0;
        bool isValueSet = false;
        const string NEWLINE_REPLACEMENT_FOR_WINDOWS_ONLY = "\r\n";


        //step 1: decode base64
        var base64SourceFile = NormalizeUrlSafeBase64Text(utf8String);

        var text = Encoding.UTF8.GetString(Convert.FromBase64String(base64SourceFile));


        //step 2: subtract all utf-8 chars by x value
        //x = first char value - 100
        
        var list = new List<string>();

        foreach (var ch in text)
        {
            var a = char.ConvertToUtf32(ch.ToString(), 0);

            if (!isValueSet)
            {
                subtractionValue = a - 100;
                isValueSet = true;
            }

            var b = char.ConvertFromUtf32(a < subtractionValue ? a : a - subtractionValue);


            //safety
            if (a < subtractionValue)
            {
                Console.WriteLine("this character is below {0} : {1},{2}", subtractionValue, ch.ToString(), a);
                continue;
            }


            //windows only
            if (b == "\n")
            {
                list.Add(NEWLINE_REPLACEMENT_FOR_WINDOWS_ONLY);
                continue;
            }

            list.Add(b);
        }

        //step 3: after this step the content is obfuscated
        //for example "<div" is look like "d<vi"
        //and we should swap chars two by two
        //you can see it by removing this part

        SwapTwoByTwoItems(list);


        //step 4: combine everthing and show the result
        return string.Join("", list);
    }

    private static void SwapTwoByTwoItems(List<string> list)
    {
        for (int j = 1; j < list.Count; j += 2)
        {
            var a = list[j];
            var b = list[j - 1];

            list[j] = b;
            list[j - 1] = a;
        }
    }

    private static string NormalizeUrlSafeBase64Text(string utf8String)
    {
        var base64SourceFile =  utf8String
            .Replace("_", "/")
            .Replace("-", "+");
        
        int mod4 = base64SourceFile.Length % 4;
        if (mod4 > 0)
        {
            base64SourceFile += new string('=', 4 - mod4);
        }
        
        return base64SourceFile;

    }
}