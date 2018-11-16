partial class Editor_Itens
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
            this.numPreço = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDescrição = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkNãoDropável = new System.Windows.Forms.CheckBox();
            this.chkEmpilhável = new System.Windows.Forms.CheckBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.numTextura = new System.Windows.Forms.NumericUpDown();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butTextura = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.butSalvar = new System.Windows.Forms.Button();
            this.butCancelar = new System.Windows.Forms.Button();
            this.butLimpar = new System.Windows.Forms.Button();
            this.butQuantidade = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbReq_Classe = new System.Windows.Forms.ComboBox();
            this.numReq_Level = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grpEquip_Bônus = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numEquip_Força = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numEquip_Resistência = new System.Windows.Forms.NumericUpDown();
            this.numEquip_Inteligência = new System.Windows.Forms.NumericUpDown();
            this.numEquip_Vitalidade = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numEquip_Agilidade = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numPoção_Experiência = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numPoção_Mana = new System.Windows.Forms.NumericUpDown();
            this.numPoção_Vida = new System.Windows.Forms.NumericUpDown();
            this.lblMana = new System.Windows.Forms.Label();
            this.grpPoção = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grpEquipamento = new System.Windows.Forms.GroupBox();
            this.cmbEquipamento_Tipo = new System.Windows.Forms.ComboBox();
            this.grpArma = new System.Windows.Forms.GroupBox();
            this.numArma_Dano = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPreço)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTextura)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReq_Level)).BeginInit();
            this.grpEquip_Bônus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Força)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Resistência)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Inteligência)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Vitalidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Agilidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoção_Experiência)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoção_Mana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoção_Vida)).BeginInit();
            this.grpPoção.SuspendLayout();
            this.grpEquipamento.SuspendLayout();
            this.grpArma.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArma_Dano)).BeginInit();
            this.SuspendLayout();
            // 
            // lstLista
            // 
            this.lstLista.FormattingEnabled = true;
            this.lstLista.Location = new System.Drawing.Point(11, 12);
            this.lstLista.Name = "lstLista";
            this.lstLista.Size = new System.Drawing.Size(202, 498);
            this.lstLista.TabIndex = 9;
            this.lstLista.SelectedIndexChanged += new System.EventHandler(this.lstLista_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numPreço);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtDescrição);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.chkNãoDropável);
            this.groupBox1.Controls.Add(this.chkEmpilhável);
            this.groupBox1.Controls.Add(this.cmbTipo);
            this.groupBox1.Controls.Add(this.numTextura);
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.butTextura);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(219, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 185);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geral";
            // 
            // numPreço
            // 
            this.numPreço.Location = new System.Drawing.Point(190, 118);
            this.numPreço.Name = "numPreço";
            this.numPreço.Size = new System.Drawing.Size(169, 20);
            this.numPreço.TabIndex = 25;
            this.numPreço.ValueChanged += new System.EventHandler(this.numPreço_ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(187, 102);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(38, 13);
            this.label18.TabIndex = 24;
            this.label18.Text = "Preço:";
            // 
            // txtDescrição
            // 
            this.txtDescrição.Location = new System.Drawing.Point(15, 77);
            this.txtDescrição.Multiline = true;
            this.txtDescrição.Name = "txtDescrição";
            this.txtDescrição.Size = new System.Drawing.Size(169, 98);
            this.txtDescrição.TabIndex = 23;
            this.txtDescrição.Validated += new System.EventHandler(this.txtDescrição_Validated);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "Descrição:";
            // 
            // chkNãoDropável
            // 
            this.chkNãoDropável.AutoSize = true;
            this.chkNãoDropável.Location = new System.Drawing.Point(190, 158);
            this.chkNãoDropável.Name = "chkNãoDropável";
            this.chkNãoDropável.Size = new System.Drawing.Size(90, 17);
            this.chkNãoDropável.TabIndex = 21;
            this.chkNãoDropável.Text = "Não dropável";
            this.chkNãoDropável.UseVisualStyleBackColor = true;
            this.chkNãoDropável.CheckedChanged += new System.EventHandler(this.chkNãoDropável_CheckedChanged);
            // 
            // chkEmpilhável
            // 
            this.chkEmpilhável.AutoSize = true;
            this.chkEmpilhável.Location = new System.Drawing.Point(190, 142);
            this.chkEmpilhável.Name = "chkEmpilhável";
            this.chkEmpilhável.Size = new System.Drawing.Size(77, 17);
            this.chkEmpilhável.TabIndex = 20;
            this.chkEmpilhável.Text = "Empilhável";
            this.chkEmpilhável.UseVisualStyleBackColor = true;
            this.chkEmpilhável.CheckedChanged += new System.EventHandler(this.chkEmpilhável_CheckedChanged);
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "Nenhum",
            "Equipamento",
            "Poção"});
            this.cmbTipo.Location = new System.Drawing.Point(190, 36);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(169, 21);
            this.cmbTipo.TabIndex = 18;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // numTextura
            // 
            this.numTextura.Location = new System.Drawing.Point(190, 79);
            this.numTextura.Name = "numTextura";
            this.numTextura.Size = new System.Drawing.Size(82, 20);
            this.numTextura.TabIndex = 12;
            this.numTextura.ValueChanged += new System.EventHandler(this.numTextura_ValueChanged);
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(15, 37);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(169, 20);
            this.txtNome.TabIndex = 10;
            this.txtNome.Validated += new System.EventHandler(this.txtNome_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Tipo:";
            // 
            // butTextura
            // 
            this.butTextura.Location = new System.Drawing.Point(278, 77);
            this.butTextura.Name = "butTextura";
            this.butTextura.Size = new System.Drawing.Size(81, 20);
            this.butTextura.TabIndex = 17;
            this.butTextura.Text = "Selecionar";
            this.butTextura.UseVisualStyleBackColor = true;
            this.butTextura.Click += new System.EventHandler(this.butTextura_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Textura:";
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
            this.butSalvar.Location = new System.Drawing.Point(219, 515);
            this.butSalvar.Name = "butSalvar";
            this.butSalvar.Size = new System.Drawing.Size(119, 25);
            this.butSalvar.TabIndex = 16;
            this.butSalvar.Text = "Salvar";
            this.butSalvar.UseVisualStyleBackColor = true;
            this.butSalvar.Click += new System.EventHandler(this.butSalvar_Click);
            // 
            // butCancelar
            // 
            this.butCancelar.Location = new System.Drawing.Point(465, 515);
            this.butCancelar.Name = "butCancelar";
            this.butCancelar.Size = new System.Drawing.Size(119, 25);
            this.butCancelar.TabIndex = 17;
            this.butCancelar.Text = "Cancelar";
            this.butCancelar.UseVisualStyleBackColor = true;
            this.butCancelar.Click += new System.EventHandler(this.butCancelar_Click);
            // 
            // butLimpar
            // 
            this.butLimpar.Location = new System.Drawing.Point(342, 515);
            this.butLimpar.Name = "butLimpar";
            this.butLimpar.Size = new System.Drawing.Size(119, 25);
            this.butLimpar.TabIndex = 18;
            this.butLimpar.Text = "Limpar";
            this.butLimpar.UseVisualStyleBackColor = true;
            this.butLimpar.Click += new System.EventHandler(this.butLimpar_Click);
            // 
            // butQuantidade
            // 
            this.butQuantidade.Location = new System.Drawing.Point(11, 515);
            this.butQuantidade.Name = "butQuantidade";
            this.butQuantidade.Size = new System.Drawing.Size(202, 25);
            this.butQuantidade.TabIndex = 15;
            this.butQuantidade.Text = "Alterar Quantidade";
            this.butQuantidade.UseVisualStyleBackColor = true;
            this.butQuantidade.Click += new System.EventHandler(this.butQuantidade_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbReq_Classe);
            this.groupBox2.Controls.Add(this.numReq_Level);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(220, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 63);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Requerimentos";
            // 
            // cmbReq_Classe
            // 
            this.cmbReq_Classe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReq_Classe.FormattingEnabled = true;
            this.cmbReq_Classe.Items.AddRange(new object[] {
            "Nenhum",
            "Arma",
            "Armadura",
            "Capacete",
            "Escudo",
            "Amuleto",
            "Poção"});
            this.cmbReq_Classe.Location = new System.Drawing.Point(190, 34);
            this.cmbReq_Classe.Name = "cmbReq_Classe";
            this.cmbReq_Classe.Size = new System.Drawing.Size(169, 21);
            this.cmbReq_Classe.TabIndex = 20;
            this.cmbReq_Classe.SelectedIndexChanged += new System.EventHandler(this.cmbReq_Classe_SelectedIndexChanged);
            // 
            // numReq_Level
            // 
            this.numReq_Level.Location = new System.Drawing.Point(14, 35);
            this.numReq_Level.Name = "numReq_Level";
            this.numReq_Level.Size = new System.Drawing.Size(169, 20);
            this.numReq_Level.TabIndex = 14;
            this.numReq_Level.ValueChanged += new System.EventHandler(this.numReq_Level_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Classe:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Level:";
            // 
            // grpEquip_Bônus
            // 
            this.grpEquip_Bônus.Controls.Add(this.label13);
            this.grpEquip_Bônus.Controls.Add(this.numEquip_Força);
            this.grpEquip_Bônus.Controls.Add(this.label7);
            this.grpEquip_Bônus.Controls.Add(this.numEquip_Resistência);
            this.grpEquip_Bônus.Controls.Add(this.numEquip_Inteligência);
            this.grpEquip_Bônus.Controls.Add(this.numEquip_Vitalidade);
            this.grpEquip_Bônus.Controls.Add(this.label10);
            this.grpEquip_Bônus.Controls.Add(this.label6);
            this.grpEquip_Bônus.Controls.Add(this.numEquip_Agilidade);
            this.grpEquip_Bônus.Controls.Add(this.label8);
            this.grpEquip_Bônus.Controls.Add(this.label11);
            this.grpEquip_Bônus.Location = new System.Drawing.Point(11, 58);
            this.grpEquip_Bônus.Name = "grpEquip_Bônus";
            this.grpEquip_Bônus.Size = new System.Drawing.Size(168, 167);
            this.grpEquip_Bônus.TabIndex = 20;
            this.grpEquip_Bônus.TabStop = false;
            this.grpEquip_Bônus.Text = "Bônus:";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(154, 27);
            this.label13.TabIndex = 56;
            this.label13.Text = "(Valores negativos também são válidos)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numEquip_Força
            // 
            this.numEquip_Força.Location = new System.Drawing.Point(10, 32);
            this.numEquip_Força.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numEquip_Força.Name = "numEquip_Força";
            this.numEquip_Força.Size = new System.Drawing.Size(72, 20);
            this.numEquip_Força.TabIndex = 43;
            this.numEquip_Força.ValueChanged += new System.EventHandler(this.numEquip_Força_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Força:";
            // 
            // numEquip_Resistência
            // 
            this.numEquip_Resistência.Location = new System.Drawing.Point(88, 32);
            this.numEquip_Resistência.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numEquip_Resistência.Name = "numEquip_Resistência";
            this.numEquip_Resistência.Size = new System.Drawing.Size(72, 20);
            this.numEquip_Resistência.TabIndex = 44;
            this.numEquip_Resistência.ValueChanged += new System.EventHandler(this.numEquip_Resistência_ValueChanged);
            // 
            // numEquip_Inteligência
            // 
            this.numEquip_Inteligência.Location = new System.Drawing.Point(10, 73);
            this.numEquip_Inteligência.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numEquip_Inteligência.Name = "numEquip_Inteligência";
            this.numEquip_Inteligência.Size = new System.Drawing.Size(72, 20);
            this.numEquip_Inteligência.TabIndex = 46;
            this.numEquip_Inteligência.ValueChanged += new System.EventHandler(this.numEquip_Inteligência_ValueChanged);
            // 
            // numEquip_Vitalidade
            // 
            this.numEquip_Vitalidade.Location = new System.Drawing.Point(11, 112);
            this.numEquip_Vitalidade.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numEquip_Vitalidade.Name = "numEquip_Vitalidade";
            this.numEquip_Vitalidade.Size = new System.Drawing.Size(72, 20);
            this.numEquip_Vitalidade.TabIndex = 52;
            this.numEquip_Vitalidade.ValueChanged += new System.EventHandler(this.numEquip_Vitalidade_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 51;
            this.label10.Text = "Vitalidade:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(85, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Resistência:";
            // 
            // numEquip_Agilidade
            // 
            this.numEquip_Agilidade.Location = new System.Drawing.Point(88, 73);
            this.numEquip_Agilidade.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numEquip_Agilidade.Name = "numEquip_Agilidade";
            this.numEquip_Agilidade.Size = new System.Drawing.Size(72, 20);
            this.numEquip_Agilidade.TabIndex = 48;
            this.numEquip_Agilidade.ValueChanged += new System.EventHandler(this.numEquip_Agilidade_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(85, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Agilidade:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Inteligência:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 53;
            this.label12.Text = "Vida:";
            // 
            // numPoção_Experiência
            // 
            this.numPoção_Experiência.Location = new System.Drawing.Point(246, 37);
            this.numPoção_Experiência.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numPoção_Experiência.Name = "numPoção_Experiência";
            this.numPoção_Experiência.Size = new System.Drawing.Size(113, 20);
            this.numPoção_Experiência.TabIndex = 50;
            this.numPoção_Experiência.ValueChanged += new System.EventHandler(this.numEquip_Experiência_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(243, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Experiência:";
            // 
            // numPoção_Mana
            // 
            this.numPoção_Mana.Location = new System.Drawing.Point(127, 37);
            this.numPoção_Mana.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numPoção_Mana.Name = "numPoção_Mana";
            this.numPoção_Mana.Size = new System.Drawing.Size(113, 20);
            this.numPoção_Mana.TabIndex = 40;
            this.numPoção_Mana.ValueChanged += new System.EventHandler(this.numEquip_Mana_ValueChanged);
            // 
            // numPoção_Vida
            // 
            this.numPoção_Vida.Location = new System.Drawing.Point(8, 37);
            this.numPoção_Vida.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numPoção_Vida.Name = "numPoção_Vida";
            this.numPoção_Vida.Size = new System.Drawing.Size(113, 20);
            this.numPoção_Vida.TabIndex = 39;
            this.numPoção_Vida.ValueChanged += new System.EventHandler(this.numEquip_Vida_ValueChanged);
            // 
            // lblMana
            // 
            this.lblMana.AutoSize = true;
            this.lblMana.Location = new System.Drawing.Point(124, 21);
            this.lblMana.Name = "lblMana";
            this.lblMana.Size = new System.Drawing.Size(37, 13);
            this.lblMana.TabIndex = 38;
            this.lblMana.Text = "Mana:";
            // 
            // grpPoção
            // 
            this.grpPoção.Controls.Add(this.numPoção_Mana);
            this.grpPoção.Controls.Add(this.numPoção_Experiência);
            this.grpPoção.Controls.Add(this.numPoção_Vida);
            this.grpPoção.Controls.Add(this.label14);
            this.grpPoção.Controls.Add(this.lblMana);
            this.grpPoção.Controls.Add(this.label12);
            this.grpPoção.Controls.Add(this.label9);
            this.grpPoção.Location = new System.Drawing.Point(219, 272);
            this.grpPoção.Name = "grpPoção";
            this.grpPoção.Size = new System.Drawing.Size(366, 77);
            this.grpPoção.TabIndex = 21;
            this.grpPoção.TabStop = false;
            this.grpPoção.Text = "Poção";
            this.grpPoção.Visible = false;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(349, 12);
            this.label14.TabIndex = 55;
            this.label14.Text = "(Valores negativos também são válidos)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grpEquipamento
            // 
            this.grpEquipamento.Controls.Add(this.cmbEquipamento_Tipo);
            this.grpEquipamento.Controls.Add(this.grpArma);
            this.grpEquipamento.Controls.Add(this.label16);
            this.grpEquipamento.Controls.Add(this.grpEquip_Bônus);
            this.grpEquipamento.Location = new System.Drawing.Point(219, 272);
            this.grpEquipamento.Name = "grpEquipamento";
            this.grpEquipamento.Size = new System.Drawing.Size(365, 237);
            this.grpEquipamento.TabIndex = 22;
            this.grpEquipamento.TabStop = false;
            this.grpEquipamento.Text = "Equipamento";
            // 
            // cmbEquipamento_Tipo
            // 
            this.cmbEquipamento_Tipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipamento_Tipo.FormattingEnabled = true;
            this.cmbEquipamento_Tipo.Items.AddRange(new object[] {
            "Arma",
            "Armadura",
            "Capacete",
            "Escudo",
            "Amuleto"});
            this.cmbEquipamento_Tipo.Location = new System.Drawing.Point(11, 31);
            this.cmbEquipamento_Tipo.Name = "cmbEquipamento_Tipo";
            this.cmbEquipamento_Tipo.Size = new System.Drawing.Size(168, 21);
            this.cmbEquipamento_Tipo.TabIndex = 22;
            this.cmbEquipamento_Tipo.SelectedIndexChanged += new System.EventHandler(this.cmbEquipamento_Tipo_SelectedIndexChanged);
            // 
            // grpArma
            // 
            this.grpArma.Controls.Add(this.numArma_Dano);
            this.grpArma.Controls.Add(this.label17);
            this.grpArma.Location = new System.Drawing.Point(190, 19);
            this.grpArma.Name = "grpArma";
            this.grpArma.Size = new System.Drawing.Size(168, 206);
            this.grpArma.TabIndex = 24;
            this.grpArma.TabStop = false;
            this.grpArma.Text = "Arma";
            // 
            // numArma_Dano
            // 
            this.numArma_Dano.Location = new System.Drawing.Point(12, 39);
            this.numArma_Dano.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numArma_Dano.Name = "numArma_Dano";
            this.numArma_Dano.Size = new System.Drawing.Size(150, 20);
            this.numArma_Dano.TabIndex = 45;
            this.numArma_Dano.ValueChanged += new System.EventHandler(this.numArma_Dano_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "Dano base:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 23;
            this.label16.Text = "Tipo:";
            // 
            // Editor_Itens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 551);
            this.ControlBox = false;
            this.Controls.Add(this.grpEquipamento);
            this.Controls.Add(this.grpPoção);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.butQuantidade);
            this.Controls.Add(this.butLimpar);
            this.Controls.Add(this.butCancelar);
            this.Controls.Add(this.butSalvar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstLista);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Editor_Itens";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor de Itens";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPreço)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTextura)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReq_Level)).EndInit();
            this.grpEquip_Bônus.ResumeLayout(false);
            this.grpEquip_Bônus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Força)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Resistência)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Inteligência)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Vitalidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquip_Agilidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoção_Experiência)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoção_Mana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoção_Vida)).EndInit();
            this.grpPoção.ResumeLayout(false);
            this.grpPoção.PerformLayout();
            this.grpEquipamento.ResumeLayout(false);
            this.grpEquipamento.PerformLayout();
            this.grpArma.ResumeLayout(false);
            this.grpArma.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArma_Dano)).EndInit();
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
    private System.Windows.Forms.Button butQuantidade;
    private System.Windows.Forms.Button butTextura;
    private System.Windows.Forms.NumericUpDown numTextura;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbTipo;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cmbReq_Classe;
    private System.Windows.Forms.NumericUpDown numReq_Level;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox grpEquip_Bônus;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.NumericUpDown numEquip_Força;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.NumericUpDown numPoção_Experiência;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.NumericUpDown numPoção_Mana;
    private System.Windows.Forms.NumericUpDown numPoção_Vida;
    private System.Windows.Forms.Label lblMana;
    private System.Windows.Forms.NumericUpDown numEquip_Resistência;
    private System.Windows.Forms.NumericUpDown numEquip_Inteligência;
    private System.Windows.Forms.NumericUpDown numEquip_Vitalidade;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown numEquip_Agilidade;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.GroupBox grpPoção;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label13;
    public System.Windows.Forms.TextBox txtDescrição;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.CheckBox chkNãoDropável;
    private System.Windows.Forms.CheckBox chkEmpilhável;
    private System.Windows.Forms.GroupBox grpEquipamento;
    private System.Windows.Forms.GroupBox grpArma;
    private System.Windows.Forms.NumericUpDown numArma_Dano;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.ComboBox cmbEquipamento_Tipo;
    private System.Windows.Forms.NumericUpDown numPreço;
    private System.Windows.Forms.Label label18;
}