using System;

namespace BlazorApp1.Classes
{
    public class Frame
    {
        private int _one;
        private int _two;
        private int _extra;

        public int FrameNumber { get; set; }
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

        public int Extra
        {
            get { return _extra; }
            set
            {
                if (FrameNumber != 10 && value != 0) throw new ApplicationException($"There is no extra throw in frame {FrameNumber}");
                _extra = value;
            }
        }

        public int Score { get; set; }

        public Frame(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public void SetShotScore(int shotNumber, int score)
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
                    if (FrameNumber != 10) throw new ApplicationException("ShotNumber 3 can only be used in frame 10.");
                    Extra = score;
                    break;
                default:
                    throw new ApplicationException("shotNumber has to be between 1 and 3.");
            }
        }

        public string ShowShotScore(int shotNumber)
        {
            if (shotNumber == 1)
            {
                return ShowCorrectCharacter(One);
            }
            if (shotNumber == 2)
            {
                if (One == 10 && FrameNumber != 10) return "";
                if (FrameNumber == 10 && One == 10 && Two == 10) return "X";
                if (FrameNumber == 10 && One == 10 && Two == 0) return "-";
                if (Two == 0) return "-";
                if (One + Two == 10) return "/";
                return Two.ToString();
            }

            if (FrameNumber == 10 && shotNumber == 3)
            {
                if (One + Two < 10) return "";
                if (One + Two == 10) return ShowCorrectCharacter(Extra);
                if (One == 10 && Two == 10) return ShowCorrectCharacter(Extra);
                if (One == 10 && Two != 10)
                {
                    if (Extra == 0) return "-";
                    if (Two + Extra == 10) return "/";
                    return Extra.ToString();
                }

                return "";
            }

            return "";
        }

        public string ShowCorrectCharacter(int shotCount)
        {
            if (shotCount == 0) return "-";
            if (shotCount == 10) return "X";
            return shotCount.ToString();
        }
    }
}
