using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WYMusicDownload
{
    public class Creator
    {
        public string signature { get; set; }
        public int authority { get; set; }
        public bool defaultAvatar { get; set; }
        public long avatarImgId { get; set; }
        public int province { get; set; }
        public int authStatus { get; set; }
        public bool followed { get; set; }
        public string avatarUrl { get; set; }
        public int accountStatus { get; set; }
        public int gender { get; set; }
        public int city { get; set; }
        public long birthday { get; set; }
        public int userId { get; set; }
        public int userType { get; set; }
        public string nickname { get; set; }
        public string description { get; set; }
        public string detailDescription { get; set; }
        public long backgroundImgId { get; set; }
        public string backgroundUrl { get; set; }
        public bool mutual { get; set; }
        public object expertTags { get; set; }
        public int djStatus { get; set; }
        public int vipType { get; set; }
        public object remarkName { get; set; }
    }

    public class Ar
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Al
    {
        public int id { get; set; }
        public string name { get; set; }
        public string picUrl { get; set; }
        public string pic_str { get; set; }
        public object pic { get; set; }
        public List<string> alia { get; set; }
        public List<string> tns { get; set; }
    }

    public class H
    {
        public int br { get; set; }
        public object fid { get; set; }
        public int size { get; set; }
        public decimal vd { get; set; }
    }

    public class M
    {
        public int br { get; set; }
        public object fid { get; set; }
        public int size { get; set; }
        public decimal vd { get; set; }
    }

    public class L
    {
        public int br { get; set; }
        public object fid { get; set; }
        public int size { get; set; }
        public decimal vd { get; set; }
    }

    public class Privilege
    {
        public int id { get; set; }
        public int fee { get; set; }
        public int payed { get; set; }
        public int st { get; set; }
        public int pl { get; set; }
        public int dl { get; set; }
        public int sp { get; set; }
        public int cp { get; set; }
        public int subp { get; set; }
        public bool cs { get; set; }
        public int maxbr { get; set; }
        public int fl { get; set; }
        public object pc { get; set; }
        public bool toast { get; set; }
        public int flag { get; set; }
    }

    public class Track
    {
        public string name { get; set; }
        public int id { get; set; }
        public int pst { get; set; }
        public int t { get; set; }
        public List<Ar> ar { get; set; }
        public List<string> alia { get; set; }
        public decimal pop { get; set; }
        public int st { get; set; }
        public string rt { get; set; }
        public int fee { get; set; }
        public int v { get; set; }
        public string crbt { get; set; }
        public string cf { get; set; }
        public Al al { get; set; }
        public int dt { get; set; }
        public H h { get; set; }
        public M m { get; set; }
        public L l { get; set; }
        public object a { get; set; }
        public string cd { get; set; }
        public int no { get; set; }
        public object rtUrl { get; set; }
        public int ftype { get; set; }
        public List<object> rtUrls { get; set; }
        public object rurl { get; set; }
        public int mst { get; set; }
        public int rtype { get; set; }
        public int cp { get; set; }
        public int mv { get; set; }
        public List<string> tns { get; set; }
        public Privilege privilege { get; set; }
        public string eq { get; set; }
    }

    public class TrackId
    {
        public int id { get; set; }
        public int v { get; set; }
    }

    public class Playlist
    {
        public List<object> subscribers { get; set; }
        public bool subscribed { get; set; }
        public Creator creator { get; set; }
        public List<Track> tracks { get; set; }
        public List<TrackId> trackIds { get; set; }
        public int status { get; set; }
        public int playCount { get; set; }
        public int trackCount { get; set; }
        public int specialType { get; set; }
        public long trackUpdateTime { get; set; }
        public int privacy { get; set; }
        public int userId { get; set; }
        public bool newImported { get; set; }
        public long coverImgId { get; set; }
        public long createTime { get; set; }
        public int subscribedCount { get; set; }
        public bool highQuality { get; set; }
        public string commentThreadId { get; set; }
        public long updateTime { get; set; }
        public object description { get; set; }
        public List<object> tags { get; set; }
        public long trackNumberUpdateTime { get; set; }
        public int adType { get; set; }
        public int cloudTrackCount { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public int shareCount { get; set; }
        public int commentCount { get; set; }
    }

    public class Privilege2
    {
        public int id { get; set; }
        public int fee { get; set; }
        public int payed { get; set; }
        public int st { get; set; }
        public int pl { get; set; }
        public int dl { get; set; }
        public int sp { get; set; }
        public int cp { get; set; }
        public int subp { get; set; }
        public bool cs { get; set; }
        public int maxbr { get; set; }
        public int fl { get; set; }
        public bool toast { get; set; }
        public int flag { get; set; }
    }

    public class MusicModel
    {
        public Playlist playlist { get; set; }
        public int code { get; set; }
        public List<Privilege2> privileges { get; set; }
    }
}
