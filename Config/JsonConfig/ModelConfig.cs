using System;
using System.IO;
using Newtonsoft.Json;

namespace Ryan.Core.Utility
{ 
    public class ModelConfig
    {
        public static  T GetSetting<T>(string configFileName="")
        {
            if (string.IsNullOrWhiteSpace(configFileName))
            {
                configFileName = typeof (T).Name;
            }
            configFileName = configFileName + ".json";
            var path = Path.Combine(CheckConfiDir(), configFileName);
            if (!File.Exists(path))
            {
                return default(T);
            }
           return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        public static  bool SaveSetting<T>(T setting,string fileName="")
        {
            try
            {
                var data = JsonConvert.SerializeObject(setting);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = setting.GetType().Name;
                }
                fileName = fileName + ".json";
               
                var path = Path.Combine(CheckConfiDir(), fileName);
                File.WriteAllText(path,data);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static string CheckConfiDir()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }

    public class RyanSetting
    {
        public bool CanGet { get; set; }
    }
}