using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using RunManager;

namespace RegularExpression
{
    class Examples
    {

        public static void Main()
        {
            Runner.RunAllExamples(new Examples());
        }

        //Examples can be found in 
        //https://msdn.microsoft.com/en-us/library/30wbz966(v=vs.110).aspx

        public static void Example1()
        {

//            Matching a Regular Expression Pattern
//The Regex.IsMatch method returns true if the string matches the pattern, or false if it does not. The IsMatch method is often used to validate string input. For example, the following code ensures that a string matches a valid social security number in the United States.

            string[] values = { "111-22-3333", "111-2-3333" };
            string pattern = @"^\d{3}-\d{2}-\d{4}$";
            foreach (string value in values)
            {
                if (Regex.IsMatch(value, pattern))
                    Console.WriteLine("{0} is a valid SSN.", value);
                else
                    Console.WriteLine("{0}: Invalid", value);
            }

            // The example displays the following output:
            //       111-22-3333 is a valid SSN.
            //       111-2-3333: Invalid


            //The regular expression pattern ^\d{ 3}-\d{ 2}-\d{ 4}$ 
            //is interpreted as shown in the following table.
            //Pattern Description
            //^ Match the beginning of the input string.
            //\d{ 3}
            //            Match three decimal digits.
            //-Match a hyphen.
            //\d{ 2}
            //            Match two decimal digits.
            //-Match a hyphen.
            //\d{ 4}
            //            Match four decimal digits.
            //$	Match the end of the input string.

        }

        public static void Example2()
        {
            // Extracting a Single Match or the First Match
            //The Regex.Match method returns a Match object that contains information about the first substring that matches a regular expression pattern. If the Match.Success property returns true, indicating that a match was found, you can retrieve information about subsequent matches by calling the Match.NextMatch method. These method calls can continue until the Match.Success property returns false.For example, the following code uses the Regex.Match(String, String) method to find the first occurrence of a duplicated word in a string.It then calls the Match.NextMatch method to find any additional occurrences.The example examines the Match.Success property after each method call to determine whether the current match was successful and whether a call to the Match.NextMatch method should follow.

            string input = "This is a a farm that that raises dairy cattle.";
            string pattern = @"\b(\w+)\W+(\1)\b";
            Match match = Regex.Match(input, pattern);
            while (match.Success)
            {
                Console.WriteLine("Duplicate '{0}' found at position {1}.",
                                  match.Groups[1].Value, match.Groups[2].Index);
                match = match.NextMatch();
            }

            // The example displays the following output:
            //       Duplicate 'a' found at position 10.
            //       Duplicate 'that' found at position 22.

            //The regular expression pattern \b(\w+)\W+(\1)\b is interpreted as shown in the following table.

            //Pattern Description
            //\b Begin the match on a word boundary.
            //(\w +)	Match one or more word characters. This is the first capturing group.
            //\W + Match one or more non-word characters.
            // (\1)    Match the first captured string.This is the second capturing group.
            //\b End the match on a word boundary.
        }

        public static void Example3()
        {
            //Extracting All Matches
            //The Regex.Matches method returns a MatchCollection object that contains information about all matches that the regular expression engine found in the input string.For example, the previous example could be rewritten to call the Matches method instead of the Match and NextMatch methods.

            string input = "This is a a farm that that raises dairy cattle.";
            string pattern = @"\b(\w+)\W+(\1)\b";
            foreach (Match match in Regex.Matches(input, pattern))
                Console.WriteLine("Duplicate '{0}' found at position {1}.",
                                  match.Groups[1].Value, match.Groups[2].Index);

            // The example displays the following output:
            //       Duplicate 'a' found at position 10.
            //       Duplicate 'that' found at position 22.
        }

        public static void Example4()
        {
//            Replacing a Matched Substring
//The Regex.Replace method replaces each substring that matches the regular expression pattern with a specified string or regular expression pattern, and returns the entire input string with replacements.For example, the following code adds a U.S.currency symbol before a decimal number in a string.


            string pattern = @"\b\d+\.\d{2}\b";
            string replacement = "$$$&";
            string input = "Total Cost: 103.64";
            Console.WriteLine(Regex.Replace(input, pattern, replacement));

            // The example displays the following output:
            //       Total Cost: $103.64

            //The regular expression pattern \b\d +\.\d{ 2}\b is interpreted as shown in the following table.

            //\b Begin the match at a word boundary.
            //\d + Match one or more decimal digits.
            //\.	Match a period.
            //\d{ 2}
            //            Match two decimal digits.
            //\b End the match at a word boundary.

            //The replacement pattern $$$& is interpreted as shown in the following table.

            //$$	The dollar sign($) character.
            //$&    The entire matched substring.
        }

