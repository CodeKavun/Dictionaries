using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TranslationDictionary translationDictionary = new TranslationDictionary("Polish-Ukrainian");
            translationDictionary.ControlLoop();
        }
    }
}
