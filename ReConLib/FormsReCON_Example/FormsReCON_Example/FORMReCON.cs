using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Net;


namespace FormsReCON_Example
{
    
    public partial class FormReCON : Form
    {
        int iRt;
        public sbyte ServerIdx = 0; //連線索引號
        public int Processing = 0;  //FTP旗標

        public String RSYS_Current;
        
        public byte CtlFolder;
        public String SubFolder;
        public String CtlHeadFilter;
        public String CtlTailFilter;
       
        public int[] pTran;
        static int index=0;
        public uint MaxpTranNum=0;

        static uint uiPackNum = 0; //累積封包數

        // For data sending
        public static int[] Pos_MAC = new int[6]{0, 0, 0, 0, 0, 0};
        public static int[] Pos_ABS = new int[6]{0, 0, 0, 0, 0, 0};
        public SocketClient mSocketClient = null;
        
        

        public void componentInitial()
        {
            Connect_GdInfo.RowCount = 5;
            Connect_GdInfo.Columns[0].Width = Connect_GdInfo.Width - Connect_GdInfo.RowHeadersWidth-2;
            Connect_GdInfo.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Connect_GdInfo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Connect_GdInfo.AutoResizeRow(0, DataGridViewAutoSizeRowMode.AllCells);
            Connect_GdInfo.Columns[0].HeaderText = "IP";
            //Connect_GdInfo.AllowUserToResizeRows = false;
            //Connect_GdInfo.AllowUserToResizeColumns = false;
            Connect_GdInfo.Rows[0].Height = (Connect_GdInfo.Height - Connect_GdInfo.ColumnHeadersHeight)  / 5;
            Connect_GdInfo.Rows[1].Height = (Connect_GdInfo.Height - Connect_GdInfo.ColumnHeadersHeight)  / 5;
            Connect_GdInfo.Rows[2].Height = (Connect_GdInfo.Height - Connect_GdInfo.ColumnHeadersHeight)  / 5;
            Connect_GdInfo.Rows[3].Height = (Connect_GdInfo.Height - Connect_GdInfo.ColumnHeadersHeight)  / 5;
            Connect_GdInfo.Rows[4].Height = (Connect_GdInfo.Height - Connect_GdInfo.ColumnHeadersHeight)  / 5;
            Connect_GdInfo.AllowUserToAddRows = false;

            RSYS_gb.ColumnCount = 2;
           
            RSYS_gb.Columns[0].Width = 120;
            RSYS_gb.Columns[1].Width = RSYS_gb.Width - RSYS_gb.Columns[0].Width-4;
            RSYS_gb.Columns[0].HeaderText = "R編號";
            RSYS_gb.Columns[1].HeaderText = "R內容";
            RSYS_gb.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RSYS_gb.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            RSYS_gb.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RSYS_gb.AutoResizeRow(0, DataGridViewAutoSizeRowMode.AllCells);
            RSYS_gb.AllowUserToResizeRows = false;
            RSYS_gb.AllowUserToResizeColumns = false;
            RSYS_gb.AllowUserToAddRows = false;



            Coor_gdMAS.ColumnCount = 2;
            Coor_gdMAS.RowCount = 6;
            Coor_gdMAS.Columns[0].Width = 40;
            Coor_gdMAS.Columns[1].Width = RSYS_gb.Width - RSYS_gb.Columns[0].Width - 4;
            Coor_gdMAS.Rows[0].Height = Coor_gdABS.Height / 6;
            Coor_gdMAS.Rows[1].Height = Coor_gdABS.Height / 6;
            Coor_gdMAS.Rows[2].Height = Coor_gdABS.Height / 6;
            Coor_gdMAS.Rows[3].Height = Coor_gdABS.Height / 6;
            Coor_gdMAS.Rows[4].Height = Coor_gdABS.Height / 6;
            Coor_gdMAS.Rows[5].Height = Coor_gdABS.Height / 6;
            Coor_gdMAS.Rows[0].Cells[0].Value = "X";
            Coor_gdMAS.Rows[1].Cells[0].Value = "Y";
            Coor_gdMAS.Rows[2].Cells[0].Value = "Z";
            Coor_gdMAS.Rows[3].Cells[0].Value = "A";
            Coor_gdMAS.Rows[4].Cells[0].Value = "B";
            Coor_gdMAS.Rows[5].Cells[0].Value = "C";
            Coor_gdMAS.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Coor_gdMAS.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Coor_gdMAS.AutoResizeRow(0, DataGridViewAutoSizeRowMode.AllCells);
            Coor_gdMAS.AllowUserToResizeRows = false;
            Coor_gdMAS.AllowUserToResizeColumns = false;
            //Coor_gdMAS.AllowUserToAddRows = false;
            
            Coor_gdABS.ColumnCount = 2;
            Coor_gdABS.RowCount = 6;
            Coor_gdABS.Columns[0].Width = 40;
            Coor_gdABS.Columns[1].Width = RSYS_gb.Width - RSYS_gb.Columns[0].Width - 4;
            Coor_gdABS.Rows[0].Height = Coor_gdABS.Height  / 6;
            Coor_gdABS.Rows[1].Height = Coor_gdABS.Height  / 6;
            Coor_gdABS.Rows[2].Height = Coor_gdABS.Height  / 6;
            Coor_gdABS.Rows[3].Height = Coor_gdABS.Height  / 6;
            Coor_gdABS.Rows[4].Height = Coor_gdABS.Height  / 6;
            Coor_gdABS.Rows[5].Height = Coor_gdABS.Height  / 6;
            Coor_gdABS.Rows[0].Cells[0].Value = "X";
            Coor_gdABS.Rows[1].Cells[0].Value = "Y";
            Coor_gdABS.Rows[2].Cells[0].Value = "Z";
            Coor_gdABS.Rows[3].Cells[0].Value = "A";
            Coor_gdABS.Rows[4].Cells[0].Value = "B";
            Coor_gdABS.Rows[5].Cells[0].Value = "C";
            Coor_gdABS.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Coor_gdABS.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Coor_gdABS.AutoResizeRow(0, DataGridViewAutoSizeRowMode.AllCells);
            Coor_gdABS.AllowUserToResizeRows = false;
            Coor_gdABS.AllowUserToResizeColumns = false;
           //Coor_gdABS.AllowUserToAddRows = false;

            Coor_gdData.ColumnCount = 2;
            Coor_gdData.RowCount = 5;
            Coor_gdData.Columns[0].Width = 60;
            Coor_gdData.Columns[1].Width = RSYS_gb.Width - RSYS_gb.Columns[0].Width - 4;
            Coor_gdData.Rows[0].Height = Coor_gdABS.Height / 5;
            Coor_gdData.Rows[1].Height = Coor_gdABS.Height / 5;
            Coor_gdData.Rows[2].Height = Coor_gdABS.Height / 5;
            Coor_gdData.Rows[3].Height = Coor_gdABS.Height / 5;
            Coor_gdData.Rows[4].Height = Coor_gdABS.Height / 5;
            Coor_gdData.Rows[0].Cells[0].Value = "檔名";
            Coor_gdData.Rows[1].Cells[0].Value = "F";
            Coor_gdData.Rows[2].Cells[0].Value = "M";
            Coor_gdData.Rows[3].Cells[0].Value = "S";
            Coor_gdData.Rows[4].Cells[0].Value = "T";
            Coor_gdData.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Coor_gdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Coor_gdData.AutoResizeRow(0, DataGridViewAutoSizeRowMode.AllCells);
            Coor_gdData.AllowUserToResizeRows = false;
            Coor_gdData.AllowUserToResizeColumns = false;
            

            Ftp_gbLocal.ColumnCount = 2;
            Ftp_gbLocal.RowCount = 6;
            Ftp_gbLocal.Columns[0].Width = 180;
            Ftp_gbLocal.Columns[1].Width = Ftp_gbLocal.Width - Ftp_gbLocal.Columns[0].Width - 4;
            Ftp_gbLocal.Rows[0].Height = (Ftp_gbLocal.Height - Ftp_gbLocal.ColumnHeadersHeight) / 6;
            Ftp_gbLocal.Rows[1].Height = (Ftp_gbLocal.Height - Ftp_gbLocal.ColumnHeadersHeight) / 6;
            Ftp_gbLocal.Rows[2].Height = (Ftp_gbLocal.Height - Ftp_gbLocal.ColumnHeadersHeight) / 6;
            Ftp_gbLocal.Rows[3].Height = (Ftp_gbLocal.Height - Ftp_gbLocal.ColumnHeadersHeight) / 6;
            Ftp_gbLocal.Rows[4].Height = (Ftp_gbLocal.Height - Ftp_gbLocal.ColumnHeadersHeight) / 6;
            Ftp_gbLocal.Rows[5].Height = (Ftp_gbLocal.Height - Ftp_gbLocal.ColumnHeadersHeight) / 6;
            Ftp_gbLocal.Columns[0].HeaderText = "檔名";
            Ftp_gbLocal.Columns[1].HeaderText = "Size";
            Ftp_gbLocal.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Ftp_gbLocal.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Ftp_gbLocal.AutoResizeRow(0, DataGridViewAutoSizeRowMode.AllCells);
            Ftp_gbLocal.AllowUserToResizeRows = false;
            Ftp_gbLocal.AllowUserToResizeColumns = false;
            Ftp_gbLocal.AllowUserToAddRows = false;

            Ftp_gbControl.ColumnCount = 2;
            Ftp_gbControl.RowCount = 6;
            Ftp_gbControl.Columns[0].Width = 180;
            Ftp_gbControl.Columns[1].Width = Ftp_gbControl.Width - Ftp_gbControl.Columns[0].Width - 4;
            Ftp_gbControl.Rows[0].Height = (Ftp_gbControl.Height - Ftp_gbControl.ColumnHeadersHeight) / 6;
            Ftp_gbControl.Rows[1].Height = (Ftp_gbControl.Height - Ftp_gbControl.ColumnHeadersHeight) / 6;
            Ftp_gbControl.Rows[2].Height = (Ftp_gbControl.Height - Ftp_gbControl.ColumnHeadersHeight) / 6;
            Ftp_gbControl.Rows[3].Height = (Ftp_gbControl.Height - Ftp_gbControl.ColumnHeadersHeight) / 6;
            Ftp_gbControl.Rows[4].Height = (Ftp_gbControl.Height - Ftp_gbControl.ColumnHeadersHeight) / 6;
            Ftp_gbControl.Rows[5].Height = (Ftp_gbControl.Height - Ftp_gbControl.ColumnHeadersHeight) / 6;
            Ftp_gbControl.Columns[0].HeaderText = "檔名";
            Ftp_gbControl.Columns[1].HeaderText = "Size";
            Ftp_gbControl.AllowUserToAddRows = false;



            Ftp_gbControl.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Ftp_gbControl.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Ftp_gbControl.AutoResizeRow(0, DataGridViewAutoSizeRowMode.AllCells);

            PROG_gdProgView.ColumnCount = 2;
            PROG_gdProgView.RowCount = 12;
            PROG_gdProgView.Columns[0].Width = 20;
            PROG_gdProgView.Columns[1].Width = PROG_gdProgView.Width - PROG_gdProgView.Columns[0].Width - 2;
            PROG_gdProgView.Rows[0].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[1].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[2].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[3].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[4].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[5].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[6].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[7].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[8].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[9].Height  = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[10].Height = (PROG_gdProgView.Height) / 12;
            PROG_gdProgView.Rows[11].Height = (PROG_gdProgView.Height) / 12;
            //PROG_gdProgView.Enabled = false;
            //this.PROG_gdProgView.DefaultCellStyle.SelectionForeColor = Color.Black;
            //this.PROG_gdProgView.DefaultCellStyle.SelectionBackColor = Color.White;


            PROG_gdMAS.ColumnCount = 2;
            PROG_gdMAS.RowCount = 6;
            PROG_gdMAS.Columns[0].Width = 40;
            PROG_gdMAS.Columns[1].Width = RSYS_gb.Width - RSYS_gb.Columns[0].Width - 4;
            PROG_gdMAS.Rows[0].Height = Coor_gdABS.Height / 6;
            PROG_gdMAS.Rows[1].Height = Coor_gdABS.Height / 6;
            PROG_gdMAS.Rows[2].Height = Coor_gdABS.Height / 6;
            PROG_gdMAS.Rows[3].Height = Coor_gdABS.Height / 6;
            PROG_gdMAS.Rows[4].Height = Coor_gdABS.Height / 6;
            PROG_gdMAS.Rows[5].Height = Coor_gdABS.Height / 6;
            PROG_gdMAS.Rows[0].Cells[0].Value = "X";
            PROG_gdMAS.Rows[1].Cells[0].Value = "Y";
            PROG_gdMAS.Rows[2].Cells[0].Value = "Z";
            PROG_gdMAS.Rows[3].Cells[0].Value = "A";
            PROG_gdMAS.Rows[4].Cells[0].Value = "B";
            PROG_gdMAS.Rows[5].Cells[0].Value = "C";
            PROG_gdMAS.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            PROG_gdMAS.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PROG_gdMAS.AutoResizeRow(0, DataGridViewAutoSizeRowMode.AllCells);
            PROG_gdMAS.AllowUserToResizeRows = false;
            PROG_gdMAS.AllowUserToResizeColumns = false;
        }

        
        public void ConnectInitial()
        {
            Console.WriteLine("[ReCON_Example]::==============Recon_Init==============\n");
           /*===============================================================*/
           /*
           /* ReCON初始化 
           /* 
           /*===============================================================*/
            
            scif_dll.DLL_USE_SETTING DllSetting = new scif_dll.DLL_USE_SETTING();   //建立鏡射記憶體物件
            DllSetting.SoftwareType = 2;    //連線PORT(1~5)
            DllSetting.TalkInfoNum = 1;     //
            DllSetting.MemSizeI = scif_dll.I_NUM;       //鏡射記憶體配置
            DllSetting.MemSizeO = scif_dll.O_NUM;
            DllSetting.MemSizeC = scif_dll.C_NUM;
            DllSetting.MemSizeS = scif_dll.S_NUM;
            DllSetting.MemSizeA = scif_dll.A_NUM;
            DllSetting.MemSizeTT = scif_dll.TT_NUM;
            DllSetting.MemSizeCT = scif_dll.CT_NUM;
            DllSetting.MemSizeR = scif_dll.R_NUM;
            DllSetting.MemSizeTS = scif_dll.TS_NUM;
            DllSetting.MemSizeTV = scif_dll.TV_NUM;
            DllSetting.MemSizeCS = scif_dll.CS_NUM;
            DllSetting.MemSizeCV = scif_dll.CV_NUM;
            DllSetting.MemSizeF = scif_dll.F_NUM;
            iRt = scif_dll.scif_Init(ref DllSetting, 1111, "15C33863AD3210BB63CDDAAB12D2851C55C169A74B5D6C15"); //initial function ( makeID 1111 for highest priority )
            System.Threading.Thread.Sleep(200);
            if (iRt == 100)     //initial success
               Console.WriteLine("[ReCON_Example]::==============Recon_Init is Success=================\n");
            else
               Console.WriteLine("[ReCON_Example]::==============Recon_Init is fail====================\n");
        }

