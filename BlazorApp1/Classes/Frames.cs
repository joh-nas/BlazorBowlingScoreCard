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
            var tmpFrames = new List<Frame>();

            for (int i = 0; i < 10; i++)
            {
                var newFrame = new Frame(i + 1);
                tmpFrames.Add(newFrame);
            }

            AllFrames = tmpFrames.ToArray();
            SetTestData();
            CalculateScore();
        }

        public void VerifyFrameScore()
        {
            foreach (var frame in AllFrames)
            {
                if (frame.FrameNumber == 10)
                {
                    if (frame.One + frame.Two + frame.Extra > 30) throw new ApplicationException("There are only 30 pins to knock down in the last frame.");
                }
                else if (frame.One + frame.Two > 10) throw new ApplicationException("There are only 10 pins to knock down in each frame.");
            }
        }

        public void CalculateScore()
        {
            VerifyFrameScore();

            var score = 0;
            for (int i = 0; i < 10; i++)
            {
                score += AllFrames[i].One;
                score += AllFrames[i].Two;
                score += AllFrames[i].Extra;

                if (i < 9)
                {
                    if (AllFrames[i].One == 10)
                    {
                        score += AllFrames[i + 1].One;
                        if (AllFrames[i + 1].One == 10)
                        {
                            if (i + 1 == 9)
                            {
                                score += AllFrames[i + 1].Two;
                            }
                            else
                            {
                                score += AllFrames[i + 2].One;
                            }
                        }
                        else
                        {
                            score += AllFrames[i + 1].Two;
                        }
                    }
                    else
                    {
                        if (AllFrames[i].One + AllFrames[i].Two == 10)
                        {
                            score += AllFrames[i + 1].One;
                        }
                    }
                }

                AllFrames[i].Score = score;
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
