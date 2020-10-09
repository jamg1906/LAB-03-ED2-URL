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
            using FileStream StreFight = new FileStream(Directory.GetCurrentDirectory() + "\\Compressed\\" + name + ".huff", FileMode.OpenOrCreate);
            using BinaryWriter Escritor = new BinaryWriter(StreFight);
            Escritor.Write(Impresor);
            Escritor.Close();
            //var data = CompresorCrack.Datos_Compresion();
            Compression.WriteRegistry(filename, Directory.GetCurrentDirectory() + "\\Compressed\\" + name + ".huff", 0.23, 0.34, 45.7);
        }

    }

}
