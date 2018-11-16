partial class Editor_Ferramentas
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
            this.panBotão = new System.Windows.Forms.GroupBox();
            this.butBotão_Textura = new System.Windows.Forms.Button();
            this.lblBotão_Textura = new System.Windows.Forms.Label();
            this.lstLista = new System.Windows.Forms.ListBox();
            this.cmbFerramentas = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkVisível = new System.Windows.Forms.CheckBox();
            this.panDigitalizador = new System.Windows.Forms.GroupBox();
            this.chkDigitalizador_Senha = new System.Windows.Forms.CheckBox();
            this.scrlDigitalizador_Largura = new System.Windows.Forms.HScrollBar();
            this.lblDigitalizador_Largura = new System.Windows.Forms.Label();
            this.scrlDigitalizador_MáxCaracteres = new System.Windows.Forms.HScrollBar();
            this.lblDigitalizador_MáxCaracteres = new System.Windows.Forms.Label();
            this.panPainel = new System.Windows.Forms.GroupBox();
            this.butPainel_Textura = new System.Windows.Forms.Button();
            this.lblPainel_Textura = new System.Windows.Forms.Label();
            this.panMarcador = new System.Windows.Forms.GroupBox();
            this.txtMarcador_Texto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butQuantidade = new System.Windows.Forms.Button();
            this.butLimpar = new System.Windows.Forms.Button();
            this.butCancelar = new System.Windows.Forms.Button();
            this.butSalvar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.panBotão.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panDigitalizador.SuspendLayout();
            this.panPainel.SuspendLayout();
            this.panMarcador.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            this.SuspendLayout();
            // 
            // panBotão
            // 
            this.panBotão.Controls.Add(this.butBotão_Textura);
            this.panBotão.Controls.Add(this.lblBotão_Textura);
            this.panBotão.Location = new System.Drawing.Point(220, 174);
            this.panBotão.Name = "panBotão";
            this.panBotão.Size = new System.Drawing.Size(306, 70);
            this.panBotão.TabIndex = 3;
            this.panBotão.TabStop = false;
            this.panBotão.Text = "Botão";
            // 
            // butBotão_Textura
            // 
            this.butBotão_Textura.Location = new System.Drawing.Point(16, 34);
            this.butBotão_Textura.Name = "butBotão_Textura";
            this.butBotão_Textura.Size = new System.Drawing.Size(273, 20);
            this.butBotão_Textura.TabIndex = 27;
            this.butBotão_Textura.Text = "Selecionar";
            this.butBotão_Textura.UseVisualStyleBackColor = true;
            this.butBotão_Textura.Click += new System.EventHandler(this.butBotão_Textura_Click);
            // 
            // lblBotão_Textura
            // 
            this.lblBotão_Textura.AutoSize = true;
            this.lblBotão_Textura.Location = new System.Drawing.Point(13, 18);
            this.lblBotão_Textura.Name = "lblBotão_Textura";
            this.lblBotão_Textura.Size = new System.Drawing.Size(55, 13);
            this.lblBotão_Textura.TabIndex = 5;
            this.lblBotão_Textura.Text = "Textura: 0";
            // 
            // lstLista
            // 
            this.lstLista.FormattingEnabled = true;
            this.lstLista.Location = new System.Drawing.Point(12, 43);
            this.lstLista.Name = "lstLista";
            this.lstLista.Size = new System.Drawing.Size(202, 212);
            this.lstLista.TabIndex = 4;
            this.lstLista.SelectedIndexChanged += new System.EventHandler(this.lstLista_SelectedIndexChanged);
            // 
            // cmbFerramentas
            // 
            this.cmbFerramentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFerramentas.Location = new System.Drawing.Point(12, 12);
            this.cmbFerramentas.Name = "cmbFerramentas";
            this.cmbFerramentas.Size = new System.Drawing.Size(202, 21);
            this.cmbFerramentas.TabIndex = 9;
            this.cmbFerramentas.SelectedIndexChanged += new System.EventHandler(this.cmbFerramentas_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNome);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chkVisível);
            this.groupBox2.Location = new System.Drawing.Point(220, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 91);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Geral";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(16, 42);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(273, 20);
            this.txtNome.TabIndex = 8;
            this.txtNome.Validated += new System.EventHandler(this.txtNome_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nome:";
            // 
            // chkVisível
            // 
            this.chkVisível.AutoSize = true;
            this.chkVisível.Location = new System.Drawing.Point(16, 68);
            this.chkVisível.Name = "chkVisível";
            this.chkVisível.Size = new System.Drawing.Size(64, 17);
            this.chkVisível.TabIndex = 5;
            this.chkVisível.Text = "Visível?";
            this.chkVisível.UseVisualStyleBackColor = true;
            this.chkVisível.CheckedChanged += new System.EventHandler(this.chkVisível_CheckedChanged);
            // 
            // panDigitalizador
            // 
            this.panDigitalizador.Controls.Add(this.chkDigitalizador_Senha);
            this.panDigitalizador.Controls.Add(this.scrlDigitalizador_Largura);
            this.panDigitalizador.Controls.Add(this.lblDigitalizador_Largura);
            this.panDigitalizador.Controls.Add(this.scrlDigitalizador_MáxCaracteres);
            this.panDigitalizador.Controls.Add(this.lblDigitalizador_MáxCaracteres);
            this.panDigitalizador.Location = new System.Drawing.Point(220, 174);
            this.panDigitalizador.Name = "panDigitalizador";
            this.panDigitalizador.Size = new System.Drawing.Size(306, 82);
            this.panDigitalizador.TabIndex = 11;
            this.panDigitalizador.TabStop = false;
            this.panDigitalizador.Text = "Digitalizador";
            // 
            // chkDigitalizador_Senha
            // 
            this.chkDigitalizador_Senha.AutoSize = true;
            this.chkDigitalizador_Senha.Location = new System.Drawing.Point(9, 60);
            this.chkDigitalizador_Senha.Name = "chkDigitalizador_Senha";
            this.chkDigitalizador_Senha.Size = new System.Drawing.Size(63, 17);
            this.chkDigitalizador_Senha.TabIndex = 8;
            this.chkDigitalizador_Senha.Text = "Senha?";
            this.chkDigitalizador_Senha.UseVisualStyleBackColor = true;
            this.chkDigitalizador_Senha.CheckedChanged += new System.EventHandler(this.chkDigitalizador_Senha_CheckedChanged);
            // 
            // scrlDigitalizador_Largura
            // 
            this.scrlDigitalizador_Largura.Location = new System.Drawing.Point(152, 37);
            this.scrlDigitalizador_Largura.Maximum = 500;
            this.scrlDigitalizador_Largura.Name = "scrlDigitalizador_Largura";
            this.scrlDigitalizador_Largura.Size = new System.Drawing.Size(137, 20);
            this.scrlDigitalizador_Largura.TabIndex = 7;
            this.scrlDigitalizador_Largura.ValueChanged += new System.EventHandler(this.scrlDigitalizador_Largura_ValueChanged);
            // 
            // lblDigitalizador_Largura
            // 
            this.lblDigitalizador_Largura.AutoSize = true;
            this.lblDigitalizador_Largura.Location = new System.Drawing.Point(149, 18);
            this.lblDigitalizador_Largura.Name = "lblDigitalizador_Largura";
            this.lblDigitalizador_Largura.Size = new System.Drawing.Size(55, 13);
            this.lblDigitalizador_Largura.TabIndex = 6;
            this.lblDigitalizador_Largura.Text = "Largura: 0";
            // 
            // scrlDigitalizador_MáxCaracteres
            // 
            this.scrlDigitalizador_MáxCaracteres.Location = new System.Drawing.Point(7, 37);
            this.scrlDigitalizador_MáxCaracteres.Name = "scrlDigitalizador_MáxCaracteres";
            this.scrlDigitalizador_MáxCaracteres.Size = new System.Drawing.Size(137, 20);
            this.scrlDigitalizador_MáxCaracteres.TabIndex = 5;
            this.scrlDigitalizador_MáxCaracteres.ValueChanged += new System.EventHandler(this.scrlDigitalizador_MáxCaracteres_ValueChanged);
            // 
            // lblDigitalizador_MáxCaracteres
            // 
            this.lblDigitalizador_MáxCaracteres.AutoSize = true;
            this.lblDigitalizador_MáxCaracteres.Location = new System.Drawing.Point(4, 18);
            this.lblDigitalizador_MáxCaracteres.Name = "lblDigitalizador_MáxCaracteres";
            this.lblDigitalizador_MáxCaracteres.Size = new System.Drawing.Size(140, 13);
            this.lblDigitalizador_MáxCaracteres.TabIndex = 4;
            this.lblDigitalizador_MáxCaracteres.Text = "Máx. de caracteres: Infinitos";
            // 
            // panPainel
            // 
            this.panPainel.Controls.Add(this.butPainel_Textura);
            this.panPainel.Controls.Add(this.lblPainel_Textura);
            this.panPainel.Location = new System.Drawing.Point(220, 174);
            this.panPainel.Name = "panPainel";
            this.panPainel.Size = new System.Drawing.Size(306, 71);
            this.panPainel.TabIndex = 12;
            this.panPainel.TabStop = false;
            this.panPainel.Text = "Painel";
            // 
            // butPainel_Textura
            // 
            this.butPainel_Textura.Location = new System.Drawing.Point(16, 37);
            this.butPainel_Textura.Name = "butPainel_Textura";
            this.butPainel_Textura.Size = new System.Drawing.Size(273, 20);
            this.butPainel_Textura.TabIndex = 26;
            this.butPainel_Textura.Text = "Selecionar";
            this.butPainel_Textura.UseVisualStyleBackColor = true;
            this.butPainel_Textura.Click += new System.EventHandler(this.butPainel_Textura_Click);
            // 
            // lblPainel_Textura
            // 
            this.lblPainel_Textura.AutoSize = true;
            this.lblPainel_Textura.Location = new System.Drawing.Point(13, 18);
            this.lblPainel_Textura.Name = "lblPainel_Textura";
            this.lblPainel_Textura.Size = new System.Drawing.Size(55, 13);
            this.lblPainel_Textura.TabIndex = 2;
            this.lblPainel_Textura.Text = "Textura: 0";
            // 
            // panMarcador
            // 
            this.panMarcador.Controls.Add(this.txtMarcador_Texto);
            this.panMarcador.Controls.Add(this.label1);
            this.panMarcador.Location = new System.Drawing.Point(220, 174);
            this.panMarcador.Name = "panMarcador";
            this.panMarcador.Size = new System.Drawing.Size(306, 70);
            this.panMarcador.TabIndex = 13;
            this.panMarcador.TabStop = false;
            this.panMarcador.Text = "Marcador";
            // 
            // txtMarcador_Texto
            // 
            this.txtMarcador_Texto.Location = new System.Drawing.Point(9, 34);
            this.txtMarcador_Texto.Name = "txtMarcador_Texto";
            this.txtMarcador_Texto.Size = new System.Drawing.Size(284, 20);
            this.txtMarcador_Texto.TabIndex = 9;
            this.txtMarcador_Texto.Validated += new System.EventHandler(this.txtMarcador_Texto_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Texto:";
            // 
            // butQuantidade
            // 
            this.butQuantidade.Location = new System.Drawing.Point(12, 262);
            this.butQuantidade.Name = "butQuantidade";
            this.butQuantidade.Size = new System.Drawing.Size(202, 25);
            this.butQuantidade.TabIndex = 19;
            this.butQuantidade.Text = "Alterar Quantidade";
            this.butQuantidade.UseVisualStyleBackColor = true;
            this.butQuantidade.Click += new System.EventHandler(this.butQuantidade_Click);
            // 
            // butLimpar
            // 
            this.butLimpar.Location = new System.Drawing.Point(323, 262);
            this.butLimpar.Name = "butLimpar";
            this.butLimpar.Size = new System.Drawing.Size(97, 25);
            this.butLimpar.TabIndex = 22;
            this.butLimpar.Text = "Limpar";
            this.butLimpar.UseVisualStyleBackColor = true;
            this.butLimpar.Click += new System.EventHandler(this.butLimpar_Click);
            // 
            // butCancelar
            // 
            this.butCancelar.Location = new System.Drawing.Point(428, 262);
            this.butCancelar.Name = "butCancelar";
            this.butCancelar.Size = new System.Drawing.Size(97, 25);
            this.butCancelar.TabIndex = 21;
            this.butCancelar.Text = "Cancelar";
            this.butCancelar.UseVisualStyleBackColor = true;
            this.butCancelar.Click += new System.EventHandler(this.butCancelar_Click);
            // 
            // butSalvar
            // 
            this.butSalvar.Location = new System.Drawing.Point(220, 262);
            this.butSalvar.Name = "butSalvar";
            this.butSalvar.Size = new System.Drawing.Size(97, 25);
            this.butSalvar.TabIndex = 20;
            this.butSalvar.Text = "Salvar";
            this.butSalvar.UseVisualStyleBackColor = true;
            this.butSalvar.Click += new System.EventHandler(this.butSalvar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numY);
            this.groupBox1.Controls.Add(this.numX);
            this.groupBox1.Controls.Add(this.lblY);
            this.groupBox1.Controls.Add(this.lblX);
            this.groupBox1.Location = new System.Drawing.Point(220, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 59);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Localização";
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(181, 26);
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(108, 20);
            this.numY.TabIndex = 8;
            this.numY.ValueChanged += new System.EventHandler(this.numY_ValueChanged);
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(36, 28);
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(108, 20);
            this.numX.TabIndex = 7;
            this.numX.ValueChanged += new System.EventHandler(this.numX_ValueChanged);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(158, 28);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(17, 13);
            this.lblY.TabIndex = 4;
            this.lblY.Text = "Y:";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(13, 28);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(17, 13);
            this.lblX.TabIndex = 3;
            this.lblX.Text = "X:";
            // 
            // Editor_Ferramentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 299);
            this.ControlBox = false;
            this.Controls.Add(this.panBotão);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butQuantidade);
            this.Controls.Add(this.panPainel);
            this.Controls.Add(this.panMarcador);
            this.Controls.Add(this.butLimpar);
            this.Controls.Add(this.butCancelar);
            this.Controls.Add(this.butSalvar);
            this.Controls.Add(this.panDigitalizador);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmbFerramentas);
            this.Controls.Add(this.lstLista);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Editor_Ferramentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor de Ferramentas";
            this.panBotão.ResumeLayout(false);
            this.panBotão.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panDigitalizador.ResumeLayout(false);
            this.panDigitalizador.PerformLayout();
            this.panPainel.ResumeLayout(false);
            this.panPainel.PerformLayout();
            this.panMarcador.ResumeLayout(false);
            this.panMarcador.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion
    public System.Windows.Forms.ListBox lstLista;
    public System.Windows.Forms.ComboBox cmbFerramentas;
    private System.Windows.Forms.GroupBox groupBox2;
    public System.Windows.Forms.CheckBox chkVisível;
    public System.Windows.Forms.TextBox txtNome;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.CheckBox chkDigitalizador_Senha;
    public System.Windows.Forms.HScrollBar scrlDigitalizador_Largura;
    private System.Windows.Forms.Label lblDigitalizador_Largura;
    public System.Windows.Forms.HScrollBar scrlDigitalizador_MáxCaracteres;
    private System.Windows.Forms.Label lblDigitalizador_MáxCaracteres;
    public System.Windows.Forms.GroupBox panDigitalizador;
    public System.Windows.Forms.GroupBox panBotão;
    public System.Windows.Forms.GroupBox panPainel;
    private System.Windows.Forms.Label lblPainel_Textura;
    public System.Windows.Forms.GroupBox panMarcador;
    public System.Windows.Forms.TextBox txtMarcador_Texto;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button butQuantidade;
    private System.Windows.Forms.Button butLimpar;
    private System.Windows.Forms.Button butCancelar;
    private System.Windows.Forms.Button butSalvar;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label lblY;
    private System.Windows.Forms.Label lblX;
    public System.Windows.Forms.NumericUpDown numY;
    public System.Windows.Forms.NumericUpDown numX;
    private System.Windows.Forms.Label lblBotão_Textura;
    private System.Windows.Forms.Button butPainel_Textura;
    private System.Windows.Forms.Button butBotão_Textura;
}