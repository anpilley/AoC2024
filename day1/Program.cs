using System.Text.RegularExpressions;

namespace day1;

class Program
{
    static void Main(string[] args)
    {
        using(var reader = new StreamReader("C:\\Dev\\Practice\\AoC2024\\day1.txt"))
        {
            string line;
            var regex = new Regex("[0-9]+");
            SortedSet<int> left = new();
            Dictionary<int, int> right = new();

            while((line = (reader.ReadLine()!)) != null)
            {
                var matches = regex.Matches(line);

                if(matches.Count() != 2)
                    throw new Exception("Unexpected number count");
                
                int l = Int32.Parse(matches[0].Value);
                int r = Int32.Parse(matches[1].Value);
                Console.WriteLine($"left: {l}, right: {r}");
                left.Add(l);

                if(right.ContainsKey(r))
                    right[r]++;
                else
                    right.Add(r, 1);
            }

            Console.WriteLine($"Added Left: {left.Count()} and Right: {right.Count()} entries");

            // forgive me, for I have used linq.
            var set = right.Where(x => left.Contains(x.Key)).ToDictionary(t => t.Key, t => t.Value);

            Console.WriteLine($"set count {set.Count()}");
            int similarity = 0;
            foreach(var val in set)
            {
                Console.WriteLine($"data: {val.Key}, count: {val.Value}");
                similarity += val.Key * val.Value;
            }

            Console.WriteLine($"similarity: {similarity}");


        }
    }
}
