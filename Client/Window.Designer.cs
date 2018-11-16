partial class Janela
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
            this.SuspendLayout();
            // 
            // Janela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 608);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Janela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Janela";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Janela_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Janela_Paint);
            this.DoubleClick += new System.EventHandler(this.Janela_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Janela_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Janela_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Janela_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Janela_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Janela_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Janela_MouseUp);
            this.ResumeLayout(false);

    }

    #endregion
}