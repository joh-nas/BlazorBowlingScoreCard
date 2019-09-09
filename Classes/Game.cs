using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorBowlingScoreCard.Classes
{
    public class Game : ComponentBase
    {
        public string[] Players { get; set; } = new string[0];

        public void AddPlayer()
        {
            var playersList = Players.ToList();
            playersList.Add("Player1");
            Players = playersList.ToArray();
        }
    }
}
