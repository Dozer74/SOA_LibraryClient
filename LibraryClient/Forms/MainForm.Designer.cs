namespace LibraryClient.Forms
{
    partial class MainForm
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
            if (disposing)
            {
                _client?.Dispose();
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
            this.btnAuthors = new System.Windows.Forms.Button();
            this.panelPlaceholder = new System.Windows.Forms.Panel();
            this.btnBooks = new System.Windows.Forms.Button();
            this.btnCreateBook = new System.Windows.Forms.Button();
            this.btnCreateAuthor = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAuthors
            // 
            this.btnAuthors.Location = new System.Drawing.Point(12, 12);
            this.btnAuthors.Name = "btnAuthors";
            this.btnAuthors.Size = new System.Drawing.Size(88, 31);
            this.btnAuthors.TabIndex = 0;
            this.btnAuthors.Text = "Авторы";
            this.btnAuthors.UseVisualStyleBackColor = true;
            this.btnAuthors.Click += new System.EventHandler(this.btnAuthors_Click);
            // 
            // panelPlaceholder
            // 
            this.panelPlaceholder.Location = new System.Drawing.Point(106, 12);
            this.panelPlaceholder.Name = "panelPlaceholder";
            this.panelPlaceholder.Size = new System.Drawing.Size(719, 403);
            this.panelPlaceholder.TabIndex = 100;
            // 
            // btnBooks
            // 
            this.btnBooks.Location = new System.Drawing.Point(12, 49);
            this.btnBooks.Name = "btnBooks";
            this.btnBooks.Size = new System.Drawing.Size(88, 31);
            this.btnBooks.TabIndex = 1;
            this.btnBooks.Text = "Книги";
            this.btnBooks.UseVisualStyleBackColor = true;
            this.btnBooks.Click += new System.EventHandler(this.btnBooks_Click);
            // 
            // btnCreateBook
            // 
            this.btnCreateBook.Location = new System.Drawing.Point(12, 365);
            this.btnCreateBook.Name = "btnCreateBook";
            this.btnCreateBook.Size = new System.Drawing.Size(88, 50);
            this.btnCreateBook.TabIndex = 4;
            this.btnCreateBook.Text = "Добавить книгу";
            this.btnCreateBook.UseVisualStyleBackColor = true;
            this.btnCreateBook.Click += new System.EventHandler(this.btnCreateBook_Click);
            // 
            // btnCreateAuthor
            // 
            this.btnCreateAuthor.Location = new System.Drawing.Point(12, 309);
            this.btnCreateAuthor.Name = "btnCreateAuthor";
            this.btnCreateAuthor.Size = new System.Drawing.Size(88, 50);
            this.btnCreateAuthor.TabIndex = 3;
            this.btnCreateAuthor.Text = "Добавить автора";
            this.btnCreateAuthor.UseVisualStyleBackColor = true;
            this.btnCreateAuthor.Click += new System.EventHandler(this.btnCreateAuthor_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(12, 86);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(88, 31);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "Обновить";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 427);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnCreateAuthor);
            this.Controls.Add(this.btnCreateBook);
            this.Controls.Add(this.panelPlaceholder);
            this.Controls.Add(this.btnBooks);
            this.Controls.Add(this.btnAuthors);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAuthors;
        private System.Windows.Forms.Panel panelPlaceholder;
        private System.Windows.Forms.Button btnBooks;
        private System.Windows.Forms.Button btnCreateBook;
        private System.Windows.Forms.Button btnCreateAuthor;
        private System.Windows.Forms.Button btnReload;
    }
}

