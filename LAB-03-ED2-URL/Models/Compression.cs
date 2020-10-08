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
            List<Compression> All = new List<Compression>();

            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + "\\test.json"))
            {
                string registry;
                registry = reader.ReadToEnd();
                JsonSerializerOptions rule = new JsonSerializerOptions { IgnoreNullValues = true };
                All = JsonSerializer.Deserialize<List<Compression>>(registry, rule);
                //Este era el lector del txt
                /*do
                {
                    registry = reader.ReadLine();
                    var attributes = registry.Split('|');
                    try
                    {
                        Compression TempCompression = new Compression();
                        TempCompression.originalName = attributes[0];
                        TempCompression.compressedFilePath = attributes[1];
                        TempCompression.compressionRatio = Convert.ToDouble(attributes[2]);
                        TempCompression.compressionFactor = Convert.ToDouble(attributes[3]);
                        TempCompression.reductionPercentage = Convert.ToDouble(attributes[4]);
                        All.Add(TempCompression);
                    }
                    catch { return null; }
                } while (!reader.EndOfStream);*/
            }
            return All;
        }
    }
}