        public static void Example5()
        {
            //            Splitting a Single String into an Array of Strings
            //The Regex.Split method splits the input string at the positions defined by a regular expression match. For example, the following code places the items in a numbered list into a string array.

            string input = "1. Eggs 2. Bread 3. Milk 4. Coffee 5. Tea";
            string pattern = @"\b\d{1,2}\.\s";
            foreach (string item in Regex.Split(input, pattern))
            {
                if (!String.IsNullOrEmpty(item))
                    Console.WriteLine(item);
            }

            // The example displays the following output:
            //       Eggs
            //       Bread
            //       Milk
            //       Coffee
            //       Tea

            //The regular expression pattern \b\d{ 1,2}\.\s is interpreted as shown in the following table.
            //            \b Begin the match at a word boundary.
            //\d{ 1,2}
            //            Match one or two decimal digits.
            //\.	Match a period.
            //\s Match a white-space character.

        }

        public static void Example6()
        {
            //The following example uses the Regex.Matches(String) method to populate a MatchCollection object with all the matches found in an input string.The example enumerates the collection, copies the matches to a string array, and records the character positions in an integer array.


            MatchCollection matches;
            List<string> results = new List<string>();
            List<int> matchposition = new List<int>();

            // Create a new Regex object and define the regular expression.
            Regex r = new Regex("abc");
            // Use the Matches method to find all matches in the input string.
            matches = r.Matches("123abc4abcd");
            // Enumerate the collection to retrieve all matches and positions.
            foreach (Match match in matches)
            {
                // Add the match string to the string array.
                results.Add(match.Value);
                // Record the character position where the match was found.
                matchposition.Add(match.Index);
            }
            // List the results.
            for (int ctr = 0; ctr < results.Count; ctr++)
                Console.WriteLine("'{0}' found at position {1}.",
                                  results[ctr], matchposition[ctr]);

            // The example displays the following output:
            //       'abc' found at position 3.
            //       'abc' found at position 7.
        }

        public static void Example7()
        {
            //The following example retrieves individual Match objects from a MatchCollection object by iterating the collection using the foreach or For Each...Next construct.The regular expression simply matches the string "abc" in the input string.

            string pattern = "abc";
            string input = "abc123abc456abc789";
            foreach (Match match in Regex.Matches(input, pattern))
                Console.WriteLine("{0} found at position {1}.",
                                  match.Value, match.Index);

            // The example displays the following output:
            //       abc found at position 0.
            //       abc found at position 6.
            //       abc found at position 12.

        }

        public static void Example8()
        {
//    The following example uses the Regex.Match(String, String) and Match.NextMatch methods to match the string "abc" in the input string.

            string pattern = "abc";
            string input = "abc123abc456abc789";
            Match match = Regex.Match(input, pattern);
            while (match.Success)
            {
                Console.WriteLine("{0} found at position {1}.", 
                           match.Value, match.Index);
                            match = match.NextMatch();                  
            }

            // The example displays the following output:
            //       abc found at position 0.
            //       abc found at position 6.
            //       abc found at position 12.
        }


        public static void Example9()
        {

            //The Match.Result method performs a specified replacement operation on the matched string and returns the result.
            //The following example uses the Match.Result method to prepend a $ symbol and a space before every number that includes two fractional digits.

            string pattern = @"\b\d+(,\d{3})*\.\d{2}\b";
            string input = "16.32\n194.03\n1,903,672.08";

            foreach (Match match in Regex.Matches(input, pattern))
                Console.WriteLine(match.Result("$$ $&"));

            // The example displays the following output:
            //       $ 16.32
            //       $ 194.03
            //       $ 1,903,672.08


            //The regular expression pattern \b\d + (,\d{ 3})*\.\d{ 2}\b is defined as shown in the following table.
            //\b Begin the match at a word boundary.
            //\d + Match one or more decimal digits.
            //(,\d{ 3})*Match zero or more occurrences of a comma followed by three decimal digits.
            //\.	Match the decimal point character.
            //\d{ 2}
            //            Match two decimal digits.
            //\b End the match at a word boundary.

        }

