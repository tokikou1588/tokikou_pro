using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Daila.NewClass;
using System.Data.OleDb;
using System.Data;
using  Daila.NewMehod;
namespace Daila
{
    public partial class frmtbProPath : Form
    {
        public frmtbProPath()
        {
            InitializeComponent();
        }
        public frmtbProPath(int intFalg1)
        {
            InitializeComponent();
            intFalg = intFalg1;
        }
        //�ؼ�����״̬��1��ʾ����Ӳ�Ʒ������Ϣ��0��ʾѡ���Ʒ������Ϣ
        public static int intFalg;
        //ʵ��
        tbProPathMenthod tbMenthod = new tbProPathMenthod();
        private void bntNew_Click(object sender, EventArgs e)
        {
            //ʵ����Ӳ�Ʒ��������
            frmtbProPathNew pathNew = new frmtbProPathNew(); 
            //������������
            pathNew.Owner = this;
            //ģʽ��ʾ����
            pathNew.ShowDialog();

       
        }


        private void frmtbProPath_Load(object sender, EventArgs e)
        {
            //���÷�������Ʒ�����Ϣ�����treeView�ؼ���
            tbMenthod.filltbProPath(treeView1,imageList1);
            //�����Ʒ��Ϣ����bntOK��ť����
            if (intFalg == 1)
            {
                bntOK.Visible = false;
                this.Text = "��Ʒ����";
            }
            else
            {
                this.Text = "ѡ���Ʒ���";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            intFalg = 0;
            this.Close();
        }

        private void bntOK_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text != "")
            {
                frmtbProduction fmtb = (frmtbProduction)this.Owner;
                fmtb.txtProPath.Text = treeView1.SelectedNode.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("��ѡ���Ʒ���࣡","��ʾ");
            }
            
        }

        private void frmtbProPath_FormClosed(object sender, FormClosedEventArgs e)
        {
            intFalg = 0;
        }


    }
}