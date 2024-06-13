using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_Practice
{
    internal class Word
    {
        public string Original { get; set; }
        public List<string> Translations { get; private set; }

        public Word(string original)
        {
            Original = original;
            Translations = new List<string>();
        }

        public void ControlLoop()
        {
            while (true)
            {
                Console.Write("\tEnter word operation (0 - add translation; 1 - change translation; 2 - remove; 3 - export; 4 - exit) - ");
                int operation = int.Parse(Console.ReadLine());

                if (operation < 0) operation = 0;
                if (operation >= 4) return;

                switch (operation)
                {
                    case 0:
                        AddTranslation();
                        break;
                    case 1:
                        ChangeTranslation();
                        break;
                    case 2:
                        RemoveTranslation();
                        break;
                    case 3:
                        Export();
                        break;
                }
            }
        }

        public void ChangeTranslation()
        {
            Console.Write("\tEnter which translation? - ");
            int index = int.Parse(Console.ReadLine());
            index = CorrectIndex(index);

            Console.Write("\tEnter new translation - ");
            string newTranslation = Console.ReadLine();

            Translations[index] = newTranslation;

            DictController.Save();
        }

        public void AddTranslation()
        {
            while (true)
            {
                Console.Write("\tEnter translation (^ to exit) - ");
                string translation = Console.ReadLine();

                if (translation == "^") break;
                if (translation == string.Empty) continue;

                Translations.Add(translation);
            }

            DictController.Save();
        }

        public void RemoveTranslation()
        {
            Console.Write("\tEnter which translation? - ");
            int index = int.Parse(Console.ReadLine());
            index = CorrectIndex(index);

            Translations.RemoveAt(index);
            DictController.Save();
        }

        public void Export()
        {
            Console.Write("\tEnter the export filename - ");
            string filename = Console.ReadLine();

            if (filename == string.Empty || filename == "^") return;

            using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
            {
                writer.WriteLine(this);
            }
        }

        public override string ToString()
        {
            string fullInfo = Original + " - ";

            int index = 0;
            foreach (string translation in Translations)
            {
                fullInfo += $"[{index}]{translation}" + (translation != Translations.Last() ? ", " : "");
                index++;
            }

            return fullInfo;
        }

        private int CorrectIndex(int index)
        {
            if (index < 0) index = 0;
            if (index >= Translations.Count) index = Translations.Count - 1;

            return index;
        }
    }
}
