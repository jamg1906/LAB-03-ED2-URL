using System;
using ClassLibrary_LAB_03_ED2_URL;

namespace ConsoleApp_LAB_03_ED2_URL
{
    class Program
    {
        static void Main(string[] args)
        {
            Huffman CompresorCrack = new Huffman();
            Console.WriteLine("Hello");   
            Console.WriteLine("Ingrese el texto");
            string Text = Console.ReadLine();
            CompresorCrack.CrearRegistros(Text);
            Console.WriteLine(CompresorCrack.Resultado_Obtenido());
            Console.ReadKey();
        }
    }
}
