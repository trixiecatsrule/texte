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
            this.input.Location = new System.Drawing.Point(25, 33);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(100, 20);
            this.input.TabIndex = 0;
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(25, 142);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(100, 20);
            this.output.TabIndex = 1;
            this.output.TextChanged += new System.EventHandler(this.output_TextChanged);
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(147, 88);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "Submit";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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

        public string inText;

        private string[] nouns = { "plūīr", "лājs", "зtē", "welantωon", "złāя", "øo", "plī" };
        private string[] endings = { "o", "λ", "on", "ol", "ot"};

        void buttonClick()
        {
            inText = this.input.Text;

            string areSame = "No";

            foreach (string ending in endings)
            {
                if (compareEnding(inText, ending)) { areSame = ending; }
            }

            this.output.Text = areSame;
        }

        bool compareEnding(string inText, string toMatch)
        {
            string[] toMatchLetters = toMatch.Split();

            string it = inText.Substring(inText.Length - toMatch.Length, toMatch.Length);

            this.output.Text = it;

            string[] words = it.Split(' ');
            string[] letters = it.Split();

            bool areSame = true;

            for (int i = letters.Length - 1; i >= 0; i--)
            {
                string letter = letters[i];
                string toMatchLetter = toMatchLetters[i];
                if (letter != toMatchLetter)
                {
                    areSame = false;
                }
            }

            return areSame;
        }
    }
}

