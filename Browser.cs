using System;
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
        private string _homeurl;
        private string _current;

        public Browser()
        {
            _homeurl = "google.com";
            Home();
        }

        public void Home()
        {
            GoTo(_homeurl);
        }

        public void SetHomePage(string url)
        { 
            _homeurl = url;
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

        public bool ControlUrl(string url)
        {
            return (url.EndsWith(".com") || url.EndsWith(".ee") || url.EndsWith(".org") || url.EndsWith(".net") || url.EndsWith(".edu"))
                    && !url.StartsWith("https") && !url.StartsWith("www")  && isAlph(url);
        }

        public void GoTo(string url)
        {
            if ((url.EndsWith(".com") || url.EndsWith(".ee") || url.EndsWith(".org") || url.EndsWith(".net") || url.EndsWith(".edu"))
                && !url.StartsWith("https") && !url.StartsWith("www")  && isAlph(url))
            {
                _back.Push(_current);
                _current = url;
                _forward.Clear();
                _history.Add(url);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vale link!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Back()
        {
            if (_back.Count > 0)
            {
                _forward.Push(_current);
                _current = _back.Pop();
                _history.Add(_current);
            }
        }

        public void Forward()
        {
            if (_forward.Count > 0)
            {
                _back.Push(_current);
                _current = _forward.Pop();
                _history.Add(_current);
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

        public void MostVisited()
        {
            Console.WriteLine();
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
                    return;
                }
            }
        }

        public void AddBookmark(string url) 
        {
            _bookmarks.Add(url);
        }

        public void ShowBookmarks()
        {
            Console.WriteLine(string.Join(", ", _bookmarks));
        }
    }
}
