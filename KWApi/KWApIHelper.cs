using System.Net.Http.Json;
using System.Text.Json;
using KWApi.Models;

namespace KWApi
{
    public class KWApIHelper
    {
        readonly string CookieUrl = "https://api.ilingku.com/int/v1/kuwo_secret_token?act=pc";
        readonly string ApiUrl = "https://www.kuwo.cn/api/www";
        HttpClient httpClient;
        public KWApIHelper()
        {
            httpClient = new HttpClient();
            _=SetHttpClientCookieAsync(httpClient);
        }
        public async Task SetHttpClientCookieAsync(HttpClient httpClient)
        {
            var cookie = await httpClient.GetFromJsonAsync<KWCookieDto>(CookieUrl);
            if (cookie is null || cookie.Code != 200)
            {
                return;
            }
            httpClient.DefaultRequestHeaders.Remove("Cookie");
            httpClient.DefaultRequestHeaders.Remove("Secret");
            httpClient.DefaultRequestHeaders.Add("Cookie", cookie.Cookie);
            httpClient.DefaultRequestHeaders.Add("Secret", cookie.Secret);
        }

        /// <summary>
        /// 以get方式请求接口
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url">附加好查询参数的url</param>
        /// <returns></returns>
        async Task<TResult?> GetAsync<TResult>(string url) where TResult : class
        {
            var runCount = 0;
            while (runCount < 2)
            {
                runCount++;
                var result = await httpClient.GetFromJsonAsync<KWResponseBase<TResult>>(url);
                if (result is null)
                {
                    return null;
                }

                if (result.Code != 200)
                {
                    await SetHttpClientCookieAsync(httpClient);
                    continue;//可能是cookie过期，重新获取cookie
                }
                return result.Data;
            }
            return null;
        }


        public async Task GetSearchResultAsync(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 每日推荐歌单
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<KWDayRcmPlayList?> GetDayRcmPlayListAsync()
        {
            var url = $"{ApiUrl}/rcm/index/playlist?loginUid=0&httpsStatus=1&reqId=b6e77101-57ba-11f0-9df9-c1e04916a71e&plat=web_www";

            var result = await GetAsync<KWDayRcmPlayList>(url);
            return result;
        }

        /// <summary>
        /// 最新歌单
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<KWRcmPlayList?> GetRcmPlayList(int count = 10)
        {
            var url = $"{ApiUrl}/classify/playlist/getRcmPlayList?pn=1&rn={count}&order=new&httpsStatus=1";
            var result = await GetAsync<KWRcmPlayList>(url);
            return result;
        }
        /// <summary>
        /// 排行榜
        /// </summary>
        /// <returns></returns>
        public async Task<List<KWBangList>?> GetBangListAsync()
        {
            var url = $"{ApiUrl}/bang/index/bangList?httpsStatus=1&reqId=e4f22072-570d-11f0-9dbd-c3d60fd911a5&plat=web_www";
            var result = await GetAsync<List<KWBangList>>(url);
            return result;
        }

        
        /// <summary>
        /// 获取banner
        /// </summary>
        /// <returns></returns>
        public async Task<List<BannerInfo>?> GetBannerListAsync()
        {
            var url = $"{ApiUrl}/banner/index/bannerList?httpsStatus=1&reqId=e09a1b70-57d2-11f0-9e9d-45fbeda80d08&plat=web_www";
            var result = await GetAsync<List<BannerInfo>>(url);
            return result;
        }

        
        /// <summary>
        /// 获取歌曲信息
        /// </summary>
        /// <returns></returns>
        public async Task<MusicInfo?> GetMusicInfoAsync(long musicId)
        {
            var url = $"{ApiUrl}/music/musicInfo?mid={musicId}&httpsStatus=1&reqId=f3457940-57e1-11f0-881d-fd59d2bf6e0d&plat=web_www&from=";
            var result = await GetAsync<MusicInfo>(url);
            return result;
        }
        
        /// <summary>
        /// 获取歌曲播放地址
        /// </summary>
        /// <returns></returns>
        public async Task<PlayUrl?> GetPlayUrlAsync(long musicId)
        {
            var url = $"{ApiUrl}/music/playUrl?mid={musicId}&type=music&httpsStatus=1&reqId=b73681d1-5be0-11f0-b63c-e3b754600bd5&plat=web_www&from=";
            var result = await GetAsync<PlayUrl>(url);
            return result;
        }
        
        /// <summary>
        /// 获取歌词
        /// </summary>
        /// <returns></returns>
        public async Task<KWLrclist?> GetLyricAsync(long musicId)
        {
            var url = $"https://www.kuwo.cn/openapi/v1/www/lyric/getlyric?musicId={musicId}&httpsStatus=1&reqId=43a32560-5be1-11f0-8c82-a5b4634c4002&plat=web_www&from=";
            var result = await GetAsync<KWLrclist>(url);
            return result;
        }


    }

}
