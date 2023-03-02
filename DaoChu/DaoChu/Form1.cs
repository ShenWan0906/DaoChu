using Dapper.Oracle;
using Loading;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic.ApplicationServices;
using ModelConvertToDto.Helpers;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using View;

namespace DaoChu
{
    public partial class DaoChuTool : Form
    {
        public DaoChuTool()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 缓冲绘制
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region 头部数据库连接
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestLianJie_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoading("正在连接数据库，请稍后", this, (obj) =>
            {
                TestDB();
            });
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
                    }
                    else
                    {
                        MessageBox.Show("连接失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败：" + ex);
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
        #endregion

        #region 左侧列表
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
        #endregion

        #region 右侧导出
        /// <summary>
        /// 导出Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuModel_Click(object sender, EventArgs e)
        {
            // 获取服务名
            var serviceName = ServiceName.Text;
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("服务名不能为空");
                return;
            }
            // 获取制定的表
            var dbList = GetCheckList();
            if (dbList.Count == 0)
            {
                MessageBox.Show("请选择要导出的表");
                return;
            }
            else
            {
                LoadingHelper.ShowLoading("正在导出Model，请稍后", this, (obj) =>
                {
                    try
                    {
                        GetDBData();
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableModel(serviceName, item, GetZhuoMianURL());
                        });
                        MessageBox.Show("Repositories导出完毕");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出Repositories失败：" + ex);
                    }
                });
            }
        }

        /// <summary>
        /// 导出Dto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuDto_Click(object sender, EventArgs e)
        {
            // 获取服务名
            var serviceName = ServiceName.Text;
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("服务名不能为空");
                return;
            }
            // 获取制定的表
            var dbList = GetCheckList();
            if (dbList.Count == 0)
            {
                MessageBox.Show("请选择要导出的表");
                return;
            }
            LoadingHelper.ShowLoading("正在导出Dto，请稍后", this, (obj) =>
            {
                try
                {
                    GetDBData();
                    dbList.ForEach(item =>
                    {
                        CreateClassHelper.SaveTableDto(serviceName, item, GetZhuoMianURL());
                    });
                    MessageBox.Show("Dto导出完毕");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出Repositories失败：" + ex);
                }
            });
        }

        /// <summary>
        /// 导出IRepositories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuIRepositories_Click(object sender, EventArgs e)
        {
            // 获取服务名
            var serviceName = ServiceName.Text;
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("服务名不能为空");
                return;
            }
            // 获取制定的表
            var dbList = GetCheckList();
            if (dbList.Count == 0)
            {
                MessageBox.Show("请选择要导出的表");
                return;
            }
            else
            {
                LoadingHelper.ShowLoading("正在导出IRepositories，请稍后", this, (obj) =>
                {
                    try
                    {
                        GetDBData();
                        // 获取制定的表
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableListIRepositories(serviceName, item, GetZhuoMianURL());
                        });
                        MessageBox.Show("Repositories导出完毕");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出Repositories失败：" + ex);
                    }
                });
            }
        }

        /// <summary>
        /// 导出Repositories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuRepositories_Click(object sender, EventArgs e)
        {
            // 获取服务名
            var serviceName = ServiceName.Text;
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("服务名不能为空");
                return;
            }
            // 获取制定的表
            var dbList = GetCheckList();
            if (dbList.Count == 0)
            {
                MessageBox.Show("请选择要导出的表");
                return;
            }
            else
            {
                LoadingHelper.ShowLoading("正在导出Repositories，请稍后", this, (obj) =>
                {
                    try
                    {
                        GetDBData();
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableListRepositories(serviceName, item, GetZhuoMianURL());
                        });
                        MessageBox.Show("Repositories导出完毕");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出Repositories失败：" + ex);
                    }
                });
            }
        }
        #endregion

        #region 公用接口
        /// <summary>
        /// 获取桌面默认地址
        /// </summary>
        /// <returns></returns>
        private string GetZhuoMianURL()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            return path;
        }
        #endregion

        #region 页面控件 
        private void ServiceName_TextChanged(object sender, EventArgs e)
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

        private void DBLeiXing_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DBMing_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void TableName_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region 待实现
        /// <summary>
        /// 导出Services
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuServices_Click(object sender, EventArgs e)
        {
            MessageBox.Show("下个版本更新，敬请期待");
        }

        /// <summary>
        /// 导出IServices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuIServices_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导出Controllers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoControllers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("下个版本更新，敬请期待");
        }
        #endregion
    }
}