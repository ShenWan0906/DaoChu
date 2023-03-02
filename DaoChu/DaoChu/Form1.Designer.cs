namespace DaoChu
{
    partial class DaoChuTool
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TableList = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DBText = new System.Windows.Forms.TextBox();
            this.TestLianJie = new System.Windows.Forms.Button();
            this.DaoChuDto = new System.Windows.Forms.Button();
            this.DaoChuModel = new System.Windows.Forms.Button();
            this.DaoChuRepositories = new System.Windows.Forms.Button();
            this.DBLeiXing = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ServiceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CheckNone = new System.Windows.Forms.Button();
            this.DaoChuIRepositories = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库类型";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TableList
            // 
            this.TableList.FormattingEnabled = true;
            this.TableList.Location = new System.Drawing.Point(120, 272);
            this.TableList.Name = "TableList";
            this.TableList.Size = new System.Drawing.Size(258, 400);
            this.TableList.TabIndex = 3;
            this.TableList.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "数据库连接";
            // 
            // DBText
            // 
            this.DBText.Location = new System.Drawing.Point(120, 105);
            this.DBText.Multiline = true;
            this.DBText.Name = "DBText";
            this.DBText.Size = new System.Drawing.Size(754, 70);
            this.DBText.TabIndex = 19;
            this.DBText.TextChanged += new System.EventHandler(this.DBMing_TextChanged);
            // 
            // TestLianJie
            // 
            this.TestLianJie.Location = new System.Drawing.Point(899, 117);
            this.TestLianJie.Name = "TestLianJie";
            this.TestLianJie.Size = new System.Drawing.Size(75, 34);
            this.TestLianJie.TabIndex = 23;
            this.TestLianJie.Text = "测试连接";
            this.TestLianJie.UseVisualStyleBackColor = true;
            this.TestLianJie.Click += new System.EventHandler(this.TestLianJie_Click);
            // 
            // DaoChuDto
            // 
            this.DaoChuDto.Location = new System.Drawing.Point(458, 316);
            this.DaoChuDto.Name = "DaoChuDto";
            this.DaoChuDto.Size = new System.Drawing.Size(126, 34);
            this.DaoChuDto.TabIndex = 26;
            this.DaoChuDto.Text = "导出Dto";
            this.DaoChuDto.UseVisualStyleBackColor = true;
            this.DaoChuDto.Click += new System.EventHandler(this.DaoChuDto_Click);
            // 
            // DaoChuModel
            // 
            this.DaoChuModel.Location = new System.Drawing.Point(610, 316);
            this.DaoChuModel.Name = "DaoChuModel";
            this.DaoChuModel.Size = new System.Drawing.Size(116, 34);
            this.DaoChuModel.TabIndex = 27;
            this.DaoChuModel.Text = "导出Model";
            this.DaoChuModel.UseVisualStyleBackColor = true;
            this.DaoChuModel.Click += new System.EventHandler(this.DaoChuModel_Click);
            // 
            // DaoChuRepositories
            // 
            this.DaoChuRepositories.Location = new System.Drawing.Point(610, 418);
            this.DaoChuRepositories.Name = "DaoChuRepositories";
            this.DaoChuRepositories.Size = new System.Drawing.Size(116, 34);
            this.DaoChuRepositories.TabIndex = 29;
            this.DaoChuRepositories.Text = "导出Repositories";
            this.DaoChuRepositories.UseVisualStyleBackColor = true;
            this.DaoChuRepositories.Click += new System.EventHandler(this.DaoChuRepositories_Click);
            // 
            // DBLeiXing
            // 
            this.DBLeiXing.FormattingEnabled = true;
            this.DBLeiXing.Location = new System.Drawing.Point(120, 45);
            this.DBLeiXing.Name = "DBLeiXing";
            this.DBLeiXing.Size = new System.Drawing.Size(295, 25);
            this.DBLeiXing.TabIndex = 37;
            this.DBLeiXing.SelectedIndexChanged += new System.EventHandler(this.DBLeiXing_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 38;
            this.label2.Text = "服务名称";
            // 
            // ServiceName
            // 
            this.ServiceName.Location = new System.Drawing.Point(120, 215);
            this.ServiceName.Multiline = true;
            this.ServiceName.Name = "ServiceName";
            this.ServiceName.Size = new System.Drawing.Size(258, 29);
            this.ServiceName.TabIndex = 39;
            this.ServiceName.TextChanged += new System.EventHandler(this.ServiceName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(458, 643);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 40;
            this.label4.Text = "导出路径：桌面";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // CheckNone
            // 
            this.CheckNone.Location = new System.Drawing.Point(32, 272);
            this.CheckNone.Name = "CheckNone";
            this.CheckNone.Size = new System.Drawing.Size(75, 34);
            this.CheckNone.TabIndex = 42;
            this.CheckNone.Text = "全选";
            this.CheckNone.UseVisualStyleBackColor = true;
            this.CheckNone.Click += new System.EventHandler(this.CheckNone_Click);
            // 
            // DaoChuIRepositories
            // 
            this.DaoChuIRepositories.Location = new System.Drawing.Point(458, 418);
            this.DaoChuIRepositories.Name = "DaoChuIRepositories";
            this.DaoChuIRepositories.Size = new System.Drawing.Size(126, 34);
            this.DaoChuIRepositories.TabIndex = 45;
            this.DaoChuIRepositories.Text = "导出IRepositories";
            this.DaoChuIRepositories.UseVisualStyleBackColor = true;
            this.DaoChuIRepositories.Click += new System.EventHandler(this.DaoChuIRepositories_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(458, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(416, 17);
            this.label7.TabIndex = 46;
            this.label7.Text = "使用流程：输入数据库地址，点击测试连接，选择表，点击需要执行的操作。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(750, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 17);
            this.label5.TabIndex = 43;
            // 
            // DaoChuTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 695);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DaoChuIRepositories);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CheckNone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ServiceName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DBLeiXing);
            this.Controls.Add(this.DaoChuRepositories);
            this.Controls.Add(this.DaoChuModel);
            this.Controls.Add(this.DaoChuDto);
            this.Controls.Add(this.TestLianJie);
            this.Controls.Add(this.DBText);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TableList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "DaoChuTool";
            this.Text = "联众智慧-导出工具 V1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private CheckedListBox TableList;
        private Label label8;
        private TextBox DBText;
        private Button TestLianJie;
        private Button DaoChuDto;
        private Button DaoChuModel;
        private Button DaoChuRepositories;
        private ComboBox DBLeiXing;
        private Label label2;
        private TextBox ServiceName;
        private Label label4;
        private Button CheckNone;
        private Button DaoChuIRepositories;
        private Label label7;
        private Label label5;
    }
}