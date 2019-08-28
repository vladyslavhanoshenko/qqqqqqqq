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
