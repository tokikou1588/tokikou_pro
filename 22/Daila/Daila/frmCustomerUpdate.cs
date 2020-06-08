using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;
using Daila.NewMehod;
using Daila.NewClass;
namespace Daila
{
    public partial class frmCustomerUpdate : Form
    {
        public frmCustomerUpdate()
        {
            InitializeComponent();
        }
        public frmCustomerUpdate(int falg)
        {
            InitializeComponent();
            intFlag = falg;
        }
        public static int intFlag;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        tbCustomerClass tbClass = new tbCustomerClass();
        tbCustomerMethod tbMenthod = new tbCustomerMethod();
        tbDistionMenthod tbDistion = new tbDistionMenthod();//ʵ���ֵ����
        private void frmCustomerUpdate_Load(object sender, EventArgs e)
        { //�󶨣ԣ���֣����ؼ�
            tbClass.khID = "";
            tbMenthod.filltbProPath(treeView1, imageList1, tbMenthod.tbCustomerSecarf(tbClass, "LikekhID"));

           
            tbDistion.filltb_Importance(cmbkhType);//�ͻ���Ҫ�̶�
            tbDistion.filltb_Industry(cmbkhHandye);//��ҵ
            tbDistion.filltb_Production(cmbkuChangPi);//��Ʒ����
            tbDistion.filltb_Type(cmbkhState);//�ͻ�״̬

            if (intFlag == 1)
            {
                this.Text = "��ӿͻ���Ϣ";
                txtId.Text = tbMenthod.tbCustomerID();//��ÿͻ�ID
                bntDelete.Enabled = false;
                bntEsce.Enabled = true;
                bntOK.Enabled = true;
                bntUpdate.Enabled = false;

            }
            if (intFlag == 2)
            {
                this.Text = "�޸Ŀͻ���Ϣ";
                bntDelete.Enabled = false;
                bntEsce.Enabled = true;
                bntOK.Enabled =false;
                bntUpdate.Enabled = true;
    
            }
            if (intFlag == 3)
            {
                this.Text = "ɾ���ͻ���Ϣ";

                bntDelete.Enabled = true;
                bntEsce.Enabled = true;
                bntOK.Enabled = false;
                bntUpdate.Enabled = false;
            }
            if (intFlag == 4)
            {
                this.Text = "��ѯ�ͻ���Ϣ";
                bntDelete.Enabled = false;
                bntEsce.Enabled = true;
                bntOK.Enabled = false;
                bntUpdate.Enabled = false;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Text != "")
            {
                if (intFlag != 1)
                {
                    tbClass.khID = treeView1.SelectedNode.Text;
                    CusotumsUpdate(tbMenthod.tbCustomerSecarf(tbClass, "khID"));
                }
            }
            
        }
        //�޸�����
        public void CusotumsUpdate(OleDbDataReader dr)
        {
            OleDbDataReader older = dr;
            older.Read();
            if(older.HasRows)
            {
               
                txtId.Text = older[0].ToString();
                txtId.Enabled = false;
                txtName.Text = older[1].ToString();
                txtkhPi.Text = older[2].ToString();
                cmbkhType.SelectedIndex = Convert.ToInt32(older[3].ToString());
                string str = older[4].ToString();
                cmbkhHandye.SelectedIndex = Convert.ToInt32(older[4].ToString());
                cmbkhState.SelectedIndex = Convert.ToInt32(older[5].ToString());
                txtkhTel.Text = older[6].ToString();
                txtOfficeTel.Text = older[7].ToString();
                txtEmail.Text = older[8].ToString();
                txtkuAddress.Text = older[9].ToString();
         
                    cmbkuChangPi.Text = older[10].ToString();
                txtkuSum.Text = older[11].ToString();
                txtkuEmploy.Text = older[13].ToString();
            }
        }
        //���
        private void tntOK_Click(object sender, EventArgs e)
        {
            Fillclass();//���÷���
            int intResult = tbMenthod.tbCustomerAdd(tbClass);//���÷�����ӿͻ���Ϣ
            if (intResult == 1)
            {
                MessageBox.Show("��ӳɹ���", "��ʾ");
                tbClass.khID = "";
                tbMenthod.filltbProPath(treeView1, imageList1, tbMenthod.tbCustomerSecarf(tbClass, "LikekhID"));
                ClearFill();
                intFlag = 2;//�޸ı��
   
            }
            else
            {
                MessageBox.Show("���ʧ�ܣ�", "��ʾ");
                ClearFill();
            }

        }

