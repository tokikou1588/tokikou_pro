using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//������s���������
using System.Data.OleDb;
using Daila.NewClass;
using Daila.NewMehod;
namespace Daila
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        
        private void bntOK_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                MessageBox.Show("�û�������Ϊ�գ�","��ʾ");
                txtUser.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("���벻��Ϊ�գ�", "��ʾ");
                txtPassword.Focus();
                return;
            }
            tbEmployeeClass tbClass = new tbEmployeeClass();
            tbClass.EmpEngish = txtUser.Text;
            tbClass.EmpPasword = txtPassword.Text;
            tbEmployeeMenthod tbMenhod=new tbEmployeeMenthod();
            if (tbMenhod.tbEmpLog(tbClass) == 1)
            {
                frmMain frman = new frmMain();
                frman.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("��¼ʧ�ܣ�","��ʾ");
                txtPassword.Text = "";
                txtUser.Text = "";
            }
            
        }

        private void bntEsce_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}