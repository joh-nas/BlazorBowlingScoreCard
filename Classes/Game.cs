using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorBowlingScoreCard.Classes
{
    public class Game : ComponentBase
    {
        public string[] Players { get; set; }
        public string[] Calculators { get; set; }
        public string SelectedCalculator { get; set; }

        public Game()
        {
            Calculators = GetCalculators();
            SelectedCalculator = nameof(RegularScoreCalculator);
            Players = new string[0];
        }
        
        public void AddPlayer()
        {
            var playersList = Players.ToList();
            playersList.Add("Player1");
            Players = playersList.ToArray();
        }

        public static string[] GetCalculators()
        {
            var type = typeof(IScoreCalculator);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            return types.Select(x => x.Name).ToArray();
        }
    }
}
