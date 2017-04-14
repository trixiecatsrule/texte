using System.Collections.Generic;
using System.Diagnostics;
using static System.Collections.Generic.Dictionary<string, int>;

namespace TexteTranslator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.input = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // input
            // 
            this.input.Enabled = false;
            this.input.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input.Location = new System.Drawing.Point(12, 12);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.ReadOnly = true;
            this.input.Size = new System.Drawing.Size(260, 95);
            this.input.TabIndex = 0;
            this.input.UseWaitCursor = true;
            // 
            // output
            // 
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Enabled = false;
            this.output.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.Location = new System.Drawing.Point(12, 142);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(260, 107);
            this.output.TabIndex = 1;
            // 
            // goButton
            // 
            this.goButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.goButton.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.goButton.Location = new System.Drawing.Point(105, 113);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "Translate";
            this.goButton.UseVisualStyleBackColor = false;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.goButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.output);
            this.Controls.Add(this.input);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.Button goButton;

        private Dictionary<string, string> sampleWords = new Dictionary<string, string>() {
            { "plūīr", "tree" },
            { "лājs", "house" },
            { "зtē", "symbol" },
            { "welantωon", "truck" },
            { "złāя", "manner" },
            { "øo", "small" },
            { "plī", "big" }
        }; //some sample words to play with

        private string[][] loadedWords;

        private Dictionary<string, int> sampleEndingMap = new Dictionary<string, int>() {
            { "o", 2 },
            { "λ", 3 },
            { "on", 4 },
            { "ol", 5 },
            { "īr", 6 }
        }; //providing "o" returns " + nominative", etc.
        //   at a certain point, this will return english endings instead of phrases like "accusative".

        private Dictionary<string, string> sampleCaseMap = new Dictionary<string, string>() {
            { "o", "+Nominative" },
            { "λ", "+Accusative" },
            { "on", "+Possessive" },
            { "ol", "+Genitive" },
            { "īr", "+Locative" }
        }; //A case map which will be used if the ending is recognized but the word isn't.

        /**
         * Runs when the form loads.
         */
        void start()
        {
            readFile("C:\\Users\\Hazel\\Downloads\\Texte\\TexteTranslator\\TexteTranslator\\Assets\\Dictionary.txt");
            print2DArray(loadedWords);

            //Only after the dictionary has loaded do we allow inputs.
            input.ReadOnly = false;
            input.Enabled = true;
        }

        /**
         * Called when goButton is clicked or enter is pressed on the form.
         */
        void buttonClick()
        {
            string inputText = this.input.Text;

            this.output.Text = ""; //Blank the output box.

            string[] inputWords = inputText.Split(' ');

            foreach(string inputWord in inputWords) //Process each word separately
            {
                string word;

                extractWordAndSuffix(inputWord, loadedWords, sampleEndingMap, sampleCaseMap, out word);
                //check to see if the inputted string includes one of the sample words/endings.

                this.output.Text += word + " "; //Introduce the space back in.
            }

            /*
             * This algorithm is super recursive!
             * While (suffix != "")
             *     Extract a suffix and the remainder string
             *     Add the extracted suffix to the front of a return string
             * Check to see if the remainder string is a word, if it is, add it to the front of the return string
             * Return the return string.
             * 
             * (A prefix function is currently a WIP.) 
             */
        }

        /**
         * Takes an inputted text, a set of potential words, and a set of potential endings, and returns the found word plus the found ending.
         * @param inText The string from which to derive the ending
         * @param words Potential words for the string
         * @param endingMap Potential endings for the string
         * @param caseMap Used if the word isn't recognized but the case still is.
         * @param[out] word The rest of the phrase (possibly translated) without the suffix
         * @param[out] suffix The translated suffix. "" if no suffix was recognized.
         */
        void extractWordAndSuffix(string inText, string[][] words, Dictionary<string, int> endingMap, Dictionary<string, string> caseMap, out string word)
        {
            string[] possibleWords = getWord(inText, words);

            if (possibleWords != null) //A word without a suffix was matched.
            {
                word = possibleWords[2]; //Default to the nominative case.
                return; //There's no need to go further. The entire phrase was a translatable word.
            }
            
            //If the return wasn't called, the input was a known word + a suffix, an unknown word + a suffix, or an unknown word.

            string ending = getEnding(inText, endingMap.Keys); //Get the ending.

            if (ending != "") //If an ending was matched...
            {
                string withoutEnding = inText.Substring(0, inText.Length - ending.Length);
                //Sets withoutEnding equal to the part of the string w/o the suffix

                possibleWords = getWord(withoutEnding, words); //See if the input, w/o the suffix, is a word.

                int suffixNumber = endingMap[ending];

                if (possibleWords != null) //If a word and a suffix was matched, return that word conjugated correctly.
                {
                    word = possibleWords[suffixNumber];
                    return;
                }
                else //No word was matched, but a suffix was. 
                     //[IN A RECURSIVE FUNCTION, THIS IS *THE ONLY* CASE WHICH SHOULD LOOP]
                {
                    word = withoutEnding + caseMap[ending];
                    return;
                }
            }
            
            //If we get here, no ending was matched and the input is not just a word.
            //So the only thing it can be is an unknown word.
            word = inText; //Nothing could be translated :(
            return;
        }

        /**
         * If the inputted word is one of those in the dictionary, returns the word. Else, returns the inputted word.
         * @param inText The word to check against the dictionary
         * @param words The dictionary of words (2D array)
         * @returns An array of possible conjugations, or null.
         */
        string[] getWord(string inText, string[][] words)
        {
            for (int i = 0; i < words.Length; i++) //For every row
            {
                string compareWord = words[i][0]; //If the word exists, the untranslated version is in the first column.
                if (inText == compareWord)
                {
                    return words[i];
                }
            }
            return null;
        }

        /**
         * An overloaded version of getWord for dictionaries.
         */
        string getWord(string inText, Dictionary<string, string> words)
        {
            string word;

            if (words.TryGetValue(inText, out word))
            {
                return word;
            }
            else
            {
                return inText;
            }
        }

        /**
         * Returns the ending from the dictionary keys which ends the given string.
         * @param inText The text from which to extract the ending.
         * @param endingMap The dictionary of ending and their maps.
         * @returns The ending from the dictionary's keys which matched the inText ending, or "" if there was no match.
         */
        string getEnding(string inText, KeyCollection endingList)
        {
            string outputString = ""; //This will only remain as "" if it is not reassigned later (ie: if no endings match)

            foreach (string ending in endingList) //loops through each possible textē ending in the endingList and compares it to the actual ending.
            {
                if (compareEnding(inText, ending))
                {
                    outputString = ending;
                    //sets outputString equal to the ending which was matched.
                    break; //exits the foreach loop. If one ending matches, there's no point in checking all the others.
                }
            }

            return outputString;
        }

        /**
         * Compares if a given string is at the end of another given string
         * @param inText The main string
         * @param toMatch The ending to compare
         * @return Whether or not the ending was at the end of the main string
         */
        bool compareEnding(string inText, string toMatch)
        {
            string[] toMatchLetters = toMatch.Split(); //toMatchLetters splits the ending string between every character

            string inTextEnding = inText.Substring(inText.Length - toMatch.Length, toMatch.Length); 
            //sets inTextEnding to the end of inText. inTextEnding is now the same length as toMatch

            string[] letters = inTextEnding.Split(); //letters splits the ending of the provided string between every character

            bool areSame = true; //this will remain true only if it is not reassigned within the function

            for (int i = 0; i < letters.Length; i++)
            {
                string letter = letters[i];
                string toMatchLetter = toMatchLetters[i];
                //gets the two corresponding letters in the inputted string and the ending and compares them
                if (letter != toMatchLetter)
                {
                    areSame = false;
                    break; //exits the for loop. If it didn't match in one place, there's no point in checking all the others.
                }
            }

            return areSame;
        }

        /**
         * Reads from a file and formats it to an array.
         * @param filePath Where the file is stored. Should start "C:\\", etc.
         */
        void readFile(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(@filePath); //reads the file into an array which now contains each line

            loadedWords = new string[lines.Length][]; //Initializes loadedWords with the right number of lines

            for (int i = 0; i < lines.Length; i++) //For every line...
            {
                loadedWords[i] = new string[7];
                //Initializes the array with as many rows as needed and 7 columns: the textē word, part of speech, and five cases
                string line = lines[i];
                string[] segments = line.Split('.');
                for (int j = 0; j < segments.Length; j++) //...split it by its spacer and map it to an array.
                {
                    string segment = segments[j];
                    loadedWords[i][j] = segment;
                }
            }
        }

        /**
         * Prints the given 2D array to the console.
         */
        void print2DArray(string[][] inputArray)
        {
            for (int i = 0; i < inputArray.Length; i++) //For every row
            {
                Debug.WriteLine(""); //Start with a new line.

                for (int j = 0; j < inputArray[i].Length; j++) //And every column within that row (ie: every element)
                {
                    Debug.Write(inputArray[i][j] + " "); //Write every element, separated by a white space.
                }
            }
        }
    }
}

