using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LAB_03_ED2_URL.Models
{
    public class Compression
    {
        public string originalName { get; set; }
        public string compressedFilePath { get; set; }
        public double compressionRatio { get; set; }
        public double compressionFactor { get; set; }
        public double reductionPercentage { get; set; }

        public static List<Compression> GetAllCompressions()
        {
            try
            {
                List<Compression> All = new List<Compression>();
                using (var reader = new StreamReader(Directory.GetCurrentDirectory() + "\\test.json"))
                {
                    string registry;
                    registry = reader.ReadToEnd();
                    JsonSerializerOptions rule = new JsonSerializerOptions { IgnoreNullValues = true };
                    All = JsonSerializer.Deserialize<List<Compression>>(registry, rule);
                }
                return All;
            }
            catch { return null; }
        }

        public static void WriteRegistry(string OriginalName, string CompressedFilePath, double CompressionRatio, double CompressionFactor, double ReductionPercentage)
        {
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\test.json");
            if (!File.Exists(path))
            {
                using (FileStream creator = File.Create(path)){ };
            }
            using (var reader = new StreamReader(path))
            {
                JsonSerializerOptions rule = new JsonSerializerOptions { IgnoreNullValues = true };
                List<Compression> All = new List<Compression>();
                Compression tempCompression = new Compression
                {
                    originalName = OriginalName,
                    compressedFilePath = CompressedFilePath,
                    compressionRatio = CompressionRatio,
                    compressionFactor = CompressionFactor,
                    reductionPercentage = ReductionPercentage,
                };
                try
                {
                    All = JsonSerializer.Deserialize<List<Compression>>(reader.ReadToEnd(), rule);
                    All.Add(tempCompression);
                    reader.Close();
                }
                catch
                {
                    All.Add(tempCompression);
                    reader.Close();
                }
                using (var writer = new StreamWriter(path))
                {
                    var AllRegistries = JsonSerializer.Serialize<List<Compression>>(All, rule);
                    writer.Write(AllRegistries);
                }
            }
        }

        public static void CompressFile(string filePath, string filename, string name)
        {
            ClassLibrary_LAB_03_ED2_URL.Huffman CompresorCrack = new ClassLibrary_LAB_03_ED2_URL.Huffman();
            using FileStream fileC = new FileStream(filePath, FileMode.OpenOrCreate);
            using BinaryReader Lector = new BinaryReader(fileC);
            int Cant_Byte_Read = 10000;
            int Aumentar_Max = 1;
            byte[] Text = new byte[Cant_Byte_Read];
            Text = Lector.ReadBytes(Cant_Byte_Read);
            while (fileC.Position < fileC.Length)
            {
                byte[] Aux = Lector.ReadBytes(Cant_Byte_Read);
                Array.Resize(ref Text, Text.Length + Aux.Length);
                Aux.CopyTo(Text, Cant_Byte_Read);
                Aumentar_Max++;
            }
            Lector.Close();
            byte[] Impresor = CompresorCrack.Compresion(Text);
<<<<<<< HEAD
         
            byte[] Result = new byte[filename.Length+1];
            Result[0] = Convert.ToByte(filename.Length);
            for (int i = 1; i <= filename.Length; i++)
            {
                Result[i] = Convert.ToByte(Convert.ToChar(filename[i-1]));
            }
            Array.Resize(ref Result, filePath.Length + Impresor.Length);
            Impresor.CopyTo(Result, filePath.Length);

            using FileStream StreFight = new FileStream(Directory.GetCurrentDirectory() + "\\Compressed\\" + name + ".huff", FileMode.OpenOrCreate);
=======
            string FinalFileName = Directory.GetCurrentDirectory() + "\\Compressed\\" + name + ".huff";
            int count = 0;
            while (File.Exists(FinalFileName))
            {
                count++;
                FinalFileName = Directory.GetCurrentDirectory() + "\\Compressed\\" + name + count + ".huff";
            }
            using FileStream StreFight = new FileStream(FinalFileName, FileMode.OpenOrCreate);
>>>>>>> 57d3363a199d78c52432b1cc8db33faf3372d046
            using BinaryWriter Escritor = new BinaryWriter(StreFight);
            Escritor.Write(Result);
            Escritor.Close();
<<<<<<< HEAD

            double[] data = CompresorCrack.Datos_Compresion();
            Compression.WriteRegistry(filename, Directory.GetCurrentDirectory() + "\\Compressed\\" + name + ".huff", data[0], data[1], data[2]);
        }

        public void Obtener_OriginalName()
        {
            byte[] TextoComprimido = new byte[32];
            int cant_CName = TextoComprimido[0];
            string Name_Original = "";
            for(int i=1; i<= cant_CName;i++)
            {
                Name_Original += Convert.ToChar(TextoComprimido[i]);
            }
            byte[] Data_retorna = new byte[TextoComprimido.Length - (cant_CName+1)];
            Array.Copy(TextoComprimido, (cant_CName + 1), Data_retorna, 0, Data_retorna.Length);
            //Aqui debe ir la parte de descompresion. 
=======
            //var data = CompresorCrack.Datos_Compresion();
            Compression.WriteRegistry(filename, FinalFileName, 0.23, 0.34, 45.7);
>>>>>>> 57d3363a199d78c52432b1cc8db33faf3372d046
        }


    }

}
