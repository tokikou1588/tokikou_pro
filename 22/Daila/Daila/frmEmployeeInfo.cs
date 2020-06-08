using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using Daila.NewMehod;
using Daila.NewClass;
using System.Data.OleDb;

namespace Daila
{
    public partial class frmEmployeeInfo : Form
    {
        public frmEmployeeInfo()
        {
            InitializeComponent();
        }
        public frmEmployeeInfo(int intCount)
        {
            InitializeComponent();
            intFalg = intCount;
        }

        public static int intFalg;

        tbEmployeeClass tbClass = new tbEmployeeClass();
        tbEmployeeMenthod tbMenthod = new tbEmployeeMenthod();
        private void frmEmployeeInfo_Load(object sender, EventArgs e)
        {
            tbClass.strEmployeeID = "";       //���Ա�����
            tbMenthod.fillEmployee(treeView1, imageList1, tbMenthod.tbEmpSecarf(tbClass)); //����Ա����Ϣ�������е�fillEmployee������TreeView�ؼ�
            tbDistionMenthod td = new tbDistionMenthod();                                  //ʵ���������CombBox�ؼ�
            td.filltb_EmpSex(cmbEmpSex);//���÷�����Ա���Ա�CombBox�ؼ�
            td.filltb_Nation(cmbEmpNation);//���÷�����Ա������CombBox�ؼ�
            td.filltb_Marital(cmbEmpState);//���÷�����Ա������״̬CombBox�ؼ�

            if (intFalg == 1) //���Ա����Ϣ����
            {
                this.Text = "���Ա����Ϣ";
                txtEmployeeID.Text = tbMenthod.EmployeeID();//�Զ����Ա�����
                this.bntOK.Visible = true;
                bntEsce.Enabled = true;
                bntDelete.Enabled = false;
                bntUpdate.Enabled = false;
            }
            if (intFalg == 2)//�޸�Ա����Ϣ"����
            {
                this.Text = "�޸�Ա����Ϣ";
                bntUpdate.Enabled = true;
                bntOK.Enabled = false;
                bntDelete.Enabled = false;
                bntEsce.Enabled = true;


            }
            if (intFalg == 3)//ɾ��Ա����Ϣ����
            {
                this.Text = "ɾ��Ա����Ϣ";
                bntOK.Enabled = false;
                bntDelete.Enabled = true;
                bntEsce.Enabled = true;
                bntUpdate.Enabled =false;
            }
            if (intFalg == 4)//��ѯԱ����Ϣ����
            {
                this.Text = "��ѯԱ����Ϣ";
                bntOK.Enabled =false;
                bntDelete.Enabled = false;
                bntEsce.Enabled = true;
                bntUpdate.Enabled = false;
            }

        }
        #region �˷������ؼ���Ϣ
        public void FiiInfo(OleDbDataReader dr)
        {
            try
            {
                OleDbDataReader oledr = dr;   //ʵ��һ��OleDbDataReader����
                oledr.Read();                //���÷������Ҽ�¼
                if (oledr.HasRows)           //�жϼ�¼�����Ƿ��м�¼
                {
                    txtEmployeeID.Text = oledr[0].ToString();
                    txtEmployeeID.Enabled = false;
                    txtEmpName.Text = oledr[1].ToString();
                    txtEmpEngish.Text = oledr[2].ToString();
                    txtPasswod.Text = oledr[3].ToString();
                    cmbEmpNation.SelectedIndex = Convert.ToInt32(dr[4].ToString());
                    cmbEmpSex.SelectedIndex = Convert.ToInt32(dr[5].ToString());
                    txtEmpIDcard.Text = oledr[6].ToString();
                    dtfldEmpBirthday.Value = Convert.ToDateTime(oledr[7].ToString());
                    txtEmpOfficeTel.Text = oledr[8].ToString();
                    txtEmpOICQ.Text = oledr[9].ToString();
                    txtEmpEmail.Text = oledr[10].ToString();
                    cmbEmpState.SelectedIndex = Convert.ToInt32(oledr[11].ToString());
                    txtEmpCity.Text = oledr[12].ToString();
                    dtEmpWorkDate.Value = Convert.ToDateTime(oledr[13].ToString());
                    dtEmpDemissionDate.Value = Convert.ToDateTime(oledr[14].ToString());
      

                    txtfldEmpRemark.Text = oledr[15].ToString();
                    if (Convert.ToInt32(oledr[16]) == 0)
                    { this.radioButton2.Checked = true; }
                    if (Convert.ToInt32(oledr[16]) == 1)
                    { rdfldEmpWorkFlag1.Checked = true; }
                }
                oledr.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());   //��ʾ������Ϣ
            }
        }
        #endregion
        #region �˷�������տؼ��ڵ�����
        public void ClearMenthod()
        {
            txtEmployeeID.Text = "";
          
            txtEmpName.Text ="";
            txtEmpEngish.Text = "";
            txtPasswod.Text = "";
            cmbEmpNation.SelectedIndex = 0;
            cmbEmpSex.SelectedIndex =0;
            txtEmpIDcard.Text = "";
            dtfldEmpBirthday.Value =DateTime.Now;
            txtEmpOfficeTel.Text = "";
            txtEmpOICQ.Text = "";
            txtEmpEmail.Text = "";
            cmbEmpState.SelectedIndex = 0;
            txtEmpCity.Text = "";
            dtEmpWorkDate.Value =DateTime.Now;
            dtEmpDemissionDate.Value = DateTime.Now;

            txtfldEmpRemark.Text = ""; 
            this.radioButton2.Checked = true; 
   
        }
        #endregion

