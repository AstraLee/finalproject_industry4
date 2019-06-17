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



namespace FormsReCON_Example
{
    class scif_dll
    {

        private const String ReCON_DLL_NAME = "scif_dll.dll";
        //========================函式庫 相關宣告開始 ========================================
        //========================版本日期:2015/05/14=========================================   
        #region scif_define.h
        public const uint MAX_SYNC_COUNT = 10;                    //Mapper中所用的最大 Sync 數
        public const uint MAX_CONTROLLER_NUM_PER_MAKER = 300;    //向 Media 讀取控制器清單時，最大的允許數量
        //-------------------------------
        public const uint BIT_CB_SIZE = 4096;    //Combin封包中包含的最大位址數 for Bit (I,O,C,S,A)
        public const uint INT_CB_SIZE = 20000;    //Combin封包中包含的最大位址數 for Int (R)
        public const uint FIX_CB_SIZE = 4096;    //Combin封包中包含的最大位址數 for Fix (double)
        public const uint MAX_DEFAULT_SIZE = 32;    //Loop Queue 可容納的通訊筆數
        public const uint MAX_POLLING_SIZE = 64;    //Loop Queue 可容納的通訊筆數
        public const uint MAX_DIRECT_SIZE = 64;    //Direct Queue 可容納的通訊筆數
        public const uint DIRECT_ADDR_MASK = 0x3F;    //Direct 位址的 mask，要與 MAX_DIRECT_SIZE搭配

        //-------------------------------
        public const uint MAX_DATA_BYTES = 1440;
        public const uint MAX_BIT_NUM = 1440;      // MAX_DATA_BYTES / 1
        public const uint MAX_WORD_NUM = 720;       // MAX_DATA_BYTES / 2
        public const uint MAX_INT_NUM = 360;       // MAX_DATA_BYTES / 4
        public const uint MAX_FIX_NUM = 180;       // MAX_DATA_BYTES / 8
        public const uint MAX_PTR_NUM = 180;       // MAX_DATA_BYTES / 8
        public const uint MAX_CB_NUM = 288;      //MAX_DATA_BYTES /(4+1)   //位址4bytes, data為byte時(1byte)
        public const uint MAX_CB_BIT_NUM = 288;      // MAX_DATA_BYTES /(4+1)
        public const uint MAX_CB_WORD_NUM = 240;      // MAX_DATA_BYTES /(4+2)
        public const uint MAX_CB_INT_NUM = 180;     // MAX_DATA_BYTES /(4+4)
        public const uint MAX_CB_FIX_NUM = 120;      // MAX_DATA_BYTES /(4+8)
        public const uint MAX_CB_PTR_NUM = 120;      // MAX_DATA_BYTES /(4+8)

        public const uint I_OFFSET = 0;
        public const uint O_OFFSET = (5120 * 1);
        public const uint C_OFFSET = (5120 * 2);
        public const uint S_OFFSET = (5120 * 3);
        public const uint A_OFFSET = (5120 * 4);
        public const uint TT_OFFSET = (5120 * 5);
        public const uint CT_OFFSET = (5120 * 6);
        public const uint RBIT_OFFSET = 100000;

        public const uint R_OFFSET = 0;
        public const uint RI_OFFSET = 8000000;
        public const uint RO_OFFSET = 8100000;
        public const uint RC_OFFSET = 8200000;
        public const uint RS_OFFSET = 8300000;
        public const uint RA_OFFSET = 8400000;
        public const uint RTT_OFFSET = 8500000;
        public const uint RCT_OFFSET = 8600000;
        public const uint TV_OFFSET = 10000000;
        public const uint TS_OFFSET = 10500000;
        public const uint CV_OFFSET = 11000000;
        public const uint CS_OFFSET = 11500000;
        public const uint F_OFFSET = 10000000;

        public const uint I_NUM = 4096;
        public const uint O_NUM = 4096;
        public const uint C_NUM = 4096;
        public const uint S_NUM = 4096;
        public const uint A_NUM = 4096;
        public const uint TT_NUM = 256;
        public const uint CT_NUM = 256;
        public const uint R_NUM = 6000000;
        public const uint TV_NUM = 256;
        public const uint TS_NUM = 256;
        public const uint CV_NUM = 256;
        public const uint CS_NUM = 256;
        public const uint F_NUM = 100000;

        //---------scif_GetCommonMsg 的引數
        public const byte SCIF_PROC_COUNTER = 1;  //porcess counter
        public const byte SCIF_MEDIA_STEP = 5;  //與媒合主機通訊的處理步驟
        public const byte SCIF_MEDIA_STATE = 6;  //與媒合主機通訊的結果
        public const byte SCIF_MAKER_ID = 7;  //製造商編號(Group)
        public const byte SCIF_FTP_STATE = 11;  //FTP 狀態
        public const byte SCIF_FTP_RESULT = 12;  //FTP 處理結果  
        public const byte SCIF_FTP_STEP = 13;  //FTP 處理步驟
        public const byte SCIF_FTP_TOTAL_PACKAGE = 21;  //FTP 傳送總封包數
        public const byte SCIF_FTP_CURRENT_PACKAGE = 22;  //FTP 已處理的封包數  
        public const byte SCIF_FTP_TOTAL_FILE = 31;  //FTP 傳輸檔案
        public const byte SCIF_FTP_CURRENT_FILE = 32;  //FTP 已處理的檔案數

        public const uint SCIF_MEM_SIZE_I = 40;   //I點個數
        public const uint SCIF_MEM_SIZE_O = 41;   //O點個數
        public const uint SCIF_MEM_SIZE_C = 42;   //C點個數
        public const uint SCIF_MEM_SIZE_S = 43;   //S點個數
        public const uint SCIF_MEM_SIZE_A = 44;   //A點個數
        public const uint SCIF_MEM_SIZE_TT = 45;   //TT點個數
        public const uint SCIF_MEM_SIZE_CT = 46;   //CT點個數
        public const uint SCIF_MEM_SIZE_R = 47;  //R點個數
        public const uint SCIF_MEM_SIZE_TV = 48;   //TV點個數
        public const uint SCIF_MEM_SIZE_TS = 49;   //TS點個數
        public const uint SCIF_MEM_SIZE_CV = 50;   //CV點個數
        public const uint SCIF_MEM_SIZE_CS = 51;   //CS點個數
        public const uint SCIF_MEM_SIZE_F = 52;   //F點個數

