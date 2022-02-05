using com.mobiquity.packer;
using System;

namespace com.mobiquity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Reading the absolute path from user input and invoke the class library Pack method to get results
            Console.WriteLine("Type the absolute path to the file:");
            string path = Console.ReadLine();

            try
            {
                string response = Packer.Pack(path);

                Console.WriteLine("Results");
                Console.WriteLine(response);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
