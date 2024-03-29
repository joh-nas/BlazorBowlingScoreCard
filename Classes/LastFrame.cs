﻿using System;
using System.Linq;

namespace BlazorBowlingScoreCard.Classes
{
    public class LastFrame : Frame
    {
        public override int Extra
        {
            get { return _extra; }
            set
            {
                _extra = value;
            }
        }

        public LastFrame() : base()
        {
        }

        public override Frame CopyFrame()
        {
            var newFrame = new LastFrame() { One = One, Two = Two, Extra = Extra, Score = Score };
            return newFrame;
        }

        internal override int[] GetPossibleInputs(int shotNumber)
        {
            if (shotNumber == 1) return Enumerable.Range(1, 10).ToArray();
            if (shotNumber == 2)
            {
                if (One != 10) return Enumerable.Range(1, 10 - One).ToArray();
                if (One == 10) return Enumerable.Range(1, 10).ToArray();
            }
            if (shotNumber == 3)
            {
                if (One + Two == 10 || One + Two == 20) return Enumerable.Range(1, 10).ToArray();
                if (One == 10) return Enumerable.Range(1, 10 - Two).ToArray();
            }

            return new int[0];
        }

        internal override void AddScore(int shotNumber, int score)
        {
            if (score >= 0 && score <= 10)
                SetShotScore(shotNumber, score);
            if (shotNumber == 1 && score == 10) SetShotScore(2, 0);

            if (One != 10 && One + Two != 10)
            {
                SetShotScore(3, 0);
            }
        }

        public override void SetShotScore(int shotNumber, int score)
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
                    Extra = score;
                    break;
                default:
                    throw new ApplicationException("shotNumber has to be between 1 and 3.");
            }
        }

        public override void SetMaxScore(int currentShot)
        {
            if (currentShot == 1)
            {
                One = 10;
                Two = 10;
                Extra = 10;
            }
            if (currentShot == 2)
            {
                if (One == 10) Two = 10;
                else Two = 10 - One;
                Extra = 10;
            }
            if (currentShot == 3)
            {
                if (One == 10 && Two == 10) Extra = 10;
                if (One + Two == 10) Extra = 10;
                if (One == 10 && Two != 10) Extra = 10 - Two;
                if (One + Two < 10) Extra = 0;
            }
        }

        public override void SetMinScore(int currentShot)
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
                if (One + Two == 10) Extra = 1;
                else Extra = 0;
            }
            if (currentShot == 3)
            {
                if (One == 10) Extra = 1;
                if (One + Two == 10) Extra = 1;
                if (One + Two < 10) Extra = 0;
            }
        }

        public override string ShowShotScore(int shotNumber)
        {
            if (shotNumber == 1)
            {
                return ShowCorrectCharacter(One);
            }
            if (shotNumber == 2)
            {
                if (One == 10 && Two == 10) return "X";
                if (One == 10 && Two == 0) return "-";
                if (Two == 0) return "-";
                if (One + Two == 10) return "/";
                return Two.ToString();
            }

            if (shotNumber == 3)
            {
                if (One + Two < 10) return "";
                if (One == 10 && Two == 10 || One + Two == 10) return ShowCorrectCharacter(Extra);
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

        internal override int SpareShotCount(int shotNumber)
        {
            if (shotNumber == 1) return 0;
            if (shotNumber == 2 && One != 10) return 10 - One;
            if (shotNumber == 3 && ((One == 0 || One == 10) && Two != 10)) return Extra - Two;
            return 0;
        }

        internal override bool IsStrikePossible(int shotNumber)
        {
            if (shotNumber == 1) return true;
            if (shotNumber == 2 && One == 10) return true;
            if (shotNumber == 3 && (One + Two == 10 || One + Two == 20)) return true;
            return false;
        }

        internal override bool IsSparePossible(int shotNumber)
        {
            if (shotNumber == 1) return false;
            if (shotNumber == 2 && One != 10) return true;
            if (shotNumber == 3 && One == 10 && Two != 10) return true;
            return false;
        }

        public override (int NextFrame, int NextShot) GetNextShot(int frameNumber, int shotNumber)
        {
            if (shotNumber == 2)
            {
                shotNumber = 3;
                return (frameNumber, shotNumber);
            }

            if (shotNumber == 1)
            {
                if (One == 10)
                {
                    shotNumber++;
                    return (frameNumber, shotNumber);
                }
                shotNumber = 2;
                return (frameNumber, shotNumber);
            }

            if (shotNumber == 2)
            {
                if (One == 10 || One + Two == 10)
                {
                    shotNumber = 3;
                    return (frameNumber, shotNumber);
                }
            }
            frameNumber++;
            shotNumber = 1;
            return (frameNumber, shotNumber);
        }

        internal override void VerifyFrameScore()
        {
            if (One + Two + Extra > 30) throw new ApplicationException("There are only 30 pins to knock down in the last frame.");
        }
    }
}
