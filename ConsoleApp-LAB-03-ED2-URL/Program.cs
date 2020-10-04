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
            //for (int i = 100; i > 0; i--)
            //{
            //    int resul = i / 8;
            //    double resul2 = Convert.ToDouble(i) / Convert.ToDouble(8);
            //    double resul2Ded = Math.Ceiling(Convert.ToDouble(i) / 8);
            //    string texto = String.Format("{0,5}{1,10}{2,25}{3,25}" + Environment.NewLine, i, resul, resul2,resul2Ded);
            //    Console.WriteLine(texto);
            //}
            string paso ="";
         while(!paso.Equals("si"))
            { 
            Console.WriteLine("Ingrese el texto");
            string Text = Console.ReadLine();
            Console.WriteLine(CompresorCrack.Proceso_Prefijado(Text));
            Console.WriteLine(CompresorCrack.Resultado_Obtenido());
                Console.WriteLine("DESEA PARAR");
                paso = Console.ReadLine(); 
            Console.ReadKey();
            }
        }
    }
}
