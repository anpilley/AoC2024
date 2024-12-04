namespace day4;


enum Direction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
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
                foreach(char c in row)
                {
                    input[i] = c;
                    i++;
                }
            }
            int counter = 0;
            for(i = 0; i < input.Length; i++)
            {
                if(input[i] == 'X')
                {
                    (int, int) coords = IndexToCoordinates(i, ylen);
                    var set = GetCoords(coords.Item1, coords.Item2, xlen, ylen);

                    Console.WriteLine($"Checking around {coords}, directions {set.Count()}");
                    
                    foreach(var dir in set)
                    {
                        bool valid = true;
                        foreach(var c in dir.Value){
                            int x1 = c.Item1;
                            int y1 = c.Item2;
                            char t = c.Item3;

                            if(input[CoordToIndex(x1, y1, ylen)] != t)
                            {
                                valid = false;
                                break;
                            }
                        }
                        if(valid)
                            counter++;
                        
                    }
                }
            }

            Console.WriteLine($"Count: {counter}");


        }
    }

    // get every valid coordinate set for a direction. Excludes ones that cross the edge.
    // hypothesis: we'll need to wrap in part 2.
    static IDictionary<Direction, (int, int, char)[]> GetCoords(int x, int y, int xlen, int ylen)
    {
        Dictionary<Direction, (int, int, char)[]> coords = new();

        // up
        if(!(x-3 < 0))
        {
            coords.Add(Direction.Up, new (int, int, char)[3]);
            coords[Direction.Up][0] = (x-1, y, 'M');
            coords[Direction.Up][1] = (x-2, y, 'A');
            coords[Direction.Up][2] = (x-3, y, 'S');
        }

        // upright
        if(!(x-3 < 0) && !(y+3 >= ylen))
        {
            coords.Add(Direction.UpRight, new (int, int, char)[3]);
            coords[Direction.UpRight][0] = (x-1, y+1, 'M');
            coords[Direction.UpRight][1] = (x-2, y+2, 'A');
            coords[Direction.UpRight][2] = (x-3, y+3, 'S');
        }

        // right
        if(!(y+3 >= ylen))
        {
            coords.Add(Direction.Right, new (int, int, char)[3]);
            coords[Direction.Right][0] = (x, y+1, 'M');
            coords[Direction.Right][1] = (x, y+2, 'A');
            coords[Direction.Right][2] = (x, y+3, 'S');
        }

        // downright
        if(!(x+3 >= xlen) && !(y+3 >= ylen))
        {
            coords.Add(Direction.DownRight, new (int, int, char)[3]);
            coords[Direction.DownRight][0] = (x+1, y+1, 'M');
            coords[Direction.DownRight][1] = (x+2, y+2, 'A');
            coords[Direction.DownRight][2] = (x+3, y+3, 'S');
        }

        // down
        if(!(x+3 >= xlen))
        {
            coords.Add(Direction.Down, new (int, int, char)[3]);
            coords[Direction.Down][0] = (x+1, y, 'M');
            coords[Direction.Down][1] = (x+2, y, 'A');
            coords[Direction.Down][2] = (x+3, y, 'S');
        }

        // downleft
        if(!(x+3 >= xlen) && !(y-3 < 0))
        {
            coords.Add(Direction.DownLeft, new (int, int, char)[3]);
            coords[Direction.DownLeft][0] = (x+1, y-1, 'M');
            coords[Direction.DownLeft][1] = (x+2, y-2, 'A');
            coords[Direction.DownLeft][2] = (x+3, y-3, 'S');
        }

        // left
        if(!(y-3 < 0))
        {
            coords.Add(Direction.Left, new (int, int, char)[3]);
            coords[Direction.Left][0] = (x, y-1, 'M');
            coords[Direction.Left][1] = (x, y-2, 'A');
            coords[Direction.Left][2] = (x, y-3, 'S');
        }

        // upleft
        if(!(x-3 < 0) && !(y-3 < 0))
        {
            coords.Add(Direction.UpLeft, new (int, int, char)[3]);
            coords[Direction.UpLeft][0] = (x-1, y-1, 'M');
            coords[Direction.UpLeft][1] = (x-2, y-2, 'A');
            coords[Direction.UpLeft][2] = (x-3, y-3, 'S');
        }

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