        //�˷���Ϊ�����������ж��ı��Ƿ�Ϊ�գ����ݲ���
        public int pulicMenhod()
        {
            int intResult = 0; //�ж��Ƿ�������Ϣ
            if (txtEmployeeID.Text == "")
            {
                MessageBox.Show("Ա����ţ�����Ϊ�գ�", "��ʾ");
                txtEmployeeID.Focus();
                return intResult;
            }
            if (txtEmpName.Text == "")
            {
                MessageBox.Show("Ա������������Ϊ�գ�", "��ʾ");
                txtEmpName.Focus();
                return intResult;
            }
            if (txtEmpEngish.Text == "")
            {
                MessageBox.Show("��¼������Ϊ�գ�", "��ʾ");
                txtEmpEngish.Focus();
                return intResult;
            }
            if (txtPasswod.Text == "")
            {
                MessageBox.Show("���룺����Ϊ�գ�", "��ʾ");
                txtPasswod.Focus();
                return intResult;
            }
            tbClass.strEmployeeID = txtEmployeeID.Text.Trim();
            tbClass.strEmpName = txtEmpName.Text.Trim();
            tbClass.EmpEngish = txtEmpEngish.Text.Trim();
            tbClass.EmpPasword = txtPasswod.Text.Trim();
            tbClass.shEmpNation = cmbEmpNation.SelectedIndex;
            tbClass.shEmpSex = cmbEmpSex.SelectedIndex;
            tbClass.strEmpIDcard = txtEmpIDcard.Text.Trim();
            tbClass.strEmpSpecialty = dtfldEmpBirthday.Value;
            tbClass.strEmpOfficeTel = txtEmpOfficeTel.Text.Trim();
            tbClass.strEmpOICQ = txtEmpOICQ.Text.Trim();
            tbClass.strEmpEmail = txtEmpEmail.Text.Trim();
            tbClass.shEmpState = cmbEmpState.SelectedIndex;
            tbClass.shEmpCity = txtEmpCity.Text;
            tbClass.EmpWorkDate = dtEmpWorkDate.Value;
            tbClass.EmpDemissionDate = dtEmpDemissionDate.Value;
            tbClass.strfldEmpRemark = txtfldEmpRemark.Text.Trim();
            if (rdfldEmpWorkFlag1.Checked == true)
            {
                tbClass.byfldEmpWorkFlag = 1;
            }
            if (this.radioButton2.Checked == true)
            {
                tbClass.byfldEmpWorkFlag = 0;

            }
            tbClass.EmpRegDate = DateTime.Now;
            intResult = 1;
            return intResult;

        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Text != "")   //�ж��Ƿ�ѡ��Ա�����
            {
                if (intFalg != 1)//�˴��ж��Ƿ�Ϊ��Ӳ��������Ϊ��Ӳ�������ִ�д���
                {
                    tbClass.strEmployeeID = treeView1.SelectedNode.Text; //��ѡ���Ա����Ÿ���ʵ����
                    FiiInfo(tbMenthod.tbEmpSecarf(tbClass));//���÷���������
                }
            }
        }
        //�˳���
        private void bntEsce_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //���Ա����Ϣ
        private void bntOK_Click(object sender, EventArgs e)
        {
           
            if (pulicMenhod() == 1)// ���÷����ж�������Ϣ�Ƿ���ȷ1��ʾ��ȷ��0������
            {
                if (tbMenthod.tbEmpSecarfInfo(tbClass) == 0)
                {

                    int intResult = tbMenthod.tbEmployeeAdd(tbClass);//���÷������Ա����Ϣ����ֵ��1��ʾ�ɹ���0��ʾʧ��
                    if (intResult == 1)//
                    {
                        MessageBox.Show("��ӳɹ���", "��ʾ");
                        tbClass.strEmployeeID = "";
                        tbMenthod.fillEmployee(treeView1, imageList1, tbMenthod.tbEmpSecarf(tbClass));
                        ClearMenthod();//��տؼ��ڵ��ı�
                        txtEmployeeID.Text = tbMenthod.EmployeeID();//�Զ����Ա�����
                        intFalg = 1;

                    }
                    else
                    {
                        MessageBox.Show("���ʧ�ܣ�", "��ʾ");
                        ClearMenthod();//��տؼ��ڵ��ı�
                        txtEmployeeID.Text = tbMenthod.EmployeeID();//�Զ����Ա�����
                        intFalg = 1;

                    }
                }
                else
                {
                    MessageBox.Show("��¼�����ظ������������룡","��ʾ");
                    txtEmpEngish.Text = "";
                    txtEmpEngish.Focus();
                }
            }


         }
        //ɾ��Ա����Ϣ
        private void bntDelete_Click(object sender, EventArgs e)
        {
            if (txtEmployeeID.Text == "")
            {
                MessageBox.Show("��ѡ��Ҫɾ����Ա����Ϣ");
                return;
            }

                tbClass.strEmployeeID = txtEmployeeID.Text.Trim();
                tbClass.byfldEmpWorkFlag = 1;
                //���÷������Ա����Ϣ
                int intResult1 = tbMenthod.tbEmployeeDelete(tbClass);
                if (intResult1 == 1)
                {
                    MessageBox.Show("ɾ���ɹ���", "��ʾ");
                    tbClass.strEmployeeID = "";
                    //���÷���ˢ��TreeView�ؼ�
                    tbMenthod.fillEmployee(treeView1, imageList1, tbMenthod.tbEmpSecarf(tbClass));
                    ClearMenthod();//��տؼ��ڵ��ı�
                    intFalg = 3;
                }
                else
                {
                    MessageBox.Show("ɾ��ʧ�ܣ�", "��ʾ");
                    ClearMenthod();//��տؼ��ڵ��ı�
                    intFalg = 3;
                  
                }
        }// end if 

        //�޸�Ա����Ϣ
        private void bntUpdate_Click(object sender, EventArgs e)
        {

            if (pulicMenhod() == 1)// ���÷����ж�������Ϣ�Ƿ���ȷ��1��ʾ��ȷ��0������
            {

                int intResult = tbMenthod.tbEmployeeUpdate(tbClass);//���÷����޸���Ϣ
                if (intResult == 1)
                {
                    MessageBox.Show("�޸ĳɹ���", "��ʾ");
                    tbClass.strEmployeeID = "";

                    tbMenthod.fillEmployee(treeView1, imageList1, tbMenthod.tbEmpSecarf(tbClass));
                    ClearMenthod();//��տؼ��ڵ��ı�
                    intFalg = 2;
                }
                else
                {
                    MessageBox.Show("�޸�ʧ�ܣ�", "��ʾ");
                    ClearMenthod(); //��տؼ��ڵ��ı�
                    intFalg = 2;//�޸�

                }
            }

        }


    }
}