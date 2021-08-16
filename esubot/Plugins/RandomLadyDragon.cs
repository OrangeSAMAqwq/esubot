using System;
using System.IO;
using esubot.Tools;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace esubot.Plugins
{
    public class RandomLadyDragon
    {
        string _dPicData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomLadyDragon");
        string _dPicFile;

        /// <summary>
        /// 构造方法，用以确认文件夹是否存在。
        /// </summary>
        public RandomLadyDragon()
        {
            if (false == System.IO.Directory.Exists(_dPicData))
            {
                System.IO.Directory.CreateDirectory(_dPicData);
            }
        }

        /// <summary>
        /// 龙图获取方法。
        /// </summary>
        /// <returns>获取的龙图地址</returns>
        public string GetRandomLadyDragonPic()
        {

            int maxNumber = FileTool.GetFileNum(_dPicData);
            Random ran = new Random(); // 引入随机数，使用系统时间作为随机数种子
            int n = ran.Next(1, maxNumber); // 随机数字限定在获取到的龙图最大值

            _dPicFile = Path.Combine(_dPicData, n + ".jpg");
            return _dPicFile;
        }


    }
}
