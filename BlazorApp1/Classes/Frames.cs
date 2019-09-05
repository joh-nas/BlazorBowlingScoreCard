using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Classes
{
    public class Frames
    {
        public Frame[] AllFrames { get; set; }

        public Frames()
        {
            AllFrames = Enumerable
                .Range(1, 10)
                .Select(x => {
                    if (x == 10) return new LastFrame();
                    return new Frame(); 
                })
                .ToArray();

            SetTestData();
            CalculateScore();
        }

        public void AddScore(int frameNumber, int shotNumber, int score)
        {
            var curFrame = AllFrames[frameNumber];
            curFrame.AddScore(shotNumber, score);
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

        public void SetShotScore(int frameNumber, int shotNumber, int score)
        {
            AllFrames[frameNumber].SetShotScore(shotNumber, score);
        }

        public string GetShotScoreString(int frameNumber, int shotNumber)
        {
            return AllFrames[frameNumber].ShowShotScore(shotNumber);
        }

        public void CalculateScore()
        {
            VerifyFrameScore();

            var score = 0;
            for (int i = 0; i < 10; i++)
            {
                var current = AllFrames[i];
                score += current.One;
                score += current.Two;
                score += current.Extra;

                if (i < 9)
                {
                    var next = AllFrames[i + 1];
                    if (current.One == 10)
                    {
                        score += next.One;
                        if (next.One == 10)
                        {
                            if (i + 1 == 9)
                            {
                                score += next.Two;
                            }
                            else
                            {
                                var secondNext = AllFrames[i + 2];
                                score += secondNext.One;
                            }
                        }
                        else
                        {
                            score += next.Two;
                        }
                    }
                    else
                    {
                        if (current.One + current.Two == 10)
                        {
                            score += next.One;
                        }
                    }
                }

                current.Score = score;
            }
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