        public void ConnectSetDefault()
        {
            iRt = scif_dll.scif_cmd_ReadS(scif_dll.SC_DEFAULT_CMD, ServerIdx, scif_dll.S_SYS_ALARM, 2);
           
        }
        public FormReCON()
        {
            IntPtr windowHandle;
            InitializeComponent();
                

            
            
            
            CommonMethod.AllocConsole();
            windowHandle = CommonMethod.FindWindow(null, System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            IntPtr closeMenu = CommonMethod.GetSystemMenu(windowHandle, IntPtr.Zero);
            Console.SetWindowPosition(0, 0);
            uint SC_CLOSE = 0xF060;
            CommonMethod.RemoveMenu(closeMenu, SC_CLOSE, 0x0);
            CommonMethod.RemoveMenu(closeMenu, 0x1000000, 0x0);
            CommonMethod.SetParent(windowHandle, pnConsole.Handle);
            CommonMethod.MoveWindow(windowHandle, 0, -27, this.pnConsole.Width, this.pnConsole.Height+27, true);

            this.componentInitial();
            
            
            
            this.ConnectInitial();
            this.ConnectSetDefault();
            
        }
        
        //----------------------------------------------------------------------------
        //
        //
        //               freshData()
        //
        //
        //----------------------------------------------------------------------------
        
        public void FormReCONRefreshData()
        {
            uint  uiRt;
            sbyte sbRt;
            String sConnectStats;
            sConnectStats = "連線狀態";
            Color ColorStatus = Color.Gray;
            uiRt = scif_dll.scif_GetTalkMsg(ServerIdx, scif_dll.SCIF_CONNECT_STATE);    //get connect state
            switch (uiRt)
            {
                case scif_dll.SC_CONN_STATE_DISCONNECT: sConnectStats = "連線關閉"; ColorStatus = Color.Gray; break;
                case scif_dll.SC_CONN_STATE_CONNECTING: sConnectStats = "連線中"; ColorStatus = Color.Yellow; break;
                case scif_dll.SC_CONN_STATE_FAIL: sConnectStats = "連線失敗"; ColorStatus = Color.Gray; break;
                case scif_dll.SC_CONN_STATE_OK: sConnectStats = "連線成功"; ColorStatus = Color.Blue; break;
                case scif_dll.SC_CONN_STATE_NORESPONSE: sConnectStats = "連線無應"; ColorStatus = Color.Gray; break;
            }
            this.Connect_lbStats.Text = sConnectStats;
            this.Connect_pnStats.BackColor = ColorStatus;
            uiRt = scif_dll.scif_GetTalkMsg(ServerIdx, scif_dll.SCIF_LOOP_QUEUE_PKG_COUNT);
            this.Connect_lbPollingCount.Text = uiRt.ToString();
            this.Connect_barPollingCount.Value = (int)uiRt;
            uiRt = scif_dll.scif_GetTalkMsg(ServerIdx, scif_dll.SCIF_DIRECT_QUEUE_PKG_COUNT);
            this.Connect_lbDirectCount.Text = uiRt.ToString();
            this.Connect_barDirectCount.Value = (int)uiRt;

            uiRt = scif_dll.scif_GetTalkMsg(ServerIdx, scif_dll.SCIF_TX_PKG_CNT); //傳送封包數
            Connect_TxCount.Text = uiRt.ToString();
            uiRt = scif_dll.scif_GetTalkMsg(ServerIdx, scif_dll.SCIF_OK_COUNT);   //接收封包數
            Connect_RxCount.Text = uiRt.ToString();
            sbRt = scif_dll.scif_ReadS(scif_dll.S_SYS_ALARM);
            if (sbRt == 1)
                this.Connect_pnAlarm.BackColor = Color.Red;
            else
                this.Connect_pnAlarm.BackColor = Color.Gray;
            sbRt = scif_dll.scif_ReadS(scif_dll.S_SYS_WARNING);
            if (sbRt == 1)
                this.Connect_pnAlarm.BackColor = Color.Red;
            else
                this.Connect_pnWarning.BackColor = Color.Gray;


           
       
            
        }
     
        public void FormRSysfreshData()
        {
           uint uiAddr;
           int Value = 0; ;
           sbyte sState;
           string sStateStr ="狀態";
          
           if (RSYS_gb.RowCount != 0)
           {
         
               for (int i = 0; i < RSYS_gb.RowCount; i++)
               {
                   uiAddr = Convert.ToUInt32(this.RSYS_gb.Rows[i].Cells[0].Value);

                   switch (RSYS_Current)
                   {
                       case "R":
                            Value = scif_dll.scif_ReadR(uiAddr); break;
                       case "I":
                            Value = scif_dll.scif_ReadI(uiAddr); break;
                       case "O":
                            Value = scif_dll.scif_ReadI(uiAddr); break;
                       case "C":
                            Value = scif_dll.scif_ReadC(uiAddr); break;
                       case "S":
                            Value = scif_dll.scif_ReadS(uiAddr); break;
                       case "A":
                            Value = scif_dll.scif_ReadA(uiAddr); break;

                   }
                   //Value = scif_dll.scif_ReadR(uiAddr);
                   this.RSYS_gb.Rows[i].Cells[1].Value = Value.ToString();
               }
           }


           if (index < MaxpTranNum)
           {
               int ShowIdx = index + 1;
               RSYS_lbStateIdx.Text = ShowIdx.ToString();
               sState = scif_dll.scif_GetTranState(pTran[index]);   //取得封包傳送狀態
               switch (sState)
               {
                   case scif_dll.SC_TRANSACTION_PENDING: sStateStr = "等待處理中"; break;
                   case scif_dll.SC_TRANSACTION_PORCESSING: sStateStr = "處理中"; break;
                   case scif_dll.SC_TRANSACTION_FINISH: sStateStr = "完成"; index = index + 1; break;
                   case scif_dll.SC_TRANSACTION_INVALID: sStateStr = "無效的索引"; index = index + 1; break;
               }
               RSYS_lbState.Text = sStateStr;
              
           }
          
        }



        
        public void FormCoorfreshData()
        {
            int Value;
            String [] str;
            //-----------------目前檔案名稱
            byte[] bufFilename = new byte[32];
            for (int i = 0; i < 6; i++)
            {
                Value = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_MACHINE_POS + Convert.ToUInt32(i));
                this.Coor_gdMAS.Rows[i].Cells[1].Value = Value.ToString();
                Value = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_POS_ABSOLUTE + Convert.ToUInt32(i));
                this.Coor_gdABS.Rows[i].Cells[1].Value = Value.ToString();
            }
            scif_dll.scif_ReadRString(scif_dll.R_PATH_W_FILENAME ,8, bufFilename);
            str = System.Text.UnicodeEncoding.GetEncoding("Big5").GetString(bufFilename).Split('\0');

