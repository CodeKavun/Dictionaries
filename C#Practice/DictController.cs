using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_Practice
{
    internal static class DictController
    {
        public static List<TranslationDictionary> Dicts { get; private set; } = new List<TranslationDictionary>();

        public static void ControlLoop()
        {
            Load();

            while (true)
            {
                Console.WriteLine("Dictionaries: ");

                int index = 0;
                foreach (TranslationDictionary dictionary in Dicts)
                {
                    Console.WriteLine($"\t[{index}] {dictionary.Name}");
                    index++;
                }

                Console.Write("\n0 - create new; 1 or others - select: ");
                int operation;

                try
                {
                    operation = int.Parse(Console.ReadLine());
                }
                catch
                {
                    operation = 0;    
                }

                switch (operation)
                {
                    case 0:
                        Create();
                        Console.Clear();
                        break;
                    default:
                        Console.Write("Select dictionary: ");
                        int dictIndex;
                        try
                        {
                            dictIndex = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            dictIndex = 0;
                        }

                        if (dictIndex < 0) dictIndex = 0;
                        else if (dictIndex >= Dicts.Count) dictIndex = Dicts.Count - 1;

                        Console.Clear();
                        Dicts[dictIndex].ControlLoop();
                        Console.Clear();

                        break;
                }
            }
        }

        private static void Create()
        {
            Console.Write("Dictionary name: ");
            string name = Console.ReadLine();

            if (name != string.Empty) Dicts.Add(new TranslationDictionary(name));
            Save();
        }

        public static void Save()
        {
            using (StreamWriter writer = new StreamWriter("Dictionaries.json", false, Encoding.UTF8))
            {
                string data = JsonConvert.SerializeObject(Dicts);
                writer.WriteLine(data);
            }
        }

        public static void Load()
        {
            if (!File.Exists("Dictionaries.json")) return;

            using (StreamReader reader = new StreamReader("Dictionaries.json"))
            {
                string data = reader.ReadToEnd();
                Dicts = JsonConvert.DeserializeObject<List<TranslationDictionary>>(data);
            }
        }
    }
}
