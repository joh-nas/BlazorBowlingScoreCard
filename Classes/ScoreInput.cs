using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorBowlingScoreCard.Classes
{
    public class ScoreInput : ComponentBase
    {
        private int _xPos;

        [Parameter]
        public EventCallback<int> ScoreClicked { get; set; }

        [Parameter]
        public int[] PossibleInputs { get; set; }

        [Parameter]
        public bool IsStrikePossible { get; set; }

        [Parameter]
        public bool IsSparePossible { get; set; }

        [Parameter]
        public int SpareShotCount { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public int XPos { get => _xPos; set => _xPos = value - 200; }

        public bool Disabled(int buttonNumber)
        {
            return !PossibleInputs.Any(x => x == buttonNumber);
        }

        public void SetScore(int score)
        {
            ScoreClicked.InvokeAsync(score);
        }
    }
}
