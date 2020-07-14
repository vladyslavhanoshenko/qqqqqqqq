using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFramework.Helpers
{
    class ReadersAndWritersToOrFromFile
    {
        public int ReadIndexFromFile(string indexFileReaderPath)
        {
            string dataFromDocument;
            int indexFromFile = 0;
            try
            {
                using (StreamReader sr = new StreamReader(indexFileReaderPath, System.Text.Encoding.Default))
                {
                    dataFromDocument = sr.ReadToEnd();
                }
                indexFromFile = int.Parse(dataFromDocument);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return indexFromFile;
        }

        public string ReadFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                var text = sr.ReadToEnd();
                return text;
            }

        }

        public void RemoveFromFile(string stringToRemove, string filePath)
        {
            var text = ReadFile(filePath).Split('\n').ToList();
            var fileIndex = text.FindIndex(i => i.Contains(stringToRemove));
            text.Remove(text.ElementAt(fileIndex));

            using(StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                foreach(var test in text)
                {
                    sw.Write(test);
                    sw.Write("\n");
                }
                
              
            }

        }

        public void RewriteFile(List<string> textToBeWrite, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                foreach(var test in textToBeWrite)
                {
                    sw.Write(test);
                    sw.Write("\n");

                }
                
            }


        }

        public string WriteToFile(string text, string urlToVerify, string readPath, string writePath)
        {
            using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
            {
                text = sr.ReadToEnd();
                if (text.Contains(urlToVerify))
                {
                    return "Url is already present";
                }
            }
            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
            {
                sw.Write(urlToVerify);
                sw.Write("\n");
            }
            return "Written";
        }

        public void WriteIndexToTheFile(int index, string indexFileWriterPath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(indexFileWriterPath, false, System.Text.Encoding.Default))
                {
                    sw.Write(index);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Index was not saved");
            }
        }
    }
}
