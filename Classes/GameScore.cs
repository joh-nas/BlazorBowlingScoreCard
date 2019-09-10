using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorBowlingScoreCard.Classes
{
    public class GameScore : ComponentBase
    {
        [Parameter]
        public string PlayerName { get; set; }
        public int CurrentFrame { get; set; }
        public int CurrentShot { get; set; }
        public int XPos { get => _xPos; 
            set 
            {
                _xPos = value;
                SetFocusInternal(CurrentFrame, CurrentShot);
            }
        }
        public bool ShowKeyboard { get; set; } = false;
        public int[] PossibleInputs { get; set; } = new int[0];
        public bool IsSparePossible { get; private set; }
        public bool IsStrikePossible { get; private set; }
        public int SpareShotCount { get; private set; }
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public Frames Frames = new Frames();
        private int _xPos;

        public GameScore()
        {
            CurrentFrame = 0;
            CurrentShot = 1;
        }

        public void AddScore(int score)
        {
            ShowKeyboard = false;
            if (score == -1) return;

            Frames.AddScore(CurrentFrame, CurrentShot, score);
            SetNextShot();
            Frames.CalculateScore();
            StateHasChanged();
        }

        private void SetNextShot()
        {
            var nextShot = Frames[CurrentFrame].GetNextShot(CurrentFrame, CurrentShot);
            CurrentFrame = nextShot.NextFrame;
            CurrentShot = nextShot.NextShot;
            var frameId = FrameId(CurrentFrame, CurrentShot);
            SetElementXPos(frameId); // Async call, the rest is done in XPos Property
        }

        public async void SetElementXPos(string frameId)
        {
            XPos = (int)await JsRuntime.InvokeAsync<double>("JsInteropFunctions.GetXPos", new[] { frameId });
        }

        public void SetFocus(MouseEventArgs args, int frameNumber, int shotNumber)
        {

            CurrentFrame = frameNumber;
            CurrentShot = shotNumber;
            XPos = (int)args.ClientX;
        }

        public void SetFocusInternal(int frameNumber, int shotNumber)
        {
            PossibleInputs = GetPossibleInputs();
            IsSparePossible = Frames.IsSparePossible(frameNumber, shotNumber);
            IsStrikePossible = Frames.IsStrikePossible(frameNumber, shotNumber);
            SpareShotCount = Frames.SpareShotCount(frameNumber, shotNumber);
            ShowKeyboard = true;
            StateHasChanged();
        }

        public int[] GetPossibleInputs()
        {
            return Frames.GetPossibleInputs(CurrentFrame, CurrentShot);
        }

        public string FrameId(int frameNumber, int shotNumber)
        {
            return $"{frameNumber}-{shotNumber}";
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
            var curFrame = Frames[frameNumber];
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
