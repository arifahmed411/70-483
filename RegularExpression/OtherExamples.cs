using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;

namespace RegularExpression
{
    class OtherExamples
    {

        public static void Example1()
        {
            string pattern = "(Mr\\.? |Mrs\\.? |Miss |Ms\\.? )";
            string[] names = { "Mr. Henry Hunt", "Ms. Sara Samuels",
                         "Abraham Adams", "Ms. Nicole Norris" };
            foreach (string name in names)
                Console.WriteLine(Regex.Replace(name, pattern, String.Empty));

            //The regular expression pattern(Mr\.? | Mrs\.? | Miss | Ms\.? ) matches any occurrence of "Mr ", "Mr. ", "Mrs ", "Mrs. ", "Miss ", "Ms or "Ms. ". The call to the Regex.Replace method replaces the matched string with String.Empty; in other words, it removes it from the original string.

            // The example displays the following output:
            //    Henry Hunt
            //    Sara Samuels
            //    Abraham Adams
            //    Nicole Norris

        }

        public static void Example2()
        {
            string pattern = @"\b(\w+?)\s\1\b";
            string input = "This this is a nice day. What about this? This tastes good. I saw a a dog.";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
                Console.WriteLine("{0} (duplicates '{1}') at position {2}",
                                  match.Value, match.Groups[1].Value, match.Index);

            //The regular expression pattern \b(\w +?)\s\1\b can be interpreted as follows:
            //\b Start at a word boundary.
            //(\w +?)	Match one or more word characters, but as few characters as possible.Together, they form a group that can be referred to as \1.
            //\s Match a white-space character.
            //\1  Match the substring that is equal to the group named \1.
            //\b Match a word boundary.


            // The example displays the following output:
            //       This this (duplicates 'This)' at position 0
            //       a a (duplicates 'a)' at position 66
        }

        public static void Example3()
        {
            string[] partNumbers = { "1298-673-4192", "A08Z-931-468A",
                              "_A90-123-129X", "12345-KKA-1230",
                              "0919-2893-1256" };
            Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");
            foreach (string partNumber in partNumbers)
                Console.WriteLine("{0} {1} a valid part number.",
                                  partNumber,
                                  rgx.IsMatch(partNumber) ? "is" : "is not");

            // The example displays the following output:
            //       1298-673-4192 is a valid part number.
            //       A08Z-931-468A is a valid part number.
            //       _A90-123-129X is not a valid part number.
            //       12345-KKA-1230 is not a valid part number.
            //       0919-2893-1256 is not a valid part number.
        }

        public static void Example4()
        {
            //A regular expression pattern can include subexpressions, which are defined by enclosing a portion of the regular expression pattern in parentheses.Every such subexpression forms a group. The Groups property provides access to information about those subexpression matches.For example, the regular expression pattern(\d{ 3})-(\d{ 3}
            //-\d{ 4}), which matches North American telephone numbers, has two subexpressions. The first consists of the area code, which composes the first three digits of the telephone number. This group is captured by the first portion of the regular expression, (\d{ 3}).The second consists of the individual telephone number, which composes the last seven digits of the telephone number.This group is captured by the second portion of the regular expression, (\d{ 3}
            //-\d{ 4}). These two groups can then be retrieved from the GroupCollection object that is returned by the Groups property, as the following example shows.


            string pattern = @"(\d{3})-(\d{3}-\d{4})";
            string input = "212-555-6666 906-932-1111 415-222-3333 425-888-9999";
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                Console.WriteLine("Area Code:        {0}", match.Groups[1].Value);
                Console.WriteLine("Telephone number: {0}", match.Groups[2].Value);
                Console.WriteLine();
            }
            Console.WriteLine();

            // The example displays the following output:
            //       Area Code:        212
            //       Telephone number: 555-6666
            //       
            //       Area Code:        906
            //       Telephone number: 932-1111
            //       
            //       Area Code:        415
            //       Telephone number: 222-3333
            //       
            //       Area Code:        425
            //       Telephone number: 888-9999
        }

