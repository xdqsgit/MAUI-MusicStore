using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWApi.Models
{

    public class RcmPlayListItem
    {
        public string Img { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>

        public string Uname { get; set; } 
        /// <summary>
        /// 收藏
        /// </summary>

        public string Favorcnt { get; set; } 

        public string Uid { get; set; }
        /// <summary>
        /// 包含歌曲数量
        /// </summary>

        public string Total { get; set; } 

        public string Digest { get; set; }
        /// <summary>
        /// 歌单名
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 听过
        /// </summary>

        public string Listencnt { get; set; }

        public string Id { get; set; } 
    }

    public class KWRcmPlayList
    {
        public int Total { get; set; }

        public List<RcmPlayListItem>? Data { get; set; }

        public int Rn { get; set; }

        public int Pn { get; set; }
    }

}
