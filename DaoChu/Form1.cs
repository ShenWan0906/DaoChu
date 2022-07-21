using Dapper.Oracle;
using ModelConvertToDto.Helpers;
using System.Collections;
using System.Data;

namespace DaoChu
{
    public partial class DaoChuTool : Form
    {
        public DaoChuTool()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new();

            dt.Columns.Add("name");

            dt.Columns.Add("value");

            DataRow dr = dt.NewRow();

            dr[0] = "Oracle";

            dr[1] = "Oracle";

            dt.Rows.Add(dr);

            this.DBLeiXing.DataSource = dt;

            this.DBLeiXing.DisplayMember = "name";

            this.DBLeiXing.ValueMember = "value";

            DBText.Text = "DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.80.161)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));PASSWORD=mcis;PERSIST SECURITY INFO=True;USER ID=mcis; enlist=dynamic;"; // 默认连接地址
            ServiceName.Text = "Mediinfo.MCIS.ErTongBJ.ORM"; // 服务名称
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestLianJie_Click(object sender, EventArgs e)
        {
            TestDB();
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBLeiXing_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DaoChuModel_Click(object sender, EventArgs e)
        {
            try
            {
                var serviceName = ServiceName.Text;
                if (!string.IsNullOrEmpty(serviceName))
                {
                    LogTiShi("开始导出");
                    GetDBData();
                    // 导出指定表Model
                    CreateClassHelper.SaveTableModel(serviceName, "EB_ZP_ZHAOPIANXX", "\\DownFile\\FuYou");//生成库里面所有Dto
                    LogTiShi("导出成功");
                }
                else
                {
                    LogTiShi("服务名不能为空");
                }
            }
            catch (Exception ex)
            {
                LogTiShi("导出失败：" + ex);
                throw;
            }
        }

        private void DBMing_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            LogText.Text = null;
        }

        private void GetDBData()
        {
            var text = DBText.Text; // 数据库名
            // 数据库配配置
            CreateClassHelper.Init(new DapperOracleHelper(text));
        }

        /// <summary>
        /// 提醒
        /// </summary>
        private void LogTiShi(string msg)
        {
            LogText.Text = null;
            this.BeginInvoke((Action)(() =>
            {
                LogText.AppendText(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
                LogText.AppendText(Environment.NewLine);
                LogText.ScrollToCaret();
            }));
        }

        private void label4_Click(object sender, EventArgs e)
        {
             
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TableList.Items.Count; i++)
            {
                if (TableList.GetItemChecked(i))
                {
                    TableList.SetItemChecked(i, false);
                }
                else
                {
                    TableList.SetItemChecked(i, true);
                }
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            TestDB();
        }

        private void TestDB() 
        {
            try
            {
                var leiXing = DBLeiXing.Text; // 数据库类型
                var serviceName = ServiceName.Text; // 服务名称
                TableList.Items.Clear();
                if (leiXing == "Oracle")
                {
                    GetDBData();
                    var resultList = CreateClassHelper.TestLianJie(serviceName);
                    if (resultList != null && resultList.Count > 0)
                    {
                        resultList.ForEach(item =>
                        {
                            if (!item.TableName.Contains("=="))
                            {
                                TableList.Items.Add(item.TableName);
                            }
                        });
                        LogTiShi("连接成功！");
                    }
                    else
                    {
                        LogTiShi("连接失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                LogTiShi("数据库连接失败：" + ex);
            }
        }
    }
}