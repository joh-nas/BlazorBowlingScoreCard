using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp1.Classes
{
    public class GameScore : ComponentBase
    {
        public Frames frames = new Frames();
        public int CurrentFrame { get; set; } = 3;
        public int CurrentShot { get; set; } = 1;

        public bool ShowKeyboard { get; set; } = false;
        public List<int> PossibleInputs { get; set; } = new List<int>();

        public void AddScore(int score)
        {
            ShowKeyboard = false;
            this.StateHasChanged();

            if (score >= 0 && score <= 10)
                frames.AllFrames[CurrentFrame].SetShotScore(CurrentShot, score);

            if (CurrentFrame == 9)
            {
                var curFrame = frames.AllFrames[CurrentFrame];
                if (curFrame.One != 10 && curFrame.One + curFrame.Two != 10)
                {
                    curFrame.SetShotScore(3, 0);
                }
            }

            frames.CalculateScore();
        }

        public void SetFocus(int frameNumber, int shotNumber)
        {
            CurrentFrame = frameNumber;
            CurrentShot = shotNumber;
            ShowKeyboard = true;
            PossibleInputs = GetPossibleInputs();
            this.StateHasChanged();
        }

        public List<int> GetPossibleInputs()
        {
            var curFrame = frames.AllFrames[CurrentFrame];
            if (CurrentShot == 1) return Enumerable.Range(1, 10).ToList();
            if (CurrentShot == 2 && CurrentFrame != 9)
            {
                if (curFrame.One != 10) return Enumerable.Range(1, 10 - curFrame.One).ToList();
                if (curFrame.One == 10) return new List<int>();
            }
            if (CurrentShot == 2 && CurrentFrame == 9)
            {
                if (curFrame.One != 10) return Enumerable.Range(1, 10 - curFrame.One).ToList();
                if (curFrame.One == 10) return Enumerable.Range(1, 10).ToList();
            }
            if (CurrentShot == 3 && CurrentFrame == 9)
            {
                if (curFrame.One + curFrame.Two == 10 || curFrame.One + curFrame.Two == 20) return Enumerable.Range(1, 10).ToList();
                if (curFrame.One == 10) return Enumerable.Range(1, 10 - curFrame.Two).ToList();
            }

            return new List<int>();
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
            if (frameNumber != 9)
            {
                if (frames.AllFrames[frameNumber].One + frames.AllFrames[frameNumber].Two > 10)
                {
                    return "errorCell";
                }
            }
            else
            {
                if (frames.AllFrames[frameNumber].One != 10 && frames.AllFrames[frameNumber].One + frames.AllFrames[frameNumber].Two > 10)
                {
                    return "errorCell";
                }

                if (frames.AllFrames[frameNumber].One == 10 && frames.AllFrames[frameNumber].Two != 10 && frames.AllFrames[frameNumber].Two + frames.AllFrames[frameNumber].Extra > 10)
                {
                    return "errorCell";
                }
            }

            return string.Empty;
        }

        public int TotalScoreColSpan(int frameNumber)
        {
            return frameNumber == 9 ? 3 : 2;
        }
    }
}
