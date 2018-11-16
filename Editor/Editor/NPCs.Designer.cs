partial class Editor_NPCs
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
            this.numTextura = new System.Windows.Forms.NumericUpDown();
            this.numAparecimento = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numVisão = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAgressividade = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTextura = new System.Windows.Forms.Label();
            this.butTextura = new System.Windows.Forms.Button();
            this.butSalvar = new System.Windows.Forms.Button();
            this.butCancelar = new System.Windows.Forms.Button();
            this.butLimpar = new System.Windows.Forms.Button();
            this.butQuantidade = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpQueda = new System.Windows.Forms.GroupBox();
            this.numQQuantidade = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbQItem = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numQChance = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.scrlQueda = new System.Windows.Forms.HScrollBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numResistência = new System.Windows.Forms.NumericUpDown();
            this.numInteligência = new System.Windows.Forms.NumericUpDown();
            this.numVitalidade = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numForça = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numAgilidade = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numExperiência = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numMana = new System.Windows.Forms.NumericUpDown();
            this.numVida = new System.Windows.Forms.NumericUpDown();
            this.lblMana = new System.Windows.Forms.Label();
            this.lblVida = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTextura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAparecimento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVisão)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.grpQueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQQuantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQChance)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResistência)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInteligência)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVitalidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForça)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAgilidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExperiência)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVida)).BeginInit();
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
            this.groupBox1.Controls.Add(this.numTextura);
            this.groupBox1.Controls.Add(this.numAparecimento);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numVisão);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbAgressividade);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblTextura);
            this.groupBox1.Controls.Add(this.butTextura);
            this.groupBox1.Location = new System.Drawing.Point(219, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 196);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // numTextura
            // 
            this.numTextura.Location = new System.Drawing.Point(15, 78);
            this.numTextura.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numTextura.Name = "numTextura";
            this.numTextura.Size = new System.Drawing.Size(187, 20);
            this.numTextura.TabIndex = 36;
            this.numTextura.ValueChanged += new System.EventHandler(this.numTextura_ValueChanged);
            // 
            // numAparecimento
            // 
            this.numAparecimento.Location = new System.Drawing.Point(160, 120);
            this.numAparecimento.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numAparecimento.Name = "numAparecimento";
            this.numAparecimento.Size = new System.Drawing.Size(139, 20);
            this.numAparecimento.TabIndex = 35;
            this.numAparecimento.ValueChanged += new System.EventHandler(this.numAparecimento_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(157, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Tempo de aparecimento(seg):";
            // 
            // numVisão
            // 
            this.numVisão.Location = new System.Drawing.Point(15, 162);
            this.numVisão.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numVisão.Name = "numVisão";
            this.numVisão.Size = new System.Drawing.Size(139, 20);
            this.numVisão.TabIndex = 33;
            this.numVisão.ValueChanged += new System.EventHandler(this.numVisão_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Campo de visão:";
            // 
            // cmbAgressividade
            // 
            this.cmbAgressividade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgressividade.FormattingEnabled = true;
            this.cmbAgressividade.Items.AddRange(new object[] {
            "Passivo",
            "Atacar ao ver",
            "Atacar ao ser atacado"});
            this.cmbAgressividade.Location = new System.Drawing.Point(15, 119);
            this.cmbAgressividade.Name = "cmbAgressividade";
            this.cmbAgressividade.Size = new System.Drawing.Size(139, 21);
            this.cmbAgressividade.TabIndex = 31;
            this.cmbAgressividade.SelectedIndexChanged += new System.EventHandler(this.cmbAgressividade_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Agressividade:";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(15, 37);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(279, 20);
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
            this.label3.Text = "Name:";
            // 
            // lblTextura
            // 
            this.lblTextura.AutoSize = true;
            this.lblTextura.Location = new System.Drawing.Point(12, 62);
            this.lblTextura.Name = "lblTextura";
            this.lblTextura.Size = new System.Drawing.Size(46, 13);
            this.lblTextura.TabIndex = 29;
            this.lblTextura.Text = "Texture:";
            // 
            // butTextura
            // 
            this.butTextura.Location = new System.Drawing.Point(208, 78);
            this.butTextura.Name = "butTextura";
            this.butTextura.Size = new System.Drawing.Size(86, 19);
            this.butTextura.TabIndex = 28;
            this.butTextura.Text = "Selecionar";
            this.butTextura.UseVisualStyleBackColor = true;
            this.butTextura.Click += new System.EventHandler(this.butTextura_Click);
            // 
            // butSalvar
            // 
            this.butSalvar.Location = new System.Drawing.Point(219, 514);
            this.butSalvar.Name = "butSalvar";
            this.butSalvar.Size = new System.Drawing.Size(97, 25);
            this.butSalvar.TabIndex = 16;
            this.butSalvar.Text = "Save";
            this.butSalvar.UseVisualStyleBackColor = true;
            this.butSalvar.Click += new System.EventHandler(this.butSalvar_Click);
            // 
            // butCancelar
            // 
            this.butCancelar.Location = new System.Drawing.Point(426, 514);
            this.butCancelar.Name = "butCancelar";
            this.butCancelar.Size = new System.Drawing.Size(97, 25);
            this.butCancelar.TabIndex = 17;
            this.butCancelar.Text = "Cancel";
            this.butCancelar.UseVisualStyleBackColor = true;
            this.butCancelar.Click += new System.EventHandler(this.butCancelar_Click);
            // 
            // butLimpar
            // 
            this.butLimpar.Location = new System.Drawing.Point(323, 514);
            this.butLimpar.Name = "butLimpar";
            this.butLimpar.Size = new System.Drawing.Size(97, 25);
            this.butLimpar.TabIndex = 18;
            this.butLimpar.Text = "Clean";
            this.butLimpar.UseVisualStyleBackColor = true;
            this.butLimpar.Click += new System.EventHandler(this.butLimpar_Click);
            // 
            // butQuantidade
            // 
            this.butQuantidade.Location = new System.Drawing.Point(11, 514);
            this.butQuantidade.Name = "butQuantidade";
            this.butQuantidade.Size = new System.Drawing.Size(202, 25);
            this.butQuantidade.TabIndex = 15;
            this.butQuantidade.Text = "Alterar Quantidade";
            this.butQuantidade.UseVisualStyleBackColor = true;
            this.butQuantidade.Click += new System.EventHandler(this.butQuantidade_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grpQueda);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.numExperiência);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numMana);
            this.groupBox2.Controls.Add(this.numVida);
            this.groupBox2.Controls.Add(this.lblMana);
            this.groupBox2.Controls.Add(this.lblVida);
            this.groupBox2.Location = new System.Drawing.Point(219, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 294);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Combat:";
            // 
            // grpQueda
            // 
            this.grpQueda.Controls.Add(this.numQQuantidade);
            this.grpQueda.Controls.Add(this.label13);
            this.grpQueda.Controls.Add(this.cmbQItem);
            this.grpQueda.Controls.Add(this.label12);
            this.grpQueda.Controls.Add(this.numQChance);
            this.grpQueda.Controls.Add(this.label11);
            this.grpQueda.Controls.Add(this.scrlQueda);
            this.grpQueda.Location = new System.Drawing.Point(9, 163);
            this.grpQueda.Name = "grpQueda";
            this.grpQueda.Size = new System.Drawing.Size(284, 118);
            this.grpQueda.TabIndex = 22;
            this.grpQueda.TabStop = false;
            this.grpQueda.Text = "Queda - 1";
            // 
            // numQQuantidade
            // 
            this.numQQuantidade.Location = new System.Drawing.Point(145, 48);
            this.numQQuantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQQuantidade.Name = "numQQuantidade";
            this.numQQuantidade.Size = new System.Drawing.Size(130, 20);
            this.numQQuantidade.TabIndex = 37;
            this.numQQuantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQQuantidade.ValueChanged += new System.EventHandler(this.numQQuantidade_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(142, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "Quantity:";
            // 
            // cmbQItem
            // 
            this.cmbQItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQItem.FormattingEnabled = true;
            this.cmbQItem.Location = new System.Drawing.Point(9, 48);
            this.cmbQItem.Name = "cmbQItem";
            this.cmbQItem.Size = new System.Drawing.Size(130, 21);
            this.cmbQItem.TabIndex = 35;
            this.cmbQItem.SelectedIndexChanged += new System.EventHandler(this.cmbQItem_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "Item:";
            // 
            // numQChance
            // 
            this.numQChance.Location = new System.Drawing.Point(9, 87);
            this.numQChance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQChance.Name = "numQChance";
            this.numQChance.Size = new System.Drawing.Size(266, 20);
            this.numQChance.TabIndex = 33;
            this.numQChance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQChance.ValueChanged += new System.EventHandler(this.numQChance_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Chance: (%)";
            // 
            // scrlQueda
            // 
            this.scrlQueda.LargeChange = 1;
            this.scrlQueda.Location = new System.Drawing.Point(7, 16);
            this.scrlQueda.Name = "scrlQueda";
            this.scrlQueda.Size = new System.Drawing.Size(265, 16);
            this.scrlQueda.TabIndex = 0;
            this.scrlQueda.ValueChanged += new System.EventHandler(this.scrlQueda_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numResistência);
            this.groupBox4.Controls.Add(this.numInteligência);
            this.groupBox4.Controls.Add(this.numVitalidade);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.numForça);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.numAgilidade);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(9, 58);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 99);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Atributos";
            // 
            // numResistência
            // 
            this.numResistência.Location = new System.Drawing.Point(96, 34);
            this.numResistência.Name = "numResistência";
            this.numResistência.Size = new System.Drawing.Size(85, 20);
            this.numResistência.TabIndex = 26;
            this.numResistência.ValueChanged += new System.EventHandler(this.numResistência_ValueChanged);
            // 
            // numInteligência
            // 
            this.numInteligência.Location = new System.Drawing.Point(187, 34);
            this.numInteligência.Name = "numInteligência";
            this.numInteligência.Size = new System.Drawing.Size(85, 20);
            this.numInteligência.TabIndex = 29;
            this.numInteligência.ValueChanged += new System.EventHandler(this.numInteligência_ValueChanged);
            // 
            // numVitalidade
            // 
            this.numVitalidade.Location = new System.Drawing.Point(96, 73);
            this.numVitalidade.Name = "numVitalidade";
            this.numVitalidade.Size = new System.Drawing.Size(85, 20);
            this.numVitalidade.TabIndex = 36;
            this.numVitalidade.ValueChanged += new System.EventHandler(this.numVitalidade_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(93, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Vitality:";
            // 
            // numForça
            // 
            this.numForça.Location = new System.Drawing.Point(6, 34);
            this.numForça.Name = "numForça";
            this.numForça.Size = new System.Drawing.Size(85, 20);
            this.numForça.TabIndex = 25;
            this.numForça.ValueChanged += new System.EventHandler(this.numForça_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Strength:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Defense:";
            // 
            // numAgilidade
            // 
            this.numAgilidade.Location = new System.Drawing.Point(6, 73);
            this.numAgilidade.Name = "numAgilidade";
            this.numAgilidade.Size = new System.Drawing.Size(85, 20);
            this.numAgilidade.TabIndex = 32;
            this.numAgilidade.ValueChanged += new System.EventHandler(this.numAgilidade_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Agility:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Inteligence:";
            // 
            // numExperiência
            // 
            this.numExperiência.Location = new System.Drawing.Point(203, 32);
            this.numExperiência.Name = "numExperiência";
            this.numExperiência.Size = new System.Drawing.Size(90, 20);
            this.numExperiência.TabIndex = 34;
            this.numExperiência.ValueChanged += new System.EventHandler(this.numExperiência_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Experience:";
            // 
            // numMana
            // 
            this.numMana.Location = new System.Drawing.Point(105, 32);
            this.numMana.Name = "numMana";
            this.numMana.Size = new System.Drawing.Size(90, 20);
            this.numMana.TabIndex = 22;
            this.numMana.ValueChanged += new System.EventHandler(this.numMana_ValueChanged);
            // 
            // numVida
            // 
            this.numVida.Location = new System.Drawing.Point(9, 32);
            this.numVida.Name = "numVida";
            this.numVida.Size = new System.Drawing.Size(90, 20);
            this.numVida.TabIndex = 21;
            this.numVida.ValueChanged += new System.EventHandler(this.numVida_ValueChanged);
            // 
            // lblMana
            // 
            this.lblMana.AutoSize = true;
            this.lblMana.Location = new System.Drawing.Point(102, 16);
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
            this.lblVida.Size = new System.Drawing.Size(27, 13);
            this.lblVida.TabIndex = 1;
            this.lblVida.Text = "Life:";
            // 
            // Editor_NPCs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 550);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.butQuantidade);
            this.Controls.Add(this.butLimpar);
            this.Controls.Add(this.butCancelar);
            this.Controls.Add(this.butSalvar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstLista);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Editor_NPCs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NPC Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTextura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAparecimento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVisão)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpQueda.ResumeLayout(false);
            this.grpQueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQQuantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQChance)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResistência)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInteligência)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVitalidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForça)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAgilidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExperiência)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVida)).EndInit();
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
    private System.Windows.Forms.Label lblTextura;
    private System.Windows.Forms.Button butTextura;
    private System.Windows.Forms.NumericUpDown numVisão;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbAgressividade;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.NumericUpDown numAgilidade;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown numInteligência;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.NumericUpDown numResistência;
    private System.Windows.Forms.NumericUpDown numForça;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.NumericUpDown numMana;
    private System.Windows.Forms.NumericUpDown numVida;
    private System.Windows.Forms.Label lblMana;
    private System.Windows.Forms.Label lblVida;
    private System.Windows.Forms.NumericUpDown numAparecimento;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.NumericUpDown numExperiência;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.NumericUpDown numVitalidade;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.NumericUpDown numTextura;
    private System.Windows.Forms.GroupBox grpQueda;
    private System.Windows.Forms.NumericUpDown numQQuantidade;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.ComboBox cmbQItem;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.NumericUpDown numQChance;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.HScrollBar scrlQueda;
    private System.Windows.Forms.GroupBox groupBox4;
}