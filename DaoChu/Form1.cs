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

            this.label4.Text = "导出地址：" + GetZhuoMianURL();

            DBText.Text = "DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.80.161)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));PASSWORD=mcis;PERSIST SECURITY INFO=True;USER ID=mcis; enlist=dynamic;"; // 默认连接地址
            ServiceName.Text = "Mediinfo.MCIS.ErTongBJ"; // 默认服务名称
        }

        /// <summary>
        /// 查询全部表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            LogTiShi("下个版本更新，敬请期待");
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

        private void DBLeiXing_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DBMing_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
             
        }


        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestLianJie_Click(object sender, EventArgs e)
        {
            LogTiShi("正在连接数据库...");
            TestDB();
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

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Click(object sender, EventArgs e)
        {
            TestDB();
        }

        /// <summary>
        /// 导出Repositories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuRepositories_Click(object sender, EventArgs e)
        {
            try
            {
                var serviceName = ServiceName.Text;
                if (!string.IsNullOrEmpty(serviceName))
                {
                    GetDBData();
                    // 获取制定的表
                    var dbList = GetCheckList();
                    if (dbList != null && dbList.Count > 0)
                    {
                        LogTiShi("开始导出Repositories");
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableListRepositories(serviceName, item, GetZhuoMianURL());
                            LogTiShi(item + "导出Repositories成功");
                        });
                    }
                    else
                    {
                        LogTiShi("请点击测试连接，然后选择要导出的表");
                    }
                }
                else
                {
                    LogTiShi("服务名不能为空");
                }
            }
            catch (Exception ex)
            {
                LogTiShi("导出Repositories失败：" + ex);
                throw;
            }
        }

        /// <summary>
        /// 导出Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuModel_Click(object sender, EventArgs e)
        {
            try
            {
                var serviceName = ServiceName.Text;
                if (!string.IsNullOrEmpty(serviceName))
                {
                    GetDBData();
                    // 获取制定的表
                    var dbList = GetCheckList();
                    if (dbList != null && dbList.Count > 0)
                    {
                        LogTiShi("开始导出Model");
                        dbList.ForEach(item => 
                        {
                            CreateClassHelper.SaveTableModel(serviceName, item, GetZhuoMianURL());
                            LogTiShi(item + "导出Model成功");
                        });
                    }
                    else
                    {
                        LogTiShi("请点击测试连接，然后选择要导出的表");
                    }
                }
                else
                {
                    LogTiShi("服务名不能为空");
                }
            }
            catch (Exception ex)
            {
                LogTiShi("导出Model失败：" + ex);
                throw;
            }
        }

        /// <summary>
        /// 导出Context
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoContext_Click(object sender, EventArgs e)
        {
            try
            {
                var serviceName = ServiceName.Text;
                if (!string.IsNullOrEmpty(serviceName))
                {
                    GetDBData();
                    // 获取制定的表
                    var dbList = GetCheckList();
                    if (dbList != null && dbList.Count > 0)
                    {
                        LogTiShi("开始导出DbContext");
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableListDbContext(serviceName, item, GetZhuoMianURL());
                            LogTiShi(item + "导出DbContext成功");
                        });
                    }
                    else
                    {
                        LogTiShi("请点击测试连接，然后选择要导出的表");
                    }
                }
                else
                {
                    LogTiShi("服务名不能为空");
                }
            }
            catch (Exception ex)
            {
                LogTiShi("导出DbContext失败：" + ex);
                throw;
            }
        }

        /// <summary>
        /// 导出Dto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuDto_Click(object sender, EventArgs e)
        {
            try
            {
                var serviceName = ServiceName.Text;
                if (!string.IsNullOrEmpty(serviceName))
                {
                    GetDBData();
                    // 获取制定的表
                    var dbList = GetCheckList();
                    if (dbList != null && dbList.Count > 0)
                    {
                        LogTiShi("开始导出Dto");
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableDto(serviceName, item, GetZhuoMianURL());
                            LogTiShi(item + "导出Dto成功");
                        });
                    }
                    else
                    {
                        LogTiShi("请点击测试连接，然后选择要导出的表");
                    }
                }
                else
                {
                    LogTiShi("服务名不能为空");
                }
            }
            catch (Exception ex)
            {
                LogTiShi("导出Dto失败：" + ex);
                throw;
            }
        }

        /// <summary>
        /// 导出Services
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuServices_Click(object sender, EventArgs e)
        {
            LogTiShi("下个版本更新，敬请期待");
        }

        /// <summary>
        /// 导出Controllers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoControllers_Click(object sender, EventArgs e)
        {
            LogTiShi("下个版本更新，敬请期待");
        }

        /// <summary>
        /// 日志
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

        /// <summary>
        /// 清除日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearLog_Click(object sender, EventArgs e)
        {
            LogText.Text = null;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
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

        /// <summary>
        /// 连接数据库
        /// </summary>
        private void GetDBData()
        {
            var text = DBText.Text; // 数据库名
            // 数据库配配置
            CreateClassHelper.Init(new DapperOracleHelper(text));
        }

        /// <summary>
        /// 获取选中的值
        /// </summary>
        /// <returns></returns>
        private List<string> GetCheckList()
        {
            List<string> result = new List<string>();
            IEnumerator myEnumerator = TableList.CheckedIndices.GetEnumerator();
            int index;
            while (myEnumerator.MoveNext())
            {
                index = (int)myEnumerator.Current;
                TableList.SelectedItem = TableList.Items[index];
                result.Add(TableList.Text);
            }
            return result;
        }

        /// <summary>
        /// 获取桌面默认地址
        /// </summary>
        /// <returns></returns>
        private string GetZhuoMianURL()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            return path;
        }

        /// <summary>
        /// 查询全部视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetAllView_Click(object sender, EventArgs e)
        {
            LogTiShi("下个版本更新，敬请期待");
        }

        /// <summary>
        /// 查询指定表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetTable_Click(object sender, EventArgs e)
        {
            LogTiShi("下个版本更新，敬请期待");
        }

        /// <summary>
        /// 查询指定视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetView_Click(object sender, EventArgs e)
        {
            LogTiShi("下个版本更新，敬请期待");
        }
    }
}