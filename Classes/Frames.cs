using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBowlingScoreCard.Classes
{
    public class Frames
    {
        public Frame[] AllFrames { get; set; }
        public (int Frame, int shot) LatestPlayedFrame { get; set; } = (0, 0);

        public Frames()
        {
            AllFrames = Enumerable
                .Range(1, 10)
                .Select(x => {
                    if (x == 10) return new LastFrame();
                    return new Frame(); 
                })
                .ToArray();

            //SetTestData();
            CalculateScore();
        }

        public void AddScore(int frameNumber, int shotNumber, int score)
        {
            var curFrame = AllFrames[frameNumber];
            curFrame.AddScore(shotNumber, score);
            UpdateLatestPlayedFrame(frameNumber, shotNumber);
        }

        internal int[] GetPossibleInputs(int frameNumber, int shotNumber)
        {
            var curFrame = AllFrames[frameNumber];
            return curFrame.GetPossibleInputs(shotNumber);
        }

        internal int SpareShotCount(int frameNumber, int shotNumber)
        {
            var curFrame = AllFrames[frameNumber];
            return curFrame.SpareShotCount(shotNumber);
        }

        internal bool IsStrikePossible(int frameNumber, int shotNumber)
        {
            var curFrame = AllFrames[frameNumber];
            return curFrame.IsStrikePossible(shotNumber);
        }

        internal void CalculateScore()
        {
            new RegularScoreCalculator().CalculateScore(AllFrames);
        }

        internal int CalculateMaxScore(int frameNumber, int shotNumber)
        {
            return new RegularScoreCalculator().CalculateMaxScore(AllFrames, (frameNumber, shotNumber));
        }

        internal bool IsSparePossible(int frameNumber, int shotNumber)
        {
            var curFrame = AllFrames[frameNumber];
            return curFrame.IsSparePossible(shotNumber);
        }

        public void VerifyFrameScore()
        {
            foreach (var frame in AllFrames)
            {
                frame.VerifyFrameScore();
            }
        }

        public Frame this[int index]
        {
            get
            {
                return AllFrames[index];
            }
        }

        private void UpdateLatestPlayedFrame(int frameNumber, int shotNumber)
        {
            var latestFrame = Math.Max(LatestPlayedFrame.Frame, frameNumber);
            if (latestFrame == LatestPlayedFrame.Frame)
            {
                var latestShot = Math.Max(shotNumber, LatestPlayedFrame.shot);
                LatestPlayedFrame = (latestFrame, latestShot);
            }
            else if (latestFrame > LatestPlayedFrame.Frame)
            {
                var latestShot = shotNumber;
                LatestPlayedFrame = (latestFrame, latestShot);
            }
        }

        public string GetShotScoreString(int frameNumber, int shotNumber)
        {
            if (frameNumber > LatestPlayedFrame.Frame) {
                return string.Empty; 
            }
            if (frameNumber == LatestPlayedFrame.Frame && shotNumber > LatestPlayedFrame.shot)
            {
                return string.Empty;
            }

            return AllFrames[frameNumber].ShowShotScore(shotNumber);
        }

        public void SetTestData()
        {
            AllFrames[0].One = 9;
            AllFrames[0].Two = 0;

            AllFrames[1].One = 7;
            AllFrames[1].Two = 2;

            AllFrames[2].One = 10;
            AllFrames[2].Two = 0;

            AllFrames[3].One = 10;
            AllFrames[3].Two = 0;

            AllFrames[4].One = 7;
            AllFrames[4].Two = 3;

            AllFrames[5].One = 6;
            AllFrames[5].Two = 4;

            AllFrames[6].One = 0;
            AllFrames[6].Two = 0;

            AllFrames[7].One = 3;
            AllFrames[7].Two = 7;

            AllFrames[8].One = 10;
            AllFrames[8].Two = 0;

            AllFrames[9].One = 10;
            AllFrames[9].Two = 10;
            AllFrames[9].Extra = 10;
        }
    }
}
