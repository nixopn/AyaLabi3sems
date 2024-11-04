using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
namespace TestConcole
{


    class Program
    {

        static void Main()
        {
            string catalog = @"C:\Users\eisne\source\repos\ConsoleApp108\ConsoleApp112";

            string fileName = "shivotnoer.xml";

            foreach (string findedFile in Directory.EnumerateFiles(catalog, fileName,
                SearchOption.AllDirectories))
            {
                FileInfo FI;
                try
                {
                    FI = new FileInfo(findedFile);
                    Console.WriteLine(FI.Name + " " + FI.FullName + " " + FI.Length + "_байт" +
                        " Создан: " + FI.CreationTime);

                }
                catch //слишком длинное имя файла
                {
                    continue;
                }

            }

            string sourceFile = "shivotnoer.xml"; // исходный файл
            string compressedFile = "shivotnoer.gz"; // сжатый файл

            async Task CompressAsync(string sourceFile, string compressedFile)
            {
                // поток для чтения исходного файла
                using FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate);
                // поток для записи сжатого файла
                using FileStream targetStream = File.Create(compressedFile);

                // поток архивации
                using GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
                await sourceStream.CopyToAsync(compressionStream); // копируем байты из одного потока в другой

                Console.WriteLine($"Сжатие файла {sourceFile} завершено.");
                Console.WriteLine($"Исходный размер: {sourceStream.Length}  сжатый размер: {targetStream.Length}");
            }


            // создание сжатого файла
            CompressAsync(sourceFile, compressedFile);




            String line;
            try
            {
                
                StreamReader chitalar = new StreamReader(@"C:\\aaaa.txt");
                
                line = chitalar.ReadLine();
                
                while (line != null)
                {
                    
                    Console.WriteLine(line);
                    
                    line = chitalar.ReadLine();
                }
                
                chitalar.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }


    }
}