using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBowlingScoreCard.Classes
{
    public class CurrentFrameScoreCalculator : IScoreCalculator
    {
        public CurrentFrameScoreCalculator()
        {

        }

        public int CalculateMaxScore(Frame[] frames, (int CurrentFrame, int CurrentShot) currentFrame)
        {
            var newFrames = frames.Select(x => x.CopyFrame()).ToArray();
            for(int i=currentFrame.CurrentFrame; i<10; i++)
            {
                newFrames[i].SetMaxScore(i == currentFrame.CurrentFrame ? currentFrame.CurrentShot : 1);
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
                if(current.One != 10) score += current.Two;

                if (current.One == 10) score += 20;
                if (current.One != 10 && current.One + current.Two == 10) score += current.One;

                current.Score = score;
            }
            return frames[9].Score;
        }
    }
}
