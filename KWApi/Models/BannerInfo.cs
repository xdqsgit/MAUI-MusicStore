using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWApi.Models
{

    public class BannerInfo
    {
        /// <summary>
        /// 图
        /// </summary>
        public string? NewPic { get; set; }
        /// <summary>
        /// 图文字
        /// </summary>
        public string? NewPicText { get; set; }
        public long StartTime { get; set; }
        public int Id { get; set; }
        public string? Pic { get; set; }
        public long EndTime { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Priority { get; set; }
        public string? NewPicTag { get; set; }
        /// <summary>
        /// 详情页
        /// </summary>
        public string? Url { get; set; }
        public string? PlayListId=>Url?.Split('/').Last();
    }


}
