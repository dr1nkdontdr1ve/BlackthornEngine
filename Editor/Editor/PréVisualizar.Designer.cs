partial class PréVisualizar
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
            this.lstLista = new System.Windows.Forms.ListBox();
            this.butSelecionar = new System.Windows.Forms.Button();
            this.chkTransparente = new System.Windows.Forms.CheckBox();
            this.scrlImagemY = new System.Windows.Forms.VScrollBar();
            this.scrlImagemX = new System.Windows.Forms.HScrollBar();
            this.picImagem = new System.Windows.Forms.PictureBox();
            this.tmpRenderizar = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picImagem)).BeginInit();
            this.SuspendLayout();
            // 
            // lstLista
            // 
            this.lstLista.FormattingEnabled = true;
            this.lstLista.Location = new System.Drawing.Point(12, 12);
            this.lstLista.Name = "lstLista";
            this.lstLista.Size = new System.Drawing.Size(202, 342);
            this.lstLista.TabIndex = 5;
            this.lstLista.SelectedIndexChanged += new System.EventHandler(this.lstLista_SelectedIndexChanged);
            // 
            // butSelecionar
            // 
            this.butSelecionar.Location = new System.Drawing.Point(12, 355);
            this.butSelecionar.Name = "butSelecionar";
            this.butSelecionar.Size = new System.Drawing.Size(202, 25);
            this.butSelecionar.TabIndex = 21;
            this.butSelecionar.Text = "Selecionar";
            this.butSelecionar.UseVisualStyleBackColor = true;
            this.butSelecionar.Click += new System.EventHandler(this.butSelecionar_Click);
            // 
            // chkTransparente
            // 
            this.chkTransparente.AutoSize = true;
            this.chkTransparente.Checked = true;
            this.chkTransparente.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTransparente.Location = new System.Drawing.Point(220, 12);
            this.chkTransparente.Name = "chkTransparente";
            this.chkTransparente.Size = new System.Drawing.Size(124, 17);
            this.chkTransparente.TabIndex = 22;
            this.chkTransparente.Text = "Fundo transparente?";
            this.chkTransparente.UseVisualStyleBackColor = true;
            // 
            // scrlImagemY
            // 
            this.scrlImagemY.Location = new System.Drawing.Point(546, 32);
            this.scrlImagemY.Name = "scrlImagemY";
            this.scrlImagemY.Size = new System.Drawing.Size(19, 328);
            this.scrlImagemY.TabIndex = 29;
            // 
            // scrlImagemX
            // 
            this.scrlImagemX.Location = new System.Drawing.Point(220, 361);
            this.scrlImagemX.Name = "scrlImagemX";
            this.scrlImagemX.Size = new System.Drawing.Size(325, 19);
            this.scrlImagemX.TabIndex = 28;
            // 
            // picImagem
            // 
            this.picImagem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picImagem.Location = new System.Drawing.Point(220, 35);
            this.picImagem.Name = "picImagem";
            this.picImagem.Size = new System.Drawing.Size(325, 325);
            this.picImagem.TabIndex = 27;
            this.picImagem.TabStop = false;
            // 
            // tmpRenderizar
            // 
            this.tmpRenderizar.Enabled = true;
            this.tmpRenderizar.Interval = 1;
            this.tmpRenderizar.Tick += new System.EventHandler(this.tmpRenderizar_Tick);
            // 
            // PréVisualizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 386);
            this.Controls.Add(this.scrlImagemY);
            this.Controls.Add(this.scrlImagemX);
            this.Controls.Add(this.picImagem);
            this.Controls.Add(this.chkTransparente);
            this.Controls.Add(this.butSelecionar);
            this.Controls.Add(this.lstLista);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PréVisualizar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PréVisualizar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PréVisualizar_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picImagem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.ListBox lstLista;
    private System.Windows.Forms.Button butSelecionar;
    public System.Windows.Forms.VScrollBar scrlImagemY;
    public System.Windows.Forms.HScrollBar scrlImagemX;
    public System.Windows.Forms.PictureBox picImagem;
    public System.Windows.Forms.CheckBox chkTransparente;
    private System.Windows.Forms.Timer tmpRenderizar;
}