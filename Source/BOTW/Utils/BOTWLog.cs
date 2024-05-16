using UnityEngine;
using Verse;

namespace BOTW
{
    /// <summary>
    /// Just a little improvement to any log messages/warnings/and errors we want to throw.
    /// Color coded for enhanced clarity!
    /// </summary>
    public static class BOTWLog
    {
        public static Color ErrorMsgCol = new(0.4f, 0.54902f, 1.0f);
        public static Color WarningMsgCol = new(0.70196f, 0.4f, 1.0f);
        public static Color MessageMsgCol = new(0.4f, 1.0f, 0.54902f);

        public static void Error(string msg)
        {
            Log.Error("[BOTW] ".Colorize(ErrorMsgCol) + msg);
        }

        public static void Warning(string msg)
        {
            Log.Warning("[BOTW] ".Colorize(WarningMsgCol) + msg);
        }

        public static void Message(string msg)
        {
            Log.Message("[BOTW] ".Colorize(MessageMsgCol) + msg);
        }
    }
}
