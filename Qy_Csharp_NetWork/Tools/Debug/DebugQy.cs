/***********************************************************
*                   *  调试输出类  *                       *
*                                                          *
*   1.根据宏判断是否需要输出                               *
*   2.可调节调试时所输出的平台（默认为Unity_Engine平台）   *
*   3.考虑日后的多种类扩展输出                             *
*                                                          *
************************************************************/

#if DEBUG
//#define CSHARP_CONSOLE
#define UNITY_ENGINE
#endif


#if DEBUG
#endif
using System.Threading;

namespace Qy_CSharp_NetWork.Tools.Debug
{
    public class DebugQy
    {
        public static object m_lockHelper = new object();

        private static bool m_Global = true;
        private static bool m_CanLog = true;
        private static bool m_CanWarning = true;
        private static bool m_CanError = true;
        private static bool m_CanLogTag = true;
        private static bool m_CanLogException = true;
        public static bool NeedLog
        {
            get
            {
                return m_Global;
            }
            set
            {
                m_Global = value;
                if (m_Global)
                {
                    m_CanLog = m_Global;
                    m_CanWarning = m_Global;
                    m_CanError = m_Global;
                    m_CanLogTag = m_Global;
                    m_CanLogException = m_Global;
                }
                else
                {
                    m_CanLog = m_Global;
                    m_CanWarning = m_Global;
                    m_CanError = m_Global;
                    m_CanLogTag = m_Global;
                    m_CanLogException = m_Global;
                }

            }
        }
        public static void SelectWhichLogCanWork(bool CanLog = true, bool CanWarning = true, bool CanError = true, bool CanLogTag = true, bool CanLogException = true)
        {
            m_CanLog = CanLog;
            m_CanWarning = CanWarning;
            m_CanError = CanError;
            m_CanLogTag = CanLogTag;
            m_CanLogException = CanLogException;
        }
        public static void Log(string logMsg)
        {
#if DEBUG
            Monitor.Enter(m_lockHelper);
            if (m_CanLog)
            {
#if CSHARP_CONSOLE
                System.Console.WriteLine("[LAM]:"+logMsg);
#elif UNITY_ENGINE
                UnityEngine.Debug.Log("[LAM]:" + logMsg);
#endif
            }
            Monitor.Exit(m_lockHelper);
#endif
        }
        public static void LogWarning(string logMsg)
        {
#if DEBUG
            Monitor.Enter(m_lockHelper);
            if (m_CanWarning)
            {
#if CSHARP_CONSOLE
                System.Console.BackgroundColor = System.ConsoleColor.Black;
                System.Console.ForegroundColor = System.ConsoleColor.DarkYellow;
                System.Console.WriteLine("[LAM]:" + "-Warning-: " + logMsg);
                System.Console.BackgroundColor = System.ConsoleColor.Black;
                System.Console.ForegroundColor = System.ConsoleColor.Gray;
#elif UNITY_ENGINE
                UnityEngine.Debug.LogWarning("[LAM]:" + logMsg);
#endif
            }
            Monitor.Exit(m_lockHelper);
#endif
        }
        public static void LogError(string logMsg)
        {
#if DEBUG
            Monitor.Enter(m_lockHelper);
            if (m_CanError)
            {
#if CSHARP_CONSOLE
                System.Console.BackgroundColor = System.ConsoleColor.Black;
                System.Console.ForegroundColor = System.ConsoleColor.DarkRed;
                System.Console.WriteLine("[LAM]:" + "-Error-: " + logMsg);
                System.Console.BackgroundColor = System.ConsoleColor.Black;
                System.Console.ForegroundColor = System.ConsoleColor.Gray;
#elif UNITY_ENGINE
                UnityEngine.Debug.LogError("[LAM]:" + logMsg);
#endif
            }
            Monitor.Exit(m_lockHelper);
#endif
        }
        public static void LogTag(string tag = "", string logMsg = "", string color = "#AEA74BFF")
        {
#if DEBUG
            Monitor.Enter(m_lockHelper);
            if (m_CanLogTag)
            {
#if CSHARP_CONSOLE
            System.Console.BackgroundColor = System.ConsoleColor.DarkGreen;
            System.Console.ForegroundColor = System.ConsoleColor.Black;
            System.Console.Write("[LAM]:" + "[" + tag + "]");
            System.Console.BackgroundColor = System.ConsoleColor.Black;
            System.Console.ForegroundColor = System.ConsoleColor.Gray;
            System.Console.WriteLine("--->" + logMsg);
#elif UNITY_ENGINE
                UnityEngine.Debug.Log("[LAM]:" + "<color=" + color + ">[" + tag + "]---></color> " + logMsg);
#endif
            }
            Monitor.Exit(m_lockHelper);
#endif
        }
        public static void LogException(System.Exception ex)
        {
#if DEBUG
            Monitor.Enter(m_lockHelper);
            if (m_CanLogException)
            {
#if CSHARP_CONSOLE
                System.Console.BackgroundColor = System.ConsoleColor.Gray;
                System.Console.ForegroundColor = System.ConsoleColor.DarkRed;
                System.Console.WriteLine("[LAM]:" + "-exception- :");
                System.Console.WriteLine("              -Message-   : " + ex.Message);
                System.Console.WriteLine("              -Source-    : " + ex.Source);
                System.Console.WriteLine("              -StackTrace-: " + ex.StackTrace);
                System.Console.BackgroundColor = System.ConsoleColor.Black;
                System.Console.ForegroundColor = System.ConsoleColor.Gray;
#elif UNITY_ENGINE
                UnityEngine.Debug.LogError("[LAM]:" + ex.Message);
#endif
            }
            Monitor.Exit(m_lockHelper);
#endif
        }
    }
}
