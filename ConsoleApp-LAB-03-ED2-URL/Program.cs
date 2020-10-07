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
            string paso ="";
            while (!paso.Equals("si"))
            {
                Console.WriteLine("Ingrese el texto");
                string Text = Console.ReadLine();
                string Comprimido = CompresorCrack.Compresion(Text);
                Console.WriteLine(Comprimido);
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine(CompresorCrack.Resultado_Obtenido());
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine(CompresorCrack.Descompresion(Comprimido));
                Console.WriteLine("DESEA PARAR");
                paso = Console.ReadLine();
            }
            Console.ReadKey();
        }
    }
}
