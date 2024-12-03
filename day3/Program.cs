using System.Text.RegularExpressions;

namespace day3;

class Program
{
    static void Main(string[] args)
    {
        var regex = new Regex("mul\\([0-9]+,[0-9]+\\)");
        var numRegex = new Regex("[0-9]+");
        
        using(var reader = new StreamReader("C:\\Dev\\Practice\\AoC2024\\day3.txt"))
        {
            string line;
            int total = 0;
            while((line = reader.ReadLine()!) != null)
            {
                var matches = regex.Matches(line).OrderBy(x => x.Index);

                foreach(var match in matches)
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
            }

            Console.WriteLine($"total: {total}");
        }
    }
}
