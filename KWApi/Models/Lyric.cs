using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWApi.Models
{
    public class KWLrclist
    {
        public List<Lyric>? Lrclist { get; set; }
    }
    public class Lyric
    {
        public string LineLyric { get; set; } = "";
        public double Time { get; set; }
    }

}
