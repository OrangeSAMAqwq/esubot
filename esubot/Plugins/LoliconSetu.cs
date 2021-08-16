using System;
using System.Collections.Generic;
using System.IO;
using esubot.Tools;
using Newtonsoft.Json;

namespace esubot.Plugins
{
    public class LoliconSetu
    {
        string _apikey = ""; 
        int _pid = 0;
        int _p = 0;
        int _uid = 0;
        string _title = "";
        string _author = "";
        string _originalUrl = "";
        string _largeUrl = "";
        bool _r18 = false;
        int _width = 0;
        int _height = 0;
        List<string> _tag;
        string _setuData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu/lolicon");
        string _setuFile;

        public LoliconSetu()
        {
        }

        /// <summary>
        /// 获取无Tag无r18的色图。
        /// </summary>
        /// <returns>色图存放地址</returns>
        public string GetSetu()
        {
            string setuJson = HttpTool.Get($"https://api.lolicon.app/setu/v2/?size=regular&r18=0", "");
            ParseSetu(setuJson); // 分析色图。
            DownloadSetu(); // 下载色图。
            return _setuFile;
            
        }

        /// <summary>
        /// 获取无tag的r18色图
        /// </summary>
        /// <returns>色图存放地址</returns>
        public string GetSetuR18()
        {
            string setuJson = HttpTool.Get($"https://api.lolicon.app/setu/v2/?size=regular&r18=1", "");
            ParseSetu(setuJson); // 分析色图。
            DownloadSetu(); // 下载色图。
            return _setuFile;
        }

        /// <summary>
        /// 处理返回的json信息。
        /// </summary>
        /// <param name="setuJson">需要反序列化的json</param>
        private int ParseSetu(string setuJson)
        {
            LoliconJson result = JsonConvert.DeserializeObject<LoliconJson>(setuJson);
            if (result.code == 0)
            {
                _originalUrl = result.data[0].url;
                _pid = result.data[0].pid;
            }
            return 0;
        }

        /// <summary>
        /// 下载色图。
        /// </summary>
        /// <returns>该方法的执行情况</returns>
        private int DownloadSetu()
        {
            if (_originalUrl.Contains("jpg"))
            {
                _setuFile = Path.Combine(_setuData, _pid + ".jpg");
            }
            else if (_originalUrl.Contains("png"))
            {
                _setuFile = Path.Combine(_setuData, _pid + ".png");
            }
            else
            {
                _setuFile = Path.Combine(_setuData, _pid + ".jpg"); // 感觉这样不太好？
            }

            byte[] pic = HttpTool.GetBytesFromUrl(this._originalUrl);
            HttpTool.WriteBytesToFile(this._setuFile, this._setuData, pic);
            return 0;
        }
    }

    /// <summary>
    /// 对Lolicon的json的解析反序列化使用的实体类。
    /// </summary>
    public class LoliconJson
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int quota { get; set; }
        public int quotaMinTtl { get; set; }
        public int count { get; set; }
        public List<SetuImageJson> data { get; set; }
    }

    /// <summary>
    /// 对Lolicon的json的图片信息反序列化使用的实体类。
    /// </summary>
    public class SetuImageJson
    {
        public int pid { get; set; }
        public int p { get; set; }
        public int uid { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string url { get; set; }
        public bool r18 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public List<string> tags { get; set; }
    }
}
