using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;

namespace BlazorBowlingScoreCard.Classes
{
    public class GameScore : ComponentBase
    {
        [Parameter]
        public string PlayerName { get; set; }
        public int CurrentFrame { get; set; } = 0;
        public int CurrentShot { get; set; } = 1;
        public double XPos { get; set; }
        public bool ShowKeyboard { get; set; } = false;
        public int[] PossibleInputs { get; set; } = new int[0];
        public bool IsSparePossible { get; private set; }
        public bool IsStrikePossible { get; private set; }
        public int SpareShotCount { get; private set; }

        public Frames frames = new Frames();

        public void AddScore(int score)
        {
            ShowKeyboard = false;

            frames.AddScore(CurrentFrame, CurrentShot, score);
            frames.CalculateScore();
            StateHasChanged();
        }

        public void SetFocus(MouseEventArgs args, int frameNumber, int shotNumber)
        {
            CurrentFrame = frameNumber;
            CurrentShot = shotNumber;
            PossibleInputs = GetPossibleInputs();
            IsSparePossible = frames.IsSparePossible(frameNumber, shotNumber);
            IsStrikePossible = frames.IsStrikePossible(frameNumber, shotNumber);
            SpareShotCount = frames.SpareShotCount(frameNumber, shotNumber);
            ShowKeyboard = true;
            XPos = args.ClientX;
            StateHasChanged();
        }

        public int[] GetPossibleInputs()
        {
            return frames.GetPossibleInputs(CurrentFrame, CurrentShot);
        }

        public string SelectedCellClass(int frameNumber, int shotNumber)
        {
            if (CurrentFrame == frameNumber && CurrentShot == shotNumber)
            {
                return "selectedCell";
            }

            return "";
        }

        public string ErrorCellClass(int frameNumber)
        {
            var curFrame = frames[frameNumber];
            if (frameNumber != 9)
            {
                if (curFrame.One + curFrame.Two > 10)
                {
                    return "errorCell";
                }
            }
            else
            {
                if (curFrame.One != 10 && curFrame.One + curFrame.Two > 10)
                {
                    return "errorCell";
                }

                if (curFrame.One == 10 && curFrame.Two != 10 && curFrame.Two + curFrame.Extra > 10)
                {
                    return "errorCell";
                }
            }

            return string.Empty;
        }
    }
}
