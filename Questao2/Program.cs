using Newtonsoft.Json;
using Questao2.ScoredGoals;
using System;
using System.Net.Http;
using System.Net;
using System.Reflection;

namespace Questao2
{
    public class Program
    {
        public static async Task Main()
        {
            var footBallClient = new FootBallClient();
            string teamName = "Paris Saint-Germain";
            int year = 2013;
            int totalGoals = await footBallClient.getTotalScoredGoalsAsync(teamName, year);

            Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

            teamName = "Chelsea";
            year = 2014;
            totalGoals = await footBallClient.getTotalScoredGoalsAsync(teamName, year);

            Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

            // Output expected:
            // Team Paris Saint - Germain scored 109 goals in 2013
            // Team Chelsea scored 92 goals in 2014
        }
    }

}