using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Daila.NewMehod;
namespace Daila
{
    public partial class frmProd : Form
    {
        public frmProd()
        {
            InitializeComponent();
        }
        public frmProd(int intFalg1)
        {
            InitializeComponent();
            intFalg = intFalg1;
        }
        public static int intFalg;
        tbProductionMenthod tbmenthod = new tbProductionMenthod();

        private void frmProd_Load(object sender, EventArgs e)
        {
            tbmenthod.filltProd(treeView1,imageList1);
        }

        private void bntNew_Click(object sender, EventArgs e)
        {
            frmtbProduction frmProd = new frmtbProduction(1);
            frmProd.Owner = this;
            frmProd.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntOK_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text != "")
            {
                //if (intFalg == 1)//��ӿͻ���Ϣʱ����
                //{
                //    frmCustomerAdd cusAdd = (frmCustomerAdd)this.Owner;
                //    cusAdd.cmbkuChangPi.Text = treeView1.SelectedNode.Text;
                   
                //    intFalg = 0;
                //    this.Close();
                //}
                if (intFalg == 2)//�޸Ŀͻ���Ϣʱ����
                {
                    frmCustomerUpdate frmUpdate = (frmCustomerUpdate)this.Owner;
                    frmUpdate.cmbkuChangPi.Text = treeView1.SelectedNode.Text;
                    intFalg = 0;
                    this.Close();
                
                }
            }
            else
            {
                MessageBox.Show("��ѡ���Ʒ��Ϣ��", "��ʾ");
            }
        }
    }
}