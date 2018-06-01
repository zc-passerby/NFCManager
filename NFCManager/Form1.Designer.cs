namespace NFCManager
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.JustForSkin = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.writeCard = new System.Windows.Forms.Button();
            this.devNo = new System.Windows.Forms.TextBox();
            this.memberNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.readCard = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(659, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // JustForSkin
            // 
            this.JustForSkin.FormattingEnabled = true;
            this.JustForSkin.Location = new System.Drawing.Point(523, 2);
            this.JustForSkin.Name = "JustForSkin";
            this.JustForSkin.Size = new System.Drawing.Size(121, 20);
            this.JustForSkin.TabIndex = 1;
            this.JustForSkin.SelectedIndexChanged += new System.EventHandler(this.JustForSkin_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.writeCard);
            this.groupBox1.Controls.Add(this.devNo);
            this.groupBox1.Controls.Add(this.memberNo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.readCard);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 323);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // writeCard
            // 
            this.writeCard.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.writeCard.Location = new System.Drawing.Point(441, 246);
            this.writeCard.Name = "writeCard";
            this.writeCard.Size = new System.Drawing.Size(101, 57);
            this.writeCard.TabIndex = 11;
            this.writeCard.Text = "写卡";
            this.writeCard.UseVisualStyleBackColor = true;
            this.writeCard.Click += new System.EventHandler(this.writeCard_Click);
            // 
            // devNo
            // 
            this.devNo.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.devNo.Location = new System.Drawing.Point(227, 205);
            this.devNo.MaxLength = 5;
            this.devNo.Name = "devNo";
            this.devNo.Size = new System.Drawing.Size(138, 31);
            this.devNo.TabIndex = 10;
            this.devNo.TextChanged += new System.EventHandler(this.setNumberRange);
            this.devNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
            // 
            // memberNo
            // 
            this.memberNo.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.memberNo.Location = new System.Drawing.Point(227, 37);
            this.memberNo.MaxLength = 4;
            this.memberNo.Name = "memberNo";
            this.memberNo.Size = new System.Drawing.Size(138, 31);
            this.memberNo.TabIndex = 9;
            this.memberNo.TextChanged += new System.EventHandler(this.setNumberRange);
            this.memberNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(365, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "（范围00001-99999）";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(371, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "（范围0001-9999）";
            // 
            // readCard
            // 
            this.readCard.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.readCard.Location = new System.Drawing.Point(103, 246);
            this.readCard.Name = "readCard";
            this.readCard.Size = new System.Drawing.Size(101, 57);
            this.readCard.TabIndex = 5;
            this.readCard.Text = "读卡";
            this.readCard.UseVisualStyleBackColor = true;
            this.readCard.Click += new System.EventHandler(this.readCard_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2.Location = new System.Drawing.Point(92, 154);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(406, 25);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "一卡对一机：请输入遥控器五位数字编号";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioChecked);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(92, 96);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(133, 25);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "一卡对多机";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioChecked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(85, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "配卡人员工号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(146, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "遥控器权限卡管理系统";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.JustForSkin);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "读写卡管理器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox JustForSkin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox devNo;
        private System.Windows.Forms.TextBox memberNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button readCard;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button writeCard;
    }
}