            this.Coor_gdData.Rows[0].Cells[1].Value = str[0];
            this.Coor_gdData.Rows[1].Cells[1].Value = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_F );
            this.Coor_gdData.Rows[2].Cells[1].Value = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_M );
            this.Coor_gdData.Rows[3].Cells[1].Value = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_S );
            this.Coor_gdData.Rows[4].Cells[1].Value = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_T );
            
        }

        public void FormPROGfreshData()
        {
           
            String[] str1;
            int i;
            //-----程式核對字串
            byte[] buf = new byte[80];
            for (i = 0; i < PROG_gdProgView.RowCount; i++)
            {
                scif_dll.scif_ReadRString(Convert.ToUInt32(scif_dll.R_PATH_PAC_STRING + (scif_dll.PAC_STRING_SIZE * i)), 80, buf);
                PROG_gdProgView.Rows[i].Cells[0].Value = "";
                str1 = System.Text.UnicodeEncoding.GetEncoding("Big5").GetString(buf).Split('\0');
                PROG_gdProgView.Rows[i].Cells[1].Value = str1[0];
            }


            int tempValue;
            tempValue = scif_dll.scif_ReadR(Convert.ToUInt32(scif_dll.R_PATH_BLOCK_CURRENT ));
            if (tempValue > 0)
            {
                if (tempValue > 11) tempValue = 10;
                else tempValue--;
                PROG_gdProgView.Rows[tempValue].Cells[0].Value = ">";
                PROG_gdProgView.CurrentCell = PROG_gdProgView.Rows[tempValue].Cells[1];
            }
            else
                PROG_gdProgView.ClearSelection();
            PROG_gdProgView.Refresh();
            //狀態更新
            int iState;
            String sStateStr="狀態";;
            iState = scif_dll.scif_ReadR(scif_dll.R_PATH_R_STATUS);
            //Path State(0:Not Ready,1:Ready,2:Cycle Start,3:Feed Hold,4:Block Stop)
            switch (iState)
            {
                case 0: sStateStr = "準備未了"; break;
                case 1: sStateStr = "準備完成"; break;
                case 2: sStateStr = "加工中";  break;
                case 3: sStateStr = "暫停";  break;
                case 4: sStateStr = "單節停止"; break;
            }
            PROG_lbStats.Text = sStateStr;

            int Value;
            
        
            for (i = 0; i < 6; i++)
            {
                Value = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_MACHINE_POS + Convert.ToUInt32(i));
                
                this.PROG_gdMAS.Rows[i].Cells[1].Value = Value.ToString();
           
            }



        }

        /*================================================== */
        /*                                                                                                                   
        /*           setpolling 
        /*  
        /*===================================================*/


        //連線頁面SetPolling
        public void FormConnectSetPolling()
        {
            scif_dll.scif_cmd_ClearAll(scif_dll.SC_POLLING_CMD, ServerIdx);
        }
        //系統資訊同步SetPolling
        public void FormRSysSetPolling()
        {
            uint uiCount,uiAddr;
            int iNum = 0;
            scif_dll.scif_cmd_ClearAll(scif_dll.SC_POLLING_CMD, ServerIdx);

            if (int.TryParse(this.RSYS_edRStart.Text, out iNum) && int.TryParse(this.RSYS_edCount.Text, out iNum))
            {
           
                uiAddr = Convert.ToUInt32(this.RSYS_edRStart.Text);
                uiCount = Convert.ToUInt32(this.RSYS_edCount.Text);
                RSYS_Current = this.RSYS_cbRIOCSA.SelectedItem.ToString();
                switch (RSYS_Current)
                {
                    case "R": 
                        scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, uiAddr, uiCount);break;
                    case "I": 
                        scif_dll.scif_cmd_ReadI(scif_dll.SC_POLLING_CMD, ServerIdx, uiAddr, uiCount);break;
                    case "O": 
                        scif_dll.scif_cmd_ReadO(scif_dll.SC_POLLING_CMD, ServerIdx, uiAddr, uiCount);break;
                    case "C":
                         scif_dll.scif_cmd_ReadC(scif_dll.SC_POLLING_CMD, ServerIdx, uiAddr, uiCount);break;
                    case "S":
                         scif_dll.scif_cmd_ReadS(scif_dll.SC_POLLING_CMD, ServerIdx, uiAddr, uiCount);break;
                    case "A":
                         scif_dll.scif_cmd_ReadA(scif_dll.SC_POLLING_CMD, ServerIdx, uiAddr, uiCount);break;

                }
                RSYS_gb.Columns[0].HeaderText = this.RSYS_cbRIOCSA.SelectedItem.ToString()+"編號";
                RSYS_gb.Columns[1].HeaderText = this.RSYS_cbRIOCSA.SelectedItem.ToString()+"內容";
                
                RSYS_gb.RowCount = Convert.ToInt32(uiCount);
                for (int i = 0; i < RSYS_gb.RowCount; i++)
                    this.RSYS_gb.Rows[i].Cells[0].Value = uiAddr + Convert.ToUInt32(i);
            }
            else
                MessageBox.Show("請輸入數字");
         
        }
        //系統資訊同步2SetPolling
        public void FormCoorSetPolling()
        {
            scif_dll.scif_cmd_ClearAll(scif_dll.SC_POLLING_CMD, ServerIdx);
            scif_dll.scif_StartCombineSet(ServerIdx);       //自動組合封包旗標
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, scif_dll.R_AXIS_R_INT_MACHINE_POS, 6);   //機械座標
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, scif_dll.R_AXIS_R_INT_POS_ABSOLUTE, 6);  //程式座標
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, scif_dll.R_PATH_W_FILENAME, 8);          //檔名
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, scif_dll.R_AXIS_R_INT_F, 1);             //F
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, scif_dll.R_AXIS_R_INT_M, 1);             //M
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, scif_dll.R_AXIS_R_INT_S, 1);             //S
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, scif_dll.R_AXIS_R_INT_T, 1);             //T
            scif_dll.scif_FinishCombineSet(ServerIdx);      //完成自動組合封包
        }
        //TFP傳輸SetPolling
        public void FormFtpfreshData()
        {
            String[] str1;
            int n;
            ushort i;
            byte State = 0;
            byte Result = 0;
            scif_dll.FTP_FILE ff = new scif_dll.FTP_FILE();
            iRt = scif_dll.scif_FtpCheckDone(ref State, ref Result);        //get ftp translation result
            switch (State)
            {
                case scif_dll.FTP_STATE_IDLE: this.Ftp_lbStats.Text = "閒置ing"; break;
                case scif_dll.FTP_STATE_UPLOAD: this.Ftp_lbStats.Text = "上傳ing"; break;
                case scif_dll.FTP_STATE_DOWNLOAD: this.Ftp_lbStats.Text = "下載ing"; break;
                case scif_dll.FTP_STATE_DELETE: this.Ftp_lbStats.Text = "刪除ing"; break;
                case scif_dll.FTP_STATE_LIST: this.Ftp_lbStats.Text = "取得目錄ing"; break;
                case scif_dll.FTP_STATE_UPLOAD_MANY: this.Ftp_lbStats.Text = "上傳多個ing"; break;
                case scif_dll.FTP_STATE_DOWNLOAD_MANY: this.Ftp_lbStats.Text = "下載多個ing"; break;
                case scif_dll.FTP_STATE_PENDING: this.Ftp_lbStats.Text = "命令設定ing"; break;

            }
            if (iRt == 1)
            {
                if (Processing == 1)
                {
                    //iRt = scif_dll.scif_FtpCheckDone(ref State, ref Result);
                    Processing = 0;  //結果出現
                    if (Result == scif_dll.FTP_RESULT_SUCCESS)
                    {
                        if ((State == scif_dll.FTP_STATE_UPLOAD) || (State == scif_dll.FTP_STATE_DOWNLOAD) || (State == scif_dll.FTP_STATE_DELETE) || (State == scif_dll.FTP_STATE_UPLOAD_MANY) || (State == scif_dll.FTP_STATE_DOWNLOAD_MANY))
                            Ftp_Reset();
                        
                        if (State == scif_dll.FTP_STATE_LIST)
                        {
                            n = scif_dll.scif_FtpReadFileCount();//2
                            Console.WriteLine(n.ToString());
                            if (n > 0)
                            {
                                Ftp_gbControl.RowCount = n;
                                for (i = 0; i < n; i++)
                                {
                                   
                                    scif_dll.scif_FtpReadFile(i, ref ff);
                                    str1 = System.Text.UnicodeEncoding.GetEncoding("Big5").GetString(ff.filename).Split('\0');
                                    Ftp_gbControl.Rows[i].Cells[0].Value = str1[0];
                                    Ftp_gbControl.Rows[i].Cells[1].Value = ff.filesize.ToString();
                                 }
                            }
                            else if (n == 0)
                            {
                                Ftp_gbControl.RowCount = 1;
                                Ftp_gbControl.Rows[0].Cells[0].Value = "";
                                Ftp_gbControl.Rows[0].Cells[1].Value = "";
                            }
                            iRt = scif_dll.scif_FileGetFileList(this.Ftp_edPath.Text, "", "");
                            if (iRt == -1)
                                MessageBox.Show("路徑無效");
                            else if (iRt == 0)
                            {
                                Ftp_gbLocal.RowCount = 1;
                                Ftp_gbLocal.Rows[0].Cells[0].Value = "";
                                Ftp_gbLocal.Rows[0].Cells[1].Value = "";
                            }
                            else if (iRt > 0)
                            {
                                Ftp_gbLocal.RowCount = iRt;
                                for (i = 0; i < iRt; i++)
                                {
                                    scif_dll.scif_FileReadFile(i, ref ff);
                                    str1 = System.Text.UnicodeEncoding.GetEncoding("Big5").GetString(ff.filename).Split('\0');
                                    Ftp_gbLocal.Rows[i].Cells[0].Value = str1[0];
                                    Ftp_gbLocal.Rows[i].Cells[1].Value = ff.filesize.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        
        public void FormPROGSetPolling()
        {
            
            scif_dll.scif_cmd_ClearAll(scif_dll.SC_POLLING_CMD, ServerIdx);
            scif_dll.scif_StartCombineSet(ServerIdx);
            //-----解譯資訊
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, Convert.ToUInt32(scif_dll.R_PATH_BLOCK_CURRENT ), 1);  //目前行號
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, Convert.ToUInt32(scif_dll.R_PATH_PAC_STRING), scif_dll.PAC_STRING_NUM * scif_dll.PAC_STRING_SIZE);   //程式核對字串
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, Convert.ToUInt32(scif_dll.R_PATH_R_STATUS), 1);  //目前狀態
            scif_dll.scif_cmd_ReadR(scif_dll.SC_POLLING_CMD, ServerIdx, scif_dll.R_AXIS_R_INT_MACHINE_POS, 6);   //機械座標
            scif_dll.scif_FinishCombineSet(ServerIdx);
        }
    
        
        private void Ftp_Reset()
        {
            CtlFolder = scif_dll.FTP_FOLDER_NCFILES;
            SubFolder = "";
            CtlHeadFilter = "";
            CtlTailFilter = "";
            scif_dll.scif_FtpGetFileList2(CtlFolder, SubFolder, CtlHeadFilter, CtlTailFilter);  //subfolder ID refer to pdf
            Processing = 1;
        }




        private void tStatus_Tick(object sender, EventArgs e)
        {
            
            this.tStatus.Enabled = false;

            FormReCONRefreshData();
            if (this.ReCONTAB.SelectedTab == RSYS_TAB)
                FormRSysfreshData();
            else if (this.ReCONTAB.SelectedTab == Coor_TAB)
                FormCoorfreshData();
            else if (this.ReCONTAB.SelectedTab == Ftp_TAB)
                FormFtpfreshData();
            else if (this.ReCONTAB.SelectedTab == PROG_TAB)
                FormPROGfreshData();

            if (mSocketClient != null)
            {
                ReadPos();
                byte[] dataFrame = new byte[sizeof(int) * Pos_ABS.Length + sizeof(int) * Pos_MAC.Length];
                Buffer.BlockCopy(Pos_MAC, 0, dataFrame, 0, sizeof(int) * Pos_MAC.Length);
                Buffer.BlockCopy(Pos_ABS, 0, dataFrame, sizeof(int) * Pos_MAC.Length, sizeof(int) * Pos_ABS.Length);
                
                mSocketClient.SendBytes(dataFrame);
            }
            this.tStatus.Enabled = true;
        }

        /*================================================== */
        /*                                                                                                                   
        /*           事件 
        /*  
        /*===================================================*/

        private void MainTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ReCONTAB.SelectedTab == Connect_TAB)
                FormConnectSetPolling();
            else if (this.ReCONTAB.SelectedTab == RSYS_TAB)
                FormRSysSetPolling();
            else if (this.ReCONTAB.SelectedTab == Coor_TAB)
                FormCoorSetPolling();
            else if (this.ReCONTAB.SelectedTab == Ftp_TAB)
                Ftp_Reset();
            else if (this.ReCONTAB.SelectedTab == PROG_TAB)
                FormPROGSetPolling();

        }


    

        private void Connect_btDetect_Click(object sender, EventArgs e)
        {
            int iCount;
            scif_dll.LOCAL_CONTROLLER_INFO Info = new scif_dll.LOCAL_CONTROLLER_INFO();

            iCount = scif_dll.scif_LocalDetectControllers();    //step1 : return the number of device in this network
            Connect_GdInfo.RowCount = iCount;
            for (ushort i = 0; i < iCount; i++)
            {
                String[] str_1, str_2;
                String str;
                iRt = scif_dll.scif_LocalReadController(i, ref Info);   //step2 : 裝置回傳網路結構
                str_1 = System.Text.Encoding.Default.GetString(Info.IP).Split('\0');
                str_2 = System.Text.Encoding.Default.GetString(Info.Name).Split('\0');
                str = Info.IPLong.ToString() + " : " + str_1[0] + " : " + str_2[0];
                Connect_GdInfo.Rows[i].HeaderCell.Value = i.ToString();
                Connect_GdInfo.Rows[i].Cells[0].Value = str_1[0];
                Connect_GdInfo.Rows[i].Height = (Connect_GdInfo.Height - Connect_GdInfo.ColumnHeadersHeight) / 5;

            }

        }

        private void Connect_btConnect_Click(object sender, EventArgs e)
        {

            scif_dll.scif_ConnectLocalList(ServerIdx, (ushort)Connect_GdInfo.CurrentCell.RowIndex);//ip連線
        }


        private void Connect_btDisConnect_Click(object sender, EventArgs e)
        {
            scif_dll.scif_Disconnect(ServerIdx);    //disconnect the device
            uiPackNum = 0;
        }

        private void Connect_btIPConnect_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Connect_cbIP.Text);
            scif_dll.scif_LocalConnectIP(ServerIdx, Connect_cbIP.Text);
        }



        private void RSYS_btSys_Click(object sender, EventArgs e)
        {
            FormRSysSetPolling();
        }

        private void RSYS_gb_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RSYS_pnEdit.Visible = false;
            byte Value;
            pTran = new int[1];
            if (RSYS_gb.CurrentCell.ColumnIndex == 1)
            {
                Value = Convert.ToByte( this.RSYS_gb.Rows[RSYS_gb.CurrentRow.Index].Cells[1].Value);
                if(Value ==1)
                    Value = 0;
                else
                    Value = 1;
                switch (RSYS_Current)
                {
                    case "R":
                       RSYS_pnEdit.Visible = true;
                       this.RSYS_edEdit.Text = this.RSYS_gb.Rows[RSYS_gb.CurrentRow.Index].Cells[1].Value.ToString();
                       break;
                    case "O":
                       pTran[0] = scif_dll.scif_cmd_WriteO(ServerIdx, Convert.ToUInt32(this.RSYS_gb.Rows[RSYS_gb.CurrentRow.Index].Cells[0].Value),Value);
                       index = 0;
                       MaxpTranNum = 1;
                       break;
                    case "C":
                        pTran[0] = scif_dll.scif_cmd_WriteC(ServerIdx, Convert.ToUInt32(this.RSYS_gb.Rows[RSYS_gb.CurrentRow.Index].Cells[0].Value),Value);
                        index = 0;
                        MaxpTranNum = 1;
                        break;
                    case "A":
                       pTran[0] = scif_dll.scif_cmd_WriteA(ServerIdx, Convert.ToUInt32(this.RSYS_gb.Rows[RSYS_gb.CurrentRow.Index].Cells[0].Value), Value);
                       index = 0;
                       MaxpTranNum = 1;
                       break;
                    
                }          
      
                
                RSYS_edEdit.Focus();
            }
        }

        private void RSYS_edEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            int iNum = 0;
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (int.TryParse(this.RSYS_edEdit.Text, out iNum))
                {
                    pTran = new int[1];
                    pTran[0] = scif_dll.scif_cmd_WriteR(ServerIdx, Convert.ToUInt32(this.RSYS_gb.Rows[RSYS_gb.CurrentRow.Index].Cells[0].Value), Convert.ToInt32(this.RSYS_edEdit.Text));
                    RSYS_pnEdit.Visible = false;
                    index = 0;
                    MaxpTranNum = 1;
                    RSYS_gb.Focus();
                }
                else
                    MessageBox.Show("請輸入數字");
            }
        }

        private void RSYS_btWrite_Click(object sender, EventArgs e)
        {
            int iNum=0;
            uint uiAddr,uiCount;
            int WrValue;
            
            if (int.TryParse(this.RSYS_edWrVaule.Text, out iNum))
            {
                uiAddr = Convert.ToUInt32(this.RSYS_edRStart.Text);
                uiCount = Convert.ToUInt32(this.RSYS_edCount.Text);
                WrValue =  Convert.ToInt32(this.RSYS_edWrVaule.Text);
                pTran = new int[uiCount];
                MaxpTranNum = uiCount;
                for (uint i = 0; i < uiCount; i++)
                {
                    switch (this.RSYS_cbWICA.SelectedItem.ToString())
                    {
                        case "R":
                            pTran[i] = scif_dll.scif_cmd_WriteR(ServerIdx, uiAddr + i, WrValue);
                            break;
                        case "O":
                            if (WrValue == 0 || WrValue == 1)
                                pTran[i] = scif_dll.scif_cmd_WriteO(ServerIdx, uiAddr + i, Convert.ToByte(WrValue));
                            else
                                MessageBox.Show("輸入有誤0~1");
                            break;


                        case "A":
                            if (WrValue == 0 || WrValue == 1)
                                pTran[i] = scif_dll.scif_cmd_WriteA(ServerIdx, uiAddr + i, Convert.ToByte(WrValue));
                            else
                                MessageBox.Show("輸入有誤0~1");
                            break;
                    }
                    
                    
                    
                    
                }
                index = 0;
                
            }
            else
                MessageBox.Show("請輸入數字");
        }

        private void Ftp_btSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.Ftp_edPath.Text = path.SelectedPath;

        }

        private void Ftp_btReset_Click(object sender, EventArgs e)
        {
            
            Ftp_Reset();            
        }

        private void Ftp_Updata_Click(object sender, EventArgs e)
        {
            String fullname;
            String filename;
            String sPath = this.Ftp_edPath.Text;


            if ((Ftp_gbLocal.RowCount > 0) && (Ftp_gbLocal.Rows[Ftp_gbLocal.CurrentCell.RowIndex].Cells[0].Value.ToString() != String.Empty))
            {
                filename = Ftp_gbLocal.Rows[Ftp_gbLocal.CurrentCell.RowIndex].Cells[0].Value.ToString();
                fullname = sPath + "\\" + filename;
                scif_dll.scif_FtpUploadFile2(CtlFolder, SubFolder, filename, fullname);
                Processing = 1;
             }
            
        }

        private void Ftp_Download_Click(object sender, EventArgs e)
        {
            String fullname;
            String filename;
            String sPath = this.Ftp_edPath.Text;
            if ((Ftp_gbControl.RowCount > 0) && (Ftp_gbControl.Rows[Ftp_gbControl.CurrentCell.RowIndex].Cells[0].Value.ToString() != String.Empty))
            {
                filename = Ftp_gbControl.Rows[Ftp_gbControl.CurrentCell.RowIndex].Cells[0].Value.ToString();
                fullname = sPath + "\\" + filename;
                scif_dll.scif_FtpDownloadFile2(CtlFolder, SubFolder, filename, fullname);
                Processing = 1;
            }
        }

        private void Ftp_btDelectlocal_Click(object sender, EventArgs e)
        {
            scif_dll.scif_FileDeleteFile(Convert.ToUInt16(this.Ftp_gbLocal.CurrentCell.RowIndex));
            System.Threading.Thread.Sleep(200);
            this.Ftp_btReset_Click(sender, e);
        }

       

        private void Ftp_btDelectControl_Click(object sender, EventArgs e)
        {
            String filename;
            if (Ftp_gbControl.RowCount > 0)
            {
                filename = Ftp_gbControl.Rows[Ftp_gbControl.CurrentCell.RowIndex].Cells[0].Value.ToString();
                scif_dll.scif_FtpDeleteFile2(CtlFolder, SubFolder, filename);
                Processing = 1;
            }
            
        }

        private void Ftp_Updatas_Click(object sender, EventArgs e)
        {
            String[] str1;
            int n;
            ushort i;
            String FilePath;
            scif_dll.FTP_FILE ff = new scif_dll.FTP_FILE();
            CtlFolder = scif_dll.FTP_FOLDER_NCFILES;
            SubFolder = "";
            CtlHeadFilter = "";
            CtlTailFilter = "";
            
            FilePath = this.Ftp_edPath.Text;
            scif_dll.scif_FtpTransferFileReset();
            n = scif_dll.scif_FileGetFileList(FilePath, "", "");
            for (i = 0; i < n; i++)
            {
                scif_dll.scif_FileReadFile(i, ref ff);
                str1 = System.Text.UnicodeEncoding.GetEncoding("Big5").GetString(ff.filename).Split('\0');
                scif_dll.scif_FtpTransferFileAdd(CtlFolder, SubFolder, str1[0], FilePath + "\\" + str1[0]);
            }

            if (scif_dll.scif_FtpUploadFiles2() == 1)
                Processing = 1;
            else
                MessageBox.Show("設定上傳檔案清單失敗!!");
            
           
        }

        private void Ftp_Downloads_Click(object sender, EventArgs e)
        {
            String[] str1;
            int n;
            ushort i;
            String FilePath;
            scif_dll.FTP_FILE ff = new scif_dll.FTP_FILE();
            CtlFolder = scif_dll.FTP_FOLDER_NCFILES;
            SubFolder = "";
            CtlHeadFilter = "";
            CtlTailFilter = "";
            FilePath = this.Ftp_edPath.Text;
            scif_dll.scif_FtpTransferFileReset();
            n = scif_dll.scif_FtpReadFileCount();
            for (i = 0; i < n; i++)
            {
                scif_dll.scif_FtpReadFile(i, ref ff);
                str1 = System.Text.UnicodeEncoding.GetEncoding("Big5").GetString(ff.filename).Split('\0');
                scif_dll.scif_FtpTransferFileAdd(CtlFolder, SubFolder, str1[0], FilePath + "\\" + str1[0]);
            }
            if (scif_dll.scif_FtpDownloadFiles2() == 1)
                Processing = 1;
            else
                MessageBox.Show("設定下載檔案清單失敗!!");
        }

        private void FormReCON_FormClosed(object sender, FormClosedEventArgs e)
        {
            scif_dll.scif_Disconnect(ServerIdx);
            scif_dll.scif_Destroy();    //release the memory
        }

        private void tPack_Tick(object sender, EventArgs e)
        {
            uint uiNowPackNum = 0, uiSubPackNum = 0;
            
           
            uiNowPackNum = scif_dll.scif_GetTalkMsg(ServerIdx, scif_dll.SCIF_LOOP_COUNT);   //get function state
            uiSubPackNum = uiNowPackNum - uiPackNum;
            uiPackNum = uiNowPackNum;
            Connect_TxCount.Text = uiSubPackNum.ToString(); 

        }

        private void PROG_btCirceStart_Click(object sender, EventArgs e)
        {
            scif_dll.scif_cmd_WriteRBit(ServerIdx, 47800, 0,1);     //輸出一pulse給47800.0暫存器(對應到cycle start按鍵)
            System.Threading.Thread.Sleep(50);
            scif_dll.scif_cmd_WriteRBit(ServerIdx, 47800, 0,0);
        }

        private void PROG_btHold_Click(object sender, EventArgs e)
        {
            scif_dll.scif_cmd_WriteRBit(ServerIdx, 47800, 1, 1);    //輸出一pulse給47800.1暫存器(對應到cycle stop按鍵)
            System.Threading.Thread.Sleep(50);
            scif_dll.scif_cmd_WriteRBit(ServerIdx, 47800, 1, 0);
        }

        private void ReadPos()
        {
            for (int i = 0; i < 6; i++)
            {
                Pos_MAC[i] = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_MACHINE_POS + Convert.ToUInt32(i));
                Pos_ABS[i] = scif_dll.scif_ReadR(scif_dll.R_AXIS_R_INT_POS_ABSOLUTE + Convert.ToUInt32(i));
            }
        }

        private void DB_btConnect_Click(object sender, EventArgs e)
        {
            mSocketClient = new SocketClient();
            mSocketClient.ConnectServer(Connect_DBIP.Text, int.Parse(Connect_DBport.Text));
            Console.WriteLine("Socket IP:" + Connect_DBIP.Text);
            Console.WriteLine("Socket Port:" + int.Parse(Connect_DBport.Text));
        }

        private void DB_btDisconnect_Click(object sender, EventArgs e)
        {
            mSocketClient.Close();
            mSocketClient = null;
        }
    

   
    

   

      

    


  
     

     

   

    
      
    }
}
