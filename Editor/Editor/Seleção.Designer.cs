partial class Seleção
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCliente_Diretório = new System.Windows.Forms.TextBox();
            this.butSelecionarDiretório_Cliente = new System.Windows.Forms.Button();
            this.butSelecionarDiretório_Servidor = new System.Windows.Forms.Button();
            this.txtServidor_Diretório = new System.Windows.Forms.TextBox();
            this.Diretório_Cliente = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.butNPCs = new System.Windows.Forms.Button();
            this.butAzulejos = new System.Windows.Forms.Button();
            this.butDados = new System.Windows.Forms.Button();
            this.butFerramentas = new System.Windows.Forms.Button();
            this.butMapas = new System.Windows.Forms.Button();
            this.butClasses = new System.Windows.Forms.Button();
            this.Diretório_Servidor = new System.Windows.Forms.FolderBrowserDialog();
            this.butItens = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCliente_Diretório);
            this.groupBox1.Controls.Add(this.butSelecionarDiretório_Cliente);
            this.groupBox1.Controls.Add(this.butSelecionarDiretório_Servidor);
            this.groupBox1.Controls.Add(this.txtServidor_Diretório);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Diretórios";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Servidor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cliente";
            // 
            // txtCliente_Diretório
            // 
            this.txtCliente_Diretório.Location = new System.Drawing.Point(61, 19);
            this.txtCliente_Diretório.Name = "txtCliente_Diretório";
            this.txtCliente_Diretório.ReadOnly = true;
            this.txtCliente_Diretório.Size = new System.Drawing.Size(204, 20);
            this.txtCliente_Diretório.TabIndex = 3;
            // 
            // butSelecionarDiretório_Cliente
            // 
            this.butSelecionarDiretório_Cliente.Location = new System.Drawing.Point(271, 18);
            this.butSelecionarDiretório_Cliente.Name = "butSelecionarDiretório_Cliente";
            this.butSelecionarDiretório_Cliente.Size = new System.Drawing.Size(29, 21);
            this.butSelecionarDiretório_Cliente.TabIndex = 2;
            this.butSelecionarDiretório_Cliente.Text = "...";
            this.butSelecionarDiretório_Cliente.UseVisualStyleBackColor = true;
            this.butSelecionarDiretório_Cliente.Click += new System.EventHandler(this.butSelecionarDiretório_Cliente_Click);
            // 
            // butSelecionarDiretório_Servidor
            // 
            this.butSelecionarDiretório_Servidor.Location = new System.Drawing.Point(271, 45);
            this.butSelecionarDiretório_Servidor.Name = "butSelecionarDiretório_Servidor";
            this.butSelecionarDiretório_Servidor.Size = new System.Drawing.Size(29, 21);
            this.butSelecionarDiretório_Servidor.TabIndex = 4;
            this.butSelecionarDiretório_Servidor.Text = "...";
            this.butSelecionarDiretório_Servidor.UseVisualStyleBackColor = true;
            this.butSelecionarDiretório_Servidor.Click += new System.EventHandler(this.butSelecionarDiretório_Servidor_Click);
            // 
            // txtServidor_Diretório
            // 
            this.txtServidor_Diretório.Location = new System.Drawing.Point(61, 46);
            this.txtServidor_Diretório.Name = "txtServidor_Diretório";
            this.txtServidor_Diretório.ReadOnly = true;
            this.txtServidor_Diretório.Size = new System.Drawing.Size(204, 20);
            this.txtServidor_Diretório.TabIndex = 5;
            // 
            // Diretório_Cliente
            // 
            this.Diretório_Cliente.Description = "Selecione o diretório do cliente";
            this.Diretório_Cliente.Tag = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.butItens);
            this.groupBox2.Controls.Add(this.butNPCs);
            this.groupBox2.Controls.Add(this.butAzulejos);
            this.groupBox2.Controls.Add(this.butDados);
            this.groupBox2.Controls.Add(this.butFerramentas);
            this.groupBox2.Controls.Add(this.butMapas);
            this.groupBox2.Controls.Add(this.butClasses);
            this.groupBox2.Location = new System.Drawing.Point(12, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 121);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Editores";
            // 
            // butNPCs
            // 
            this.butNPCs.Location = new System.Drawing.Point(206, 51);
            this.butNPCs.Name = "butNPCs";
            this.butNPCs.Size = new System.Drawing.Size(94, 26);
            this.butNPCs.TabIndex = 11;
            this.butNPCs.Text = "NPCs";
            this.butNPCs.UseVisualStyleBackColor = true;
            this.butNPCs.Click += new System.EventHandler(this.butNPCs_Click);
            // 
            // butAzulejos
            // 
            this.butAzulejos.Location = new System.Drawing.Point(6, 51);
            this.butAzulejos.Name = "butAzulejos";
            this.butAzulejos.Size = new System.Drawing.Size(94, 26);
            this.butAzulejos.TabIndex = 10;
            this.butAzulejos.Text = "Azulejos";
            this.butAzulejos.UseVisualStyleBackColor = true;
            this.butAzulejos.Click += new System.EventHandler(this.butAzulejos_Click);
            // 
            // butDados
            // 
            this.butDados.Location = new System.Drawing.Point(6, 19);
            this.butDados.Name = "butDados";
            this.butDados.Size = new System.Drawing.Size(94, 26);
            this.butDados.TabIndex = 9;
            this.butDados.Text = "Dados";
            this.butDados.UseVisualStyleBackColor = true;
            this.butDados.Click += new System.EventHandler(this.butDados_Click);
            // 
            // butFerramentas
            // 
            this.butFerramentas.Location = new System.Drawing.Point(106, 19);
            this.butFerramentas.Name = "butFerramentas";
            this.butFerramentas.Size = new System.Drawing.Size(94, 26);
            this.butFerramentas.TabIndex = 8;
            this.butFerramentas.Text = "Ferramentas";
            this.butFerramentas.UseVisualStyleBackColor = true;
            this.butFerramentas.Click += new System.EventHandler(this.butFerramentas_Click);
            // 
            // butMapas
            // 
            this.butMapas.Location = new System.Drawing.Point(106, 51);
            this.butMapas.Name = "butMapas";
            this.butMapas.Size = new System.Drawing.Size(94, 26);
            this.butMapas.TabIndex = 7;
            this.butMapas.Text = "Mapas";
            this.butMapas.UseVisualStyleBackColor = true;
            this.butMapas.Click += new System.EventHandler(this.butMapas_Click);
            // 
            // butClasses
            // 
            this.butClasses.Location = new System.Drawing.Point(206, 19);
            this.butClasses.Name = "butClasses";
            this.butClasses.Size = new System.Drawing.Size(94, 26);
            this.butClasses.TabIndex = 6;
            this.butClasses.Text = "Classes";
            this.butClasses.UseVisualStyleBackColor = true;
            this.butClasses.Click += new System.EventHandler(this.butClasses_Click);
            // 
            // Diretório_Servidor
            // 
            this.Diretório_Servidor.Description = "Selecione o diretório do servidor";
            this.Diretório_Servidor.Tag = "";
            // 
            // butItens
            // 
            this.butItens.Location = new System.Drawing.Point(6, 83);
            this.butItens.Name = "butItens";
            this.butItens.Size = new System.Drawing.Size(94, 26);
            this.butItens.TabIndex = 12;
            this.butItens.Text = "Itens";
            this.butItens.UseVisualStyleBackColor = true;
            this.butItens.Click += new System.EventHandler(this.butItens_Click);
            // 
            // Seleção
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 232);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Seleção";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editores";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Seleção_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button butSelecionarDiretório_Cliente;
    public System.Windows.Forms.FolderBrowserDialog Diretório_Cliente;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button butSelecionarDiretório_Servidor;
    public System.Windows.Forms.FolderBrowserDialog Diretório_Servidor;
    public System.Windows.Forms.TextBox txtCliente_Diretório;
    public System.Windows.Forms.TextBox txtServidor_Diretório;
    private System.Windows.Forms.Button butClasses;
    private System.Windows.Forms.Button butMapas;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button butFerramentas;
    private System.Windows.Forms.Button butDados;
    private System.Windows.Forms.Button butAzulejos;
    private System.Windows.Forms.Button butNPCs;
    private System.Windows.Forms.Button butItens;
}