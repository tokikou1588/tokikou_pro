using System;
using System.Collections.Generic;
using System.Text;
//��Դ����51aspx�Ѽ�����
namespace Daila.NewClass
{
	public class tbEmployeeClass
	{
        private string EmployeeID;          //Ա�����
		 public string strEmployeeID
			 {
				 get{ return EmployeeID;}
				 set{ EmployeeID=value;}
			 }
             private string EmpName;        //Ա������
		 public string strEmpName
			 {
				 get{ return EmpName;}
				 set{ EmpName=value;}
			 }
             private int EmpNation;         //Ա�����
		 public int  shEmpNation
			 {
				 get{ return EmpNation;}
				 set{ EmpNation=value;}
			 }
		 private int EmpSex;
		 public int  shEmpSex
			 {
				 get{ return EmpSex;}
				 set{ EmpSex=value;}
			 }
		 private string EmpIDcard;          //���֤
		 public string strEmpIDcard
			 {
				 get{ return EmpIDcard;}
				 set{ EmpIDcard=value;}
			 }
		 private DateTime EmpSpecialty;
		 public DateTime strEmpSpecialty        //����
			 {
				 get{ return EmpSpecialty;}
				 set{ EmpSpecialty=value;}
			 }
		 private string EmpOfficeTel;           //�칫�绰
		 public string strEmpOfficeTel
			 {
				 get{ return EmpOfficeTel;}
				 set{ EmpOfficeTel=value;}
			 }
		 private string EmpOICQ;
		 public string strEmpOICQ
			 {
				 get{ return EmpOICQ;}
				 set{ EmpOICQ=value;}
			 }
		 private string EmpEmail;
		 public string strEmpEmail
			 {
				 get{ return EmpEmail;}
				 set{ EmpEmail=value;}
			 }
		 private int EmpState;              //����״̬
		 public int shEmpState
			 {
				 get{ return EmpState;}
				 set{ EmpState=value;}
			 }
		 private string EmpCity;
		 public string shEmpCity
			 {
				 get{ return EmpCity;}
				 set{ EmpCity=value;}
			 }

		 private string fldEmpRemark;       //��������
		 public string strfldEmpRemark
			 {
				 get{ return fldEmpRemark;}
				 set{ fldEmpRemark=value;}
			 }
		 private int fldEmpWorkFlag;        //�Ƿ���ְ
		 public int byfldEmpWorkFlag
			 {
				 get{ return fldEmpWorkFlag;}
				 set{ fldEmpWorkFlag=value;}
			 }
        private DateTime empWorkDate;
        public DateTime EmpWorkDate
        {
            get { return empWorkDate; }
            set { empWorkDate = value; }
        }
        private DateTime empDemissionDate;
        public DateTime EmpDemissionDate
        {
            get { return empDemissionDate; }
            set { empDemissionDate = value; }
        }
        private DateTime EmpRegDate1;
        public DateTime EmpRegDate
        {
            get { return EmpRegDate1; }
            set { EmpRegDate1 = value; }

    
        }
        private string EmpEngish1;      //��¼����
        public string EmpEngish
        {
            get { return EmpEngish1; }
            set { EmpEngish1 = value; }
        }
        private string EmpPasword1;     //����
        public string EmpPasword
        {
            get { return EmpPasword1; }
            set { EmpPasword1 = value; }
        }
  
	} 
}