        private void tntEsce_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmProd prod = new frmProd(2);
            prod.Owner = this;
            prod.ShowDialog();
        }
        //�޸�
        private void bntUpdate_Click(object sender, EventArgs e)
        {
            if (Fillclass()== 1)//���÷���
            {
                int intResult = tbMenthod.tbCustomerUpdate(tbClass);//���÷�����ӿͻ���Ϣ
                if (intResult == 1)
                {
                    MessageBox.Show("�޸ĳɹ���", "��ʾ");
                    tbClass.khID = "";
                    tbMenthod.filltbProPath(treeView1, imageList1, tbMenthod.tbCustomerSecarf(tbClass, "LikekhID"));
                    ClearFill(); 
                    intFlag = 2;

                }
                else
                {
                    MessageBox.Show("�޸�ʧ�ܣ�", "��ʾ");
                    ClearFill();
                    intFlag = 2;

                }
            }
        }
        //��������
        public int Fillclass()
        {
            int intFalg=0;
            if (txtName.Text == "")
            {
                MessageBox.Show("��˾���ƣ�����Ϊ�գ�", "��ʾ");

                return intFalg;
            }
            if (txtkhPi.Text == "")
            {
                MessageBox.Show("�ͻ�����������Ϊ�գ�", "��ʾ");
                return intFalg;
            }
            if (txtkuAddress.Text == "")
            {
                MessageBox.Show("��˾��ַ������Ϊ�գ�", "��ʾ");
                return intFalg;
            }
            if (txtkhTel.Text == "")
            {
                MessageBox.Show("�ͻ��绰������Ϊ�գ�", "��ʾ");
                return intFalg;
            }
            if (txtOfficeTel.Text == "")
            {
                MessageBox.Show("�칫�绰������Ϊ�գ�", "��ʾ");
                return intFalg;
            }
            if (txtkuSum.Text == "")
            {
                MessageBox.Show("��Ʒ����������Ϊ�գ�", "��ʾ");
                return intFalg;
            }
            if (txtkuEmploy.Text == "")
            {
                MessageBox.Show("�����ˣ�����Ϊ�գ�", "��ʾ");
                return intFalg;
            }
            tbClass.khID = txtId.Text.Trim();
            tbClass.khHandye = cmbkhHandye.SelectedIndex;
            tbClass.khName = txtName.Text.Trim();
            tbClass.khPi = txtkhPi.Text.Trim();
            tbClass.khState = cmbkhState.SelectedIndex;
            tbClass.khType = cmbkhType.SelectedIndex;
            tbClass.kuAddress = txtkuAddress.Text.Trim();
            tbClass.kuChangPi = cmbkuChangPi.Text;
            tbClass.kuDate = DateTime.Now;
            tbClass.kuDateLaste = DateTime.Now;
            tbClass.kuEmail = txtEmail.Text.Trim();
            tbClass.kuTel = txtkhTel.Text;
            tbClass.kuSum = Convert.ToInt32(txtkuSum.Text.Trim());
            tbClass.kuOffice = txtOfficeTel.Text.Trim();
            tbClass.kuFalg = 0;
            tbClass.kuEmploy = txtkuEmploy.Text.Trim();
            intFlag = 1;
            return intFlag;
        }

        //ɾ��
        private void bntDelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("��ѡ��Ҫɾ���ļ�¼");
                return;
            }
            tbClass.khID = txtId.Text.Trim();
            tbClass.kuFalg = 1;
            int intResult = tbMenthod.tbCustomerDelete(tbClass);//���÷�����ӿͻ���Ϣ
            if (intResult == 1)
            {
                MessageBox.Show("ɾ���ɹ���", "��ʾ");
                tbClass.khID = "";
                tbMenthod.filltbProPath(treeView1, imageList1, tbMenthod.tbCustomerSecarf(tbClass, "LikekhID"));
                ClearFill();
                intFlag = 3;
      
            }
            else
            {
                MessageBox.Show("ɾ��ʧ�ܣ�", "��ʾ");
                ClearFill();
                intFlag = 3;
           
            }

        }//
        //�˷���������пؼ��ڵ�����
        public void ClearFill()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtkhPi.Text = "";
            cmbkhType.SelectedIndex = 0;

            cmbkhHandye.SelectedIndex = 0;
            cmbkhState.SelectedIndex =0;
            txtkhTel.Text = "";
            txtOfficeTel.Text = "";
            txtEmail.Text = "";
            txtkuAddress.Text = ""; ;

            cmbkuChangPi.Text = "";
            txtkuSum.Text = "";
            txtkuEmploy.Text = "";
        
        }
    }
}