﻿namespace Mir2Assistant.TabForms.Demo.TabForms
{
    partial class MonsterForm
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
            components = new System.ComponentModel.Container();
            button1 = new Button();
            listBox1 = new ListBox();
            bindingSource1 = new BindingSource(components);
            button2 = new Button();
            listBox2 = new ListBox();
            bindingSource2 = new BindingSource(components);
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(145, 421);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "砍怪";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(1, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(273, 412);
            listBox1.TabIndex = 1;
            listBox1.DrawItem += listBox1_DrawItem;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(385, 421);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "放技能";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // listBox2
            // 
            listBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 17;
            listBox2.Location = new Point(280, 3);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(201, 412);
            listBox2.TabIndex = 4;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // MonsterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 450);
            Controls.Add(listBox2);
            Controls.Add(button2);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Name = "MonsterForm";
            Text = "MonsterForm";
            Load += MonsterForm_Load;
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private ListBox listBox1;
        private BindingSource bindingSource1;
        private Button button2;
        private ListBox listBox2;
        private BindingSource bindingSource2;
        private System.Windows.Forms.Timer timer1;
    }
}