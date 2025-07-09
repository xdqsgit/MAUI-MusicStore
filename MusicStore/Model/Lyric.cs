using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Model
{
    public class Lyric
    {
        public int Order { get; set; }
        public double Time { get; set; }
        public string Text { get; set; }="";
    }
}
