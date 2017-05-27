using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexteTranslator
{
    class DictionaryEntry
    {
        private string baseWord;
        private string partOfSpeech;
        private string[] conjugations;

        public DictionaryEntry(string[] dictionaryLine)
        {
            baseWord = dictionaryLine[0];
            partOfSpeech = dictionaryLine[1];

            conjugations = new string[dictionaryLine.Length - 1];
            Array.Copy(dictionaryLine, 2, conjugations, 0, dictionaryLine.Length - 2);
            //sets conjugations equal to dictionaryLine's elements, minus the first two.
        }

        public DictionaryEntry(string baseW, string pos, string[] allConjugations)
        {
            baseWord = baseW;
            partOfSpeech = pos;
            conjugations = allConjugations;
        }

        public string getBase()
        {
            return baseWord;
        }

        public string[] getConjugations()
        {
            return conjugations;
        }

        public string getConjugation(int number)
        {
            return conjugations[number];
        }

        public override string ToString()
        {
            string toReturn = baseWord + " " + partOfSpeech + " ";
            foreach (string conjugation in conjugations)
            {
                toReturn += conjugation + " ";
            }

            return toReturn;
        }
    }
}
