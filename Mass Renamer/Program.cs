using System;
using System.Text;

namespace MassRenamer
{
    class Program
    {
        static private void Quit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (args.Length == 0) 
            {
                Console.WriteLine("No files were specified, nothing to do.");
                Quit();
                return;
            }
            var editList = new EditList(args);
            editList.Edit();
            editList.Rename();
            Quit();
        }
    }
}
