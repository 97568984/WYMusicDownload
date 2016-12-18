using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;
using Newtonsoft.Json;
using System.Numerics;

namespace WYMusicDownload
{
    public partial class Form1 : Form
    {
        const string USER_AGENT = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
        string csrf = "";
        bool wbLoading = true;
        bool isDownloading = false;
        List<DownloadModel> downloadList = new List<DownloadModel>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                txtCookie.Text = txtCookie.Text.Trim();
                Regex reg = new Regex(@"__csrf=(?<csrf>\w*);");
                var csrfReg = reg.Match(txtCookie.Text);
                if (csrfReg.Success)
                {
                    csrf = csrfReg.Groups["csrf"].Value;
                    if (!string.IsNullOrWhiteSpace(csrf))
                    {
                        WebClient wc = new WebClient();
                        wc.Encoding = Encoding.UTF8;
                        wc.Headers["User-Agent"] = USER_AGENT;
                        wc.Headers["Cookie"] = txtCookie.Text;
                        wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                        string data = "{\"id\":\"123591024\",\"offset\":\"0\",\"total\":\"true\",\"limit\":\"1000\",\"n\":\"1000\",\"csrf_token\":\"" + csrf + "\"}";
                        var dataTemp = Encrypted(data);
                        data = string.Format("params={0}&encSecKey={1}", Uri.EscapeDataString(dataTemp.Item1), dataTemp.Item2);
                        var res = wc.UploadString("http://music.163.com/weapi/v3/playlist/detail?csrf_token=" + csrf, "POST", data);
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            MusicModel music = JsonConvert.DeserializeObject<MusicModel>(res);
                            if (music != null && music.code == 200)
                            {
                                txtCookie.Enabled = false;
                                btnLogin.Enabled = false;
                                btnLogin.Text = "登录成功";

                                txtMusicId.Enabled = true;
                                btnDownload.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show("登录失败");
                            }
                        }
                        else
                        {
                            MessageBox.Show("登录失败");
                        }
                    }
                    else
                    {
                        MessageBox.Show("登录失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败");
            }
        }
        private Tuple<string, string> Encrypted(string str)
        {
            string encText = "";
            string encSecKey = "";
            var key = GetAESKey(16);
            encText = AESEncrypt(str, "0CoJUm6Qyw8W8jud");
            encText = AESEncrypt(encText, key);
            encSecKey = RSAEncrypt(key);
            return Tuple.Create<string, string>(encText, encSecKey);
        }
        private string GetAESKey(int a)
        {
            Random ran = new Random();
            string b = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", c = "";
            for (var i = 0; a > i; i++)
            {
                var temp = ran.NextDouble() * b.Length;
                temp = Math.Floor(temp);
                c += b[(int)temp];
            }
            return c;
        }
        /// <summary> 
        /// 字符串转16进制字节数组 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        private static string AESEncrypt(string plainText, string key, string iv = "0102030405060708")
        {
            var keybytes = Encoding.UTF8.GetBytes(key);   //自行設定
            var ivbytes = Encoding.UTF8.GetBytes(iv);         //自行設定
            byte[] encrypted;
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = keybytes;
                rijAlg.IV = ivbytes;
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        public string RSAEncrypt(string sSource)
        {
            string res = "";
            byte[] n = new byte[] { 0x00, 0xe0, 0xb5, 0x09, 0xf6, 0x25, 0x9d, 0xf8, 0x64, 0x2d, 0xbc, 0x35, 0x66, 0x29, 0x01, 0x47, 0x7d, 0xf2, 0x26, 0x77, 0xec, 0x15, 0x2b, 0x5f, 0xf6, 0x8a, 0xce, 0x61, 0x5b, 0xb7, 0xb7, 0x25, 0x15, 0x2b, 0x3a, 0xb1, 0x7a, 0x87, 0x6a, 0xea, 0x8a, 0x5a, 0xa7, 0x6d, 0x2e, 0x41, 0x76, 0x29, 0xec, 0x4e, 0xe3, 0x41, 0xf5, 0x61, 0x35, 0xfc, 0xcf, 0x69, 0x52, 0x80, 0x10, 0x4e, 0x03, 0x12, 0xec, 0xbd, 0xa9, 0x25, 0x57, 0xc9, 0x38, 0x70, 0x11, 0x4a, 0xf6, 0xc9, 0xd0, 0x5c, 0x4f, 0x7f, 0x0c, 0x36, 0x85, 0xb7, 0xa4, 0x6b, 0xee, 0x25, 0x59, 0x32, 0x57, 0x5c, 0xce, 0x10, 0xb4, 0x24, 0xd8, 0x13, 0xcf, 0xe4, 0x87, 0x5d, 0x3e, 0x82, 0x04, 0x7b, 0x97, 0xdd, 0xef, 0x52, 0x74, 0x1d, 0x54, 0x6b, 0x8e, 0x28, 0x9d, 0xc6, 0x93, 0x5b, 0x3e, 0xce, 0x04, 0x62, 0xdb, 0x0a, 0x22, 0xb8, 0xe7 };
            byte[] et = new byte[] { 0x01, 0x00, 0x01 };
            var modulus = new BigInteger(n.Reverse().ToArray());
            var exponent = new BigInteger(et.Reverse().ToArray());
            var data = new BigInteger(Encoding.UTF8.GetBytes(sSource).ToArray());
            var result = BigInteger.ModPow(data, exponent, modulus);
            res = BitConverter.ToString(result.ToByteArray().Reverse().ToArray()).Replace("-", "");
            return res.ToLower();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wbLoading = false;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    string musicIdStr = txtMusicId.Text.Trim();
                    var idList = musicIdStr.Split(',');
                    foreach (var item in idList)
                    {
                        string musicId = item;
                        if (downloadList.Any(d => d.MusicId == musicId))
                        {
                            continue;
                        }
                        WebClient wc = new WebClient();
                        wc.Encoding = Encoding.UTF8;
                        wc.Headers["User-Agent"] = USER_AGENT;
                        wc.Headers["Cookie"] = txtCookie.Text;
                        var res = wc.DownloadString("http://music.163.com/song?id=" + musicId);
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            if (res.Contains("你要查找的网页找不到"))
                            {
                                MessageBox.Show("歌曲ID不存在");
                            }
                            else
                            {
                                Regex reg = new Regex("<em class=\"f-ff2\">(?<name>.*)</em>");
                                Match match = reg.Match(res);
                                if (match.Success)
                                {
                                    var musicName = match.Groups["name"].Value.Trim();
                                    if (string.IsNullOrWhiteSpace(musicName))
                                    {
                                        MessageBox.Show("歌曲名获取失败，添加失败");
                                    }
                                    else
                                    {
                                        var downloadModel = new DownloadModel()
                                        {
                                            DownloadUrl = GetDownloadUrl(musicId),
                                            MusicId = musicId,
                                            MusicName = musicName,
                                            State = DownloadState.未下载
                                        };
                                        if (string.IsNullOrWhiteSpace(downloadModel.DownloadUrl))
                                        {
                                            int index = 0;
                                            while (index <= 5)
                                            {
                                                downloadModel.DownloadUrl = GetDownloadUrl(musicId);
                                                if (string.IsNullOrWhiteSpace(downloadModel.DownloadUrl))
                                                {
                                                    System.Threading.Thread.Sleep(2000);
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                                index++;
                                            }
                                        }
                                        downloadList.Add(downloadModel);
                                        ListViewItem lvItem = new ListViewItem();
                                        lvItem.Text = downloadModel.MusicId;
                                        lvItem.SubItems.Add(downloadModel.MusicName);
                                        lvItem.SubItems.Add(downloadModel.State.ToString());
                                        listView1.Items.Add(lvItem);
                                        if (!isDownloading)
                                        {
                                            System.Threading.Tasks.Task.Factory.StartNew(DownloadWork);
                                        }
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("添加失败");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("添加失败");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("添加失败");
                }
                finally
                {
                    btnDownload.Enabled = true;
                    txtMusicId.Text = string.Empty;
                }
            });
        }
        private string GetDownloadUrl(string musicId)
        {
            string url = "";
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.Headers["User-Agent"] = USER_AGENT;
                wc.Headers["Cookie"] = txtCookie.Text;
                wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                string data = string.Format("{{\"ids\":\"[{0}]\",\"br\":128000,\"csrf_token\":\"{1}\"}}", musicId, csrf);
                var dataTemp = Encrypted(data);
                data = string.Format("params={0}&encSecKey={1}", Uri.EscapeDataString(dataTemp.Item1), dataTemp.Item2);
                var res = wc.UploadString("http://music.163.com/weapi/song/enhance/player/url?csrf_token=" + csrf, "POST", data);
                DownloadInfoModel info = JsonConvert.DeserializeObject<DownloadInfoModel>(res);
                url = info.data[0].url;
            }
            catch (Exception)
            {
                url = "";
            }
            return url;
        }
        private void DownloadWork()
        {
            DownloadModel model = null;
            int index = 0;

            while (true)
            {
                try
                {
                    model = downloadList.FirstOrDefault(d => d.State == DownloadState.未下载);
                    if (model == null)
                    {
                        isDownloading = false;
                        break;
                    }
                    else
                    {
                        isDownloading = true;
                        string downloadFolder = Path.Combine(Application.StartupPath, "Music");
                        if (!Directory.Exists(downloadFolder))
                            Directory.CreateDirectory(downloadFolder);
                        string path = Path.Combine(downloadFolder, model.MusicName + Path.GetExtension(model.DownloadUrl));
                        WebClient wc = new WebClient();
                        wc.Encoding = Encoding.UTF8;
                        wc.Headers["User-Agent"] = USER_AGENT;
                        wc.Headers["Cookie"] = txtCookie.Text;
                        index = downloadList.IndexOf(model);
                        model.State = DownloadState.下载中;
                        RefreshItem(index);
                        wc.DownloadFile(model.DownloadUrl, path);
                        model.State = DownloadState.下载完毕;
                        System.Threading.Thread.Sleep(500);
                        DeleteingId3Tag(path);
                        RefreshItem(index);
                    }
                }
                catch
                {
                    model.State = DownloadState.下载出错;
                    RefreshItem(index);
                }
            }

        }
        private void RefreshItem(int index)
        {
            ListViewItem item = listView1.Items[index];
            if (item != null)
            {
                item.Text = downloadList[index].MusicId;
                item.SubItems[1].Text = downloadList[index].MusicName;
                item.SubItems[2].Text = downloadList[index].State.ToString();
            }
        }
        private void DeleteingId3Tag(string path)
        {
            var mp3 = File.ReadAllBytes(path);
            int skip = 0;
            if (Encoding.ASCII.GetString(mp3, 0, 3) == "ID3")
                skip = 7 + BitConverter.ToInt32(mp3.Skip(6).Take(4).Reverse().ToArray(), 0);

            int take = mp3.Length - skip;
            if (Encoding.ASCII.GetString(mp3, mp3.Length - 128, 3) == "TAG")
                take -= 128;

            File.WriteAllBytes(path, mp3.Skip(skip).Take(take).ToArray());
        }
    }
}
