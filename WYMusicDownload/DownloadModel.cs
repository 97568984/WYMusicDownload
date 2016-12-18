using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WYMusicDownload
{
    public class DownloadModel
    {
        public string MusicId { get; set; }
        public string MusicName { get; set; }
        public string  DownloadUrl { get; set; }
        public DownloadState State { get; set; }
    }
    public enum DownloadState
    {
        未下载,
        下载中,
        下载完毕,
        下载出错
    }
}
