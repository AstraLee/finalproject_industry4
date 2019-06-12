using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FormsReCON_Example
{
    class CommonMethod
    {
        public const int GWL_STYLE = (-16);
        public const int WS_VISIBLE = 0x10000000;
        /// <summary>
        /// //启动窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern bool AllocConsole();
        /// <summary>
        /// /释放窗口，即关闭
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "FreeConsole")]
        public static extern bool FreeConsole();
        /// <summary>
        /// //找出运行的窗口
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary>
        /// //取出窗口运行的菜单
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        public extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        /// <summary>
        /// 灰掉按钮
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        public extern static IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
        /// <summary>
        /// 修改标题
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern bool SetConsoleTitle(string strMessage);

        /// <summary>
        /// 设置控制台(Console)在什么组件上显示
        /// </summary>
        /// <param name="hWndChild">Console窗体</param>
        /// <param name="hWndNewParent">显示Console的组件</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        /// <summary>
        /// Remove border and whatnot
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        public static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);
        /// <summary>
        /// 改变窗体的大小
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="repaint"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
        /// <summary>
        /// 设置字体
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="wAttributes"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int SetConsoleTextAttribute(IntPtr hConsoleOutput, int wAttributes);

        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    }
}
