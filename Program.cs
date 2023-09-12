using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veebibrauser
{
    public class Program
    {
        public static void Main()
        {
            Browser browser = new Browser();
            browser.Current();
            browser.GoTo("youtube");
            browser.Current();
            browser.GoTo("youtube.edu");
            browser.Current();
            browser.GoTo("3.com");
            browser.Current();
            browser.AddBookmark("youtube.com");
        }
    }
}
