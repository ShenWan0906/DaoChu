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
        /// �������
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // ��˫������ƴ��ڵ������ӿؼ�
                return cp;
            }
        }

        /// <summary>
        /// ��ʼ������
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

            this.label4.Text = "������ַ��" + GetZhuoMianURL();

            DBText.Text = "DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.80.161)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));PASSWORD=mcis;PERSIST SECURITY INFO=True;USER ID=mcis; enlist=dynamic;"; // Ĭ�����ӵ�ַ
            ServiceName.Text = "Mediinfo.MCIS.ErTongBJ"; // Ĭ�Ϸ�������
        }

        #region ͷ�����ݿ�����
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestLianJie_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoading("�����������ݿ⣬���Ժ�", this, (obj) =>
            {
                TestDB();
            });
        }

        /// <summary>
        /// �������ݿ�
        /// </summary>
        private void TestDB()
        {
            try
            {
                var leiXing = DBLeiXing.Text; // ���ݿ�����
                var serviceName = ServiceName.Text; // ��������
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
                        MessageBox.Show("����ʧ�ܣ�");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("���ݿ�����ʧ�ܣ�" + ex);
            }
        }

        /// <summary>
        /// �������ݿ�
        /// </summary>
        private void GetDBData()
        {
            var text = DBText.Text; // ���ݿ���
            // ���ݿ�������
            CreateClassHelper.Init(new DapperOracleHelper(text));
        }
        #endregion

        #region ����б�
        /// <summary>
        /// ȫѡ
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
        /// ��ȡѡ�е�ֵ
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

        #region �Ҳർ��
        /// <summary>
        /// ����Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuModel_Click(object sender, EventArgs e)
        {
            // ��ȡ������
            var serviceName = ServiceName.Text;
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("����������Ϊ��");
                return;
            }
            // ��ȡ�ƶ��ı�
            var dbList = GetCheckList();
            if (dbList.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫ�����ı�");
                return;
            }
            else
            {
                LoadingHelper.ShowLoading("���ڵ���Model�����Ժ�", this, (obj) =>
                {
                    try
                    {
                        GetDBData();
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableModel(serviceName, item, GetZhuoMianURL());
                        });
                        MessageBox.Show("Repositories�������");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("����Repositoriesʧ�ܣ�" + ex);
                    }
                });
            }
        }

        /// <summary>
        /// ����Dto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuDto_Click(object sender, EventArgs e)
        {
            // ��ȡ������
            var serviceName = ServiceName.Text;
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("����������Ϊ��");
                return;
            }
            // ��ȡ�ƶ��ı�
            var dbList = GetCheckList();
            if (dbList.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫ�����ı�");
                return;
            }
            LoadingHelper.ShowLoading("���ڵ���Dto�����Ժ�", this, (obj) =>
            {
                try
                {
                    GetDBData();
                    dbList.ForEach(item =>
                    {
                        CreateClassHelper.SaveTableDto(serviceName, item, GetZhuoMianURL());
                    });
                    MessageBox.Show("Dto�������");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("����Repositoriesʧ�ܣ�" + ex);
                }
            });
        }

        /// <summary>
        /// ����IRepositories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuIRepositories_Click(object sender, EventArgs e)
        {
            // ��ȡ������
            var serviceName = ServiceName.Text;
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("����������Ϊ��");
                return;
            }
            // ��ȡ�ƶ��ı�
            var dbList = GetCheckList();
            if (dbList.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫ�����ı�");
                return;
            }
            else
            {
                LoadingHelper.ShowLoading("���ڵ���IRepositories�����Ժ�", this, (obj) =>
                {
                    try
                    {
                        GetDBData();
                        // ��ȡ�ƶ��ı�
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableListIRepositories(serviceName, item, GetZhuoMianURL());
                        });
                        MessageBox.Show("Repositories�������");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("����Repositoriesʧ�ܣ�" + ex);
                    }
                });
            }
        }

        /// <summary>
        /// ����Repositories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuRepositories_Click(object sender, EventArgs e)
        {
            // ��ȡ������
            var serviceName = ServiceName.Text;
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("����������Ϊ��");
                return;
            }
            // ��ȡ�ƶ��ı�
            var dbList = GetCheckList();
            if (dbList.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫ�����ı�");
                return;
            }
            else
            {
                LoadingHelper.ShowLoading("���ڵ���Repositories�����Ժ�", this, (obj) =>
                {
                    try
                    {
                        GetDBData();
                        dbList.ForEach(item =>
                        {
                            CreateClassHelper.SaveTableListRepositories(serviceName, item, GetZhuoMianURL());
                        });
                        MessageBox.Show("Repositories�������");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("����Repositoriesʧ�ܣ�" + ex);
                    }
                });
            }
        }
        #endregion

        #region ���ýӿ�
        /// <summary>
        /// ��ȡ����Ĭ�ϵ�ַ
        /// </summary>
        /// <returns></returns>
        private string GetZhuoMianURL()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            return path;
        }
        #endregion

        #region ҳ��ؼ� 
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

        #region ��ʵ��
        /// <summary>
        /// ����Services
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuServices_Click(object sender, EventArgs e)
        {
            MessageBox.Show("�¸��汾���£������ڴ�");
        }

        /// <summary>
        /// ����IServices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoChuIServices_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ����Controllers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoControllers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("�¸��汾���£������ڴ�");
        }
        #endregion
    }
}