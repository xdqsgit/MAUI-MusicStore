using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWApi.Models
{
    internal class KWCookieDto
    {
        public int Code { get; set; }
        public string? Cookie { get; set; }
        public string? Secret { get; set; }
        public string? Msg { get; set; }
    }
}