        public static void Example5()
        {
            //The GroupCollection object returned by the Match.Groups property is a zero - based collection object that always has at least one member. If the regular expression engine cannot find any matches in a particular input string, the Group.Success property of the single Group object in the collection (the object at index 0) is set to false and the Group object's Value property is set to String.Empty. If the regular expression engine can find a match, the first element of the GroupCollection object (the element at index 0) returned by the Groups property contains a string that matches the entire regular expression pattern. Each subsequent element, from index one upward, represents a captured group, if the regular expression includes capturing groups. For more information, see the "Grouping Constructs and Regular Expression Objects" section of the Grouping Constructs in Regular Expressions article.

            string text = "One car red car blue car";
            string pat = @"(\w+)\s+(car)";

            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(text);
            int matchCount = 0;
            while (m.Success)
            {
                Console.WriteLine("Match" + (++matchCount));
                for (int i = 1; i <= 2; i++)
                {
                    Group g = m.Groups[i];
                    Console.WriteLine("Group" + i + "='" + g + "'");
                    CaptureCollection cc = g.Captures;
                    for (int j = 0; j < cc.Count; j++)
                    {
                        Capture c = cc[j];
                        System.Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
                    }
                }
                m = m.NextMatch();
            }

            // This example displays the following output:
            //       Match1
            //       Group1='One'
            //       Capture0='One', Position=0
            //       Group2='car'
            //       Capture0='car', Position=4
            //       Match2
            //       Group1='red'
            //       Capture0='red', Position=8
            //       Group2='car'
            //       Capture0='car', Position=12
            //       Match3
            //       Group1='blue'
            //       Capture0='blue', Position=16
            //       Group2='car'
            //       Capture0='car', Position=21
        }

        public static void Example6()
        {
            RegexUtilities util = new RegexUtilities();
            string[] emailAddresses = { "david.jones@proseware.com", "d.j@server1.proseware.com",
                                  "jones@ms1.proseware.com", "j.@server1.proseware.com",
                                  "j@proseware.com9", "js#internal@proseware.com",
                                  "j_9@[129.126.118.1]", "j..s@proseware.com",
                                  "js*@proseware.com", "js@proseware..com",
                                  "js@proseware.com9", "j.s@server1.proseware.com",
                                   "\"j\\\"s\\\"\"@proseware.com", "js@contoso.中国" };

            foreach (var emailAddress in emailAddresses)
            {
                if (util.IsValidEmail(emailAddress))
                    Console.WriteLine("Valid: {0}", emailAddress);
                else
                    Console.WriteLine("Invalid: {0}", emailAddress);
            }

            // The example displays the following output:
            //       Valid: david.jones@proseware.com
            //       Valid: d.j@server1.proseware.com
            //       Valid: jones@ms1.proseware.com
            //       Invalid: j.@server1.proseware.com
            //       Valid: j@proseware.com9
            //       Valid: js#internal@proseware.com
            //       Valid: j_9@[129.126.118.1]
            //       Invalid: j..s@proseware.com
            //       Invalid: js*@proseware.com
            //       Invalid: js@proseware..com
            //       Valid: js@proseware.com9
            //       Valid: j.s@server1.proseware.com
            //       Valid: "j\"s\""@proseware.com
            //       Valid: js@contoso.ä¸­å›½
        }

        public static void Example7()
        {

            //The RegexStringValidator object contains the rules necessary to validate a string object based on a regular expression. The rules are established when an instance of the RegexStringValidator class is created.
            //The CanValidate method determines whether the object type being validated matches the expected type.The object being validated is passed as a parameter of the Validate method.


            // Display title.
            Console.WriteLine("ASP.NET Validators");
            Console.WriteLine();

            // Create RegexString and Validator.
            string testString = "someone@example.com";
            string regexString =
             @"^[a-zA-Z\.\-_]+@([a-zA-Z\.\-_]+\.)+[a-zA-Z]{2,4}$";
            RegexStringValidator myRegexValidator =
             new RegexStringValidator(regexString);

            // Determine if the object to validate can be validated.
            Console.WriteLine("CanValidate: {0}",
              myRegexValidator.CanValidate(testString.GetType()));

            try
            {
                // Attempt validation.
                myRegexValidator.Validate(testString);
                Console.WriteLine("Validated.");
            }
            catch (ArgumentException e)
            {
                // Validation failed.
                Console.WriteLine("Error: {0}", e.Message.ToString());
            }

        }

    }
}
