using System;
using System.IO;

namespace terrain
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: Json2wmapConv.exe mapname.jm mapname.wmap");
                return;
            }
            try
            {
                FileInfo fi = new FileInfo(args[0].ToString());
                if (fi.Exists)
                    terrain.Json2WmapC.Convert(args[0], args[1]);
                else
                {
                    Console.WriteLine("Can't find file: " + fi.FullName);
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }
            Console.WriteLine("done");
        }
    }
}