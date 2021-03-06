﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WYMusicDownload
{
    public class Datum
    {
        public int id { get; set; }
        public string url { get; set; }
        public int br { get; set; }
        public int size { get; set; }
        public string md5 { get; set; }
        public int code { get; set; }
        public int expi { get; set; }
        public string type { get; set; }
        public double gain { get; set; }
        public int fee { get; set; }
        public object uf { get; set; }
        public int payed { get; set; }
        public int flag { get; set; }
        public bool canExtend { get; set; }
    }

    public class DownloadInfoModel
    {
        public List<Datum> data { get; set; }
        public int code { get; set; }
    }
}
