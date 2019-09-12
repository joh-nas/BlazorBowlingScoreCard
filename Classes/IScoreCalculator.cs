using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBowlingScoreCard.Classes
{
    public interface IScoreCalculator
    {
        int CalculateScore(Frame[] frames);
        int CalculateMaxScore(Frame[] frames, (int CurrentFrame, int CurrentShot) currentFrame);
    }
}
