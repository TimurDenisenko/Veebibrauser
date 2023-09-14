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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("          Browser TAR\n\n[1] Mine avalehele\n[2] Muuda kodulehte\n[3] Mine lehele\n[4] Vaadake järjehoidjaid\n" +
                                  "[5] Vaadake ajalugu\n[6] Näita enimkülastatud\n");
                ConsoleKeyInfo level = Console.ReadKey();
                switch (level.KeyChar)
                {
                    case '1':
                        browser.Home();
                        break;
                    case '2':
                        browser.SetHomePage();
                        break;
                    case '3':
                        browser.GoTo();
                        break;
                    case '4':
                        browser.ShowBookmarks();
                        break;
                    case '5':
                        browser.ShowHistory();
                        break;
                    case '6':
                        browser.MostVisited();
                        break;
                }
            }
        }
    }
}
