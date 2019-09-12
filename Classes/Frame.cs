using System;
using System.Linq;

namespace BlazorBowlingScoreCard.Classes
{
    public class Frame
    {
        private int _one;
        private int _two;
        protected int _extra;

        public int One
        {
            get { return _one; }
            set
            {
                if (value < 0 || value > 10) throw new ApplicationException("You can shoot between 0 and 10 pins in one shot.");
                _one = value;
            }
        }

        public int Two
        {
            get { return _two; }
            set
            {
                if (value < 0 || value > 10) throw new ApplicationException("You can shoot between 0 and 10 pins in one shot.");
                _two = value;
            }
        }

        public virtual int Extra
        {
            get { return _extra; }
            set
            {
                if (value != 0) throw new ApplicationException($"There is no extra throw in this frame");
                _extra = value;
            }
        }

        public int Score { get; set; }

        public Frame()
        {
        }

        public virtual Frame CopyFrame()
        {
            var newFrame = new Frame() { One = One, Two = Two, Extra = Extra, Score = Score };
            return newFrame;
        }

        internal virtual int[] GetPossibleInputs(int shotNumber)
        {
            if (shotNumber == 1) return Enumerable.Range(1, 10).ToArray();
            if (shotNumber == 2)
            {
                if (One != 10) return Enumerable.Range(1, 10 - One).ToArray();
                if (One == 10) return new int[0];
            }

            return new int[0];
        }

        internal virtual void AddScore(int shotNumber, int score)
        {
            if (score >= 0 && score <= 10)
                SetShotScore(shotNumber, score);
            if (shotNumber == 1 && score == 10) SetShotScore(2, 0);
        }

        public virtual void SetShotScore(int shotNumber, int score)
        {
            switch (shotNumber)
            {
                case 1: 
                    One = score;
                    break;
                case 2: 
                    Two = score;
                    break;
                case 3: 
                    if (score != 0) throw new ApplicationException("ShotNumber 3 can only be used in frame 10.");
                    Extra = score;
                    break;
                default:
                    throw new ApplicationException("shotNumber has to be between 1 and 3.");
            }
        }

        public virtual void SetMaxScore(int currentShot)
        {
            if (currentShot == 1)
            {
                One = 10;
                Two = 0;
                Extra = 0;
            }
            if (currentShot == 2)
            {
                Two = 10 - One;
                Extra = 0;
            }
        }

        public virtual void SetMinScore(int currentShot)
        {
            if (currentShot == 1)
            {
                One = 1;
                Two = 1;
                Extra = 0;
            }
            if (currentShot == 2)
            {
                Two = 1;
                Extra = 0;
            }
        }
        
        public virtual string ShowShotScore(int shotNumber)
        {
            if (shotNumber == 1)
            {
                return ShowCorrectCharacter(One);
            }
            if (shotNumber == 2)
            {
                if (One == 10) return "";
                if (Two == 0) return "-";
                if (One + Two == 10) return "/";
                return Two.ToString();
            }

            return "";
        }

        internal virtual int SpareShotCount(int shotNumber)
        {
            if (shotNumber == 1) return 0;
            if (shotNumber == 2 && One != 10) return 10 - One;
            return 0;
        }

        internal virtual bool IsStrikePossible(int shotNumber)
        {
            if (shotNumber == 1) return true;
            return false;
        }

        internal virtual bool IsSparePossible(int shotNumber)
        {
            if (shotNumber == 1) return false;
            if (shotNumber == 2 && One != 10) return true;
            return false;
        }

        internal virtual void VerifyFrameScore()
        {
            if (One + Two > 10) throw new ApplicationException("There are only 10 pins to knock down in each frame.");
        }

        public virtual (int NextFrame, int NextShot) GetNextShot(int frameNumber, int shotNumber)
        {
            if (shotNumber == 1)
            {
                if (One == 10)
                {
                    frameNumber++;
                    return (frameNumber, shotNumber);
                }
                shotNumber = 2;
                return (frameNumber, shotNumber);
            }

            if (shotNumber == 2)
            {
                frameNumber++;
                shotNumber = 1;
            }
            return (frameNumber, shotNumber);
        }

        public string ShowCorrectCharacter(int shotCount)
        {
            if (shotCount == 0) return "-";
            if (shotCount == 10) return "X";
            return shotCount.ToString();
        }

        public override string ToString()
        {
            return $"One: {One}, Two: {Two}, Extra: {Extra}, Score: {Score}";
        }
    }
}
