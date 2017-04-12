﻿using System.Collections.Generic;

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
            this.input.Location = new System.Drawing.Point(12, 12);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(260, 95);
            this.input.TabIndex = 0;
            // 
            // output
            // 
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Location = new System.Drawing.Point(12, 142);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(260, 107);
            this.output.TabIndex = 1;
            this.output.UseWaitCursor = true;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(105, 113);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "Translate";
            this.goButton.UseVisualStyleBackColor = true;
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.Button goButton;

        public string inText; //the inputted text

        private string[] nouns = { "plūīr", "лājs", "зtē", "welantωon", "złāя", "øo", "plī" }; //some sample nouns to play with

        private Dictionary<string, string> endingMap = new Dictionary<string, string>() {
            { "o", "Nominative" },
            { "λ", "Accusative" },
            { "on", "Possessive" },
            { "ol", "Genitive" },
            { "ot", "Locative" }
        }; //providing "o" returns "nominative", etc.
        //   at a certain point, this will return english endings instead of phrases like "accusative".

        /**
         * Called when goButton is clicked.
         */
        void buttonClick()
        {
            inText = this.input.Text;

            string outputString = inText; //This will only remain as the inputted text if it is not reassigned later (ie: if no endings match)

            foreach (string ending in endingMap.Keys) //loops through each possible textē ending in the endingMap and compares it to the actual ending.
            {
                if (compareEnding(inText, ending)) {
                    outputString = inText.Substring(0, inText.Length - ending.Length) + " + " + endingMap[ending]; 
                    //sets outputString equal to the part of the string not including the ending + the phrase which represents the ending which was matched.
                    break; //exits the foreach loop. If one ending matches, there's no point in checking all the others.
                }
            }

            this.output.Text = outputString;
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
    }
}

