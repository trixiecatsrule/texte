using System.Collections;
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
            this.loadButton = new System.Windows.Forms.Button();
            this.loadInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // input
            // 
            this.input.Enabled = false;
            this.input.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.output.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(197, 258);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 20);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // loadInput
            // 
            this.loadInput.Location = new System.Drawing.Point(12, 258);
            this.loadInput.Name = "loadInput";
            this.loadInput.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.loadInput.Size = new System.Drawing.Size(179, 20);
            this.loadInput.TabIndex = 4;
            this.loadInput.Text = "BasicDictionary.txt";
            // 
            // Form1
            // 
            this.AcceptButton = this.goButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 294);
            this.Controls.Add(this.loadInput);
            this.Controls.Add(this.loadButton);
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
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.TextBox loadInput;

        private Dictionary<string, string> sampleWords = new Dictionary<string, string>() {
            { "plūīr", "tree" },
            { "лājs", "house" },
            { "зtē", "symbol" },
            { "welantωon", "truck" },
            { "złāя", "manner" },
            { "øo", "small" },
            { "plī", "big" }
        }; //some sample words to play with

        private DictionaryEntry[] loadedWords;

        private Dictionary<string, int> sampleEndingMap = new Dictionary<string, int>() {
            { "o", 0 },
            { "λ", 1 },
            { "on", 2 },
            { "ol", 3 },
            { "ot", 4 }
        }; //providing "o" returns " + nominative", etc.
           //   at a certain point, this will return english endings instead of phrases like "accusative".

        private Dictionary<string, int> sampleBeginningMap = new Dictionary<string, int>() {
            { "agno", 0 },
            { "egā", 1 },
            { "āme", 2 },
            { "o", 3 },
            { "жo", 4 }
        };

        private Dictionary<string, string> sampleCaseMap = new Dictionary<string, string>() {
            { "o", "+Nominative" },
            { "λ", "+Accusative" },
            { "on", "+Possessive" },
            { "ol", "+Genitive" },
            { "ot", "+Locative" }
        }; //A case map which will be used if the ending is recognized but the word isn't.

        /*
         * Runs when the form loads or when the load button is clicked.
         */
        void loadDictionary()
        {
            input.ReadOnly = true;
            input.Enabled = false;

            //readFile("C:\\Users\\Hazel\\Downloads\\Texte\\TexteTranslator\\TexteTranslator\\Assets\\Dictionary.txt");
            readFile("..\\..\\Assets\\" + this.loadInput.Text);
            print2DArray(loadedWords);

            //Only after the dictionary has loaded do we allow inputs.
            input.ReadOnly = false;
            input.Enabled = true;
        }

        /*
         * Called when goButton is clicked or enter is pressed on the form.
         */
        void buttonClick()
        {
            string inputText = this.input.Text;

            this.output.Text = ""; //Blank the output box.

            if (inputText != "")
            {
                string[] inputWords = inputText.Split(' ');

                foreach (string inputWord in inputWords) //For each word
                {
                    ArrayList possibleWords = getPossibleWords(inputWord, loadedWords);

                    this.output.Text += "[";

                    foreach (string possibleWord in possibleWords) //For each possible base word
                    {
                        string prefixString;
                        string suffixString;
                        getPrefixAndSuffixStrings(inputWord, possibleWord, out prefixString, out suffixString);
                        this.output.Text += " " + prefixString + ":" + possibleWord + ":" + suffixString;

                        //Check prefixes
                        ArrayList prefixOutput;
                        string prefixLeftover = getEndings(true, prefixString, sampleBeginningMap.Keys, out prefixOutput);

                        //Print prefixes
                        Debug.WriteLine("");
                        Debug.Write("Leftover: " + prefixLeftover + " Matched Beginnings: ");
                        foreach (string beginning in prefixOutput)
                        {
                            Debug.Write(beginning + " ");
                        }

                        //Check suffixes
                        ArrayList suffixOutput;
                        string suffixLeftover = getEndings(false, suffixString, sampleEndingMap.Keys, out suffixOutput);

                        //Print suffixes
                        Debug.WriteLine("");
                        Debug.Write("Leftover: " + suffixLeftover + " Matched Endings: ");
                        foreach (string ending in suffixOutput)
                        {
                            Debug.Write(ending + " ");
                        }
                    }

                    this.output.Text += " ]";
                }

                this.output.Text += "\r\n\r\n";
            }
        }

        /*
         * Takes a complete word and a possible base word, and returns two strings, removing the base word to get the two sides.
         * @param completeWord The word in its entirety
         * @param baseWord The word to be removed
         * @param[out] prefixString The first half of completeWord, with baseWord removed
         * @param[out] suffixString The second half
         */
         void getPrefixAndSuffixStrings(string completeWord, string baseWord, out string prefixString, out string suffixString)
        {
            int baseLoc = completeWord.IndexOf(baseWord);

            prefixString = completeWord.Substring(0, baseLoc); //From position 0, of length baseLoc = everything before baseLoc
            suffixString = completeWord.Substring(baseLoc + baseWord.Length); //From the end of baseWord, until the end of the string
        }

        /*
         * Takes a string in textē and a set of words to match and returns any word matches in textē.
         * @param inText The text from which to extract words
         * @param words The words to potentially match
         * @return A list of the words which were matched
         */
        ArrayList getPossibleWords(string inText, DictionaryEntry[] dictionary)
        {
            ArrayList matches = new ArrayList();

            for (int i = 0; i < inText.Length; i++) //loops and checks all possible subdivisions of the word with the dictionary
            {
                string checkString = inText.Substring(i); 
                //truncates progressively larger numbers of letters off end of checkString - removes a letter from the front
                int repeat = checkString.Length;

                for (int n = 0; n < repeat; n++)
                {
                    for (int j = 0; j < dictionary.Length; j++)
                    {
                        string vocabWord = dictionary[j].getBase();
                        if (checkString == vocabWord)
                        {
                            matches.Add(vocabWord);
                        }
                    }

                    checkString = checkString.Substring(0, checkString.Length - 1); //removes a letter from the back
                }
            }

            return matches;
        }

        /*
         * If the inputted word is one of those in the dictionary, returns the word. Else, returns the inputted word.
         * @param inText The word to check against the dictionary
         * @param words The dictionary of words (2D array)
         * @returns An array of possible conjugations, or null.
         */
        DictionaryEntry getWord(string inText, DictionaryEntry[] words)
        {
            for (int i = 0; i < words.Length; i++) //For every row
            {
                string compareWord = words[i].getBase(); //If the word exists, the untranslated version is the base.
                if (inText == compareWord)
                {
                    return words[i];
                }
            }
            return null;
        }

        /*
         * An overloaded version of getWord for dictionaries. OUT OF DATE
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

        /*
         * Returns the endings from the dictionary keys which ends the given string.
         * @param isPrefixes True for prefix checks, false for suffix checks
         * @param inText The text from which to extract the ending.
         * @param endingList A list of the dictionary of ending's keys.
         * @param[out] output The endings from the dictionary's keys which matched the inText ending, or "" if there was no match.
         * @return If all the endings were matched, an empty string. Else, the bit which didn't work.
         */
        string getEndings(bool isPrefixes, string inText, KeyCollection endingList, out ArrayList output)
        {
            ArrayList outputStrings = new ArrayList();

            string text = inText;

            bool matchedEnding = true;

            while (text.Length > 0 && matchedEnding == true)
            {
                matchedEnding = false;
                foreach (string ending in endingList) //loops through each possible textē ending in the endingList and compares it to the actual ending.
                {
                    if (compareEnding(isPrefixes, text, ending))
                    {
                        outputStrings.Add(ending);

                        if (isPrefixes)
                        {
                            text = text.Substring(ending.Length); //Make text itself, minus the beginning
                        }
                        else
                        {
                            text = text.Substring(0, text.Length - ending.Length); //Make text itself, minus the ending
                        }

                        matchedEnding = true;
                        break; //exits the foreach loop. If one ending matches at this position, there's no point in checking all the others.
                    }
                }
                //MatchedEnding will be false if no ending was matched (ie: the string's ending isn't a suffix)
            }

            output =  outputStrings;

            return text;
        }

        /*
         * Compares if a given string is at the end of another given string
         * @param isPrefixes True for prefix checks, false for suffix checks
         * @param inText The main string
         * @param toMatch The ending to compare
         * @return Whether or not the ending was at the end of the main string
         */
        bool compareEnding(bool isPrefixes, string inText, string toMatch)
        {
            if (inText.Length - toMatch.Length < 0)
            {
                return false; //A string of length 2 cannot, by definition, be the end a string of length 1
            }

            string[] toMatchLetters = toMatch.Split(); //toMatchLetters splits the ending string between every character

            string inTextEnding;

            if (isPrefixes)
            {
                inTextEnding = inText.Substring(0, toMatch.Length);
                //sets inTextEnding to the START of inText. It is now the same length as toMatch
            }
            else
            {
                inTextEnding = inText.Substring(inText.Length - toMatch.Length, toMatch.Length);
                //sets inTextEnding to the END of inText. inTextEnding is now the same length as toMatch
            }

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

        /*
         * Reads from a file and formats it to an array.
         * @param filePath Where the file is stored. Should start "C:\\", etc.
         */
        void readFile(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(@filePath); //reads the file into an array which now contains each line

            loadedWords = new DictionaryEntry[lines.Length]; //Initializes loadedWords with the right number of lines

            for (int i = 0; i < lines.Length; i++) //For every line...
            {
                //Initializes the array with as many rows as needed and 7 columns: the textē word, part of speech, and five cases
                string line = lines[i];
                string[] segments = line.Split('.');
                loadedWords[i] = new DictionaryEntry(segments);
            }
        }

        /*
         * Prints the given 2D array to the console.
         */
        void print2DArray(DictionaryEntry[] inputArray)
        {
            for (int i = 0; i < inputArray.Length; i++) //For every row
            {
                Debug.WriteLine(inputArray[i].ToString());
            }
        }
    }
}

