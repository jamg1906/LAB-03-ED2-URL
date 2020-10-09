using System;
using System.IO;
using ClassLibrary_LAB_03_ED2_URL;

namespace ConsoleApp_LAB_03_ED2_URL
{
    class Program
    {
        static void Main(string[] args)
        {
            Huffman CompresorCrack = new Huffman();
            Console.WriteLine("Hello");
            //CompresorCrack.WriteRegistry("Name", "Come Back...Be Here.mp3", 23.88, 90.3, 0.23);
            //string paso = "";
            //while (!paso.Equals("si"))
            //{
            //    Console.WriteLine("Ingrese el texto");
            //    string Text = Console.ReadLine();
            //    byte[] texto = new byte[Text.Length];
            //    for (int i = 0; i < Text.Length; i++)
            //    {
            //        texto[i] = Convert.ToByte(Convert.ToChar(Text[i]));
            //    }
            //    byte[] Comprimido = CompresorCrack.Compresion(texto);
            //    string result = "";
            //    foreach (byte bit in Comprimido)
            //    {
            //        result += Convert.ToString(Convert.ToChar(bit));
            //    }
            //    Console.WriteLine(Comprimido.ToString());
            //    Console.WriteLine(result);
            //    Console.WriteLine("------------------------------------------------------------------------");
            //    Console.ReadKey();
            //    Console.WriteLine("------------------------------------------------------------------------");
            //    Console.WriteLine("------------------------------------------------------------------------");
            //    Console.WriteLine(CompresorCrack.Resultado_Obtenido());
            //    Console.WriteLine("------------------------------------------------------------------------");
            //    Console.WriteLine("------------------------------------------------------------------------");
            //    Console.WriteLine("------------------------------------------------------------------------");
            //    result = "";
            //    byte[] Descomprimido = CompresorCrack.Descompresion(Comprimido);
            //    foreach (byte bit in Descomprimido)
            //    {
            //        result += Convert.ToString(Convert.ToChar(bit));
            //    }
            //    Console.WriteLine(result);
            //    Console.WriteLine("DESEA PARAR");
            //    paso = Console.ReadLine();
            //}

            using FileStream file = new FileStream("C:\\Users\\Diego Veliz\\Desktop\\MundoCiego.txt", FileMode.OpenOrCreate);
            using BinaryReader Lector = new BinaryReader(file);
            int Cant_Byte_Read = 10000;
            int Aumentar_Max = 1;
            byte[] Text = new byte[Cant_Byte_Read];
            Text = Lector.ReadBytes(Cant_Byte_Read);
            while (file.Position < file.Length)
            {
                byte[] Aux = Lector.ReadBytes(Cant_Byte_Read);
                Array.Resize(ref Text, Text.Length + Aux.Length);
                Aux.CopyTo(Text, Cant_Byte_Read * Aumentar_Max);
                Aumentar_Max++;
            }
            Lector.Close();
            byte[] Impresor = CompresorCrack.Compresion(Text);

            using FileStream StreFight = new FileStream("C:\\Users\\Diego Veliz\\Desktop\\PruebaFinalWorld.txt", FileMode.OpenOrCreate);
            using BinaryWriter Escritor = new BinaryWriter(StreFight);
            Escritor.Write(Impresor);
            Escritor.Close();

            //Console.WriteLine(CompresorCrack.Resultado_Obtenido());




            //using FileStream file = new FileStream("C:\\Users\\Diego Veliz\\Desktop\\PruebaFinalWorld.txt", FileMode.OpenOrCreate);
            //using BinaryReader Lector = new BinaryReader(file);
            //int Cant_Byte_Read = 10000;
            //int Aumentar_Max = 1;
            //byte[] Text = new byte[Cant_Byte_Read];
            //Text = Lector.ReadBytes(Cant_Byte_Read);
            //while (file.Position < file.Length)
            //{
            //    byte[] Aux = Lector.ReadBytes(Cant_Byte_Read);
            //    Array.Resize(ref Text, Text.Length + Aux.Length);
            //    Aux.CopyTo(Text, Cant_Byte_Read * Aumentar_Max);
            //    Aumentar_Max++;
            //}
            //Lector.Close();
            //byte[] Impresor = CompresorCrack.Descompresion(Text);
            //using FileStream StreFight = new FileStream("C:\\Users\\Diego Veliz\\Desktop\\FinalResulMundo.txt", FileMode.OpenOrCreate);
            //using BinaryWriter Escritor = new BinaryWriter(StreFight);
            //Escritor.Write(Impresor);
            //Escritor.Close();

            Console.WriteLine("Hecho");
            Console.ReadKey();
        }
    }
}