        //-----scif_GetTalkMsg 的引數
        public const sbyte SCIF_CONNECT_STATE = 2;  //連線狀態
        public const sbyte SCIF_REMOTE_IPLONG = 3;  //目前的連線對象
        public const sbyte SCIF_CONNECT_STEP = 4;  //連線步驟
        public const sbyte SCIF_CONNECT_RESPONSE = 5;  //連線回應狀態
        public const sbyte SCIF_TALK_STATE = 6;  //資料通訊狀態
        public const sbyte SCIF_RESPONSE_TIME = 11;  //目前封包的反應時間
        public const sbyte SCIF_OK_COUNT = 12;  //正確封包次數
        public const sbyte SCIF_CRC_ERR_CNT = 13;  //CRC錯誤次數
        public const sbyte SCIF_LOOP_QUEUE_PKG_COUNT = 21;  //LOOP QUEUE中的封包筆數
        public const sbyte SCIF_DIRECT_QUEUE_PKG_COUNT = 22;  //Direct Queue中的封包筆數
        public const sbyte SCIF_LOOP_COUNT = 23;  //LOOP QUEUE的查詢迴圈次數
        public const sbyte SCIF_TX_PKG_CNT = 24;  //傳送封包個數
        public const sbyte SCIF_TX_PKG_RETRY_CNT = 25;  //封包重送次數
        public const sbyte SCIF_TX_CONNECT_CNT = 26;  //送出要求連線封包次數
        public const sbyte SCIF_RX_UNEXPECT_CNT = 27;  //不期待收到的封包
        public const sbyte SCIF_RX_ERR_FMT_CNT = 28;  //格式錯誤的封包
        public const sbyte SCIF_RX_CONNECT_CNT = 29;  //接收連線封包次數




        //連線狀態 由  scif_GetTalkMsg(SCIF_CONNECT_STATE)  取得
        public const uint SC_CONN_STATE_DISCONNECT = 0;   //連線關閉
        public const uint SC_CONN_STATE_CONNECTING = 1;   //連線中
        public const uint SC_CONN_STATE_FAIL = 2;   //連線失敗
        public const uint SC_CONN_STATE_OK = 3;  //連線正常
        public const uint SC_CONN_STATE_NORESPONSE = 4;   //連線無回應


        //------------連線回應狀態
        public const uint CONNECT_RESULT_NORESPONSE = 0;
        public const uint CONNECT_RESULT_OLD_SOFTWARE_CLEAR = 1;   //原本佔用的軟體已經清除
        public const uint CONNECT_RESULT_INVALID_SOFTWARE = 11;    //無效的軟體代號
        public const uint CONNECT_RESULT_DISABLE_SOFTWARE = 12;     //軟體功能停用
        public const uint CONNECT_RESULT_DISABLE_INTERNET = 13;     //停用自外網來的連線
        public const uint CONNECT_RESULT_CLOSED_SOFTWARE = 14;      //軟體功能關閉
        public const uint CONNECT_RESULT_CLOSED_INTERNET = 15;     //關閉自外網來的連線 
        public const uint CONNECT_RESULT_INVALID_MAKERID = 16;     //不相符的MakerID
        public const uint CONNECT_RESULT_WAIT_CONFIRM = 21;     //等待人機確認中
        public const uint CONNECT_RESULT_SOFTWARE_CONNECTED = 31;   //軟體已連線
        public const uint CONNECT_RESULT_SOFTWARE_REJECTED = 32;     //停用自外網來的連線
        public const uint CONNECT_RESULT_SOFTWARE_INREQ = 41;       //其他使用者佔用此軟體
        public const uint CONNECT_RESULT_PENDING = 50;              //



        //-----FTP目標資料夾
        public const byte FTP_FOLDER_NCFILES = 10;
        public const byte FTP_FOLDER_MACRO_MAKER = 20;
        public const byte FTP_FOLDER_MACRO = 21;
        public const byte FTP_FOLDER_OPEN_HMI =24; 
        public const byte FTP_FOLDER_MACHINE = 30;
        public const byte FTP_FOLDER_SETUP = 40;
        public const byte FTP_FOLDER_SETUP_MACHINE = 41;
        public const byte FTP_FOLDER_BAK = 50;
        public const byte FTP_FOLDER_DATA = 51;
        public const byte FTP_FOLDER_LANGUAGE = 52;
        public const byte FTP_FOLDER_LANGUAGE_DEF = 53;
        public const byte FTP_FOLDER_LOG = 54;
        public const byte FTP_FOLDER_RECORD = 55;
        //------------增加三個 FTP資料夾，指到根目錄
        public const byte FTP_FOLDER_NCFILES_ROOT = 25;
        public const byte FTP_FOLDER_MACRO_MAKER_ROOT = 26;
        public const byte FTP_FOLDER_MACRO_ROOT = 27;



        



        //------FTP 狀態
        public const byte FTP_STATE_IDLE = 0;  //閒置
        public const byte FTP_STATE_UPLOAD = 1;  //上傳
        public const byte FTP_STATE_DOWNLOAD = 2;  //下載
        public const byte FTP_STATE_DELETE = 3;  //刪除
        public const byte FTP_STATE_LIST = 11;  //取得目錄
        public const byte FTP_STATE_UPLOAD_MANY = 21;  //上傳多個
        public const byte FTP_STATE_DOWNLOAD_MANY = 22;  //下載多個
        public const byte FTP_STATE_DELETE_MANY = 23;  //刪除多個
        public const byte FTP_STATE_MAKE_DIR = 30;  //建立目錄
        public const byte FTP_STATE_PENDING = 99;  //命令設定中

        //------FTP 處理結果
        public const uint FTP_RESULT_IDLE = 0;       //無
        public const byte FTP_RESULT_PROCESSING = 1;      //處理中
        public const byte FTP_RESULT_SUCCESS = 2;       //成功
        public const byte FTP_RESULT_FAIL_TO_READ_LOCAL_FILE = 11;       //讀取本地檔案失敗
        public const byte FTP_RESULT_FAIL_TO_WRITE_LOCAL_FILE = 12;       //寫入本地檔案失敗
        public const byte FTP_RESULT_FAIL_TO_READ_REMOTE_FILE = 13;      //讀取遠端檔案失敗
        public const byte FTP_RESULT_FAIL_TO_WRITE_REMOTE_FILE = 14;       //寫入遠端檔案失敗
        public const byte FTP_RESULT_FAIL_TO_SET_COMMAND = 15;       //命令傳送失敗
        public const byte FTP_RESULT_FAIL_TO_COMMUNICATION = 16;       //通訊錯誤
        public const byte FTP_RESULT_FILE_MISMATCH = 17;      //檔案比對不正確


        //------MEDIA 處理結果
        public const uint MEDIA_RESULT_IDLE = 0;
        public const uint MEDIA_RESULT_PENDING = 1;
        public const uint MEDIA_RESULT_PROCESSING = 2;
        public const uint MEDIA_RESULT_SUCCESS = 3;
        public const uint MEDIA_RESULT_FAIL = 4;

        //------------資料通訊狀態
        public const uint TALK_STATE_NORMAL = 0;
        public const uint TALK_STATE_ERROR = 1;
        public const uint TALK_STATE_OVER_RETRY = 2;


        //------單筆通訊資料的狀態
        public const sbyte SC_TRANSACTION_PENDING = 0;    //等待處理中
        public const sbyte SC_TRANSACTION_PORCESSING = 1;    //處理中
        public const sbyte SC_TRANSACTION_FINISH = 2;    //完成
        public const sbyte SC_TRANSACTION_INVALID = 3;    //無效的索引

