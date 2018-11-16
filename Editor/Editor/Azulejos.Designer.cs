partial class Editor_Azulejos
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
             scrlAzulejoX = new System.Windows.Forms.HScrollBar();
             scrlAzulejoY = new System.Windows.Forms.VScrollBar();
             picAzulejo = new System.Windows.Forms.PictureBox();
             grpAtributos = new System.Windows.Forms.GroupBox();
             optBloqueio = new System.Windows.Forms.RadioButton();
             butLimpar = new System.Windows.Forms.Button();
             butCancelar = new System.Windows.Forms.Button();
             butSalvar = new System.Windows.Forms.Button();
             grpAzulejo = new System.Windows.Forms.GroupBox();
             scrlAzulejo = new System.Windows.Forms.HScrollBar();
             groupBox2 = new System.Windows.Forms.GroupBox();
             optBloqDirecional = new System.Windows.Forms.RadioButton();
             optAtributos = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)( picAzulejo)).BeginInit();
             grpAtributos.SuspendLayout();
             grpAzulejo.SuspendLayout();
             groupBox2.SuspendLayout();
             SuspendLayout();
            // 
            // scrlAzulejoX
            // 
             scrlAzulejoX.LargeChange = 1;
             scrlAzulejoX.Location = new System.Drawing.Point(14, 453);
             scrlAzulejoX.Name = "scrlAzulejoX";
             scrlAzulejoX.Size = new System.Drawing.Size(256, 19);
             scrlAzulejoX.TabIndex = 69;
            // 
            // scrlAzulejoY
            // 
             scrlAzulejoY.Cursor = System.Windows.Forms.Cursors.Default;
             scrlAzulejoY.LargeChange = 1;
             scrlAzulejoY.Location = new System.Drawing.Point(270, 69);
             scrlAzulejoY.Maximum = 255;
             scrlAzulejoY.Name = "scrlAzulejoY";
             scrlAzulejoY.Size = new System.Drawing.Size(19, 384);
             scrlAzulejoY.TabIndex = 70;
            // 
            // picAzulejo
            // 
             picAzulejo.BackColor = System.Drawing.Color.Black;
             picAzulejo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
             picAzulejo.Location = new System.Drawing.Point(14, 69);
             picAzulejo.Name = "picAzulejo";
             picAzulejo.Size = new System.Drawing.Size(256, 384);
             picAzulejo.TabIndex = 68;
             picAzulejo.TabStop = false;
             picAzulejo.MouseDown += new System.Windows.Forms.MouseEventHandler( picAzulejo_MouseDown);
            // 
            // grpAtributos
            // 
             grpAtributos.Controls.Add( optBloqueio);
             grpAtributos.Location = new System.Drawing.Point(303, 141);
             grpAtributos.Name = "grpAtributos";
             grpAtributos.Size = new System.Drawing.Size(98, 264);
             grpAtributos.TabIndex = 71;
             grpAtributos.TabStop = false;
             grpAtributos.Text = "Atributos";
            // 
            // optBloqueio
            // 
             optBloqueio.AutoSize = true;
             optBloqueio.Location = new System.Drawing.Point(6, 19);
             optBloqueio.Name = "optBloqueio";
             optBloqueio.Size = new System.Drawing.Size(66, 17);
             optBloqueio.TabIndex = 75;
             optBloqueio.TabStop = true;
             optBloqueio.Text = "Bloqueio";
             optBloqueio.UseVisualStyleBackColor = true;
             optBloqueio.CheckedChanged += new System.EventHandler( optBloqueio_CheckedChanged);
            // 
            // butLimpar
            // 
             butLimpar.Location = new System.Drawing.Point(303, 431);
             butLimpar.Name = "butLimpar";
             butLimpar.Size = new System.Drawing.Size(97, 21);
             butLimpar.TabIndex = 74;
             butLimpar.Text = "Limpar";
             butLimpar.UseVisualStyleBackColor = true;
             butLimpar.Click += new System.EventHandler( butLimpar_Click);
            // 
            // butCancelar
            // 
             butCancelar.Location = new System.Drawing.Point(303, 451);
             butCancelar.Name = "butCancelar";
             butCancelar.Size = new System.Drawing.Size(97, 21);
             butCancelar.TabIndex = 73;
             butCancelar.Text = "Cancelar";
             butCancelar.UseVisualStyleBackColor = true;
             butCancelar.Click += new System.EventHandler( butCancelar_Click);
            // 
            // butSalvar
            // 
             butSalvar.Location = new System.Drawing.Point(303, 411);
             butSalvar.Name = "butSalvar";
             butSalvar.Size = new System.Drawing.Size(97, 21);
             butSalvar.TabIndex = 72;
             butSalvar.Text = "Salvar";
             butSalvar.UseVisualStyleBackColor = true;
             butSalvar.Click += new System.EventHandler( butSalvar_Click);
            // 
            // grpAzulejo
            // 
             grpAzulejo.Controls.Add( scrlAzulejo);
             grpAzulejo.Location = new System.Drawing.Point(12, 12);
             grpAzulejo.Name = "grpAzulejo";
             grpAzulejo.Size = new System.Drawing.Size(389, 49);
             grpAzulejo.TabIndex = 75;
             grpAzulejo.TabStop = false;
             grpAzulejo.Text = "Azulejo: 1";
            // 
            // scrlAzulejo
            // 
             scrlAzulejo.LargeChange = 1;
             scrlAzulejo.Location = new System.Drawing.Point(9, 18);
             scrlAzulejo.Minimum = 1;
             scrlAzulejo.Name = "scrlAzulejo";
             scrlAzulejo.Size = new System.Drawing.Size(374, 19);
             scrlAzulejo.TabIndex = 16;
             scrlAzulejo.Value = 1;
             scrlAzulejo.ValueChanged += new System.EventHandler( scrlAzulejo_ValueChanged);
            // 
            // groupBox2
            // 
             groupBox2.Controls.Add( optAtributos);
             groupBox2.Controls.Add( optBloqDirecional);
             groupBox2.Location = new System.Drawing.Point(303, 69);
             groupBox2.Name = "groupBox2";
             groupBox2.Size = new System.Drawing.Size(98, 66);
             groupBox2.TabIndex = 76;
             groupBox2.TabStop = false;
             groupBox2.Text = "Definir";
            // 
            // optBloqDirecional
            // 
             optBloqDirecional.BackColor = System.Drawing.Color.Transparent;
             optBloqDirecional.Location = new System.Drawing.Point(6, 42);
             optBloqDirecional.Name = "optBloqDirecional";
             optBloqDirecional.Size = new System.Drawing.Size(91, 17);
             optBloqDirecional.TabIndex = 76;
             optBloqDirecional.Text = "Bloqueio Dire.";
             optBloqDirecional.UseVisualStyleBackColor = false;
             optBloqDirecional.CheckedChanged += new System.EventHandler( optBloqDirecional_CheckedChanged);
            // 
            // optAtributos
            // 
             optAtributos.AutoSize = true;
             optAtributos.Checked = true;
             optAtributos.Location = new System.Drawing.Point(6, 19);
             optAtributos.Name = "optAtributos";
             optAtributos.Size = new System.Drawing.Size(66, 17);
             optAtributos.TabIndex = 75;
             optAtributos.TabStop = true;
             optAtributos.Text = "Atributos";
             optAtributos.UseVisualStyleBackColor = true;
             optAtributos.CheckedChanged += new System.EventHandler( optAtributos_CheckedChanged);
            // 
            // Editor_Azulejos
            // 
             AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
             AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             ClientSize = new System.Drawing.Size(413, 487);
             ControlBox = false;
             Controls.Add( groupBox2);
             Controls.Add( butCancelar);
             Controls.Add( grpAzulejo);
             Controls.Add( butLimpar);
             Controls.Add( butSalvar);
             Controls.Add( grpAtributos);
             Controls.Add( scrlAzulejoX);
             Controls.Add( scrlAzulejoY);
             Controls.Add( picAzulejo);
             FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
             Name = "Editor_Azulejos";
             StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
             Text = "Editor de Azulejos";
            ((System.ComponentModel.ISupportInitialize)( picAzulejo)).EndInit();
             grpAtributos.ResumeLayout(false);
             grpAtributos.PerformLayout();
             grpAzulejo.ResumeLayout(false);
             groupBox2.ResumeLayout(false);
             groupBox2.PerformLayout();
             ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.HScrollBar scrlAzulejoX;
    public System.Windows.Forms.VScrollBar scrlAzulejoY;
    public System.Windows.Forms.PictureBox picAzulejo;
    private System.Windows.Forms.GroupBox grpAtributos;
    private System.Windows.Forms.Button butLimpar;
    private System.Windows.Forms.Button butCancelar;
    private System.Windows.Forms.Button butSalvar;
    private System.Windows.Forms.RadioButton optBloqueio;
    private System.Windows.Forms.GroupBox grpAzulejo;
    public System.Windows.Forms.HScrollBar scrlAzulejo;
    private System.Windows.Forms.GroupBox groupBox2;
    public System.Windows.Forms.RadioButton optBloqDirecional;
    public System.Windows.Forms.RadioButton optAtributos;
}