using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Model
{
    public class PlayListItem
    {
        public string? Name { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? Pic { get; set; }
        public double Duration { get; set; }
        public int Id { get; set; }
    }
}
