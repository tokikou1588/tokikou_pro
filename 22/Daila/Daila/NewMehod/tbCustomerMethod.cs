using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


using Daila.NewClass;
using System.Data.OleDb;
using System.Data;


namespace Daila.NewMehod
{
   public class tbCustomerMethod
    {
        private OleDbCommand oledCmd = null;
        private OleDbConnection oledCon = null;
        private OleDbDataAdapter oledDDa = null;
        private OleDbDataReader oledDr = null;
        private DataSet ds;
        private tbDaiConnection tbDai = null;
        #region//��ӿͻ���Ϣ������ֵ��������ʾ�ɹ���0��ʾʧ��
       public int tbCustomerAdd(tbCustomerClass Customer)
        {
            int intFalg = 0;
            try
            {
                tbDai = new tbDaiConnection();
                oledCon = tbDai.OledCon();
                string strAdd = "insert into tb_Customer ";
                strAdd += "values('" + Customer.khID + "','" + Customer.khName + "',";
                strAdd += "'" + Customer.khPi + "','" + Customer.khType + "','" + Customer.khHandye + "','"+ Customer.khState+"',";
                strAdd += "'" + Customer.kuTel + "','" + Customer.kuOffice + "','" + Customer.kuEmail + "',";
                strAdd += "'" + Customer.kuAddress + "','" + Customer.kuChangPi + "','" + Customer.kuSum + "',";
                strAdd += "'" + Customer.kuDate + "','" + Customer.kuEmploy + "','" + Customer.kuDateLaste + "','" + Customer.kuFalg + "')";
                oledCmd = new OleDbCommand(strAdd, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//��ӳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }// end if 
        #endregion
        #region//�޸Ŀͻ���Ϣ������ֵ��������ʾ�ɹ���0��ʾʧ��
       public int tbCustomerUpdate(tbCustomerClass Customer)
        {
            int intFalg = 0;
            try
            {
                tbDai = new tbDaiConnection();
                oledCon = tbDai.OledCon();
                string strUpdate = "update tb_Customer set ";
                strUpdate += "khName ='" + Customer.khName + "',";
                strUpdate += "khPi ='" + Customer.khPi + "',khType='" + Customer.khType + "',khHandye='" + Customer.khHandye + "',khState='" + Customer.khState + "',";
                strUpdate += "khTel='" + Customer.kuTel + "',kuOffice='" + Customer.kuOffice + "',kuEmail='" + Customer.kuEmail + "',";
                strUpdate += "kuAddress='" + Customer.kuAddress + "',kuChangPi='" + Customer.kuChangPi + "',kuSum='" + Customer.kuSum + "',";
                strUpdate += "kuDate='" + Customer.kuDate + "',kuEmploy='" + Customer.kuEmploy + "',kuDateLaste='" + Customer.kuDateLaste + "',kuFalg='" + Customer.kuFalg + "'";
                strUpdate += " where khID='" + Customer.khID + "'";
                oledCmd = new OleDbCommand(strUpdate, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//��ӳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }// end if 
               #endregion
        #region//ɾ���ͻ���Ϣ������ֵ��������ʾ�ɹ���0��ʾʧ�ܣ����ͻ�ɾ������޸�Ϊ1
       public int tbCustomerDelete(tbCustomerClass Customer)
        {
            int intFalg = 0;
            try
            {
                tbDai = new tbDaiConnection();
                oledCon = tbDai.OledCon();
                string strUpdate = "update tb_Customer set ";
   
                strUpdate += "kuFalg='" + Customer.kuFalg + "'";
                strUpdate += " where khID='" + Customer.khID + "'";
                oledCmd = new OleDbCommand(strUpdate, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//��ӳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }// end if 
       #endregion
        #region//��ѯ�ͻ���Ϣ�����أ�OleDbDataReader����
        public OleDbDataReader tbCustomerSecarf(tbCustomerClass Customer, string strFalg)
        {
            string strSecarf = null;
            try
            {
                tbDai = new tbDaiConnection();
                oledCon = tbDai.OledCon();
                switch (strFalg)
                { 
                    case "khID"://�ͻ�ID
                        strSecarf = "select * from tb_Customer where khID='"+Customer.khID+"'";
                        break;
                    case "LikekhID"://�ͻ�ID
                        if (Customer.khID=="")
                        { strSecarf = "select * from tb_Customer where kuFalg= 0"; }
                        else
                        {
                            strSecarf = "select * from tb_Customer where khID like '%" + Customer.khID + "%' and kuFalg= 0";
                        }
                        break;
                    case "khName"://�ͻ�����
                        strSecarf = "select * from tb_Customer where khPi='" + Customer.khPi + "' and kuFalg= 0";
                        break;
                    case "LikekhName"://�ͻ�����
                        if (Customer.khPi != "")
                        {
                            strSecarf = "select * from tb_Customer where khPi like'%" + Customer.khPi + "%' and kuFalg= 0";
                        }
                        else
                        {
                            strSecarf = "select * from tb_Customer where kuFalg= 0";
                        }
                      //  "select * from tb_TelSend where TelContent like '%" + textBox1.Text + "%'"; 
                        break;
                    case "kuComputy"://��˾����
                        strSecarf = "select * from tb_Customer where khName='" + Customer.khName + "'and kuFalg= 0 ";
                        break;
                    case "kuComputylike"://��˾����
                        if (Customer.khName != "")
                        {
                            strSecarf = "select * from tb_Customer where khName like'%" + Customer.khName + "%' and kuFalg= 0";
                        }
                        else
                        {
                            strSecarf = "select * from tb_Customer where kuFalg= 0";
                        }
        
                        break;
                    case "khTel"://�绰��ѯ
                        strSecarf = "select * from tb_Customer where khTel='" + Customer.kuTel + "' or kuOffice='"+Customer.kuOffice+"'";
                        break;
                    case "khTelLike"://�绰��ѯ
                        if (Customer.kuTel != "" || Customer.kuOffice!="")
                        {
                            strSecarf = "select * from tb_Customer where khTel like '%" + Customer.kuTel + "%' or kuOffice like '%" + Customer.kuOffice + "%'";

                        }
                        else
                        {
                            strSecarf = "select * from tb_Customer where kuFalg= 0";
                        }

                        break;   
                }
                oledCmd = new OleDbCommand(strSecarf, oledCon);
                oledDr = oledCmd.ExecuteReader();//
                return oledDr;
            }
            catch (Exception ee)
            {
                return oledDr;
            }
        }// end if 
        #endregion
        #region //���ؼ�
       public int fillDataGridView(object dataGridView, OleDbDataReader dr,int i)
       {
           int intFalg = 0;
           try
           {
               
               oledDr = null;
               System.Windows.Forms.DataGridView dav = (System.Windows.Forms.DataGridView)dataGridView;
               oledDr = dr;
               dav.RowCount =i ;
               while (oledDr.Read())
               {
                   dav[0, 0].Value = oledDr[0].ToString();
                   dav[1, 0].Value = oledDr[1].ToString();
                   dav[2, 0].Value = oledDr[2].ToString();
                   dav[3, 0].Value = oledDr[7].ToString();
                   dav[4, 0].Value = oledDr[10].ToString();
                   intFalg++;
               }
               oledDr.Close();
               return intFalg;
           }
           catch (Exception ee)
           {
               return intFalg;
           }
       
       }
        #endregion
        #region //���ɿͻ���� ���磺KH-20071118114255
        public string tbCustomerID()
        {
           int intYear= DateTime.Now.Day;
            int intMonth=DateTime.Now.Month;
            int intDate=DateTime.Now.Year;
            int intHour=DateTime.Now.Hour;
            int intSecond=DateTime.Now.Second;
           int intMinute=DateTime.Now.Minute;
           string strTime = null;
           strTime = intYear.ToString();
           if (intMonth < 10)
           {
               strTime += "0" + intMonth.ToString();
           }
           else
           {
               strTime += intMonth.ToString();
           }
           if (intDate < 10)
           {
               strTime += "0" + intDate.ToString();
           }
           else
           {
               strTime += intDate.ToString();
           }
           if (intHour < 10)
           {
               strTime += "0" + intHour.ToString();
           }
           else
           {
               strTime += intHour.ToString();
           }
           if (intMinute < 10)
           {

               strTime += "0" + intMinute.ToString();
           }
           else
           {
               strTime += intMinute.ToString();
           }
           if (intSecond < 10)
           {

               strTime += "0" + intSecond.ToString();
           }
           else
           {
               strTime += intSecond.ToString();
           }
            

            return ("KH-" + strTime);



        }// end if 
        #endregion
        #region //���ͻ�����Ϣ TrreView�ؼ�
        public void filltbProPath(object objTreeView, object obimage,OleDbDataReader dr)
        {
            try
            {
                tbDai = new tbDaiConnection();
                oledCon = tbDai.OledCon();
                oledDr = dr;
                if (objTreeView.GetType().ToString() == "System.Windows.Forms.TreeView")
                {
                    System.Windows.Forms.ImageList imlist = (System.Windows.Forms.ImageList)obimage;

                    System.Windows.Forms.TreeView TV = (System.Windows.Forms.TreeView)objTreeView;
                    TV.Nodes.Clear();

                    TV.ImageList = imlist;
                    System.Windows.Forms.TreeNode TN = new System.Windows.Forms.TreeNode("�ͻ����", 0, 1);
                    while (oledDr.Read())
                    {
                        TN.Nodes.Add("",oledDr[0].ToString(), 0, 1);
                        
                    }
                    TV.Nodes.Add(TN);
                    oledDr.Close();
                    TV.ExpandAll();
                }
            }//
            catch (Exception ee)
            {

            }

        }// end fi
        #endregion 
    }
}
