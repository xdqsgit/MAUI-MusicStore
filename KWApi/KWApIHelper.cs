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
    }

}
