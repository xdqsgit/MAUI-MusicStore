using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWApi.Models
{
    internal class KWResponseBase<T> where T : class
    {
        public string? ReqId { get; set; }
        public int Code{ get; set; }
        public string? Msg{ get; set; }
        public T? Data { get; set; }
        public bool? Success { get; set; }
    }
}