        //============通訊封包錯誤編號
        // 錯誤碼為 0                                     //沒有發生錯誤
        public const uint SCIF_ERROR_INVALID_PACKET_SET = 255;      //Local檢查到此封包設定無效
        //其他編號的錯誤碼                      由主機傳回的錯誤---直接記錄代碼即可

        //---一些定義                                   
        public const uint FILENAME_LENGTH = 32;   //檔案名稱的最大字元數
        public const uint MAX_FILE_LIST_NUM = 240;  //最大的檔案清單大小
        public const uint MAX_TRANSFER_FILE_COUNT = 128;  //一次傳輸的最大檔案量
        public const uint MAX_SOFTWARE_COUNT = 5;   //最大的軟體種類數

        //錯誤訊息來源
        public const uint ERROR_TYPE_NONE = 0;
        public const uint ERROR_TYPE_POLLING = 1;
        public const uint ERROR_TYPE_DIRECT = 2;

        //命令種類
        public const sbyte SC_DEFAULT_CMD = 0;  // Default Command -- 當要在畫面上同時顯示多個控制器的資訊時，應使用此種封包
        public const sbyte SC_POLLING_CMD = 1;  // Polling Command
        public const sbyte SC_DIRECT_CMD = 2;  // Direct read & setting


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ERROR_MSG
        {
            public byte Type;
            public byte Cmd;
            public byte Error;
            public byte Reserve;
            public int addr;
            public int num;
        };

        //指標資料的結構

        //指標資料的結構
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC_POINTER
        {
            public uint PtrA;
            public uint PtrV;
        };

