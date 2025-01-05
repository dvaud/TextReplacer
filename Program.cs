using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the folder path:");
        string folderPath = Console.ReadLine();

        Console.WriteLine("Enter the target word to replace:");
        string targetWord = Console.ReadLine();

        Console.WriteLine("Enter the replacement word:");
        string replacementWord = Console.ReadLine();

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                try
                {
                    string tempFile = Path.GetTempFileName();

                    using (StreamReader reader = new StreamReader(file))
                    using (StreamWriter writer = new StreamWriter(tempFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string modifiedLine = line.Replace(targetWord, replacementWord);
                            writer.WriteLine(modifiedLine);
                        }
                    }

                    // Replace original file with the modified temp file
                    File.Copy(tempFile, file, true);
                    File.Delete(tempFile);

                    Console.WriteLine($"Processed file: {file}");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {file}: {ex.Message}");
                }
            }
        }
        else
        {
            Console.WriteLine("The specified folder does not exist.");
        }
    }
}
