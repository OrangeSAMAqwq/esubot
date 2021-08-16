using esubot.Core;
using esubot.Message;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace esubot
{
    public static class Program
    {

        /// <summary>
        /// Main method for bot action using async.
        /// </summary>
        /// <returns>status</returns>
        public static async Task Main()
        {
            Init init = new Init(); // 初始化参数文件。

            /*
             * 下列是对coreconfig反序列化读取各项配置。
             */
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            string config = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/config/CoreConfig.yaml");
            var sb = deserializer.Deserialize<CoreConfig>(config);

            string ip = sb.ip; // ip。
            int port = int.Parse(sb.port); // 端口。
            string authkey = sb.authkey; // http authkey。
            long qq = long.Parse(sb.account); // 账号。

            /*
             * Mirai初始化。
             */

            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions(ip, port, authkey); //MiraiQQ Http。
            await using MiraiHttpSession session = new MiraiHttpSession();

            /*
             * 插件装载：实例化插件对象。
             */
            GroupEventNotice groupEventNotice = new GroupEventNotice();
            MsgGroupManager msgGroupManager = new MsgGroupManager();

            session.AddPlugin(groupEventNotice);
            session.AddPlugin(msgGroupManager);

            /*
             * QQ
             */
            await session.ConnectAsync(options, qq); // QQ号
            while (true)
            {
                if (await Console.In.ReadLineAsync() == "exit")
                {
                    return;
                }
            }
        }
    }
}