using System.Text.RegularExpressions;

namespace day5;

public class PageComparer : IComparer<int>
{
    private Dictionary<int, List<int>> pageMap;

    public PageComparer(Dictionary<int, List<int>> pageMap)
    {
        this.pageMap = pageMap;
    }

    public int Compare(int x, int y)
    {
        if(!pageMap.ContainsKey(x))
        {
            return 0;
        }
        
        List<int> set = pageMap[x];

        foreach(int p in set)
        {
            if(y == p)
                return -1;
        }

        return 1;

    }
}

class Program
{
    static void Main(string[] args)
    {
        int total = 0;
        int badtotal = 0;
        Dictionary<int, List<int>> pageMap = new();
        var regex = new Regex("(?<pageordermap>[0-9]+\\|[0-9]+)|[0-9]+");
        var numregex = new Regex("[0-9]+");
        var pageComp = new PageComparer(pageMap);

        using(var reader = new StreamReader("C:\\Dev\\Practice\\AoC2024\\day5.txt"))
        {
            string line;
            bool pageset = true;
            while((line = reader.ReadLine()!) != null)
            {
                if(line.Length == 0)
                {
                    pageset = false;
                    continue;
                }

                if(pageset)
                {
                    var matches = regex.Matches(line);

                    if(matches[0].Groups["pageordermap"].Success)
                    {
                        var nums = numregex.Matches(matches[0].Value);
                        int page = Int32.Parse(nums[0].Value);
                        int mapto = Int32.Parse(nums[1].Value);

                        if(!pageMap.ContainsKey(page))
                        {
                             pageMap[page] = new List<int>();    
                        }
                        pageMap[page].Add(mapto);
                    }
                }
                else
                {
                    Dictionary<int, int> seenPages = new();
                    bool valid = true;
                    MatchCollection matches = numregex.Matches(line);
                    int[] pagearray = new int[matches.Count()];
                    int i = 0;
                    foreach(Match match in matches)
                    {
                        int page = Int32.Parse(match.Value);
                        pagearray[i] = page;
                        i++;
                    }

                    for(int j = 0; j < pagearray.Length; j++)
                    {
                        int page = pagearray[j];
                    
                        seenPages.Add(page, j);

                        if(!pageMap.ContainsKey(page))
                        {   
                            continue;
                        }
                        
                        List<int> beforeSet = pageMap[page];
                        foreach(int p in beforeSet)
                        {
                            if(seenPages.ContainsKey(p))
                            {
                                valid = false;
                                break;
                            }
                        }
                        if(!valid)
                        {
                            Console.WriteLine($"Line {line} not valid");
                            break;
                        }
                    }
                    

                    if(valid)
                    {
                        // truncation is okay here, that will get us the midpoint
                        int mid = (int)(matches.Count() / 2);
                        int midpage = Int32.Parse(matches[mid].Value);
                        Console.WriteLine($"Line {line} valid, midpage {midpage}");
                        total += midpage;
                    }
                    else
                    {
                        Array.Sort(pagearray, pageComp);

                        int mid = (int)(matches.Count() / 2);
                        int midpage = pagearray[mid];
                        Console.WriteLine($"Line {line} invalid, midpage {midpage}");
                        badtotal += midpage;
                    }
                }
            }

            Console.WriteLine($"Total: {total}");
            Console.WriteLine($"Bad Total: {badtotal}");
            
        }
    }

    
}
