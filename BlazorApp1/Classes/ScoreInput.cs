﻿using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp1.Classes
{
    public class ScoreInput : ComponentBase
    {
        [Parameter]
        public EventCallback<int> ScoreClicked { get; set; }

        [Parameter]
        public List<int> PossibleInputs { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

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