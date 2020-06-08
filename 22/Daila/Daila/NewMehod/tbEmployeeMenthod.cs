using System;
using System.Collections.Generic;
using System.Text;

using Daila.NewClass;
using System.Data.OleDb;
using System.Data;
namespace Daila.NewMehod
{
    public class tbEmployeeMenthod
    {
        private OleDbCommand oledCmd = null; 
        private OleDbConnection oledCon = null;
        private OleDbDataAdapter oledDDa = null;
        private OleDbDataReader oledDr = null;
        private tbDaiConnection tbDai = null;
        #region//���Ա����Ϣ������ֵ��������ʾ�ɹ���0��ʾʧ��
        public int tbEmployeeAdd(tbEmployeeClass Customer)
        {
            int intFalg = 0;
            try
            {
                tbDai = new tbDaiConnection();������//ʵ��һ���������ݿ����
                oledCon = tbDai.OledCon();����������//�����ݿ�
                string strAdd = "insert into tb_Employee "; //��������ַ���
                strAdd += "values('" + Customer.strEmployeeID + "','" + Customer.strEmpName + "','"+ Customer.EmpEngish+"','"+ Customer.EmpPasword +"','" + Customer.shEmpNation + "',";
                strAdd += "'" + Customer.shEmpSex + "','" + Customer.strEmpIDcard + "','" + Customer.strEmpSpecialty + "','" + Customer.strEmpOfficeTel + "',";
                strAdd += "'" + Customer.strEmpOICQ + "','" + Customer.strEmpEmail + "','" + Customer.shEmpState + "',";
                strAdd += "'" + Customer.shEmpCity + "','" + Customer.EmpWorkDate + "','" + Customer.EmpDemissionDate + "',";
                strAdd += "'" + Customer.strfldEmpRemark + "','" + Customer.byfldEmpWorkFlag +"','" + Customer.EmpRegDate + "')";
                oledCmd = new OleDbCommand(strAdd, oledCon);��//ʵ��OleDbCommand����
                if (oledCmd.ExecuteNonQuery() != 0)            //���÷���ִ����Ӳ�����������0��ӳɹ�
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
     
        #region//�޸�Ա����Ϣ������ֵ��������ʾ�ɹ���0��ʾʧ��
        public int tbEmployeeUpdate(tbEmployeeClass Customer)
        {
            int intFalg = 0;
            try
            {
                tbDai = new tbDaiConnection();       //ʵ��һ���������ݿ����
                oledCon = tbDai.OledCon();           //�����ݿ�
                string strUpdate = "update tb_Employee set ";   //��������ַ���
                strUpdate += "EmpName ='" + Customer.strEmpName + "',EmpNation= '" + Customer.shEmpNation + "',EmpEngish='" + Customer.EmpEngish + "',EmpPasword='" + Customer.EmpPasword + "',";
                strUpdate += "EmpSex='" + Customer.shEmpSex + "',EmpIDcard='" + Customer.strEmpIDcard + "',fldEmpBirthday='" + Customer.strEmpSpecialty + "',EmpOfficeTel='" + Customer.strEmpOfficeTel + "',";
                strUpdate += "EmpOICQ='" + Customer.strEmpOICQ + "',EmpEmail='" + Customer.strEmpEmail + "',EmpState='" + Customer.shEmpState + "',";
                strUpdate += "EmpCity='" + Customer.shEmpCity + "',EmpWorkDate='" + Customer.EmpWorkDate + "',EmpDemissionDate='" + Customer.EmpDemissionDate + "',";
                strUpdate += "fldEmpRemark='" + Customer.strfldEmpRemark + "',fldEmpWorkFlag='" + Customer.byfldEmpWorkFlag + "',EmpRegDate='" + Customer.EmpRegDate + "'";
                strUpdate += " where EmployeeID='" + Customer.strEmployeeID + "'";
                oledCmd = new OleDbCommand(strUpdate, oledCon);                  //ʵ��OleDbCommand����
                if (oledCmd.ExecuteNonQuery() != 0)                     //���÷���ִ����Ӳ�����������0��ӳɹ�
                {
                    intFalg = 1;                        //�޸ĳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
             #endregion
   
        #region //ɾ��Ա����Ϣ���˷������ǽ�Ա����ְ״̬�޸�Ϊ�ƣ���壱��ʾ�ɹ���0��ʾʧ��
        public int tbEmployeeDelete(tbEmployeeClass Customer)
        {
            int intFalg = 0;
            try
            {
                tbDai = new tbDaiConnection();                    //ʵ��һ���������ݿ����
                oledCon = tbDai.OledCon();                        //�������ݿ�
                string strUpdate = "update tb_Employee set ";     //����ɾ���ַ���
                strUpdate += "fldEmpWorkFlag='" + Customer.byfldEmpWorkFlag + "'";
                strUpdate += " where EmployeeID='" + Customer.strEmployeeID + "'";
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
        #region//��ѯԱ����Ϣ�����أ�OleDbDataReader����
        public OleDbDataReader tbEmpSecarf(tbEmployeeClass Customer)
        {
            string strSecarf = null;
            try
            {
                tbDai = new tbDaiConnection();//ʵ��������
                oledCon = tbDai.OledCon();//�����ݿ�����
                if (Customer.strEmployeeID != "")
                {
                    strSecarf = "select * from tb_Employee where EmployeeID='" + Customer.strEmployeeID + "'";
                }
                else
                {
                    strSecarf = "select * from tb_Employee where fldEmpWorkFlag = false";
                
                }
                oledCmd = new OleDbCommand(strSecarf, oledCon);//ʵ��OleDbCommand����
                oledDr = oledCmd.ExecuteReader();//���÷���
                return oledDr;
            }
            catch (Exception ee)
            {
                return oledDr;
            }
        }// end if 
        #endregion
        #region //����Ա����� ���磺YG-20071118114255
        public string EmployeeID()
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
            

            return ("YG-" + strTime);



        }// end if 
        #endregion
        #region //���ͻ�����Ϣ TrreView�ؼ�
        public void fillEmployee(object objTreeView, object obimage, OleDbDataReader dr)
        {
            try
            {
                tbDai = new tbDaiConnection();
                oledCon = tbDai.OledCon();
                oledDr = dr;
                if (objTreeView.GetType().ToString() == "System.Windows.Forms.TreeView") //�жϲ�������
                {
                    System.Windows.Forms.ImageList imlist = (System.Windows.Forms.ImageList)obimage; //ʵ����������
                    System.Windows.Forms.TreeView TV = (System.Windows.Forms.TreeView)objTreeView;  //ʵ����������
                    TV.Nodes.Clear(); //����ڵ�����
                    TV.ImageList = imlist; //���ÿؼ�����
                    System.Windows.Forms.TreeNode TN = new System.Windows.Forms.TreeNode("Ա�����", 0, 1);//��Ӹ��ڵ�
                    while (oledDr.Read())
                    {
                        TN.Nodes.Add("", oledDr[0].ToString(), 0, 1); //����ӽڵ�

                    }
                    TV.Nodes.Add(TN);
                    oledDr.Close();
                    TV.ExpandAll();//չ
                }
            }//
            catch (Exception ee)
            {

            }

        }// end fi
          #endregion 
        #region//��¼��Ϣ
        public int tbEmpLog(tbEmployeeClass Customer)
        {
            string strSecarf = null;
            int intFalg = 0;
            try
            {
                tbDai = new tbDaiConnection();
                oledCon = tbDai.OledCon();

                strSecarf = "select * from tb_Employee where EmpEngish='" + Customer.EmpEngish + "' and EmpPasword='" + Customer.EmpPasword + "'";
               
                oledCmd = new OleDbCommand(strSecarf, oledCon);
                oledDr = oledCmd.ExecuteReader();//
                oledDr.Read();
                if (oledDr.HasRows)
                {
                    intFalg = 1;
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }// end if 

                #endregion
        public int tbEmpSecarfInfo(tbEmployeeClass Customer)
        {
            string strSecarf = null;
            int intFalg=0;
            try
            {
                tbDai = new tbDaiConnection();//ʵ��������
                oledCon = tbDai.OledCon();//�����ݿ�����
                strSecarf = "select * from tb_Employee where EmpEngish ='" + Customer.EmpEngish + "'";
         
                oledCmd = new OleDbCommand(strSecarf, oledCon);//ʵ��OleDbCommand����
                oledDr = oledCmd.ExecuteReader();//���÷���
                oledDr.Read();
                if(oledDr.HasRows)
                {
                    intFalg=1;
                }
                oledDr.Close();
                return intFalg;
            }
            catch (Exception ee)
            {
                  return intFalg;
            }
        }// end if 
    


    }
}
