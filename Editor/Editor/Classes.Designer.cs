partial class Editor_Classes
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
            this.lstLista = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butSalvar = new System.Windows.Forms.Button();
            this.butCancelar = new System.Windows.Forms.Button();
            this.butLimpar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numAgilidade = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numInteligência = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numResistência = new System.Windows.Forms.NumericUpDown();
            this.numForça = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numMana = new System.Windows.Forms.NumericUpDown();
            this.numVida = new System.Windows.Forms.NumericUpDown();
            this.lblMana = new System.Windows.Forms.Label();
            this.lblVida = new System.Windows.Forms.Label();
            this.grpTextura = new System.Windows.Forms.GroupBox();
            this.lblFTextura = new System.Windows.Forms.Label();
            this.butFTextura = new System.Windows.Forms.Button();
            this.lblMTextura = new System.Windows.Forms.Label();
            this.butMTextura = new System.Windows.Forms.Button();
            this.butQuantidade = new System.Windows.Forms.Button();
            this.numVitalidade = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numAparecer_Mapa = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numAparecer_X = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numAparecer_Y = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbAparecer_Direção = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAgilidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInteligência)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numResistência)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForça)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVida)).BeginInit();
            this.grpTextura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVitalidade)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAparecer_Mapa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAparecer_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAparecer_Y)).BeginInit();
            this.SuspendLayout();
            // 
            // lstLista
            // 
            this.lstLista.FormattingEnabled = true;
            this.lstLista.Location = new System.Drawing.Point(11, 12);
            this.lstLista.Name = "lstLista";
            this.lstLista.Size = new System.Drawing.Size(202, 446);
            this.lstLista.TabIndex = 9;
            this.lstLista.SelectedIndexChanged += new System.EventHandler(this.lstLista_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(219, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 70);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geral";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(15, 37);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(273, 20);
            this.txtNome.TabIndex = 10;
            this.txtNome.Validated += new System.EventHandler(this.txtNome_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nome:";
            // 
            // butSalvar
            // 
            this.butSalvar.Location = new System.Drawing.Point(219, 467);
            this.butSalvar.Name = "butSalvar";
            this.butSalvar.Size = new System.Drawing.Size(97, 25);
            this.butSalvar.TabIndex = 16;
            this.butSalvar.Text = "Salvar";
            this.butSalvar.UseVisualStyleBackColor = true;
            this.butSalvar.Click += new System.EventHandler(this.butSalvar_Click);
            // 
            // butCancelar
            // 
            this.butCancelar.Location = new System.Drawing.Point(426, 467);
            this.butCancelar.Name = "butCancelar";
            this.butCancelar.Size = new System.Drawing.Size(97, 25);
            this.butCancelar.TabIndex = 17;
            this.butCancelar.Text = "Cancelar";
            this.butCancelar.UseVisualStyleBackColor = true;
            this.butCancelar.Click += new System.EventHandler(this.butCancelar_Click);
            // 
            // butLimpar
            // 
            this.butLimpar.Location = new System.Drawing.Point(323, 467);
            this.butLimpar.Name = "butLimpar";
            this.butLimpar.Size = new System.Drawing.Size(97, 25);
            this.butLimpar.TabIndex = 18;
            this.butLimpar.Text = "Limpar";
            this.butLimpar.UseVisualStyleBackColor = true;
            this.butLimpar.Click += new System.EventHandler(this.butLimpar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numAgilidade);
            this.groupBox2.Controls.Add(this.numVitalidade);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numInteligência);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numResistência);
            this.groupBox2.Controls.Add(this.numForça);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numMana);
            this.groupBox2.Controls.Add(this.numVida);
            this.groupBox2.Controls.Add(this.lblMana);
            this.groupBox2.Controls.Add(this.lblVida);
            this.groupBox2.Location = new System.Drawing.Point(219, 276);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 185);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Atributos Bases";
            // 
            // numAgilidade
            // 
            this.numAgilidade.Location = new System.Drawing.Point(154, 118);
            this.numAgilidade.Name = "numAgilidade";
            this.numAgilidade.Size = new System.Drawing.Size(139, 20);
            this.numAgilidade.TabIndex = 32;
            this.numAgilidade.ValueChanged += new System.EventHandler(this.numAgilidade_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(151, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Agilidade:";
            // 
            // numInteligência
            // 
            this.numInteligência.Location = new System.Drawing.Point(10, 117);
            this.numInteligência.Name = "numInteligência";
            this.numInteligência.Size = new System.Drawing.Size(138, 20);
            this.numInteligência.TabIndex = 29;
            this.numInteligência.ValueChanged += new System.EventHandler(this.numInteligência_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Inteligência:";
            // 
            // numResistência
            // 
            this.numResistência.Location = new System.Drawing.Point(154, 72);
            this.numResistência.Name = "numResistência";
            this.numResistência.Size = new System.Drawing.Size(139, 20);
            this.numResistência.TabIndex = 26;
            this.numResistência.ValueChanged += new System.EventHandler(this.numResistência_ValueChanged);
            // 
            // numForça
            // 
            this.numForça.Location = new System.Drawing.Point(10, 72);
            this.numForça.Name = "numForça";
            this.numForça.Size = new System.Drawing.Size(138, 20);
            this.numForça.TabIndex = 25;
            this.numForça.ValueChanged += new System.EventHandler(this.numForça_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Resistência:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Força:";
            // 
            // numMana
            // 
            this.numMana.Location = new System.Drawing.Point(154, 32);
            this.numMana.Name = "numMana";
            this.numMana.Size = new System.Drawing.Size(139, 20);
            this.numMana.TabIndex = 22;
            this.numMana.ValueChanged += new System.EventHandler(this.numMana_ValueChanged);
            // 
            // numVida
            // 
            this.numVida.Location = new System.Drawing.Point(9, 32);
            this.numVida.Name = "numVida";
            this.numVida.Size = new System.Drawing.Size(139, 20);
            this.numVida.TabIndex = 21;
            this.numVida.ValueChanged += new System.EventHandler(this.numVida_ValueChanged);
            // 
            // lblMana
            // 
            this.lblMana.AutoSize = true;
            this.lblMana.Location = new System.Drawing.Point(156, 16);
            this.lblMana.Name = "lblMana";
            this.lblMana.Size = new System.Drawing.Size(37, 13);
            this.lblMana.TabIndex = 3;
            this.lblMana.Text = "Mana:";
            // 
            // lblVida
            // 
            this.lblVida.AutoSize = true;
            this.lblVida.Location = new System.Drawing.Point(6, 16);
            this.lblVida.Name = "lblVida";
            this.lblVida.Size = new System.Drawing.Size(31, 13);
            this.lblVida.TabIndex = 1;
            this.lblVida.Text = "Vida:";
            // 
            // grpTextura
            // 
            this.grpTextura.Controls.Add(this.lblFTextura);
            this.grpTextura.Controls.Add(this.butFTextura);
            this.grpTextura.Controls.Add(this.lblMTextura);
            this.grpTextura.Controls.Add(this.butMTextura);
            this.grpTextura.Location = new System.Drawing.Point(219, 88);
            this.grpTextura.Name = "grpTextura";
            this.grpTextura.Size = new System.Drawing.Size(304, 70);
            this.grpTextura.TabIndex = 19;
            this.grpTextura.TabStop = false;
            this.grpTextura.Text = "Texturas:";
            // 
            // lblFTextura
            // 
            this.lblFTextura.AutoSize = true;
            this.lblFTextura.Location = new System.Drawing.Point(12, 51);
            this.lblFTextura.Name = "lblFTextura";
            this.lblFTextura.Size = new System.Drawing.Size(61, 13);
            this.lblFTextura.TabIndex = 31;
            this.lblFTextura.Text = "Feminina: 0";
            // 
            // butFTextura
            // 
            this.butFTextura.Location = new System.Drawing.Point(104, 44);
            this.butFTextura.Name = "butFTextura";
            this.butFTextura.Size = new System.Drawing.Size(189, 20);
            this.butFTextura.TabIndex = 30;
            this.butFTextura.Text = "Selecionar";
            this.butFTextura.UseVisualStyleBackColor = true;
            this.butFTextura.Click += new System.EventHandler(this.butFTextura_Click);
            // 
            // lblMTextura
            // 
            this.lblMTextura.AutoSize = true;
            this.lblMTextura.Location = new System.Drawing.Point(12, 26);
            this.lblMTextura.Name = "lblMTextura";
            this.lblMTextura.Size = new System.Drawing.Size(67, 13);
            this.lblMTextura.TabIndex = 29;
            this.lblMTextura.Text = "Masculina: 0";
            // 
            // butMTextura
            // 
            this.butMTextura.Location = new System.Drawing.Point(104, 19);
            this.butMTextura.Name = "butMTextura";
            this.butMTextura.Size = new System.Drawing.Size(189, 20);
            this.butMTextura.TabIndex = 28;
            this.butMTextura.Text = "Selecionar";
            this.butMTextura.UseVisualStyleBackColor = true;
            this.butMTextura.Click += new System.EventHandler(this.butMTextura_Click);
            // 
            // butQuantidade
            // 
            this.butQuantidade.Location = new System.Drawing.Point(11, 467);
            this.butQuantidade.Name = "butQuantidade";
            this.butQuantidade.Size = new System.Drawing.Size(202, 25);
            this.butQuantidade.TabIndex = 15;
            this.butQuantidade.Text = "Alterar Quantidade";
            this.butQuantidade.UseVisualStyleBackColor = true;
            this.butQuantidade.Click += new System.EventHandler(this.butQuantidade_Click);
            // 
            // numVitalidade
            // 
            this.numVitalidade.Location = new System.Drawing.Point(10, 159);
            this.numVitalidade.Name = "numVitalidade";
            this.numVitalidade.Size = new System.Drawing.Size(138, 20);
            this.numVitalidade.TabIndex = 34;
            this.numVitalidade.ValueChanged += new System.EventHandler(this.numVitalidade_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Vitalidade:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.numAparecer_X);
            this.groupBox3.Controls.Add(this.cmbAparecer_Direção);
            this.groupBox3.Controls.Add(this.numAparecer_Y);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.numAparecer_Mapa);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(219, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 106);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Nascimento";
            // 
            // numAparecer_Mapa
            // 
            this.numAparecer_Mapa.Location = new System.Drawing.Point(9, 36);
            this.numAparecer_Mapa.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAparecer_Mapa.Name = "numAparecer_Mapa";
            this.numAparecer_Mapa.Size = new System.Drawing.Size(139, 20);
            this.numAparecer_Mapa.TabIndex = 23;
            this.numAparecer_Mapa.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAparecer_Mapa.ValueChanged += new System.EventHandler(this.numAparecer_Mapa_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Mapa:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(151, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Direção:";
            // 
            // numAparecer_X
            // 
            this.numAparecer_X.Location = new System.Drawing.Point(9, 78);
            this.numAparecer_X.Name = "numAparecer_X";
            this.numAparecer_X.Size = new System.Drawing.Size(139, 20);
            this.numAparecer_X.TabIndex = 27;
            this.numAparecer_X.ValueChanged += new System.EventHandler(this.numAparecer_X_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "X:";
            // 
            // numAparecer_Y
            // 
            this.numAparecer_Y.Location = new System.Drawing.Point(155, 78);
            this.numAparecer_Y.Name = "numAparecer_Y";
            this.numAparecer_Y.Size = new System.Drawing.Size(138, 20);
            this.numAparecer_Y.TabIndex = 27;
            this.numAparecer_Y.ValueChanged += new System.EventHandler(this.numAparecer_Y_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(152, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Y:";
            // 
            // cmbAparecer_Direção
            // 
            this.cmbAparecer_Direção.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAparecer_Direção.FormattingEnabled = true;
            this.cmbAparecer_Direção.Items.AddRange(new object[] {
            "Acima",
            "Abaixo",
            "Esquerda",
            "Direita"});
            this.cmbAparecer_Direção.Location = new System.Drawing.Point(154, 35);
            this.cmbAparecer_Direção.Name = "cmbAparecer_Direção";
            this.cmbAparecer_Direção.Size = new System.Drawing.Size(139, 21);
            this.cmbAparecer_Direção.TabIndex = 28;
            this.cmbAparecer_Direção.SelectedIndexChanged += new System.EventHandler(this.cmbAparecer_Direção_SelectedIndexChanged);
            // 
            // Editor_Classes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 497);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.butQuantidade);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpTextura);
            this.Controls.Add(this.butLimpar);
            this.Controls.Add(this.butCancelar);
            this.Controls.Add(this.butSalvar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstLista);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Editor_Classes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor de Classes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAgilidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInteligência)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numResistência)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForça)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVida)).EndInit();
            this.grpTextura.ResumeLayout(false);
            this.grpTextura.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVitalidade)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAparecer_Mapa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAparecer_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAparecer_Y)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion
    public System.Windows.Forms.ListBox lstLista;
    private System.Windows.Forms.GroupBox groupBox1;
    public System.Windows.Forms.TextBox txtNome;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button butSalvar;
    private System.Windows.Forms.Button butCancelar;
    private System.Windows.Forms.Button butLimpar;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label lblMana;
    private System.Windows.Forms.Label lblVida;
    private System.Windows.Forms.GroupBox grpTextura;
    private System.Windows.Forms.NumericUpDown numAgilidade;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown numInteligência;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.NumericUpDown numResistência;
    private System.Windows.Forms.NumericUpDown numForça;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown numMana;
    private System.Windows.Forms.NumericUpDown numVida;
    private System.Windows.Forms.Button butQuantidade;
    private System.Windows.Forms.NumericUpDown numVitalidade;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lblFTextura;
    private System.Windows.Forms.Button butFTextura;
    private System.Windows.Forms.Label lblMTextura;
    private System.Windows.Forms.Button butMTextura;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.NumericUpDown numAparecer_X;
    private System.Windows.Forms.ComboBox cmbAparecer_Direção;
    private System.Windows.Forms.NumericUpDown numAparecer_Y;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.NumericUpDown numAparecer_Mapa;
    private System.Windows.Forms.Label label7;
}