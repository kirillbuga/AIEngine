namespace Visualization
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
            this.components = new System.ComponentModel.Container();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ant = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.isGridVisible = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 5;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(623, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Genetic";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.ant);
            this.tabPage2.Controls.Add(this.isGridVisible);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(772, 434);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Visualization";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ant
            // 
            this.ant.AccumBits = ((byte)(0));
            this.ant.AutoCheckErrors = false;
            this.ant.AutoFinish = false;
            this.ant.AutoMakeCurrent = true;
            this.ant.AutoSwapBuffers = true;
            this.ant.BackColor = System.Drawing.Color.Black;
            this.ant.ColorBits = ((byte)(32));
            this.ant.DepthBits = ((byte)(16));
            this.ant.Location = new System.Drawing.Point(0, 3);
            this.ant.Name = "ant";
            this.ant.Size = new System.Drawing.Size(608, 431);
            this.ant.StencilBits = ((byte)(0));
            this.ant.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(780, 460);
            this.tabControl1.TabIndex = 2;
            // 
            // isGridVisible
            // 
            this.isGridVisible.AutoSize = true;
            this.isGridVisible.Location = new System.Drawing.Point(623, 84);
            this.isGridVisible.Name = "isGridVisible";
            this.isGridVisible.Size = new System.Drawing.Size(73, 17);
            this.isGridVisible.TabIndex = 1;
            this.isGridVisible.Text = "Show grid";
            this.isGridVisible.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 513);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage2;
        private Tao.Platform.Windows.SimpleOpenGlControl ant;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox isGridVisible;
    }
}