        //單筆通訊的資料結構
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SC_DATA
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1440)]
            public Byte[] Bytes;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 720)]
            public UInt16[] Words;          //word 資料
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 360)]
            public UInt32[] Ints;          //整數
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 180)]
            public Double[] Fixs;      //double
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 180)]
            public SC_POINTER[] Ptrs;      //指標的值

            public SC_DATA(object any)
            {
                this.Bytes = new Byte[1440];
                this.Words = new UInt16[720];           //word 資料  
                this.Ints = new UInt32[360];             //整數
                this.Fixs = new Double[180];             //double
                this.Ptrs = new SC_POINTER[180];      //指標 的 值
            }
        }
        //自動偵測主機的回應封包
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct LOCAL_CONTROLLER_INFO
        {
            public uint IPLong;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] IP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] Name;
        };


        //由媒介主機取回的控制器資訊
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MEDIA_CONTROLLER_INFO
        {
            public int Idx;
            public uint CtrID;
            public uint Port;
            public uint IPLong;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] IP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] Name;
        };

        //FTP 或本地列舉檔案清單傳回的檔案資訊
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FTP_FILE
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] filename;
            public uint filesize;
            public ushort year;
            public byte month;
            public byte day;
            public byte hour;
            public byte minute;
            public byte second;
            public byte Reserve;
        };

        //FTP檔案傳輸的設定資料
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FTP_TRANFER_FILE
        {
            public byte Folder;
            public string SubFolder;
            public string Filename;
            public string LocalFilename;
        };

        //功能設定的結構
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FUNCTION_SETTING
        {
            public uint MakerID;
            public uint MakerPwd;
            public uint MediaIPLong1;
            public uint MediaIPLong2;
            public byte HaveMediaFunction;       //有使用媒合主機功能
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct DLL_USE_SETTING
        {
            public uint TalkInfoNum;           //連線數目
            public uint SoftwareType;          //軟體種類   
            public uint MemSizeI;
            public uint MemSizeO;
            public uint MemSizeC;
            public uint MemSizeS;
            public uint MemSizeA;
            public uint MemSizeTT;
            public uint MemSizeCT;
            public uint MemSizeR;
            public uint MemSizeTV;
            public uint MemSizeTS;
            public uint MemSizeCV;
            public uint MemSizeCS;
            public uint MemSizeF;
        };


        //-----控制器的功能設定
        [StructLayout(LayoutKind.Sequential)]
        public struct FUNC_SETTING
        {
            public UInt32 MediaServerIP1;      //媒介主機 IP 1
            public UInt32 MediaServerIP2;      //媒介主機 IP 2
            public Byte LocalDetectEnable;   //是否回應區網的偵測
            public Byte MediaPrivateEnable;  //向媒介主機取得 Private ID功能
            public Byte MediaReportEnable;   //向媒介主機通報上線
            public Byte Reserve1;
            public Byte[] SoftwareEnable;  //軟體功能
            //bit0(啟用/停用)
            //bit1(是否允許外網連入)
            //bit2(是否無條件接受連線)
            public Byte[] Reserve2;
            public FUNC_SETTING(UInt32 MediaServerIP1, UInt32 MediaServerIP2, Byte LocalDetectEnable, Byte MediaPrivateEnable, Byte MediaReportEnable, Byte Reserve1, Byte[] SoftwareEnable, Byte[] Reserve2)
            {
                this.MediaServerIP1 = 0;
                this.MediaServerIP2 = 0;
                this.LocalDetectEnable = 0;
                this.MediaPrivateEnable = 0;
                this.MediaReportEnable = 0;
                this.Reserve1 = 0;
                this.SoftwareEnable = new Byte[5];
                this.Reserve2 = new Byte[3];
                SoftwareEnable.CopyTo(this.SoftwareEnable, 0);
                Reserve2.CopyTo(this.Reserve2, 0);

            }
        }
        #endregion scif_define.h
        //-------------------------------------------
        #region scif_define_pac.h

        public const uint R_LOAD = 250096;   // Load of X-axis

        //===========================人機自訂的參數=============================== 
        public const uint R_SERVO_ON = 0;    //1~32軸Servo On, Bit型式
        public const uint R_MPG_HOME = 1;   //是否在PLC中將手輪速度填入 回Home速度比率，每個bit代表1軸
        public const uint R_MPG_POSITION = 2;   //是否在PLC中將手輪速度填入 定位速度比率，每個bit代表1軸
        public const uint R_HOME_SEQ = 49000;    //1~32步驟的回原點順序
        public const uint R_HMI_AXIS_NAME = 49032;    //1~32軸稱文字

        //===========================警告、警報===================================
        //警告
        public const uint R_ALARM_PLC = 29000;//PLC Alarm 50個
        public const uint ALARM_PLC_SIZE = 50;//PLC Alarm 50個
        public const uint R_ALARM_OP = 80100;//OP Alarm(200x32個)
        public const uint ALARM_OP_SIZE = 200;//OP Alarm(200x32個)
        public const uint R_ALARM_MOT = 80300;//MOT Alarm(500x32個)
        public const uint ALARM_MOT_SIZE = 500;//MOT Alarm(500x32個)
        public const uint R_ALARM_INT = 80800;//INT Alarm(100x32個)
        public const uint ALARM_INT_SIZE = 100;//INT Alarm(100x32個)
        public const uint R_ALARM_MACRO = 80900;//MACRO Alarm(100x32個)
        public const uint ALARM_MACRO_SIZE = 100;//MACRO Alarm(100x32個)
        //警報
        public const uint R_WARNING_PLC = 29050;//PLC Warning 50個
        public const uint WARNING_PLC_SIZE = 50;//PLC Warning 50個
        public const uint R_WARNING_OP = 81100;//OP Warning(200x32個)
        public const uint WARNING_OP_SIZE = 100;//OP Warning(200x32個)
        public const uint R_WARNING_MOT = 81200;//MOT Warning(500x32個)
        public const uint WARNING_MOT_SIZE = 100;//MOT Warning(500x32個)
        public const uint R_WARNING_INT = 81300;//INT Warning(100x32個)
        public const uint WARNING_INT_SIZE = 100;//INT Warning(100x32個)
        public const uint R_WARNING_MACRO = 81400;//MACRO Warning(100x32個)
        public const uint WARNING_MACRO_SIZE = 100;//MACRO Warning(100x32個)  
        //==============================================================================


        //===========================系統類資訊=========================================
        public const uint SYS_CS_START = 3000;//系統類 C, S 的開始位址
        public const uint SYS_CS_USE = 5;//系統類的使用大小
        public const uint SYS_R_START = 28000;//系統類 R 的開始位址
        public const uint SYS_R_USE = 70;//系統類的使用大小
        //RESET    
        //(當發生警報時，可由此功能清除警報，勿用上緣訊號觸發，否則將不會有作用)	W	P/V Mode
        public const uint C_SYS_RESET = 3000;//系統RESET訊號。0：OFF，1：ON
        //警報與警告
        public const uint S_SYS_ALARM = 3000;//系統警報訊號。0：無，1：有
        public const uint S_SYS_WARNING = 3001;//系統警告訊號。0：無，1：有
        //REMOTE IO異常檢測
        public const uint S_SYS_REMOTE_1 = 3002;//REMOTE 1 IO狀態。0：正常，1：異常
        public const uint S_SYS_REMOTE_2 = 3003;//REMOTE 2 IO狀態。0：正常，1：異常
        //編輯保護功能
        public const uint C_SYS_EDIT_PROTECT = 3004;//編輯保護功能啟動(0:可編輯,1:不可編輯)
        public const uint S_SYS_EDIT_PROTECT = 3004;//編輯保護功能啟動(0:可編輯,1:不可編輯)
        //時間計時功能
        public const uint R_SYS_W_BIT_TIMER = 28064;//時間計數功能 0:暫停,1:起動
        public const uint R_SYS_W_BIT_TIMER_CLEAR = 28065;//清除時間計數 0:不清除,1:清除  
        public const uint R_SYS_R_BIT_TIMER_CLEAR = 28066;//清除時間計數完成 0:未完成,1:完成
        public const uint R_SYS_R_INT_TIMER_MS = 28000;//第1~32組時間計數功能毫秒數值(ms)
        public const uint R_SYS_R_INT_TIMER_HOUR = 28032;//第1~32組時間計數功能小時數值(hour)

        public const uint R_SYS_INFO = 20000;//新增定義 PLC將警報警告急停之資訊寫到R20000 
        //==============================================================================


        //===========================路徑類資訊=========================================
        //----------------- R 範圍定義                        
        public const uint PATH_R_START = 17000;//第一路徑開始的位址
        public const uint PATH_R_SIZE = 500;//每個路徑的資源數目
        public const uint PATH_R_USE = 30;//路徑使用的 R 個數 
        //----------------- C, S 範圍定義
        public const uint PATH_CS_START = 0;//第一路徑 C, S的開始位址
        public const uint PATH_CS_SIZE = 500;
        public const uint PATH_CS_USE = 10;
        //----------------- R 值定義
        public const uint R_PATH_W_SPEED_RATIO_MOVE = 17000;//快速定位速度百分比(0.01%)
        public const uint R_PATH_W_SPEED_RATIO_CUT = 17001;//切削進給速度百分比(0.01%)
        public const uint R_PATH_W_MODE = 17002;//Path Mode Select (0:Manual,1:Auto)
        public const uint R_PATH_R_STATUS = 17003;//Path State(0:Not Ready,1:Ready,2:Cycle Start,3:Feed Hold,4:Block Stop)


        public const uint R_PATH_R_INT_CUT_COUNT = 17019;//加工計數
      
        
        public const uint R_PATH_W_UPDATE = 17020;//通知解譯資料更新訊號(0:無 1:通知更新)
        public const uint R_PATH_R_START_LINE = 17021;//加工起始行號
        public const uint R_PATH_W_FILENAME = 17022;//加工檔名,共8個暫存器*4byte=32byte,每個R值可填入4個字元,
        //以ASCII方式填入,最大32字元。由R17022 低byte開始讀,遇到
        //某一個byte為0則視為名稱結尾。 R17022~R17029

        public const uint R_PATH_W_INT_CUT_ACC_L_TIME = 60100;//第1路徑　切削進給 直線部份 前加減速時間 (ms)
        public const uint R_PATH_W_INT_CUT_ACC_S_TIME = 60106;//第1路徑 切削進給 S型部份  前加減速時間 (ms)
        public const uint R_PATH_W_INT_CUT_SPEED_MAX = 60172;//第1路徑 切削進給 最大速度　（KLU/min)
       


        //----------------- C, S 定義
        //啟動、暫停
        public const uint C_PATH_CYCLE_START = 0;//Cycle Start
        public const uint C_PATH_FREE_HOLD = 1;//Feed Hold
        public const uint C_PATH_RESET = 2;//Reset(此功能不清除路徑警報)
        public const uint S_PATH_CYCLE_START = 0;//Cycle Start
        public const uint S_PATH_FREE_HOLD = 1;//Feed Hold
        public const uint S_PATH_RESET = 2;//Reset(此功能不清除路徑警報)
        //單步執行
        public const uint C_PATH_SINGLE_BLOCK = 3;//Path Single Block
        public const uint S_PATH_SINGLE_BLOCK = 3;//Path Single Block

        //運作模式               
        public const uint C_PATH_DRY_RUN = 4;//Path Dry Run
        public const uint C_PATH_MACHINE_LOCK = 5;//Path Machine Lock
        public const uint C_PATH_MST_IGONRE = 6;//MST Ignore
        public const uint C_PATH_NOT_READY = 7;//Not READY
        public const uint C_PATH_OPTIONAL_STOP = 8;//Optional stop
        public const uint S_PATH_DRY_RUN = 4;//Path Dry Run
        public const uint S_PATH_MACHINE_LOCK = 5;//Path Machine Lock
        public const uint S_PATH_MST_IGONRE = 6;//MST Ignore
        public const uint S_PATH_NOT_READY = 7;//Not READY
        public const uint S_PATH_OPTIONAL_STOP = 8;//Optional stop 

        //移動單節資訊
        public const uint S_PATH_BLOCK_STATUS = 94;//路徑移動單節狀態(0：結束，1：開始)
        public const uint S_PATH_BLOCK_BEFORE_FINISH = 95;//路徑移動單節比較量前檢查完成訊號(0：未完，1：完畢)
        public const uint S_PATH_BLOCK_AFTER_FINISH = 96;//路徑移動單節比較量後檢查完成訊號(0：未完，1：完畢)


        //程式再啟動
        public const uint C_PATH_RESTART = 15;//Program Restart Mode
        public const uint S_PATH_RESTART = 15;//Program Restart Mode
        public const uint R_PATH_W_RESTART_MODE = 17007;//程式再啟動方式, 0:行號, 1:序號
        public const uint R_PATH_W_RESTART_LINE = 17008;//程式再啟動行號或序號   
        public const uint R_PATH_R_RESTART_STATUS = 17009;//程式再啟動狀態(0:無,1:再啟中,2:找到指定行,-1:找不到)


        //=========================PAC8000 define============================================



        //軸一般資訊
        public const uint R_AXIS_W_BIT_EMG = 10000;//設定EMG發生時的信號值
        public const uint R_AXIS_R_BIT_EMG = 10001;//目前的EMG信號值
        public const uint R_AXIS_W_BIT_RESET = 10002;//設定Reset發生時的信號值
        public const uint R_AXIS_R_BIT_RESET = 10003;//目前的Reset信號值  
        public const uint R_AXIS_W_BIT_MODE = 10004;//軸模式切換訊號。0：位置模式，1：速度模式
        public const uint R_AXIS_R_BIT_MODE = 10005;//軸模式切換訊號完成訊號

        public const uint R_AXIS_R_BIT_MOVING = 10006;//軸移動狀態。0：靜止，1：轉動
        public const uint R_AXIS_R_BIT_MOVING_CW = 10021;//軸正向移動狀態。0：靜止或反轉，1：正轉
        public const uint R_AXIS_R_BIT_MOVING_CCW = 10022;//軸反向移動狀態。0：靜止或正轉，1：反轉

        public const uint R_AXIS_R_BIT_ALARM = 10011;//警報。0：無，1：有
        public const uint R_AXIS_R_BIT_WARNING = 10012;//警告。0：無，1：有

        public const uint R_AXIS_W_BIT_HW_LIMIT_CW = 10013;//設定是否觸發正向硬體極限。0：OFF，1：ON
        public const uint R_AXIS_W_BIT_HW_LIMIT_CCW = 10014;//設定是否觸發負向硬體極限。0：OFF，1：ON
        public const uint R_AXIS_R_BIT_SW_LIMIT_CW = 10015;//是否發生超過軟體極限正向極限值(0:無,1:有)
        public const uint R_AXIS_R_BIT_SW_LIMIT_CCW = 10016;//是否發生超過軟體極限負向極限值(0:無,1:有)

        public const uint R_AXIS_R_BIT_VCMD_BREAK = 10017;//是否發生Vcmd編碼器斷線(0:無,1:有)
        public const uint R_AXIS_R_BIT_DISMATCH = 10018;//軸命令與迴授超過最大誤差功能
        public const uint R_AXIS_R_BIT_ZERO_HOME = 10019;//是否軸原點位置完成且機械座標為零(0:否,1:是)

        //機械鎖定
        public const uint R_AXIS_W_BIT_MACHINE_LOCK = 10023;//第1~32軸機械鎖定功能。0：不鎖定,，1：鎖定 
        public const uint R_AXIS_R_BIT_MACHINE_LOCK = 10023;//第1~32軸是否曾經啟動過機械鎖定。0：否,，1：是

        //軸齒節誤差補償量的更新功能
        //當該軸進行雷射量測補償間隙時，可用此功能可使其修改之補償值生效。
        //軸發生警報時，此功能失效。
        //更新時機只能在該軸機械座標為0時，此功能才會有效。
        public const uint R_AXIS_W_BIT_POSTION_ADJ = 10030;//齒節誤差補償量更新啟動/關閉訊號。0：OFF，1：ON
        public const uint R_AXIS_R_BIT_POSTION_ADJ = 10031;//齒節誤差補償量更新啟動/關閉完成訊號

        //機械座標
        public const uint R_AXIS_R_INT_MACHINE_POS = 11564;//第1~32軸機械座標位置(LU)
        //軸編碼器數值資訊
        public const uint R_AXIS_R_INT_ENCODER = 11632;//第1~32軸編碼器數值(Pulse)
        public const uint R_AXIS_R_INT_ENCODER_POS = 11932;//第1~32軸編碼器座標(LU) 

        //位置資訊
        public const uint R_AXIS_R_INT_POS_ABSOLUTE = 83000;//第1~32軸絕對座標(LU)
        public const uint R_AXIS_R_INT_POS_RELATE = 83032;//第1~32軸相對座標(LU)
        public const uint R_AXIS_R_INT_POS_MACHINE = 83064;//第1~32軸機械座標(LU)
        public const uint R_AXIS_R_INT_SPEED_POS = 83100;//第1~32軸位置模式速度顯示(KLU/min)(K：位置模式速度倍率常數，由軸參數R071500~R071531設定)
        public const uint R_AXIS_R_INT_SPEED_SPEED = 83132;//第1~32軸速度模式速度顯示(KLU/min)(K：速度模式速度倍率常數，由軸參數R072232~R072263設定)
        public const uint R_AXIS_R_INT_POS_ENCODER = 83164;//第1~32軸編碼器數值(Pulse)
        public const uint R_AXIS_R_INT_POS_PULSE_LAG = 83200;//第1~32軸伺服誤差(Pulse)
        public const uint R_AXIS_R_INT_POS_VCMD_LAG = 83264;//第1~32軸Vcmd伺服誤差(Pulse) 
        public const uint R_AXIS_R_INT_POS_LEFT_DIS = 83400;//第1~32剩餘移動量(LU)
        public const uint R_AXIS_W_INT_POS_OFFSET = 3094032;  //第1~32軸座標偏移(LU)  
        public const uint R_AXIS_W_INT_APPLY_OFFSET = 3021201; //設為2 --> 套用座標偏移
        //MSTF
        public const uint R_AXIS_R_INT_F = 82066;
        public const uint R_AXIS_R_INT_M = 3006197;
        public const uint R_AXIS_R_INT_S = 3006207;
        public const uint R_AXIS_R_INT_T = 3006212;
        //軟體極限功能
        public const uint R_AXIS_W_BIT_SW_LIMIT_SRC_CW = 11596;//軟體極限正向I點訊號來源。0：OFF，1：ON
        public const uint R_AXIS_W_BIT_SW_LIMIT_SRC_CCW = 11597;//軟體極限負向I點訊號來源。0：OFF，1：ON
        public const uint R_AXIS_W_INT_SW_LIMIT_CW = 11500;//第1~32軸軟體極限正向極限值(LU)(原點復歸完成後生效)
        public const uint R_AXIS_W_INT_SW_LIMIT_CCW = 11532;//第1~32軸軟體極限負向極限值(LU)(原點復歸完成後生效)

        //原點復歸 ---- 軸發生警報時，此功能失效。
        public const uint R_AXIS_W_BIT_HOME = 10007;//原點復歸啟動/關閉訊號。0：OFF，1：ON
        public const uint R_AXIS_R_BIT_HOME = 10008;//原點復歸啟動/關閉完成訊號
        public const uint R_AXIS_W_BIT_HOME_SIGNAL = 10009;//原點復歸擋塊訊號來源。0：OFF，1：ON

        public const uint R_AXIS_R_BIT_HOME_FINISH = 10010;//原點位置是否完成。0：未完，1：完畢

        public const uint R_AXIS_R_BIT_HOME_RUNNING = 10020;//原點復歸狀態。0：等待中，1：執行中
        public const uint R_AXIS_W_INT_HOME_SPEED_RATIO = 11732;//第1~32軸原點復歸第一段速度比例(0.01%)
        public const uint R_AXIS_W_INT_HOME_OFFSET_ADJ = 11764;//第1~32軸原點復歸偏移量補值距離(LU)

        public const uint R_AXIS_W_INT_HOME_OFFSET = 77064;//第1~32軸原點復歸偏移量距離(LU)
        public const uint R_AXIS_W_INT_HOME_PAUSE_TIME = 77100;//第1~32軸原點復歸暫停時間（ms)

        public const uint R_AXIS_W_HOME_SPPED_1 = 77132;//第1~32軸原點復歸第一段速度(KLU/min) 
        public const uint R_AXIS_W_HOME_SPPED_2 = 77164;//第1~32軸原點復歸第一段速度(KLU/min)

        //JOG功能 ---- 軸發生警報時，此功能失效。
        public const uint R_AXIS_W_BIT_JOG = 11096;//JOG啟動/關閉訊號。0：OFF，1：ON
        public const uint R_AXIS_W_BIT_JOG_DIRECTION = 11098;//JOG方向。0：+，1：-
        public const uint R_AXIS_R_BIT_JOG = 11097;//JOG啟動/關閉完成訊號
        public const uint R_AXIS_W_INT_JOG_SPEED_CW = 11000;//第1~32軸JOG+速度(KLU/min)
        public const uint R_AXIS_W_INT_JOG_SPEED_CCW = 11032;//第1~32軸JOG-速度(KLU/min)
        public const uint R_AXIS_W_INT_JOG_SPEED_RATIO = 11064;//第1~32軸JOG速度比例(0.01%)

        //定位功能 ---- 軸發生警報時，此功能失效。
        public const uint R_AXIS_W_BIT_MOVE = 11196;//定位啟動/關閉訊號。0：OFF，1：ON
        public const uint R_AXIS_R_BIT_MOVE_FINISH = 11197;//定位完成訊號。0：未完成，1：完成
        public const uint R_AXIS_W_BIT_MOVE_DIRECTION = 11198;//定位方向。0：不反向，1：反向
        public const uint R_AXIS_W_BIT_MOVE_MODE = 11199;//定位距離種類。0：增量，1：絕對
        public const uint R_AXIS_W_INT_MOVE_DISTANCE = 11100;//第1~32軸定位距離(LU)
        public const uint R_AXIS_W_INT_MOVE_SPEED = 11132;//第1~32軸定位速度(KLU/min)
        public const uint R_AXIS_W_INT_MOVE_SPEED_RATIO = 11164;//第1~32軸定位速度比例(0.01%)
        //定位檢查
        public const uint R_AXIS_W_INT_MOVE_BEFORE = 11800;//第1~32軸定位比較量前檢查距離(LU)
        public const uint R_AXIS_W_INT_MOVE_AFTER = 11832;//第1~32軸定位比較量後檢查距離(LU)
        public const uint R_AXIS_R_BIT_MOVE_BEFORE_CK = 11896;//第1~32軸定位比較量前檢查完成訊號 0：未完，1：完畢
        public const uint R_AXIS_R_BIT_MOVE_AFTER_CK = 11897;//第1~32軸定位比較量後檢查完成訊號 0：未完，1：完畢

        //速度功能 ---- 軸發生警報時，此功能失效。
        public const uint R_AXIS_W_BIT_SPEED = 11296;//速度啟動/關閉訊號。0：OFF，1：ON
        public const uint R_AXIS_R_BIT_SPEED = 11297;//速度啟動/關閉完成訊號
        public const uint R_AXIS_R_BIT_SPEED_IN_ALLOW = 11298;//第1~32軸速度模式命令與回授誤是否在差容許範圍內(OFF：超出範圍，ON：在範圍內)
        public const uint R_AXIS_W_INT_SPEED_SPEED = 11200;//第1~32軸速度設定(KLU/min)
        public const uint R_AXIS_W_INT_SPEED_RATIO = 11364;//第1~32軸速度比例(0.01%)

        //手輪功能
        public const uint R_AXIS_W_BIT_MPG = 11496;   //第1~32軸MPG位置啟動/關閉訊號。0：OFF，1：ON
        public const uint R_AXIS_R_BIT_MPG = 11496;   //第1~32軸MPG位置啟動/關閉完成訊號
        public const uint R_AXIS_W_INT_MPG_RATIO_POS = 11400;   //第1~32軸MPG位置功能倍率(0.0001)  
        public const uint R_AXIS_W_INT_MPG_RATIO_SPEED = 11432;   //第1~32軸

        //軸同步追隨控制功能 ---- 軸發生警報時，此功能失效
        public const uint R_AXIS_W_BIT_FOLLOW = 11696;//同步追隨控制啟動/關閉訊號。0：OFF，1：ON
        public const uint R_AXIS_R_BIT_FOLLOW = 11697;//同步追隨控制啟動/關閉完成訊號
        public const uint R_AXIS_W_BIT_FOLLOW_DIRECTION = 11698;//軸同步追隨控制方向。0：不反向，1：反向
        public const uint R_AXIS_W_INT_FOLLOW_AXIS = 11600;//同步追隨控制功能CMR分子。(設定值必需大於0，否則設定值將不被接受)
        public const uint R_AXIS_W_INT_FOLLOW_CMR_U = 11664;//同步追隨控制功能追隨軸號。0：不使用，1~32：第1~32軸
        public const uint R_AXIS_W_INT_FOLLOW_CMR_D = 11700;//同步追隨控制功能CMR分母(設定值必需大於0，否則設定值將不被接受)

        //加減速時間 
        public const uint R_AXIS_W_INT_MOVE_ACC_L_TIME = 71132;//第1~32軸 快速移動 直線部份 後加減速時間 (ms)
        public const uint R_AXIS_W_INT_MOVE_ACC_S_TIME = 71164;//第1~32軸 快速移動 S型部份  後加減速時間 (ms)    
        public const uint R_AXIS_W_INT_CUT_ACC_L_TIME = 71200;//第1~32軸 切削進給 直線部份 後加減速時間 (ms)
        public const uint R_AXIS_W_INT_CUT_ACC_S_TIME = 71232;//第1~32軸 切削進給 S型部份  後加減速時間 (ms)
        //==============================================================================

        //路徑共用全域變數
        public const uint F_SYS_GLOBAL_START = 10033000;  //全域變數的開始位址
        public const uint F_SYS_GLOBAL_SIZE = 5000;    //全域變數的個數


        //----------------- Fix值，範圍定義
        public const uint F_PATH_GLOBAL_START = 10000000;//全域變數的開始位址
        public const uint F_PATH_GLOBAL_SIZE = 5000;   //全域變數的個數
        public const uint F_PATH_LOCAL_START = 10030000; //區域變數的開始位址
        public const uint F_PATH_LOCAL_SIZE = 500;     //區域變數的個數


        // 解譯資訊 
        public const uint PAC_STRING_SIZE = 20;
        public const uint PAC_STRING_NUM = 21;
        public const uint PAC_PATH_SIZE = 5000;
        public const uint R_PATH_PAC_STRING = 3021500;
        public const uint BLOCK_PATH_SIZE = 2000;
        public const uint R_PATH_BLOCK_CURRENT = 3006072;

        //============================加工程式 警報 ====================================
        public const uint R_NC_ALARM_PATH = 3021276;  //警報單節所屬檔案目錄
        public const uint R_NC_ALARM_PATH_SIZE = 64;     //警報單節所屬檔案目錄--資料長度
        public const uint R_NC_ALARM_FILENAME = 3021340;   //警報單節所屬檔名
        public const uint R_NC_ALARM_FILENAME_SIZE = 8;     //警報單節所屬檔名--資料長度
        public const uint R_NC_ALARM_LINE = 3021348;  //警報單節所屬行號
        public const uint R_NC_ALARM_STRING = 3021349;  //警報解譯動態字串  
        public const uint R_NC_ALARM_STRING_SIZE = 64;     //警報解譯動態字串--資料長度
        //==============================================================================        
        #endregion scif_define_pac.h
        #region scif.dll
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_Init(ref DLL_USE_SETTING pUseSetting, int MakerID, string pEncString);
        //[DllImport("scif_vc_x64.dll",CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [DllImport(ReCON_DLL_NAME, EntryPoint = "scif_Init_2")]
        public static extern int scif_Init_2(ref DLL_USE_SETTING pUseSetting, string IniFilename);
        [DllImport(ReCON_DLL_NAME, EntryPoint = "scif_Destroy")]
        public static extern void scif_Destroy();
        [DllImport(ReCON_DLL_NAME)]
        public static extern void scif_SetTalkSoftwareType(sbyte SessionIdx, uint SoftwareType);
        [DllImport(ReCON_DLL_NAME)]
        public static extern void scif_SetMediaIP(string MediaIP);
        [DllImport(ReCON_DLL_NAME)]
        public static extern void scif_SetDebug(byte level);




        //=============================讀取 Local data =============
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_SetMirror(sbyte ServerIdx);
        [DllImport(ReCON_DLL_NAME)]
        public static extern sbyte scif_ReadI(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern sbyte scif_ReadO(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern sbyte scif_ReadC(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern sbyte scif_ReadS(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern sbyte scif_ReadA(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern sbyte scif_ReadTMR(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern sbyte scif_ReadCNT(uint addr);

        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_ReadR(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_ReadTMRV(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_ReadTMRS(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_ReadCNTV(uint addr);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_ReadCNTS(uint addr);

        [DllImport(ReCON_DLL_NAME)]
        public static extern double scif_ReadF(uint addr);



        //===================區域網路中偵測主機功能
        //自動偵測主機功能
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_LocalDetectControllers();
        //讀取取得的控制器資料筆數
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_LocalReadControllerCount();
        //讀取取得的控制器資料
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_LocalReadController(ushort Index, ref LOCAL_CONTROLLER_INFO Info);
        //與取得的控制器清單中的Index值進行連線
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_ConnectLocalList(sbyte ServerIdx, ushort Index);


        //--==================連線功能=================================
        //直接輸入控制器IP進行連線
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_LocalConnectIP(sbyte ServerIdx, string IP);
        //中斷連線
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_Disconnect(sbyte ServerIdx);


        //--=================檔案傳輸功能
        //設定 FTP 索引
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpSetTalk(sbyte ServerIdx);
        //上傳檔案
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpUploadFile(byte Folder, string Filename, string LocalFilename);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpUploadFile2(byte Folder, string SubFolder, string Filename, string LocalFilename);
        //下載檔案
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpDownloadFile(byte Folder, string Filename, string LocalFilename);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpDownloadFile2(byte Folder, string SubFolder, string Filename, string LocalFilename);
        //刪除檔案
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpDeleteFile(byte Folder, string Filename);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpDeleteFile2(byte Folder, string SubFolder, string Filename);


        //增加幾個函式，方便 C# 使用
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpTransferFileReset();
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpTransferFileAdd(byte Folder, string SubFolder, string Filename, string LocalFilename);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpUploadFiles2();
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpDownloadFiles2();
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpDeleteFiles2();


        //----------------------------遠端檔案清單 
        //建立資料夾
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpMakeDir(byte Folder, string DirName);
        //取得檔案清單
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpGetFileList(byte Folder, string HeadFilter, string TailFilter);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpGetFileList2(byte Folder, string SubFolder, string HeadFilter, string TailFilter);
        //取得執行結果
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpCheckDone(ref byte State, ref byte Result);
        //讀取FTP檔案清單筆數
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpReadFileCount();
        //讀取FTP檔案名稱
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FtpReadFile(ushort Index, ref FTP_FILE file);

        //----------------------------本地端檔案清單
        //取得本地端檔案清單
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FileGetFileList(string Path, string HeadFilter, string TailFilter);
        //讀取本地檔案清單筆數
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FileReadFileCount();
        //讀取本地檔案名稱
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FileReadFile(ushort Index, ref FTP_FILE file);
        //刪除本地檔案名稱
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_FileDeleteFile(ushort Index);







        //--====================取得內部資訊
        //-----取得一般的資訊
        [DllImport(ReCON_DLL_NAME)]
        public static extern uint scif_GetCommonMsg(byte id);
        //-----取得連線通訊的資訊
        [DllImport(ReCON_DLL_NAME)]
        public static extern uint scif_GetTalkMsg(sbyte ServerIdx, sbyte id);
        //-----取得錯誤訊息 
        [DllImport(ReCON_DLL_NAME)]
        public static extern void scif_GetTalkError(sbyte ServerIdx, ref ERROR_MSG Msg);



        //============================= 取得通訊處理狀態 ================================
        [DllImport(ReCON_DLL_NAME)]
        public static extern sbyte scif_GetTranState(int pTran);
        //========= 由 Default Loop Queue 中尋找資料並讀出 =============
        [DllImport(ReCON_DLL_NAME)]
        public static extern IntPtr scif_GetDefaultQueueDataPointer(sbyte SessionIdx, byte TranIdx);
        [DllImport(ReCON_DLL_NAME)]
        public static extern IntPtr scif_GetDataPointerByTranPointer(int TranPointer);
        //==============================================================================


        //========= 從這裡以下的部份開始，SessionIdx設定為-1時，代表同時對全部的 連線進行設定 ============

        //----------清除命令
        [DllImport(ReCON_DLL_NAME)]
        public static extern void scif_cmd_ClearAll(sbyte type, sbyte SessionIdx);


        //等待直接命令完成
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_WaitDirectCmdDone(sbyte SessionIdx, uint MaxWaitTime);


        //設定組合大封包的最大數

        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_SetMaxGroupPkg(sbyte SessionIdx, byte Counte);


        // ========================================================
        //以下函式回傳值若為 0 ，代表指令初拒絕，若不為 0 ，代表是該筆通訊的指標
        //此時，將其帶入 scif_GetTranState 的引數中，取得該筆通訊的狀態，將會是 SC_TRANSACTION_RESET
        //一段時間之後再帶入 scif_GetTranState 的引數中，回傳值若為 SC_TRANSACTION_FINISH 代表該筆通訊已被處理
        // ========================================================

        //  ============================= Single write ============
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteI(sbyte ServerIdx, uint addr, byte val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteO(sbyte ServerIdx, uint addr, byte val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteC(sbyte ServerIdx, uint addr, byte val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteS(sbyte ServerIdx, uint addr, byte val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteA(sbyte ServerIdx, uint addr, byte val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteTMR(sbyte ServerIdx, uint addr, byte val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteCNT(sbyte ServerIdx, uint addr, byte val);

        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteR(sbyte ServerIdx, uint addr, int val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteTMRV(sbyte ServerIdx, uint addr, int val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteTMRS(sbyte ServerIdx, uint addr, int val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteCNTV(sbyte ServerIdx, uint addr, int val);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteCNTS(sbyte ServerIdx, uint addr, int val);

        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteF(sbyte ServerIdx, uint addr, double val);


        //  ============================= Multi write
        // num 的最大值 MAX_BIT_NUM
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiI(sbyte ServerIdx, uint addr, uint num, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiO(sbyte ServerIdx, uint addr, uint num, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiC(sbyte ServerIdx, uint addr, uint num, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiS(sbyte ServerIdx, uint addr, uint num, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiA(sbyte ServerIdx, uint addr, uint num, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiTMR(sbyte ServerIdx, uint addr, uint num, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiCNT(sbyte ServerIdx, uint addr, uint num, byte[] data);

        // num 的最大值 MAX_INT_NUM
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiR(sbyte ServerIdx, uint addr, uint num, int[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiTMRV(sbyte ServerIdx, uint addr, uint num, int[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiTMRS(sbyte ServerIdx, uint addr, uint num, int[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiCNTV(sbyte ServerIdx, uint addr, uint num, int[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiCNTS(sbyte ServerIdx, uint addr, uint num, int[] data);

        // num 的最大值 MAX_FIX_NUM
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteMultiF(sbyte ServerIdx, uint addr, uint num, double[] data);

        //  ============================= combin write ============   
        // num 的最大值 MAX_CB_BIT_NUM
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteI(sbyte ServerIdx, uint num, uint[] addr, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteO(sbyte ServerIdx, uint num, uint[] addr, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteC(sbyte ServerIdx, uint num, uint[] addr, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteS(sbyte ServerIdx, uint num, uint[] addr, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteA(sbyte ServerIdx, uint num, uint[] addr, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteTMR(sbyte ServerIdx, uint num, uint[] addr, byte[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteCNT(sbyte ServerIdx, uint num, uint[] addr, byte[] data);

        // num 的最大值 MAX_CB_INT_NUM
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteR(sbyte ServerIdx, uint num, uint[] addr, uint[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteTMRV(sbyte ServerIdx, uint num, uint[] addr, uint[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteTMRS(sbyte ServerIdx, uint num, uint[] addr, uint[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteCNTV(sbyte ServerIdx, uint num, uint[] addr, uint[] data);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteCNTS(sbyte ServerIdx, uint num, uint[] addr, uint[] data);

        // num 的最大值 MAX_CB_FIX_NUM
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteF(sbyte ServerIdx, uint num, uint[] addr, double[] data);

        //  ========================= continue read
        // num 的最大值 MAX_BIT_NUM 
        // 當 num 小於等於  MAX_CB_BIT_NUM/2 且 有呼叫 scif_StartComboinSet() 且 為POLLING command 時，將會被放到 combine
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadI(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadO(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadC(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadS(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadA(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadTMR(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadCNT(sbyte type, sbyte ServerIdx, uint addr, uint num);

        // num 的最大值 MAX_INT_NUM
        // 當 num 小於等於  MAX_CB_INT_NUM/2 且 有呼叫 scif_StartComboinSet() 且 為POLLING command 時，將會被放到 combine
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadR(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadTMRV(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadTMRS(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadCNTV(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadCNTS(sbyte type, sbyte ServerIdx, uint addr, uint num);

        // num 的最大值 MAX_FIX_NUM
        // 當 num 小於等於  MAX_CB_FIX_NUM/2 且 有呼叫 scif_StartComboinSet() 且 為POLLING command 時，將會被放到 combine
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadF(sbyte type, sbyte ServerIdx, uint addr, uint num);
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_ReadP(sbyte type, sbyte ServerIdx, uint addr, uint num);

        //===============字串存取功能==============
        //由鏡射記憶體中讀取字串
        [DllImport(ReCON_DLL_NAME)]
        public static extern uint scif_ReadRString(uint addr, uint BufSize, byte[] Buf);
        //寫入字串到 R
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteRString(sbyte ServerIdx, uint addr, uint BufSize, byte[] Buf);
        //寫入 R bit
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cmd_WriteRBit(sbyte ServerIdx, uint addr, byte BitIdx, byte BitValue);
        //寫入 多個 R bit
        [DllImport(ReCON_DLL_NAME)]
        public static extern int scif_cb_WriteRBit(sbyte ServerIdx, uint num, uint[] addr, byte[] BitIdx, byte[] BitValue);
        //=========================================
        //===============開始與結束 Combin Queue==================  
        //在呼叫 scif_StartComboin 之後，未呼叫scif_FinishComboin()之前，
        //若 scif_cmd_Readxxx() 時傳入的 num 太小，該命令的內容將會被放到一暫存的Buf
        //等到 呼叫 scif_FinishComboin 之時，才會將 Buf 的內容重新整理過，自動填入
        //scif_cb_Readxxx 的命令中
        //設定自動組合旗標
        [DllImport(ReCON_DLL_NAME)]
        public static extern void scif_StartCombineSet(sbyte ServerIdx);
        //完成自動組合設定並開始產生組合封包
        [DllImport(ReCON_DLL_NAME)]
        public static extern void scif_FinishCombineSet(sbyte ServerIdx);

        #endregion scif.dll

    }
}
