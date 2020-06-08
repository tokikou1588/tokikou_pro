using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Daila.NewClass;//���������ռ�
using System.Data.OleDb;

using System.IO;
using Daila.NewMehod;
namespace Daila
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        public enum CHANNEL_STATE : short
        {
            CH_FREE,//0"����"
            CH_RECEIVEID,//���������
            CH_WAITSECONDRING,//2" �ȴ��ڶ�������"
            CH_WELCOME,//3"�����Ƕ����绰������ʾϵͳ
             CH_WELCOME0,
             CH_WELCOME1,
            CH_SELECT,//8"ѡ����"
            CH_SELECT1,//9
            CH_PLAYRESULT,//10"���Ų�ѯ���";
            CH_RECORDFILE,//11
            CH_PLAYRECORD,//12
            CH_OFFHOOK,//13
            CH_WAITUSERONHOOK,//14"�ȴ����߹һ�"
            CH_ConBusy,//�˹�æ
            CH_ACCOUNT1,//���Ժ�
            CH_ACCOUNT2,//��������
            CH_ACCOUNT3,
            CH_ACCOUNT4,//�ѽ�ͨ
            CH_ACCOUNT,//�ѽ�ͨ
            CH_ConBusy0,//�Է���æ
            CH_Connect,//����ת��"
            CH_Over,//��һ�
            CH_IsOver,//�Է��ѹһ�
            CH_PASSWORD0,//�벦��
            CH_PASSWORD,//�벦��
            CH_PASSWORD1,//�ѽ�ͨ
            CH_RECEIVEID2,//�������
            CH_DIAL,//�κ�
            CH_CHECKSIG,
            CH_PLAY,
            CH_ONHOOK,
            CH_BUSY,
            CH_NOBODY,
            CH_NOSIGNAL,
            CH_NODIALTONE,
            CH_NORESULT
        }
        public enum CHANNEL_TYPE : short
        {
            User,//0 ����
            Trunk,//1����
            Empty//2����
        }

        public enum SigType : int//�ź������ͣ�����������ֵ��
        {
            SIG_STOP,//0       ֹͣ�����ź���
            SIG_DIALTONE,//1  ������
            SIG_BUSY1,//2      æ��һ
            SIG_BUSY2,//3      æ����
            SIG_RINGBACK//  ������

        }
        public enum HangUp : int
        {


            HANG_UP_FLAG_FALSE,//0 û�йһ�
            HANG_UP_FLAG_TRUE,//1�Ѿ��һ����ӽ���HANG_UP_FLAG_START                                                                   ״̬��ʼ���һ�ʱ�����0.5�롣��
            HANG_UP_FLAG_START,//2 	  �� ��ʼ�һ�
            HANG_UP_FLAG_PRESS_R //3 ����һ�²��


        }

        public struct LINESTRUCT                                 //����linestruct
        {
            public CHANNEL_TYPE nType;
            public CHANNEL_STATE State;
            //	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]				
            public string CallerID;
            //	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Dtmf;
            public int nTimeElapse;
            public string UserCity;
            public int intTimeCount;


        }
        public LINESTRUCT[] Lines;
        public LINESTRUCT[] DaiLines;   //����linesΪlinestruct[];
        //public long LoadDriveFlag;
        public short TotalLine;
        public short[] sWitCh = new short[128];
        public short[] SigCheck = new short[128];
        public bool[] bUser = new bool[128];
        public short[] Dtmf = new short[128];
        public static int intLY=0;//�ؼ��绰��Ϣ
        #region//��������¼�
        private void frmMain_Load(object sender, EventArgs e)
        {
            //ʵʼ��LIStVIEW�ؼ�
            lvQs.Clear();
            lvQs.LargeImageList = imageList1;
            lvQs.Items.Add("��ӿͻ���Ϣ", 0);
            lvQs.Items.Add("ɾ���ͻ���Ϣ", 1);
            lvQs.Items.Add("�޸Ŀͻ���Ϣ", 0);
            lvQs.Items.Add("��ѯ�ͻ���Ϣ", 1);
            //ʵʼ��������
            //LoadDriveFlag = D160A.LoadDRV();      //public static extern long LoadDRV();
            //if (LoadDriveFlag == -1)       //��ǰΪ1���ĳ�-1��
            //{
            //    MessageBox.Show("Load drive failure");
            //    return;
            //}

            //TotalLine = D160A.CheckValidCh();        //  ����ڵ�ǰ�����ڿ��õ�ͨ��������           //public static extern short CheckValidCh();
            //if (D160A.EnableCard(TotalLine, 8 * 512) == 1)//public static extern long EnableCard(short wusedCh, short wFileBufLen);
            //{
            //    MessageBox.Show("Enable card error");
            //    return;
            //}

            //Lines = new LINESTRUCT[TotalLine];     //public LINESTRUCT[] Lines;
            //  DaiLines = new LINESTRUCT[TotalLine]; 
            //for (short i = 0; i < TotalLine; i++)
            //{
            //    Lines[i].nType = (CHANNEL_TYPE)D160A.CheckChType(i);
            //    Lines[i].State = (CHANNEL_STATE)(0);//(i);//0

            //    Lines[i].CallerID = "";//
            //    Lines[i].Dtmf = "";
            //    SigCheck[i] = (short)1;
            //    sWitCh[i] = (short)-1;
            //    bUser[i] = true;
            //    Dtmf[i] = (short)-1;
            //    ChnlState_LV.Items.Add(i.ToString());
            //    if (Lines[i].nType == CHANNEL_TYPE.User)
            //    { ChnlState_LV.Items[i].SubItems.Add("����"); }
            //    if (Lines[i].nType == CHANNEL_TYPE.Trunk)
            //    { ChnlState_LV.Items[i].SubItems.Add("����"); };
            //    if (Lines[i].nType == CHANNEL_TYPE.Empty)
            //    { ChnlState_LV.Items[i].SubItems.Add("����"); };
            //    if (Lines[i].State == CHANNEL_STATE.CH_FREE)
            //    { ChnlState_LV.Items[i].SubItems.Add("����"); }
            //    ChnlState_LV.Items[i].SubItems.Add("");
            //    ChnlState_LV.Items[i].SubItems.Add("");
            //    ChnlState_LV.Items[i].SubItems.Add("");
            //}

            //D160A.SetBusyPara((short)350);
            //D160A.Sig_Init(0);
            //timer1.Enabled = true;			
       
        }//end if 
        #endregion
        #region //��ȡ�ؼ�����
        public int DataGridViewCount(OleDbDataReader dr)
        {
            int intFalg = 0;
            try
            {
                OleDbDataReader oledDr = dr;
                while (oledDr.Read())
                {   

                    intFalg++;
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }

        }
                #endregion
        #region//���ؼ�
        public int DataGridFalg(OleDbDataReader dr, int intCount)
        {
            int intFalg = 0;
            try
            {
                OleDbDataReader oledDr = dr;
                intFalg = 0;
                dataGridView1.RowCount = intCount;
                while (oledDr.Read())
                {
                    this.dataGridView1[0, intFalg].Value = oledDr[0].ToString();
                    this.dataGridView1[1, intFalg].Value = oledDr[1].ToString();
                    this.dataGridView1[2, intFalg].Value = oledDr[2].ToString();
                    this.dataGridView1[3, intFalg].Value = oledDr[7].ToString();
                    this.dataGridView1[4, intFalg].Value = oledDr[10].ToString();
                    intFalg++;
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion
        #region//�˷�������ͨ����¼
        public void tbTelRecordAdd(string stringId,int intChannel)
        {
            tbTelRecordClass tbClass = new tbTelRecordClass();
            tbTelRecordMenthod tbMenhod = new tbTelRecordMenthod();
            tbCustomerMethod tbCustom = new tbCustomerMethod();
            tbCustomerClass tbCuClass = new tbCustomerClass();
            tbClass.strfldTel = stringId;
            tbClass.daSearchDate = DateTime.Now;
            tbClass.shfldChannel = intChannel;
            if (intLY == 1)
            {
                tbCuClass.kuTel = stringId;
                tbCuClass.kuOffice = stringId;
                int intFalgTel = DataGridFalg(tbCustom.tbCustomerSecarf(tbCuClass, "khTel"), DataGridViewCount(tbCustom.tbCustomerSecarf(tbCuClass, "khTel")));
                tbClass.strsType = "����";
            }
            if (intLY == 0)
            {
                tbClass.strsType = "���";
            }
            tbMenhod.tbTelRecordAdd(tbClass);
            intLY = 0;

        }
        #endregion
        #region ����timer1�ؼ�����DaiWorkIng����ά������������
        private void timer1_Tick(object sender, EventArgs e)
        {
            DaiWorkIng();
        }
        #endregion
        #region �˷������������������ķ���ʵ�֣��Զ�̨����ת�˹�̨
        private void DaiWorkIng()
        {                                   //239�ж�Ӧ475��
            //D160A.PUSH_PLAY();//��ڲ������޺������أ���;����������ά���ļ�¼�����ĳ������У����ڴ������Ĵ�ѭ���е��á�
            //D160A.FeedSigFunc();//ά�ֶ������弰�ź����ĺ��������ڳ����ѭ���е��á�
            byte[] FileName;//ԭ����
            short TmpDTMF,Code;
            for (short i = 0; i < TotalLine; i++)      //TotalLine = D160A.CheckValidCh();   
            {
                ListViewState(i);
                switch (Lines[i].State)
                {

                    case CHANNEL_STATE.CH_FREE:  //DJ160Int.CH_FREE://0"����"   //252��Ӧ267��  ch_free;
                        if (D160A.RingDetect(i) == true)
                        {
                            if (Lines[i].nType == CHANNEL_TYPE.User)//����
                            {
                                // ĳһͨ����ʼ�һ���⣻��ĳͨ��ժ���󣬿��Ե��ñ��������ú���ֻ������ͨ����Ч��
                                D160A.StartHangUpDetect(i);
                                Lines[i].State = CHANNEL_STATE.CH_OFFHOOK;
                                Lines[i].nTimeElapse = 0;
                                Lines[i].intTimeCount = 0;
                                sWitCh[i] = i;
                            }
                            if (Lines[i].nType == CHANNEL_TYPE.Trunk)//����
                            {//��ͨ��״̬��ֵ
                                Lines[i].State = CHANNEL_STATE.CH_RECEIVEID;
                                D160A.ResetCallerIDBuffer(i);   //�������Buf��Ϊ������������׼��
                                Lines[i].nTimeElapse = 0;
                                sWitCh[i] = i;
                            }
                        }
                        break;                          //267��Ӧ252��
                    case CHANNEL_STATE.CH_RECEIVEID: //"�������к���//268��Ӧ283��ch_receiveID;
                        byte[] TmpCID = new byte[32];
                        int iResult = 0;
                        iResult = D160A.GetCallerIDStr(i, TmpCID); //�������к���
                        if (iResult == 3 || iResult == 4)//�������У�����ժ��ʹ��
                        {
                            for (int j = 0; j < TmpCID.Length; j++)//�洢�绰��
                            {
                              
                                Lines[i].CallerID = Lines[i].CallerID + ((char)TmpCID[j]).ToString();
                                intLY = 1;
                            
                            }
                            //�������
                            string strin = Encoding.UTF8.GetString(TmpCID).Substring(8,11);
                            D160A.OffHook(i);
                            D160A.StartSigCheck(i);
                            Lines[i].State = CHANNEL_STATE.CH_OFFHOOK;
                            tbTelRecordAdd(strin, (int)i);
                        }
                        break;   //283��Ӧ268
                    case CHANNEL_STATE.CH_OFFHOOK:         //ch_offhook;
                        D160A.InitDtmfBuf(i);
                        StartPlayFileEx(i, "Bank.001");//����
                        Lines[i].State = CHANNEL_STATE.CH_WELCOME;//��ӭ
                        break;
                    case CHANNEL_STATE.CH_WELCOME:						//ch_welcome;
                        TmpDTMF = D160A.GetDtmfCode(i);//���ܰ���,����ֵ-1 ��������û��DTMF����
                        if (TmpDTMF != -1)
                        {
                            D160A.StopPlayFile(i);//ֹͣ����
                            Lines[i].Dtmf = Lines[i].Dtmf + CvtDTMF(TmpDTMF);//TmpDTMF.ToString();//DTMF����ת��ASCII
                            Lines[i].State = CHANNEL_STATE.CH_SELECT;//����������8λ�����ʺ�	
                        }
                        else//��Ӵ���
                        {
                            if (D160A.CheckPlayEnd(i))//���ָ��ͨ�������Ƿ����0δ����1����
                            {
                                D160A.StopPlayFile(i);//ֹͣ����
                                StartPlayFileEx(i, "Bank.004");
                                Lines[i].State = CHANNEL_STATE.CH_SELECT;
                            }
                        }
                        break;
                    case CHANNEL_STATE.CH_SELECT:       //ch_select;

                        TmpDTMF = D160A.GetDtmfCode(i);
                        if (TmpDTMF != -1)
                        {
                            Lines[i].Dtmf = TmpDTMF.ToString();
                            switch (Lines[i].Dtmf)
                            {
                                case "1":
                                    D160A.StopPlayFile(i);
                                    D160A.RsetIndexPlayFile(i);

                                    FileName = GetFilePath("bank.005");
                                    D160A.AddIndexPlayFile(i, FileName);

                                    FileName = GetFilePath("d5");
                                    D160A.AddIndexPlayFile(i, FileName);

                                    D160A.StartIndexPlayFile(i);
                                    Lines[i].State = CHANNEL_STATE.CH_PLAYRESULT;
                                    break;

                                case "2":
                                    D160A.StopPlayFile(i);
                                    D160A.RsetIndexPlayFile(i);

                                    FileName = GetFilePath("bank.006");
                                    D160A.AddIndexPlayFile(i, FileName);


                                    FileName = GetFilePath("bank.008");
                                    D160A.AddIndexPlayFile(i, FileName);

                                    D160A.StartIndexPlayFile(i);
                                    Lines[i].State = CHANNEL_STATE.CH_PLAYRESULT;
                                    break;

                                case "3":
                                    D160A.StopPlayFile(i);
                                    D160A.RsetIndexPlayFile(i);

                                    FileName = GetFilePath("bank.007");
                                    D160A.AddIndexPlayFile(i, FileName);

                                    FileName = GetFilePath("d3");
                                    D160A.AddIndexPlayFile(i, FileName);

                                    D160A.StartIndexPlayFile(i);
                                    Lines[i].State = CHANNEL_STATE.CH_PLAYRESULT;
                                    break;
                                case "4"://ת�˹�
                                    Lines[i].State = CHANNEL_STATE.CH_WELCOME1;
                                    break;
                            }
                        }
                        else if (Lines[i].intTimeCount == 3)//�ж��ظ���������
                        { Lines[i].State = CHANNEL_STATE.CH_PLAYRESULT; }
                        else if (Lines[i].nTimeElapse != 1000)
                        { Lines[i].nTimeElapse += 20; }
                        else if (Lines[i].nTimeElapse == 1000)
                        {
                            Lines[i].State = CHANNEL_STATE.CH_WELCOME;
                            Lines[i].nTimeElapse = 0;
                            Lines[i].intTimeCount++;
                        }
                        break;
                    case CHANNEL_STATE.CH_PLAYRESULT:        //ch_playresult;

                        if (D160A.CheckIndexPlayFile(i))
                        {
                            D160A.StopIndexPlayFile(i);
                            if (Lines[i].nType == CHANNEL_TYPE.Trunk)
                            { ResetChnl(i); }
                            else if (Lines[i].nType == CHANNEL_TYPE.User)
                            {//�����ź����Ĳ��š�������ʵ����ʹ���ڴ�ѭ��������ʵ�ֵġ����У�
                                //��������ʱ�䳤��Ϊ��0.75�룬ֹͣ3�룻
                                //æ��һ��ʱ�䳤��Ϊ��0.35�룬ֹͣ0.35�룻
                                //æ������ʱ�䳤��Ϊ��0.7�룬ֹͣ0.7�룻
                                D160A.StartPlaySignal(i, (int)(SigType.SIG_BUSY1));
                                Lines[i].State = CHANNEL_STATE.CH_WAITUSERONHOOK;
                            }
                        }
                        break;
                    case CHANNEL_STATE.CH_WAITUSERONHOOK:    //H_WAITUSERONHOOK
                        if (!D160A.RingDetect(i))
                        {
                            D160A.StartPlaySignal(i, (int)(SigType.SIG_STOP));
                            ResetChnl(i);
                        }
                        break;
                    //ת�˹�ģ��
                    case CHANNEL_STATE.CH_WELCOME1:
                        D160A.InitDtmfBuf(sWitCh[i]);//���ժ������·ͨ��
                        StartPlayFileEx(sWitCh[i], "Bank.001");//�������ֵȴ���ͨ
                        Lines[i].State = CHANNEL_STATE.CH_WELCOME0;
                        break;
                    case CHANNEL_STATE.CH_WELCOME0:
                        if (D160A.CheckPlayEnd(sWitCh[i]))//���ժ������·ͨ�����������Ƿ����
                        {
                            D160A.StopPlayFile(sWitCh[i]);//ֹͣժ������·ͨ������
                            Lines[i].State = CHANNEL_STATE.CH_PASSWORD;//ѡ��ͨ����
                        }
                        break;
                    case CHANNEL_STATE.CH_PASSWORD://ѡ��ͨ����
                        Code = D160A.GetDtmfCode(sWitCh[i]);//��ժ������·ͨ��������Ҫ���ӵ��ڼ�ͨ��
                        if (Code != -1)
                        {
                            Dtmf[i] = Code;//��ѡ�����ӵ�ͨ���Ÿ�ֵ������
                            if (Dtmf[i] > 0 && Dtmf[i] < 8 || Dtmf[i] == 10)
                            {

                                D160A.StartPlaySignal(sWitCh[i], (int)SigType.SIG_STOP);//��������ͨ��������
                                Lines[i].State = CHANNEL_STATE.CH_SELECT1;//�ж�ѡ��ͨ����״̬

                            }
                        }
                        break;
                    case CHANNEL_STATE.CH_SELECT1://�ж�ѡ��ͨ����״̬
                        if (Lines[Dtmf[i]].State == CHANNEL_STATE.CH_FREE)//�жϱ���ͨ���Ƿ����
                        {
                            D160A.FeedRealRing(Dtmf[i]);//�Ա���ͨ����������������ѡ����˹���ϯ
                            D160A.StartPlaySignal(sWitCh[i], (int)SigType.SIG_RINGBACK);//�����ź����Ĳ��ţ�SIG_RINGBACK  ������
                            Lines[Dtmf[i]].State = CHANNEL_STATE.CH_RECEIVEID;//��ʾ����ͨ����״̬
                            Lines[i].State = CHANNEL_STATE.CH_ACCOUNT1;
                        }
                        else//
                        {
                            D160A.StartPlaySignal(sWitCh[i], (int)SigType.SIG_BUSY2);//��������ͨ���ź����Ĳ���
                            Lines[Dtmf[i]].State = CHANNEL_STATE.CH_ConBusy0;//��ʾ����ͨ����״̬
                            Lines[i].State = CHANNEL_STATE.CH_ConBusy;
                        }
                        break;
                    case CHANNEL_STATE.CH_ConBusy://�˹�����ʾͨ��æ
                        if (Lines[sWitCh[i]].nType == CHANNEL_TYPE.Trunk)
                        { }
                        else if (Lines[sWitCh[i]].nType == CHANNEL_TYPE.User)
                        {
                            bUser[Dtmf[i]] = false;
                            Lines[i].State = CHANNEL_STATE.CH_ACCOUNT1;//�²�����
                        }
                        break;
                    case CHANNEL_STATE.CH_ACCOUNT1:
                        //��ⱻ��ͨ���Ƿ�ժ��
                        if (D160A.OffHookDetect(Dtmf[i]) == 1 && bUser[Dtmf[i]] == true)//���¼��ģ���ժ�һ�״̬��ֻ��¼��ģ����Ч��
                        {

                            D160A.StartPlaySignal(sWitCh[i], (int)SigType.SIG_STOP);//�����ź����Ĳ���,ֹͣ
                            D160A.StopPlay(sWitCh[i]);//�����ļ��Ĳ���/////////////////////
                            Lines[sWitCh[i]].State = CHANNEL_STATE.CH_Over;//��һ�
                            Lines[i].State = CHANNEL_STATE.CH_ACCOUNT2;//�ѹһ�
                            break;
                        }//�������Ĳ�ɶ���
                        else if (D160A.HangUpDetect(Dtmf[i]) == (int)HangUp.HANG_UP_FLAG_PRESS_R && sWitCh[i] != i)
                        {//���ĳһͨ���Ĺһ�״̬,����һ�²��
                            D160A.FeedPower(Dtmf[i]);//�Ա�����ͨ�����磬ͬʱֹͣ������,ͬʱֹͣ����
                            ResetChnl(Dtmf[i]);//���÷���
                            D160A.StartPlaySignal(i, (int)SigType.SIG_STOP);//���÷���
                            D160A.StopPlay(sWitCh[i]);
                            Dtmf[i] = sWitCh[i];//�����к��븳ֵ�����к���
                            Lines[Dtmf[i]].State = CHANNEL_STATE.CH_Connect;//����ת��
                            Lines[sWitCh[i]].State = CHANNEL_STATE.CH_PASSWORD1;//����ת��
                            Lines[i].State = CHANNEL_STATE.CH_ACCOUNT;
                            bUser[Dtmf[i]] = true;
                            break;
                        }  //�ڶԷ���������ʱ�һ����� 
                        else if (Lines[i].nType == CHANNEL_TYPE.User)//����
                        {
                            if (D160A.HangUpDetect(sWitCh[i]) == (int)HangUp.HANG_UP_FLAG_TRUE)//�Ѿ��һ�
                            {
                                ResetPower(i); //�����Զ��巽��
                                ResetChnl(Dtmf[i]); //�����Զ��巽��
                                bUser[i] = true;
                                break;
                            }
                        }//�ڶԷ���������ʱ�һ�����
                        break;
                    case CHANNEL_STATE.CH_ACCOUNT2://�ѹ�
                        D160A.FeedPower(Dtmf[i]);//��ĳһ·����ͨ�����磬ͬʱֹͣ������
                        Lines[i].State = CHANNEL_STATE.CH_ACCOUNT;//ֹͣ�ļ�
                        Lines[Dtmf[i]].State = CHANNEL_STATE.CH_PASSWORD1;//�ѽ�ͨ  
                        break;
                    case CHANNEL_STATE.CH_ACCOUNT://ֹͣ�����ļ�
                        D160A.StopPlay(i);//ֹͣ����
                        sWitCh[i] = (short)-1;

                        if (D160A.SetLink(i, Dtmf[i]) == 1) //��·��ͨ��
                        {
                            Lines[i].State = CHANNEL_STATE.CH_ACCOUNT3;
                        }
                        break;

                    case CHANNEL_STATE.CH_ACCOUNT3:
                        D160A.StartHangUpDetect(Dtmf[i]);//ĳһͨ����ʼ�һ���⣻��ĳͨ��ժ���󣬿��Ե��ñ��������ú���ֻ������ͨ����Ч��
                        Lines[i].State = CHANNEL_STATE.CH_ACCOUNT4;
                        break;
                    case CHANNEL_STATE.CH_ACCOUNT4:
                        // '���ת���û��Ĳ��
                        if (D160A.HangUpDetect(Dtmf[i]) == (int)HangUp.HANG_UP_FLAG_PRESS_R)//���ĳһͨ���Ĺһ�״̬,����һ�²��
                        {
                            sWitCh[i] = Dtmf[i];
                            Lines[Dtmf[i]].State = CHANNEL_STATE.CH_Connect;//����ת��
                            D160A.ClearLink(i, Dtmf[i]);//��·�����ͨ��
                            D160A.StopPlay(Dtmf[i]);
                            Dtmf[i] = -1;
                            Lines[i].State = CHANNEL_STATE.CH_WELCOME;
                            StartPlayFileEx(sWitCh[i], "Dial.001");//�������ֵȴ���
                        }
                        //  '��ⱻ���û��һ�
                        if (D160A.HangUpDetect(Dtmf[i]) == (int)HangUp.HANG_UP_FLAG_TRUE)
                        {
                            SigCheck[Dtmf[i]] = (short)D160A.ClearLink(i, Dtmf[i]);//��·�����ͨ��
                            if (SigCheck[Dtmf[i]] == 0)
                            {
                                D160A.StartPlaySignal(i, (int)SigType.SIG_BUSY1);//���÷���
                            }
                            ResetPower(i);//�Զ��巽������
                            ResetChnl(Dtmf[i]);//�Զ��巽��2
                            Lines[i].State = CHANNEL_STATE.CH_IsOver;

                        }
                        //'���Ϊ���ߵ������û��һ�
                        if (Lines[i].nType == CHANNEL_TYPE.User)
                        {
                            if (D160A.HangUpDetect(i) == (int)HangUp.HANG_UP_FLAG_TRUE)
                            {
                                SigCheck[Dtmf[i]] = (short)D160A.ClearLink(i, Dtmf[i]);//��·�����ͨ��
                                if (D160A.ClearLink(i, Dtmf[i]) == 0)
                                {
                                    D160A.StartPlaySignal(i, (int)SigType.SIG_BUSY1);//���÷���
                                }
                                ResetChnl(i);//���ú���
                            }
                        }
                        break;

                }  //473��Ӧ251��

                //�ж�����if (Lines[i].nType == CHANNEL_TYPE.Trunk && Lines[i].State != CHANNEL_STATE.CH_FREE)
                if (Lines[i].nType == CHANNEL_TYPE.Trunk && Lines[i].State != CHANNEL_STATE.CH_FREE && Lines[i].State != CHANNEL_STATE.CH_RECEIVEID)
                {
                    if (D160A.Sig_CheckBusy(i) == 1)//���йһ�æ�����,1 ��⵽æ��
                    {
                        SigCheck[i] = (short)D160A.ClearLink(i, Dtmf[i]); // ��·�����ͨ��
                        if (SigCheck[i] == 0)
                        {
                            D160A.StartPlaySignal(i, (int)SigType.SIG_BUSY1);//���÷���
                            Lines[Dtmf[i]].State = CHANNEL_STATE.CH_Over;
                        }
                        ResetChnl(i);//���÷���ResetChnl(i)
                    }

                }//�ж���������
                else if (Lines[i].nType == CHANNEL_TYPE.User && Lines[i].State != CHANNEL_STATE.CH_FREE && Lines[i].State != CHANNEL_STATE.CH_RECEIVEID)
                {      
                    if (D160A.RingDetect(i))
                    {
                        if (SigCheck[i] == 0)
                        {
                            Lines[i].State = CHANNEL_STATE.CH_Over;
                        }
                    }
                    else if (D160A.HangUpDetect(i) == (int)HangUp.HANG_UP_FLAG_TRUE)
                    {
                        ResetChnl(i); 
                    }
                }

            }
        }
        #endregion
        #region �˷���������Ƶ�ļ�
        private void StartPlayFileEx(short wChnlNo, string FileName)
        {
            byte[] FName = GetFilePath(FileName);//ԭ����
            D160A.StartPlayFile(wChnlNo, FName, 0);	//public static extern bool StartPlayFile(short wChnlNo, byte[] FileName, int StartPos);		
        }
        #endregion
        #region �˷���������Ƶ�ļ���·��
        private byte[] GetFilePath(string FileName)
        {
            //��ȡ�ļ�·��//˵���������ļ���·���в��ܺ�������
            string strPath = Application.StartupPath.Substring(0, Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")).LastIndexOf("\\"));
            strPath = strPath.Substring(0, strPath.LastIndexOf("\\"));
            strPath += @"\Voice\" + FileName;
            char[] TmpFName = new char[128];
            byte[] FName = new byte[128];
            for (int i = 0; i < strPath.Length; i++)
            {
                TmpFName[i] = Convert.ToChar(strPath.Substring(i, 1));
            }
            for (int i = 0; i < TmpFName.Length; i++)
            {
                FName[i] = (byte)TmpFName[i];
            }
            return FName;
        }
        #endregion
        #region �˷�����̬����������ʾ
        private void ListViewState(short wChnlNo)
        {                                  //519��Ӧ552��
            string strTmp = "";                             //��ʼ��strTmp;
            switch (Lines[wChnlNo].State)
            {
                case CHANNEL_STATE.CH_FREE: //����                 //��ch_free;
                    strTmp = "����";
                    break;
                case CHANNEL_STATE.CH_WELCOME:              //��ch_welcome;
                    strTmp = "��ҵ�绰�ͷ���ʾϵͳ";
                    break;
                case CHANNEL_STATE.CH_SELECT:                 //��ch_select;
                    strTmp = "��1��˾���ܣ���2��Ʒ���ܣ���3�Ż����ߣ���4�˹�����";
                    break;
                case CHANNEL_STATE.CH_WAITUSERONHOOK:// "�ȴ����߹һ�";
                    strTmp = "�ȴ����߹һ�";
                    break;
                case CHANNEL_STATE.CH_ConBusy:
                    strTmp = "�Է���æ";
                    break;
 
                case CHANNEL_STATE.CH_ConBusy0:
                    strTmp = "�Է���æ";
                    break;
                case CHANNEL_STATE.CH_Connect:
                    strTmp = "����ת��";
                    break;
                case CHANNEL_STATE.CH_Over:
                    strTmp = "��һ�";
                    break;
                case CHANNEL_STATE.CH_IsOver:
                    strTmp = "�Է��ѹһ�";
                    break;
                case CHANNEL_STATE.CH_PASSWORD0:
                case CHANNEL_STATE.CH_PASSWORD:
                    strTmp = "�벦��";
                    break;
                case CHANNEL_STATE.CH_PASSWORD1:
                case CHANNEL_STATE.CH_ACCOUNT4:
                case CHANNEL_STATE.CH_ACCOUNT:
                    strTmp = "�ѽ�ͨ";
                    break;
                case CHANNEL_STATE.CH_RECEIVEID:
                    strTmp = "��������롣����";
                    break;
                case CHANNEL_STATE.CH_ACCOUNT1:
                    strTmp = "���Ժ򡣡�����";
                    break;
                case CHANNEL_STATE.CH_ACCOUNT2:
                    strTmp = "�������ӡ�����";
                    break;
            }
            if (ChnlState_LV.Items[wChnlNo].SubItems[2].Text != Lines[wChnlNo].State.ToString())
                ChnlState_LV.Items[wChnlNo].SubItems[2].Text = Lines[wChnlNo].State.ToString();

            if (ChnlState_LV.Items[wChnlNo].SubItems[3].Text != Lines[wChnlNo].CallerID)
                ChnlState_LV.Items[wChnlNo].SubItems[3].Text = Lines[wChnlNo].CallerID;

            if (ChnlState_LV.Items[wChnlNo].SubItems[4].Text != Lines[wChnlNo].Dtmf)
                ChnlState_LV.Items[wChnlNo].SubItems[4].Text = Lines[wChnlNo].Dtmf;


            if (ChnlState_LV.Items[wChnlNo].SubItems[5].Text != strTmp)
                ChnlState_LV.Items[wChnlNo].SubItems[5].Text = strTmp;

        }
        #endregion
        #region �˷����ؼ�״״��ǣ�ֹͣ����
        private void ResetChnl(short wChnlNo)
        {


            D160A.StopPlay(wChnlNo);
            D160A.StartPlaySignal(wChnlNo, (int)SigType.SIG_STOP);
          //  if (Lines[ChannelID].State == CHANNEL_STATE.CH_WELCOME || Lines[ChannelID].State == CHANNEL_STATE.CH_PASSWORD || Lines[ChannelID].State == CHANNEL_STATE.CH_SELECT)
            if ((Lines[wChnlNo].State == CHANNEL_STATE.CH_WELCOME) || (Lines[wChnlNo].State == CHANNEL_STATE.CH_ACCOUNT) || (Lines[wChnlNo].State == CHANNEL_STATE.CH_PASSWORD) || (Lines[wChnlNo].State == CHANNEL_STATE.CH_SELECT1) || (Lines[wChnlNo].State == CHANNEL_STATE.CH_OFFHOOK))
            {
                D160A.StopPlayFile(wChnlNo);//ֹͣ�����ļ�
            }
            else if (Lines[wChnlNo].State == CHANNEL_STATE.CH_PLAYRESULT)
            { D160A.StopIndexPlayFile(wChnlNo); }
            else if (Lines[wChnlNo].State == CHANNEL_STATE.CH_ACCOUNT1)//��������ͨ����
            {
                D160A.FeedPower(wChnlNo);
            }else if (Lines[wChnlNo].State == CHANNEL_STATE.CH_ACCOUNT3)//����
            { D160A.ClearLink(wChnlNo,Dtmf[wChnlNo]); }//���ͨ��ͨ·

            //����
            if (Lines[wChnlNo].nType == CHANNEL_TYPE.Trunk)
            {
                //����ź�
                D160A.StartSigCheck(wChnlNo);
                //���߹һ�
                D160A.HangUp(wChnlNo);
                D160A.Sig_ResetCheck(wChnlNo);

            }
            if (Lines[wChnlNo].nType == CHANNEL_TYPE.User)
            {
                D160A.FeedPower(wChnlNo);
            }
            bUser[wChnlNo] = true;
            SigCheck[wChnlNo] = (short)1;
            sWitCh[wChnlNo] = (short)-1;
            Dtmf[wChnlNo] = (short)-1;
            Lines[wChnlNo].Dtmf = "";
            Lines[wChnlNo].CallerID = "";
            Lines[wChnlNo].nTimeElapse = 0;
            //Lines[wChnlNo].State = CHANNEL_STATE.CH_FREE(0);//����
            Lines[wChnlNo].State = (CHANNEL_STATE)(0);
      
        }
        #endregion 
        #region �˷���������յ绰����DTMF
        private string CvtDTMF(short DTMFCode)
        {
            char c;
            switch (DTMFCode)
            {
                case 10:
                    c = '0';
                    break;
                case 11:
                    c = '*';
                    break;
                case 12:
                    c = '#';
                    break;
                case 15:
                    c = (char)(DTMFCode - 13 + (short)'a');
                    break;
                case 0:
                    c = 'd';
                    break;
                default:
                    c = (char)(DTMFCode + (short)'0');//change DTMF from number to ASCII
                    break;
            }
            return c.ToString();

        }
        #endregion
        #region LIistView�ؼ���Button��ť�ĵ����¼�
        private void bntKu_Click(object sender, EventArgs e)
        {
            lvQs.Dock = DockStyle.None;
            bntKu.Dock = DockStyle.Top;
            bntEm.Dock = DockStyle.Bottom;
            bntQt.SendToBack();
            bntQt.Dock = DockStyle.Bottom;
            lvQs.BringToFront();
            lvQs.Dock = DockStyle.Bottom;
            lvQs.Clear();

            lvQs.Items.Add("��ӿͻ���Ϣ", 0);
            lvQs.Items.Add("ɾ���ͻ���Ϣ", 1);
            lvQs.Items.Add("�޸Ŀͻ���Ϣ", 2);
            lvQs.Items.Add("��ѯ�ͻ���Ϣ", 3);


        }

        private void bntEm_Click(object sender, EventArgs e)
        {
            lvQs.Dock = DockStyle.None;
            bntEm.Dock = DockStyle.Top;
            bntKu.Dock = DockStyle.Top;
            bntQt.Dock = DockStyle.Bottom;
            lvQs.Dock = DockStyle.Bottom;
            lvQs.Clear();

            lvQs.Items.Add("���Ա����Ϣ", 0);
            lvQs.Items.Add("ɾ��Ա����Ϣ", 1);
            lvQs.Items.Add("�޸�Ա����Ϣ", 2);
            lvQs.Items.Add("��ѯԱ����Ϣ", 3);

        }

        private void bntQt_Click(object sender, EventArgs e)
        {
            lvQs.Dock = DockStyle.None;
            bntQt.SendToBack();
            bntQt.Dock = DockStyle.Top;
            bntEm.SendToBack();
            bntEm.Dock = DockStyle.Top;
            bntKu.SendToBack();
            bntKu.Dock = DockStyle.Top;
            lvQs.Dock = DockStyle.Bottom;
            lvQs.Clear();

            lvQs.Items.Add("��Ʒ��Ϣ", 0);
            lvQs.Items.Add("��Ʒ����", 1);
            lvQs.Items.Add("ͨ����Ϣ", 2);
            lvQs.Items.Add("�绰��ѯ", 3);
        }

        private void tlsKuMenuItemAdd_Click(object sender, EventArgs e)
        {
            frmCustomerUpdate CustomerUpdate1 = new frmCustomerUpdate(1);//���
            CustomerUpdate1.Owner = this;
            CustomerUpdate1.ShowDialog();

        }
        #endregion 
        #region ������˵��ĵ����¼�
        private void tlsKuMenuItemDelete_Click(object sender, EventArgs e)
        {
            frmCustomerUpdate CustomerUpdate3 = new frmCustomerUpdate(3);
            CustomerUpdate3.Owner = this;
            CustomerUpdate3.ShowDialog();
        }

        private void tlsKuMenuItemUpdate_Click(object sender, EventArgs e)
        {
            frmCustomerUpdate CustomerUpdate2 = new frmCustomerUpdate(2);
            CustomerUpdate2.Owner = this;
            CustomerUpdate2.ShowDialog();
        }

        private void tlsKuMenuItemSecar_Click(object sender, EventArgs e)
        {
            frmCustomer CustomerF = new frmCustomer();
            CustomerF.Owner = this;
            CustomerF.ShowDialog();
        }

        private void tlsEmMenuItenAdd_Click(object sender, EventArgs e)
        {
            frmEmployeeInfo frmInfo1 = new frmEmployeeInfo(1);
            frmInfo1.Owner = this;
            frmInfo1.ShowDialog();
        }

        private void tlsEmMenuItemSecar_Click(object sender, EventArgs e)
        {
            frmEmployeeInfo frmInfo2 = new frmEmployeeInfo(2);
            frmInfo2.Owner = this;
            frmInfo2.ShowDialog();
        }

        private void tlsEmMenuItemDelete_Click(object sender, EventArgs e)
        {
            frmEmployeeInfo frmInfo3 = new frmEmployeeInfo(3);
            frmInfo3.Owner = this;
            frmInfo3.ShowDialog();
        }

        private void tlsEmMenuItemUpdate_Click(object sender, EventArgs e)
        {
            frmEmployeeInfo frmInfo4 = new frmEmployeeInfo(4);
            frmInfo4.Owner = this;
            frmInfo4.ShowDialog();
        }

        private void tlsCpMenuItemInfo_Click(object sender, EventArgs e)
        {
            frmtbProPath frmpath = new frmtbProPath(1);
            frmpath.Owner = this;
            frmpath.ShowDialog();
        }

        private void tlsCpMenuItemKind_Click(object sender, EventArgs e)
        {
            frmtbProduction frmProd = new frmtbProduction();
            frmProd.Owner = this;
            frmProd.ShowDialog();
        }
          #endregion
        #region listView1_Click�¼�
        private void listView1_Click(object sender, EventArgs e)
        {
            if (lvQs.SelectedItems[0].Text == "��Ʒ��Ϣ")
            {
                frmtbProduction frmProd1 = new frmtbProduction();
                frmProd1.Owner = this;
                frmProd1.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "��Ʒ����")
            {
                frmtbProPath frmpath1 = new frmtbProPath();
                frmpath1.Owner = this;
                frmpath1.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "ͨ����Ϣ")
            {
                frmDai dai = new frmDai();
                dai.Owner = this;
                dai.ShowDialog();

            }
            if (lvQs.SelectedItems[0].Text == "�绰��ѯ")
            {
                frmTel tel = new frmTel();
                tel.Owner = this;
                tel.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "���Ա����Ϣ")
            {
                frmEmployeeInfo frmInfo11 = new frmEmployeeInfo(1);
                frmInfo11.Owner = this;
                frmInfo11.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "ɾ��Ա����Ϣ")
            {
                frmEmployeeInfo frmInfo33 = new frmEmployeeInfo(3);
                frmInfo33.Owner = this;
                frmInfo33.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "�޸�Ա����Ϣ")
            {
                frmEmployeeInfo frmInfo11 = new frmEmployeeInfo(2);
                frmInfo11.Owner = this;
                frmInfo11.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "��ѯԱ����Ϣ")
            {
                frmEmployeeInfo frmInfo22 = new frmEmployeeInfo(4);
                frmInfo22.Owner = this;
                frmInfo22.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "��ӿͻ���Ϣ")
            {
                frmCustomerUpdate CustomerUpdate11 = new frmCustomerUpdate(1);
                CustomerUpdate11.Owner = this;
                CustomerUpdate11.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "�޸Ŀͻ���Ϣ")
            {
                frmCustomerUpdate CustomerUpdate22 = new frmCustomerUpdate(2);
                CustomerUpdate22.Owner = this;
                CustomerUpdate22.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "ɾ���ͻ���Ϣ")
            {
                frmCustomerUpdate CustomerUpdate33 = new frmCustomerUpdate(3);
                CustomerUpdate33.Owner = this;
                CustomerUpdate33.ShowDialog();
            }
            if (lvQs.SelectedItems[0].Text == "��ѯ�ͻ���Ϣ")
            {
                frmCustomer CustomerF = new frmCustomer();
                CustomerF.Owner = this;
                CustomerF.ShowDialog();
            }


        }
        #endregion
        public void ResetPower(short I)
        {
            D160A.FeedPower(Dtmf[I]);
            D160A.StartPlaySignal(sWitCh[I], (int)SigType.SIG_STOP);
            SigCheck[Dtmf[I]] = (short)D160A.ClearLink(I, Dtmf[I]);
            if (SigCheck[Dtmf[I]] == 0)
            {
                D160A.StartPlaySignal(I, (int)SigType.SIG_BUSY1);
            }
            Lines[I].State = CHANNEL_STATE.CH_IsOver;

        }



        private void �˳�EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("�Ƿ����Ҫ�˳�����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result ==DialogResult.Yes)
            {

                Application.Exit();
            }
        }

        private void tlsDaMenuItemSecar_Click(object sender, EventArgs e)
        {
            frmTel tel = new frmTel();
            tel.Owner = this;
            tel.ShowDialog();
        }

        private void tlsDaMenuItemLaida_Click(object sender, EventArgs e)
        {
            frmDai dai = new frmDai();
            dai.Owner = this;
            dai.ShowDialog();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }




     






    }
}