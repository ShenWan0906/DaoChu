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
            this.label3 = new System.Windows.Forms.Label();
            this.LogText = new System.Windows.Forms.TextBox();
            this.GetAllView = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.DBText = new System.Windows.Forms.TextBox();
            this.ClearLog = new System.Windows.Forms.Button();
            this.TestLianJie = new System.Windows.Forms.Button();
            this.Refresh = new System.Windows.Forms.Button();
            this.DaoChuDto = new System.Windows.Forms.Button();
            this.DaoChuModel = new System.Windows.Forms.Button();
            this.DaoChuServices = new System.Windows.Forms.Button();
            this.GetAllTable = new System.Windows.Forms.Button();
            this.DaoControllers = new System.Windows.Forms.Button();
            this.DaoContext = new System.Windows.Forms.Button();
            this.TableName = new System.Windows.Forms.TextBox();
            this.ViewName = new System.Windows.Forms.TextBox();
            this.GetTable = new System.Windows.Forms.Button();
            this.GetView = new System.Windows.Forms.Button();
            this.DaoChuRepositories = new System.Windows.Forms.Button();
            this.DBLeiXing = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ServiceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CheckNone = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DaoChuIRepositories = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 754);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "日志";
            // 
            // LogText
            // 
            this.LogText.Location = new System.Drawing.Point(120, 689);
            this.LogText.Multiline = true;
            this.LogText.Name = "LogText";
            this.LogText.ReadOnly = true;
            this.LogText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogText.Size = new System.Drawing.Size(733, 110);
            this.LogText.TabIndex = 6;
            // 
            // GetAllView
            // 
            this.GetAllView.Location = new System.Drawing.Point(645, 272);
            this.GetAllView.Name = "GetAllView";
            this.GetAllView.Size = new System.Drawing.Size(108, 34);
            this.GetAllView.TabIndex = 7;
            this.GetAllView.Text = "查询全部视图";
            this.GetAllView.UseVisualStyleBackColor = true;
            this.GetAllView.Click += new System.EventHandler(this.GetAllView_Click);
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
            // ClearLog
            // 
            this.ClearLog.Location = new System.Drawing.Point(899, 700);
            this.ClearLog.Name = "ClearLog";
            this.ClearLog.Size = new System.Drawing.Size(75, 34);
            this.ClearLog.TabIndex = 22;
            this.ClearLog.Text = "清除日志";
            this.ClearLog.UseVisualStyleBackColor = true;
            this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
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
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(39, 272);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(75, 34);
            this.Refresh.TabIndex = 24;
            this.Refresh.Text = "刷新";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // DaoChuDto
            // 
            this.DaoChuDto.Location = new System.Drawing.Point(637, 532);
            this.DaoChuDto.Name = "DaoChuDto";
            this.DaoChuDto.Size = new System.Drawing.Size(116, 34);
            this.DaoChuDto.TabIndex = 26;
            this.DaoChuDto.Text = "导出Dto";
            this.DaoChuDto.UseVisualStyleBackColor = true;
            this.DaoChuDto.Click += new System.EventHandler(this.DaoChuDto_Click);
            // 
            // DaoChuModel
            // 
            this.DaoChuModel.Location = new System.Drawing.Point(467, 532);
            this.DaoChuModel.Name = "DaoChuModel";
            this.DaoChuModel.Size = new System.Drawing.Size(126, 34);
            this.DaoChuModel.TabIndex = 27;
            this.DaoChuModel.Text = "导出Model";
            this.DaoChuModel.UseVisualStyleBackColor = true;
            this.DaoChuModel.Click += new System.EventHandler(this.DaoChuModel_Click);
            // 
            // DaoChuServices
            // 
            this.DaoChuServices.Location = new System.Drawing.Point(637, 457);
            this.DaoChuServices.Name = "DaoChuServices";
            this.DaoChuServices.Size = new System.Drawing.Size(116, 34);
            this.DaoChuServices.TabIndex = 28;
            this.DaoChuServices.Text = "导出Services";
            this.DaoChuServices.UseVisualStyleBackColor = true;
            this.DaoChuServices.Click += new System.EventHandler(this.DaoChuServices_Click);
            // 
            // GetAllTable
            // 
            this.GetAllTable.Location = new System.Drawing.Point(467, 272);
            this.GetAllTable.Name = "GetAllTable";
            this.GetAllTable.Size = new System.Drawing.Size(116, 34);
            this.GetAllTable.TabIndex = 2;
            this.GetAllTable.Text = "查询全部表";
            this.GetAllTable.UseVisualStyleBackColor = true;
            this.GetAllTable.Click += new System.EventHandler(this.button1_Click);
            // 
            // DaoControllers
            // 
            this.DaoControllers.Location = new System.Drawing.Point(467, 457);
            this.DaoControllers.Name = "DaoControllers";
            this.DaoControllers.Size = new System.Drawing.Size(126, 34);
            this.DaoControllers.TabIndex = 30;
            this.DaoControllers.Text = "导出Controllers";
            this.DaoControllers.UseVisualStyleBackColor = true;
            this.DaoControllers.Click += new System.EventHandler(this.DaoControllers_Click);
            // 
            // DaoContext
            // 
            this.DaoContext.Location = new System.Drawing.Point(808, 457);
            this.DaoContext.Name = "DaoContext";
            this.DaoContext.Size = new System.Drawing.Size(116, 34);
            this.DaoContext.TabIndex = 31;
            this.DaoContext.Text = "导出Context";
            this.DaoContext.UseVisualStyleBackColor = true;
            this.DaoContext.Click += new System.EventHandler(this.DaoContext_Click);
            // 
            // TableName
            // 
            this.TableName.Location = new System.Drawing.Point(467, 340);
            this.TableName.Multiline = true;
            this.TableName.Name = "TableName";
            this.TableName.Size = new System.Drawing.Size(286, 29);
            this.TableName.TabIndex = 32;
            // 
            // ViewName
            // 
            this.ViewName.Location = new System.Drawing.Point(467, 395);
            this.ViewName.Multiline = true;
            this.ViewName.Name = "ViewName";
            this.ViewName.Size = new System.Drawing.Size(286, 29);
            this.ViewName.TabIndex = 33;
            // 
            // GetTable
            // 
            this.GetTable.Location = new System.Drawing.Point(766, 335);
            this.GetTable.Name = "GetTable";
            this.GetTable.Size = new System.Drawing.Size(108, 34);
            this.GetTable.TabIndex = 34;
            this.GetTable.Text = "查询指定表";
            this.GetTable.UseVisualStyleBackColor = true;
            this.GetTable.Click += new System.EventHandler(this.GetTable_Click);
            // 
            // GetView
            // 
            this.GetView.Location = new System.Drawing.Point(766, 395);
            this.GetView.Name = "GetView";
            this.GetView.Size = new System.Drawing.Size(108, 34);
            this.GetView.TabIndex = 35;
            this.GetView.Text = "查询指定视图";
            this.GetView.UseVisualStyleBackColor = true;
            this.GetView.Click += new System.EventHandler(this.GetView_Click);
            // 
            // DaoChuRepositories
            // 
            this.DaoChuRepositories.Location = new System.Drawing.Point(637, 606);
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
            this.ServiceName.Size = new System.Drawing.Size(624, 29);
            this.ServiceName.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 814);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 40;
            this.label4.Text = "导出路径：桌面";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // CheckNone
            // 
            this.CheckNone.Location = new System.Drawing.Point(39, 350);
            this.CheckNone.Name = "CheckNone";
            this.CheckNone.Size = new System.Drawing.Size(75, 34);
            this.CheckNone.TabIndex = 42;
            this.CheckNone.Text = "全选";
            this.CheckNone.UseVisualStyleBackColor = true;
            this.CheckNone.Click += new System.EventHandler(this.CheckNone_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(750, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 17);
            this.label5.TabIndex = 43;
            this.label5.Text = "(多个用英文逗号隔开)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(320, 814);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 17);
            this.label6.TabIndex = 44;
            this.label6.Text = "当前版本 V1.0.1";
            // 
            // DaoChuIRepositories
            // 
            this.DaoChuIRepositories.Location = new System.Drawing.Point(467, 606);
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
            // DaoChuTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 850);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DaoChuIRepositories);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CheckNone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ServiceName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DBLeiXing);
            this.Controls.Add(this.GetView);
            this.Controls.Add(this.GetTable);
            this.Controls.Add(this.ViewName);
            this.Controls.Add(this.TableName);
            this.Controls.Add(this.DaoContext);
            this.Controls.Add(this.DaoControllers);
            this.Controls.Add(this.DaoChuRepositories);
            this.Controls.Add(this.DaoChuServices);
            this.Controls.Add(this.DaoChuModel);
            this.Controls.Add(this.DaoChuDto);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.TestLianJie);
            this.Controls.Add(this.ClearLog);
            this.Controls.Add(this.DBText);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.GetAllView);
            this.Controls.Add(this.LogText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TableList);
            this.Controls.Add(this.GetAllTable);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DaoChuTool";
            this.Text = "导出工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private CheckedListBox TableList;
        private Label label3;
        private TextBox LogText;
        private Button GetAllView;
        private Label label8;
        private TextBox DBText;
        private Button ClearLog;
        private Button TestLianJie;
        private Button Refresh;
        private Button DaoChuDto;
        private Button DaoChuModel;
        private Button DaoChuServices;
        private Button GetAllTable;
        private Button DaoControllers;
        private Button DaoContext;
        private TextBox TableName;
        private TextBox ViewName;
        private Button GetTable;
        private Button GetView;
        private Button DaoChuRepositories;
        private ComboBox DBLeiXing;
        private Label label2;
        private TextBox ServiceName;
        private Label label4;
        private Button CheckNone;
        private Label label5;
        private Label label6;
        private Button DaoChuIRepositories;
        private Label label7;
    }
}