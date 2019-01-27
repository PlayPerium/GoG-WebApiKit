/* 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GoG_WebApiKit
{
    public class ApiClient
    {
        public HttpClient GetHttpClient(string lcCode)
        {
            var cookieContainer = new CookieContainer();

            Cookie languageCookie = new Cookie("gog_lc", lcCode);

            cookieContainer.Add(new Uri("https://embed.gog.com"), languageCookie);

            var handler = new HttpClientHandler { CookieContainer = cookieContainer };

            HttpClient client = new HttpClient(handler)
            {
                DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") } }
            };

            return client;
        }
    }
}
