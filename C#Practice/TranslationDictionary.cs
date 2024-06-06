using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_Practice
{
    internal class TranslationDictionary
    {
        public string Name { get; set; }
        public List<Word> Word { get; private set; }

        public TranslationDictionary(string name)
        {
            Name = name;
            Word = new List<Word>();
        }

        public void ControlLoop()
        {
            while (true)
            {
                Console.Write("Enter dictionary operation (0 - add; 1 - change word; 2 - like 1 but by original word; 3 - remove;\n 4 - save; 5 - load; 6 - show words; 7 - find word) - ");
                int operation = int.Parse(Console.ReadLine());

                if (operation < 0) operation = 0;
                if (operation > 7) operation = 7;

                switch (operation)
                {
                    case 0:
                        Add();
                        Console.Clear();
                        break;
                    case 1:
                        ChangeOriginalWord();
                        Console.Clear();
                        break;
                    case 2:
                        ChangeOriginalWordByName();
                        Console.Clear();
                        break;
                    case 3:
                        RemoveWord();
                        Console.Clear();
                        break;
                    case 4:
                        Save();
                        break;
                    case 5:
                        Load();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine(this);
                        break;
                    case 7:
                        FindWord();
                        Console.Clear();
                        break;
                }
            }
        }

        public void Add()
        {
            while (true)
            {
                Console.Write("Enter original word (^ to exit) - ");
                string original = Console.ReadLine();

                if (original == "^") break;
                if (original == string.Empty) continue;

                Word word = new Word(original);
                word.AddTranslation();

                Word.Add(word);
            }
        }

        public void ChangeOriginalWord()
        {
            Console.Write("Enter word index to change - ");
            int index = int.Parse(Console.ReadLine());
            index = CorrectIndex(index);

            Console.Write("Enter new original word - ");
            string newOriginal = Console.ReadLine();
            Word[index].Original = newOriginal;
        }

        public void ChangeOriginalWordByName()
        {
            Console.Write("Enter new original word - ");
            string oldOriginal = Console.ReadLine();

            Console.Write("Enter new original word - ");
            string newOriginal = Console.ReadLine();
            Word.Find(w => w.Original == oldOriginal).Original = newOriginal;
        }

        public void RemoveWord()
        {
            Console.Write("Enter which word? - ");
            int index = int.Parse(Console.ReadLine());
            index = CorrectIndex(index);

            Word.RemoveAt(index);
        }

        public void Save()
        {
            string data = JsonConvert.SerializeObject(Word, Formatting.Indented);

            using (StreamWriter writer = new StreamWriter("Dictionary.json", false, Encoding.UTF8))
            {
                writer.WriteLine(data);
            }

            Console.WriteLine($"{Name} dictionary has been saved!");
        }

        public void Load()
        {
            if (!File.Exists("Dictionary.json"))
            {
                Console.WriteLine("No such file was found!");
                return;
            }

            using (StreamReader reader = new StreamReader("Dictionary.json"))
            {
                List<Word> data = JsonConvert.DeserializeObject<List<Word>>(reader.ReadToEnd());
                Word = data;
            }

            Console.WriteLine($"{Name} dictionary data has been loaded!");
        }

        public void FindWord()
        {
            Console.Write("Enter the found word - ");
            string foundWord = Console.ReadLine();
            Console.Clear();

            Word word = Word.Find(w => w.Original.Contains(foundWord));
            Console.WriteLine(word);


            word.ControlLoop();
        }

        public override string ToString()
        {
            string wordList = string.Empty;

            int index = 0;
            foreach (Word word in Word)
            {
                wordList += $"[{index}]{word}\n";
                index++;
            }

            return wordList;
        }

        private int CorrectIndex(int index)
        {
            if (index < 0) index = 0;
            if (index >= Word.Count) index = Word.Count - 1;

            return index;
        }
    }
}
