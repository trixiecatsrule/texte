using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexteTranslator
{
    class DictionaryEntry
    {
        protected string baseWord;
        protected string[] partOfSpeech;
        protected string[] conjugations;

        public static readonly Dictionary<string, int> numOfPOSMap = new Dictionary<string, int>()
        {
            { "pn", 1 },
            { "pr", 1 },
            { "cconj", 1 },
            { "sconj", 2 },
            { "part", 1 },
            { "adj", 1 },
            { "v1", 2 },
            { "v2", 2 },
            { "v3", 2 },
            { "n", 3 }
        };

        public DictionaryEntry()
        {
            baseWord = "";
            partOfSpeech = new string[0];
            conjugations = new string[0];
        }

        public DictionaryEntry(string[] dictionaryLine)
        {
            baseWord = dictionaryLine[0];

            int numOfPOS = numOfPOSMap[dictionaryLine[1]];
            partOfSpeech = new string[numOfPOS];

            for (int i = 0; i < numOfPOS; i++)
            {
                partOfSpeech[i] = dictionaryLine[i + 1];
            }

            conjugations = new string[dictionaryLine.Length - (numOfPOS + 1)];
            Array.Copy(dictionaryLine, numOfPOS + 1, conjugations, 0, dictionaryLine.Length - (numOfPOS + 1));
            //sets conjugations equal to dictionaryLine's elements, minus the first two.
        }

        public DictionaryEntry(string baseW, string[] pos, string[] allConjugations)
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

        public string[] getPOS()
        {
            return partOfSpeech;
        }

        public override string ToString()
        {
            string toReturn = baseWord.ToUpper() + " (";
            foreach (string POS in partOfSpeech)
            {
                toReturn += POS + ", ";
            }
            toReturn = toReturn.Substring(0, toReturn.Length - 2) + "): ";
            foreach (string conjugation in conjugations)
            {
                toReturn += conjugation + " ";
            }

            return toReturn;
        }
    }
}
