using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        using (var reader = new StreamReader("C:\\Dev\\Practice\\AoC2024\\day2.txt"))
        {
            var regex = new Regex("[0-9]+");
            string line;
            int safeCount = 0;
            while((line = reader.ReadLine()!) != null)
            {
                var matches = regex.Matches(line).OrderBy(x => x.Index);
                List<int> data = new();

                if(matches.Count() <= 1)
                    throw new Exception("Unexpected number of reports?");

                foreach(var match in matches)
                {
                    int num = Int32.Parse(match.Value);
                    data.Add(num);
                }

                if(CheckSafe(data))
                {
                    safeCount++;
                }
                else
                {
                    // Shame upon my ancestors for brute forcing this.
                    for(int i = 0; i < data.Count(); i++)
                    {
                        if(CheckSafe(data, i))
                        {
                            safeCount++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($" Safe Count {safeCount}");
        }
    }


    static bool CheckSafe(List<int> data, int skip = -1)
    {

        int prev = -1;
        bool safe = true;
        Direction dir = Direction.Unknown;

        for(int i = 0; i < data.Count(); i++) 
        {
            if(i == skip)
                continue;
            
            int num = data[i];

            if(prev == -1)
            {   
                prev = num;
                continue;
            }
            
            // no match
            if(prev < num && dir == Direction.Decreasing)
            {

                safe = false;
                break;
            }

            if(prev > num && dir == Direction.Increasing)
            {

                safe = false;
                break;
            }

            if(Math.Abs(prev - num) > 3)
            {

                safe = false;
                break;
            }

            if(prev == num)
            {

                safe = false;
                break;
            }

            if(dir == Direction.Unknown && prev < num)
            {
                dir = Direction.Increasing;
            }
            if(dir == Direction.Unknown && prev > num)
            {
                dir = Direction.Decreasing;
            }

            prev = num;
            
        }

        if(safe)
            return true;

        return false;
    }


    enum Direction {
        Increasing,
        Decreasing,
        Unknown
    };

}