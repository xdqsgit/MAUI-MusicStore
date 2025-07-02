using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWApi.Models
{
    using System;
    using System.Collections.Generic;

    public class MusicList
    {
        public string? Musicrid { get; set; }
        public string? Barrage { get; set; }
        public string? Ad_type { get; set; }
        public string? Artist { get; set; }
        public Mvpayinfo? Mvpayinfo { get; set; }
        public string? Pic { get; set; }
        public int Isstar { get; set; }
        public int Rid { get; set; }
        public int Duration { get; set; }
        public string? Score100 { get; set; }
        public string? Ad_subtype { get; set; }
        public string? Content_type { get; set; }
        public int Track { get; set; }
        public int Hasmv { get; set; }
        public string? ReleaseDate { get; set; }
        public string? Album { get; set; }
        public int Albumid { get; set; }
        public string? Pay { get; set; }
        public int Artistid { get; set; }
        public string? Albumpic { get; set; }
        public int Originalsongtype { get; set; }
        public bool IsListenFee { get; set; }
        public string? Pic120 { get; set; }
        public string? Name { get; set; }
        public int Online { get; set; }
        public PayInfo? PayInfo { get; set; }
        public string? Tme_musician_adtype { get; set; }
    }

    public class Mvpayinfo
    {
        public int Play { get; set; }
        public int Vid { get; set; }
        public int Down { get; set; }
    }

    public class PayInfo
    {
        public string? Play { get; set; }
        public string? Nplay { get; set; }
        public string? Overseas_nplay { get; set; }
        public string? Local_encrypt { get; set; }
        public int Limitfree { get; set; }
        public int Refrain_start { get; set; }
        public FeeType? FeeType { get; set; }
        public string? Down { get; set; }
        public string? Ndown { get; set; }
        public string? Download { get; set; }
        public int CannotDownload { get; set; }
        public string? Overseas_ndown { get; set; }
        public string? Listen_fragment { get; set; }
        public int Refrain_end { get; set; }
        public int CannotOnlinePlay { get; set; }
        public int Paytype { get; set; }
        public Paytagindex? Paytagindex { get; set; }
    }

    public class FeeType
    {
        public string? Song { get; set; }
        public string? Vip { get; set; }
    }

    public class Paytagindex
    {
        public int S { get; set; }
        public int F { get; set; }
        public int ZP { get; set; }
        public int H { get; set; }
        public int ZPGA201 { get; set; }
        public int ZPLY { get; set; }
        public int HR { get; set; }
        public int L { get; set; }
        public int ZPGA501 { get; set; }
        public int DB { get; set; }
        public int AR501 { get; set; }
    }
    /// <summary>
    /// 排行榜
    /// </summary>
    public class KWBangList
    {
        public string? Leader { get; set; }
        public string? Num { get; set; }
        public string? Name { get; set; }
        public string? Pic { get; set; }
        public string? Id { get; set; }
        public string? Pub { get; set; }
        public List<MusicList>? MusicList { get; set; }
    }

}