        public static void Example10()
        {
            //The following example defines a regular expression that uses grouping constructs to capture the month, day, and year of a date.

            string pattern = @"\b(\w+)\s(\d{1,2}),\s(\d{4})\b";
            string input = "Born: July 28, 1989";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
                for (int ctr = 0; ctr < match.Groups.Count; ctr++)
                    Console.WriteLine("Group {0}: {1}", ctr, match.Groups[ctr].Value);

            // The example displays the following output:
            //       Group 0: July 28, 1989
            //       Group 1: July
            //       Group 2: 28
            //       Group 3: 1989

            //The regular expression pattern \b(\w +)\s(\d{ 1,2}),\s(\d{ 4})\b is defined as shown in the following table.

            //\b Begin the match at a word boundary.
            //(\w +)	Match one or more word characters. This is the first capturing group.
            //\s Match a white-space character.
            //(\d{ 1,2})	Match one or two decimal digits. This is the second capturing group.
            //,	Match a comma.
            //\s Match a white-space character.
            //(\d{ 4})	Match four decimal digits. This is the third capturing group.
            //\b End the match on a word boundary.
        }

        public static void Example11()
        {

            //The following example uses nested grouping constructs to capture substrings into groups. The regular expression pattern (a(b))c matches the string "abc".It assigns the substring "ab" to the first capturing group, and the substring "b" to the second capturing group.

            List <int> matchposition = new List<int>();
            List<string> results = new List<string>();
            // Define substrings abc, ab, b.
            Regex r = new Regex("(a(b))c");
            Match m = r.Match("abdabc");
            for (int i = 0; m.Groups[i].Value != ""; i++)
            {
                // Add groups to string array.
                results.Add(m.Groups[i].Value);
                // Record character position.
                matchposition.Add(m.Groups[i].Index);
            }

            // Display the capture groups.
            for (int ctr = 0; ctr < results.Count; ctr++)
                Console.WriteLine("{0} at position {1}",
                                  results[ctr], matchposition[ctr]);
            // The example displays the following output:
            //       abc at position 3
            //       ab at position 3
            //       b at position 4
        }

        public static void Example12()
        {
            //The following example uses named grouping constructs to capture substrings from a string that contains data in the format "DATANAME:VALUE", which the regular expression splits at the colon (:).

            Regex r = new Regex("^(?<name>\\w+):(?<value>\\w+)");
            Match m = r.Match("Section1:119900");
            Console.WriteLine(m.Groups["name"].Value);
            Console.WriteLine(m.Groups["value"].Value);
            // The example displays the following output:
            //       Section1
            //       119900
        }

        //The regular expression pattern ^(?<name>\w+):(?<value>\w+) is defined as shown in the following table.
        //        ^	Begin the match at the beginning of the input string.
        //(?<name>\w+)	Match one or more word characters.The name of this capturing group is name.
        //:	Match a colon.
        //(?<value>\w+)   Match one or more word characters.The name of this capturing group is value.

        public static void Example13()
        {
            //The following example provides an illustration. In the regular expression pattern aaa(bbb) * ccc, the first capturing group(the substring "bbb") can be matched zero or more times.Because the input string "aaaccc" matches the pattern, the capturing group does not have a match.


            string pattern = "aaa(bbb)*ccc";
            string input = "aaaccc";
            Match match = Regex.Match(input, pattern);
            Console.WriteLine("Match value: {0}", match.Value);
            if (match.Groups[1].Success)
                Console.WriteLine("Group 1 value: {0}", match.Groups[1].Value);
            else
                Console.WriteLine("The first capturing group has no match.");

            // The example displays the following output:
            //       Match value: aaaccc
            //       The first capturing group has no match.
        }

        public static void Example14()
        {
            //Quantifiers can match multiple occurrences of a pattern that is defined by a capturing group. In this case, the Value and Length properties of a Group object contain information only about the last captured substring.For example, the following regular expression matches a single sentence that ends in a period. It uses two grouping constructs: The first captures individual words along with a white - space character; the second captures individual words.As the output from the example shows, although the regular expression succeeds in capturing an entire sentence, the second capturing group captures only the last word.

            string pattern = @"\b((\w+)\s?)+\.";
            string input = "This is a sentence. This is another sentence.";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                Console.WriteLine("Match: " + match.Value);
                Console.WriteLine("Group 2: " + match.Groups[2].Value);
            }

            // The example displays the following output:
            //       Match: This is a sentence.
            //       Group 2: sentence
        }

