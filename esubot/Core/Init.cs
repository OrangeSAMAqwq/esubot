using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace esubot.Core
{
    public class Init
    {
        /// <summary>
        /// 初始化类方法，将在类实例化后被执行。
        /// </summary>
        public Init()
        {
            InitDirectory();
            InitCoreConfig();
        }

        /// <summary>
        /// 初始化机器人所需要使用到的文件夹。
        /// </summary>
        /// <returns>执行情况</returns>
        private static int InitDirectory()
        {
            //需要使用到的数据路径。
            string currPath = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = currPath + @"/config/";
            string dataPath = currPath + @"/data/";
            //string databasePath = currPath + @"/database/";

            //检测路径是否存在，不存在则创建。
            if (false == System.IO.Directory.Exists(configPath))
            {
                System.IO.Directory.CreateDirectory(configPath);
            }
            if (false == System.IO.Directory.Exists(dataPath))
            {
                System.IO.Directory.CreateDirectory(dataPath);
            }
            //if (false == System.IO.Directory.Exists(databasePath))
            //{
            //    System.IO.Directory.CreateDirectory(databasePath);
            //}

            //输出执行情况。
            Log.LogOut("", "初始化:数据文件夹:执行成功");
            return 0;
        }

        /// <summary>
        /// 初始化核心配置文件
        /// </summary>
        /// <returns>执行情况</returns>
        private int InitCoreConfig() // 初始化核心配置文件
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/config/";
            string configFilePath = configPath + @"CoreConfig.yaml";

            if (false == System.IO.File.Exists(configFilePath))
            {
                FileStream fs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();

                var config = new CoreConfig
                {
                    account = "2984530578",
                    ip = "127.0.0.1",
                    port = "8080",
                    authkey = "1145141919810",
                };
                var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(config);

                FileStream fsc = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter swc = new StreamWriter(fsc);
                swc.Write(yaml);
                swc.Close();
                Log.LogOut("", "初始化:InitCoreConfig:执行成功");

            }
            return 0;
        }
    }

    /// <summary>
    /// 对MiraiCS所需要使用的MiraiHttp的核心参数账号、密码、端口、authkey的实体类，用以yaml的序列化和反序列化。
    /// </summary>
    public class CoreConfig
    {
        public string account { get; set; }
        public string ip { get; set; }
        public string port { get; set; }
        public string authkey { get; set; }
    }
}
