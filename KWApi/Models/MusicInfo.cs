using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWApi.Models
{
 
    public class MusicInfo
    {
        public string? Musicrid { get; set; }
        public string? Barrage { get; set; }
        public string? Ad_type { get; set; }
        public string? Artist { get; set; }
        public string? Pic { get; set; }
        public string? UpPcStr { get; set; }
        public int Isstar { get; set; }
        public int Rid { get; set; }
        public int Duration { get; set; }
        public string? Score100 { get; set; }
        public string? Ad_subtype { get; set; }
        public string? Content_type { get; set; }
        public int MvPlayCnt { get; set; }
        public int Track { get; set; }
        public string? ReleaseDate { get; set; }
        public string? Album { get; set; }
        public string? Pay { get; set; }
        public bool HasLossless { get; set; }
        public int Hasmv { get; set; }
        public int Albumid { get; set; }
        public int Artistid { get; set; }
        public int Originalsongtype { get; set; }
        public string? Albumpic { get; set; }
        public string? SongTimeMinutes { get; set; }
        public bool IsListenFee { get; set; }
        public string? MvUpPcStr { get; set; }
        public string? Pic120 { get; set; }
        public string? Albuminfo { get; set; }
        public string? Name { get; set; }
        public string? Tme_musician_adtype { get; set; }
        public int Online { get; set; }
        public Payinfo? PayInfo { get; set; }
    }


    public class Payinfo
    {
        public string? Overseas_ndown { get; set; }
        public string? Play { get; set; }
        public string? Nplay { get; set; }
        public string? Overseas_nplay { get; set; }
        public string? Local_encrypt { get; set; }        
        public string? Down { get; set; }
        public string? Ndown { get; set; }
        public string? Download { get; set; }
        public int Limitfree { get; set; }
        public int Refrain_start { get; set; }
        public int CannotDownload { get; set; }
        public int Refrain_end { get; set; }
        public int CannotOnlinePlay { get; set; }
        public int Paytype { get; set; }
        public Feetype? FeeType { get; set; }
        public Paytagindex? Paytagindex { get; set; }
    }

    public class Feetype
    {
        public string Song { get; set; } = "";
        public string Vip { get; set; } = "";
    }

}
