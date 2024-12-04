namespace day4;


enum Direction
{
    One,
    Two,
    Three,
    Four
}

class Program
{
    static void Main(string[] args)
    {
        char[] input;
        int xlen = 0, ylen = 0;
        List<string> data = new();
        using(var reader = new StreamReader("C:\\Dev\\Practice\\AoC2024\\day4.txt"))
        {
            string line;
            while((line = reader.ReadLine()!)!= null)
            {
                data.Add(line);
            }
            xlen = data.Count();
            ylen = data[0].Length;

            input = new char[xlen * ylen];
            int i = 0;
            foreach(var row in data)
            {
                row.CopyTo(0, input, CoordToIndex(i,0, ylen), row.Length);
                i++;
            }
            int counter = 0;
            for(i = 0; i < input.Length; i++)
            {
                if(input[i] == 'A')
                {
                    (int, int) coords = IndexToCoordinates(i, ylen);
                    var set = GetCoords(coords.Item1, coords.Item2, xlen, ylen);

                    Console.WriteLine($"Checking around {coords}, directions {set.Count()}");
                    
                    bool valid = false;
                    foreach(var dir in set)
                    {
                        bool dirvalid = true;
                        foreach(var c in dir.Value){
                            int x1 = c.Item1;
                            int y1 = c.Item2;
                            char t = c.Item3;

                            if(input[CoordToIndex(x1, y1, ylen)] != t)
                            {
                                dirvalid = false;
                                break;
                            }
                        }

                        if(dirvalid){
                            valid = true;
                            break;
                        }
                    }
                    
                    if(valid)
                        counter++;
                }
            }

            Console.WriteLine($"Count: {counter}");
        }
    }

    // get every valid coordinate set for a direction. Excludes ones that cross the edge.
    // hypothesis: we'll need to wrap in part 2. edit: nope :(
    static IDictionary<Direction, (int, int, char)[]> GetCoords(int x, int y, int xlen, int ylen)
    {
        Dictionary<Direction, (int, int, char)[]> coords = new();

        // if we're on an edge, nothing to check.
        if(x == 0 || x == xlen-1 || y == 0 || y==ylen-1)
            return coords;

        // One
        // M M
        //  A
        // S S
        coords.Add(Direction.One, new(int, int, char)[4]);
        coords[Direction.One][0] = (x-1, y-1, 'M');
        coords[Direction.One][1] = (x+1, y-1, 'M');
        coords[Direction.One][2] = (x-1, y+1, 'S');
        coords[Direction.One][3] = (x+1, y+1, 'S');

        // Two
        // S M
        //  A
        // S M
        coords.Add(Direction.Two, new(int, int, char)[4]);
        coords[Direction.Two][0] = (x-1, y-1, 'S');
        coords[Direction.Two][1] = (x+1, y-1, 'M');
        coords[Direction.Two][2] = (x-1, y+1, 'S');
        coords[Direction.Two][3] = (x+1, y+1, 'M');

        // Three
        // S S
        //  A
        // M M
        coords.Add(Direction.Three, new(int, int, char)[4]);
        coords[Direction.Three][0] = (x-1, y-1, 'S');
        coords[Direction.Three][1] = (x+1, y-1, 'S');
        coords[Direction.Three][2] = (x-1, y+1, 'M');
        coords[Direction.Three][3] = (x+1, y+1, 'M');

        // four
        // M S
        //  A
        // M S
        coords.Add(Direction.Four, new(int, int, char)[4]);
        coords[Direction.Four][0] = (x-1, y-1, 'M');
        coords[Direction.Four][1] = (x+1, y-1, 'S');
        coords[Direction.Four][2] = (x-1, y+1, 'M');
        coords[Direction.Four][3] = (x+1, y+1, 'S');

        return coords;

    }

    static int CoordToIndex(int x, int y, int ylen)
    {
        return x * ylen + y;
    }

    static (int, int) IndexToCoordinates(int i, int ylen)
    {
        return((int)(i / ylen), i % ylen);
    }
}
