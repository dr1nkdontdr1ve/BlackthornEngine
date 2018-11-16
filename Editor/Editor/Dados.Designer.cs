partial class Editor_Dados
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numPorta = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGame_Nome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numMáx_Personagens = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numMáx_Jogadores = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.butCancelar = new System.Windows.Forms.Button();
            this.butSalvar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPorta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMáx_Personagens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMáx_Jogadores)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMensagem);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numPorta);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtGame_Nome);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numMáx_Personagens);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numMáx_Jogadores);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 196);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Básicos";
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(9, 80);
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(246, 20);
            this.txtMensagem.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Mensagem Inicial";
            // 
            // numPorta
            // 
            this.numPorta.Location = new System.Drawing.Point(9, 119);
            this.numPorta.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPorta.Name = "numPorta";
            this.numPorta.Size = new System.Drawing.Size(246, 20);
            this.numPorta.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Porta:";
            // 
            // txtGame_Nome
            // 
            this.txtGame_Nome.Location = new System.Drawing.Point(9, 41);
            this.txtGame_Nome.Name = "txtGame_Nome";
            this.txtGame_Nome.Size = new System.Drawing.Size(246, 20);
            this.txtGame_Nome.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nome do Game:";
            // 
            // numMáx_Personagens
            // 
            this.numMáx_Personagens.Location = new System.Drawing.Point(135, 161);
            this.numMáx_Personagens.Name = "numMáx_Personagens";
            this.numMáx_Personagens.Size = new System.Drawing.Size(120, 20);
            this.numMáx_Personagens.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Máximo de Personagens:";
            // 
            // numMáx_Jogadores
            // 
            this.numMáx_Jogadores.Location = new System.Drawing.Point(9, 161);
            this.numMáx_Jogadores.Name = "numMáx_Jogadores";
            this.numMáx_Jogadores.Size = new System.Drawing.Size(120, 20);
            this.numMáx_Jogadores.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Máximo de Jogadores:";
            // 
            // butCancelar
            // 
            this.butCancelar.Location = new System.Drawing.Point(150, 214);
            this.butCancelar.Name = "butCancelar";
            this.butCancelar.Size = new System.Drawing.Size(125, 25);
            this.butCancelar.TabIndex = 20;
            this.butCancelar.Text = "Cancelar";
            this.butCancelar.UseVisualStyleBackColor = true;
            this.butCancelar.Click += new System.EventHandler(this.butCancelar_Click);
            // 
            // butSalvar
            // 
            this.butSalvar.Location = new System.Drawing.Point(12, 214);
            this.butSalvar.Name = "butSalvar";
            this.butSalvar.Size = new System.Drawing.Size(129, 25);
            this.butSalvar.TabIndex = 19;
            this.butSalvar.Text = "Salvar";
            this.butSalvar.UseVisualStyleBackColor = true;
            this.butSalvar.Click += new System.EventHandler(this.butSalvar_Click);
            // 
            // Editor_Dados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 250);
            this.ControlBox = false;
            this.Controls.Add(this.butCancelar);
            this.Controls.Add(this.butSalvar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Editor_Dados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor de Dados";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPorta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMáx_Personagens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMáx_Jogadores)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button butCancelar;
    private System.Windows.Forms.Button butSalvar;
    public System.Windows.Forms.NumericUpDown numMáx_Jogadores;
    public System.Windows.Forms.NumericUpDown numMáx_Personagens;
    public System.Windows.Forms.TextBox txtGame_Nome;
    public System.Windows.Forms.NumericUpDown numPorta;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    public System.Windows.Forms.TextBox txtMensagem;
}