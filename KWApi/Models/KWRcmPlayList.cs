using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWApi.Models
{

    public class RcmPlayListItem
    {
        public string? Img { get; set; }
        public string? Img700 { get; set; }
        public string? Img300 { get; set; }
        public string? Img500 { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>

        public string? Uname { get; set; } 
        /// <summary>
        /// 收藏
        /// </summary>

        public string? Favorcnt { get; set; } 

        public string? Uid { get; set; }
        /// <summary>
        /// 包含歌曲数量
        /// </summary>

        public int Total { get; set; } 

        public string Digest { get; set; }
        /// <summary>
        /// 歌单名
        /// </summary>

        public string? Info { get; set; } 
        public string? Name { get; set; }
        /// <summary>
        /// 听过
        /// </summary>

        public int Listencnt { get; set; }

        public long Id { get; set; } 
    }
     

    public class KWRcmPlayList
    {
        public int Total { get; set; }

        public List<RcmPlayListItem>? Data { get; set; }

        public int Rn { get; set; }

        public int Pn { get; set; }
    }

}
