using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veebibrauser
{
    public class Browser
    {
        private Stack<string> _back = new Stack<string>();
        private Stack<string> _forward = new Stack<string>();
        private List<string> _history = new List<string>();
        private List<string> _bookmarks = new List<string>();
        private string _homeurl = "1";
        private string _current;

        public Browser(string url)
        {
            _homeurl = url;
            Home();
        }

        public Browser()
        {

        }

        public void Home()
        {
            Console.Clear();
            if (_homeurl == "1")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Viga!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
                return;
            }
            GoTo(_homeurl);
        }

        public void SetHomePage()
        {
            Console.Clear();
            Console.WriteLine("Kirjutage link");
            string url = Console.ReadLine();
            if (ControllUrl(url))
            {
                _homeurl = url;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Avaleht lisatud!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vale link!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            }
        }

        public bool isAlph(string url)
        {
            int i = 0;
            foreach (char item in url)
            {
                if (item=='.' && i==0)
                {
                    i++;
                }
                else if (Char.IsLetter(item))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void GoTo()
        {
            Console.Clear();
            Console.WriteLine("Kirjutage link");
            string url = Console.ReadLine();
            if (ControllUrl(url))
            {
                _back.Push(_current);
                _current = url;
                _forward.Clear();
                _history.Add(url);
                Console.Clear();
                if (url != _homeurl && !_bookmarks.Contains(url))
                {
                    Console.WriteLine("<- + ->  Enter");
                }
                else
                {
                    Console.WriteLine("<-  ->  Enter");
                }
                Current();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Back();
                        break;
                    case ConsoleKey.RightArrow:
                        Forward();
                        break;
                    case ConsoleKey.Add:
                        _bookmarks.Add(url);
                        GoTo(url);
                        break;
                    case ConsoleKey.Enter:
                        GoTo();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vale link!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            }
        }

        public void GoTo(string url)
        {
            Console.Clear();
            if (ControllUrl(url))
            {
                _back.Push(_current);
                _current = url;
                _forward.Clear();
                Console.WriteLine("<-  -> Enter");
                Current();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Back();
                        break;
                    case ConsoleKey.RightArrow:
                        Forward();
                        break;
                    case ConsoleKey.Enter:
                        GoTo();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vale link!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            }
        }

        public void Back()
        {
            Console.Clear();
            if (_back.Count > 0)
            {
                _forward.Push(_current);
                _current = _back.Pop();
                _history.Add(_current);
                Console.WriteLine("<-  -> Enter");
                Current();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Back();
                        break;
                    case ConsoleKey.RightArrow:
                        Forward();
                        break;
                    case ConsoleKey.Enter:
                        GoTo();
                        break;
                }
            }
        }

        public void Forward()
        {
            Console.Clear();
            if (_forward.Count > 0)
            {
                _back.Push(_current);
                _current = _forward.Pop();
                _history.Add(_current);
                Console.WriteLine("<-  -> Enter");
                Current();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Back();
                        break;
                    case ConsoleKey.RightArrow:
                        Forward();
                        break;
                    case ConsoleKey.Enter:
                        GoTo();
                        break;
                }
            }
        }

        public void Current()
        {
            Console.WriteLine("https://www."+_current+"/");
        }

        public List<string> History()
        {
            return _history;
        }

        public void ShowHistory()
        {
            Console.Clear();
            Console.WriteLine(string.Join("\n", _history));
            Console.ReadLine();
        }

        public void MostVisited()
        {
            Console.Clear();
            string urlMax = "";
            int urlMaxInt = 0;
            List<string> urlMaxList = new List<string>();
            List<int> urlMaxIntList = new List<int>();
            List<string> MostVisited = new List<string>();
            var q = _history.GroupBy(x => x).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count);
            foreach (var x in q)
            {
                urlMaxList.Add(x.Value);
                urlMaxIntList.Add(x.Count);
            }
            int index;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    index = urlMaxIntList.IndexOf(urlMaxIntList.Max());
                    MostVisited.Add(urlMaxList[index]);
                    urlMaxList.Remove(urlMaxList[index]);
                    urlMaxIntList.RemoveAt(index);
                    Console.WriteLine(MostVisited[i]);
                }
                catch (Exception)
                {
                    Console.ReadLine();
                    return;
                }
            }
            Console.ReadLine();
        }

        public void AddBookmark(string url) 
        {
            if (ControllUrl(url))
            {
                _bookmarks.Add(url);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vale link!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void ShowBookmarks()
        {
            Console.Clear();
            Console.WriteLine(string.Join("\n", _bookmarks));
            Console.ReadLine();
        }

        public bool ControllUrl(string url)
        {
                return ((url.EndsWith(".com") || url.EndsWith(".ee") || url.EndsWith(".org") || url.EndsWith(".net") || url.EndsWith(".edu") || url.EndsWith(".ru"))
                     && !url.StartsWith("https") && !url.StartsWith("www") && isAlph(url));
        }
    }
}
