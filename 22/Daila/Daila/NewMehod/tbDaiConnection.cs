using System;
using System.Collections.Generic;
using System.Text;
//���������ռ�
using System.Data.OleDb;
using System.Windows.Forms;
//�����ǻ�ȡ���ݿ�����
namespace Daila.NewMehod
{
    public class tbDaiConnection
    {
        private OleDbConnection con=null;
        public OleDbConnection OledCon()
        {
            //�����������ݿ���ַ���
            string reportPath = Application.StartupPath.Substring(0, Application.StartupPath.Substring(0,
             Application.StartupPath.LastIndexOf("\\")).LastIndexOf("\\"));
            reportPath += @"\DB_51aspx\db_Dainla.mdb";
            string ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + reportPath;
             //����OleDbConnection����
            con = new OleDbConnection(ConStr); 
            con.Open();
            return con;
         }//end if
  
        
    }
}
