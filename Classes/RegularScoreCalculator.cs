using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBowlingScoreCard.Classes
{
    public class RegularScoreCalculator : IScoreCalculator
    {
        public int CalculateMaxScore(Frame[] frames, (int CurrentFrame, int CurrentShot) currentFrame)
        {
            var newFrames = frames.Select(x => x.CopyFrame()).ToArray();
            for(int i=currentFrame.CurrentFrame; i<10; i++)
            {
                newFrames[i].SetMaxScore(i == currentFrame.CurrentFrame ? currentFrame.CurrentShot : 1);
                Console.WriteLine(newFrames[i].ToString());
            }

            return CalculateScore(newFrames);
        }

        public int CalculateScore(Frame[] frames)
        {
            var score = 0;
            for (int i = 0; i < 10; i++)
            {
                var current = frames[i];
                score += current.One;
                score += current.Two;
                score += current.Extra;

                if (i < 9)
                {
                    var next = frames[i + 1];
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
                                var secondNext = frames[i + 2];
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
            return frames[9].Score;
        }
    }
}
