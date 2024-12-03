using System.Text.RegularExpressions;

namespace day3;

class Program
{
    static void Main(string[] args)
    {
        var regex = new Regex("(?<mul>mul\\([0-9]+,[0-9]+\\))|(?<do>do\\(\\))|(?<dont>don't\\(\\))");
        var numRegex = new Regex("[0-9]+");
        
        using(var reader = new StreamReader("C:\\Dev\\Practice\\AoC2024\\day3.txt"))
        {
            string line;
            int total = 0;
            bool enabled = true;
            while((line = reader.ReadLine()!) != null)
            {
                var matches = regex.Matches(line).OrderBy(x => x.Index);

                foreach(var match in matches)
                {
                    if(match.Groups["mul"].Success && enabled)
                    {
                        Console.WriteLine($"match: {match.Value}");
                        var numMatches = numRegex.Matches(match.Value);
                        if(numMatches.Count != 2)
                            throw new Exception("Missing match count");

                        int n1 = Int32.Parse(numMatches[0].Value);
                        int n2 = Int32.Parse(numMatches[1].Value);
                        int mul = n1 * n2;
                        total+= mul;
                    }
                    else if(match.Groups["mul"].Success && !enabled)
                    {
                        Console.WriteLine($"Skipping {match.Value}");
                    }
                    else if(match.Groups["do"].Success)
                    {
                        Console.WriteLine("Enabling");
                        enabled = true;
                    }
                    else if(match.Groups["dont"].Success)
                    {
                        Console.WriteLine("disabling");
                        enabled = false;
                    }

                }
            }

            Console.WriteLine($"total: {total}");
        }
    }
}
