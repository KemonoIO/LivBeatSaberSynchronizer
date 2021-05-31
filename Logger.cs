using System;
using System.IO;

namespace LivBeatSaberSynchronizer
{
    public class Logger
    {
        readonly StreamWriter log;

        public Logger()
        {
            string logPath = Utility.GetUserProfilePath() +
                @"\Documents\LIV\Plugins\CameraBehaviours\LivBeatSaberSynchronizer.log";
            log = new StreamWriter(logPath, true);
        }

        ~Logger()
        {
            log.Close();
        }

        public void Log(String message)
        {
            log.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + message);
            log.Flush();
        }
    }
}