        public static void Example15()
        {
            string pattern = "((a(b))c)+";
            string input = "abcabcabc";

            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                Console.WriteLine("Match: '{0}' at position {1}",
                                  match.Value, match.Index);
                GroupCollection groups = match.Groups;
                for (int ctr = 0; ctr < groups.Count; ctr++)
                {
                    Console.WriteLine("   Group {0}: '{1}' at position {2}",
                                      ctr, groups[ctr].Value, groups[ctr].Index);
                    CaptureCollection captures = groups[ctr].Captures;
                    for (int ctr2 = 0; ctr2 < captures.Count; ctr2++)
                    {
                        Console.WriteLine("      Capture {0}: '{1}' at position {2}",
                                          ctr2, captures[ctr2].Value, captures[ctr2].Index);
                    }
                }
            }

            // The example displays the following output:
            //       Match: 'abcabcabc' at position 0
            //          Group 0: 'abcabcabc' at position 0
            //             Capture 0: 'abcabcabc' at position 0
            //          Group 1: 'abc' at position 6
            //             Capture 0: 'abc' at position 0
            //             Capture 1: 'abc' at position 3
            //             Capture 2: 'abc' at position 6
            //          Group 2: 'ab' at position 6
            //             Capture 0: 'ab' at position 0
            //             Capture 1: 'ab' at position 3
            //             Capture 2: 'ab' at position 6
            //          Group 3: 'b' at position 7
            //             Capture 0: 'b' at position 1
            //             Capture 1: 'b' at position 4
            //             Capture 2: 'b' at position 7
        }

        public static void Example16()
        {
            //The following example uses the regular expression(Abc) + to find one or more consecutive runs of the string "Abc" in the string "XYZAbcAbcAbcXYZAbcAb".The example illustrates the use of the Group.Captures property to return multiple groups of captured substrings.

            int counter;
            Match m;
            CaptureCollection cc;
            GroupCollection gc;

            // Look for groupings of "Abc".
            Regex r = new Regex("(Abc)+");
            // Define the string to search.
            m = r.Match("XYZAbcAbcAbcXYZAbcAb");
            gc = m.Groups;

            // Display the number of groups.
            Console.WriteLine("Captured groups = " + gc.Count.ToString());

            // Loop through each group.
            for (int i = 0; i < gc.Count; i++)
            {
                cc = gc[i].Captures;
                counter = cc.Count;

                // Display the number of captures in this group.
                Console.WriteLine("Captures count = " + counter.ToString());

                // Loop through each capture in the group.
                for (int ii = 0; ii < counter; ii++)
                {
                    // Display the capture and its position.
                    Console.WriteLine(cc[ii] + "   Starts at character " +
                         cc[ii].Index);
                }
            }
            // The example displays the following output:
            //       Captured groups = 2
            //       Captures count = 1
            //       AbcAbcAbc   Starts at character 3
            //       Captures count = 3
            //       Abc   Starts at character 3
            //       Abc   Starts at character 6
            //       Abc   Starts at character 9  
        }

        public static void Example17()
        {
            //The following example parses an input string for the temperature of selected cities.A comma(",") is used to separate a city and its temperature, and a semicolon(";") is used to separate each city's data. The entire input string represents a single match. In the regular expression pattern ((\w+(\s\w+)*),(\d+);)+, which is used to parse the string, the city name is assigned to the second capturing group, and the temperature is assigned to the fourth capturing group.


            string input = "Miami,78;Chicago,62;New York,67;San Francisco,59;Seattle,58;";
            string pattern = @"((\w+(\s\w+)*),(\d+);)+";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                Console.WriteLine("Current temperatures:");
                for (int ctr = 0; ctr < match.Groups[2].Captures.Count; ctr++)
                    Console.WriteLine("{0,-20} {1,3}", match.Groups[2].Captures[ctr].Value,
                                      match.Groups[4].Captures[ctr].Value);
            }

            // The example displays the following output:
            //       Current temperatures:
            //       Miami                 78
            //       Chicago               62
            //       New York              67
            //       San Francisco         59


            //The regular expression ((\w+(\s\w+)*),(\d+);)+ is defined as shown in the following table.

            //\w + Match one or more word characters.
            // (\s\w +) * Match zero or more occurrences of a white - space character followed by one or more word characters. This pattern matches multi-word city names. This is the third capturing group.
            //  (\w + (\s\w +) *)	Match one or more word characters followed by zero or more occurrences of a white - space character and one or more word characters.This is the second capturing group.
            //,	Match a comma.
            //(\d +)   Match one or more digits.This is the fourth capturing group.
            //; Match a semicolon.
            //((\w + (\s\w +) *),(\d +);)+Match the pattern of a word followed by any additional words followed by a comma, one or more digits, and a semicolon, one or more times. This is the first capturing group.
        }


    }
}
