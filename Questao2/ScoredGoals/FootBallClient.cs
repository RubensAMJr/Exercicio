using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2.ScoredGoals
{
    public class FootBallClient
    {
        private const string _urlFootBallMatches = "https://jsonmock.hackerrank.com/api/football_matches";

        public async Task<int> getTotalScoredGoalsAsync(string teamName, int year)
        {
            return await getTotal("team1", teamName, year) + await getTotal("team2", teamName, year);
        }

        private static async Task<int> getTotal(string teamType,string teamName, int year)
        {
            int totalGoals = 0;
            int page = 1;
            var response = new FootBallClientResponse();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    do
                    {
                        var urlBuilder = new StringBuilder(_urlFootBallMatches);
                        urlBuilder.Append($"?year={year}&{teamType}={Uri.EscapeDataString(teamName)}&page={page}");
                        HttpResponseMessage httpResponse = await client.GetAsync(urlBuilder.ToString());

                        if (httpResponse.Content != null)
                        {
                            response = JsonConvert.DeserializeObject<FootBallClientResponse>(await httpResponse.Content.ReadAsStringAsync());
                            totalGoals += response.Data.Sum(d => teamType == "team1" ? d.Team1goals : d.Team2goals);
                            page++;
                        }
                        else
                            break;
                    }
                    while (page <= response.Total_Pages);
                }
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }

            return totalGoals;
        }
    }
}
