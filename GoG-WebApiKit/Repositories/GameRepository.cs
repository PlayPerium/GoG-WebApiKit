/* 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GoG_WebApiKit.Repositories
{
    public class GameRepository
    {
        public async Task<List<JToken>> GetAllGames(string lcCode)
        {
            HttpClient client = new ApiClient().GetHttpClient(lcCode);

            int totalPages = (int)JObject.Parse(await client.GetStringAsync("https://embed.gog.com/games/ajax/filtered?mediaType=game&limit=48&page=1"))["totalPages"];

            List<JToken> games = new List<JToken>();

            for (int i = 1; i < totalPages; i++)
            {
                JObject data = JObject.Parse(await client.GetStringAsync($"https://embed.gog.com/games/ajax/filtered?mediaType=game&limit=48&page={i}"));
                games.AddRange(data["products"].ToList());

                Console.WriteLine($"Processed: {i} from {totalPages}...");

                await Task.Delay(1000);
            }

            return games;
        }
    }
}
