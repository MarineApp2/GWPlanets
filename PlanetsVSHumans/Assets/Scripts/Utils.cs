using System;
using UnityEngine;

namespace Utils
{
    public static class Numbers
    {
        public const int million = 1000000;
        public const long billion = 1000000000;
        public const long trillion = 1000000000000;
    }

    // countdown timer in seconds
    public class Timer
    {
        // timer
        private float time = 0.0f;
        // is the Timer ticking?
        private bool ticking = false;

        // time stamp
        private double t = 0.0f;

        // function to be called in a loop
        // returns if the timer reached 0, specified its timer count
        public bool Bip(float count)
        {
            if (!ticking)
            {
                ticking = true;
                return false;
            }
            else
            {
                time += Time.deltaTime;

                if (time >= count)
                {
                    time = 0.0f;
                    ticking = false;
                    return true;
                }

                return false;
            }
        }

        // update the timer's life time
        public void UpdateTimeStamp()
        {
            t += Time.deltaTime;
        }

        public void ResetStamp()
        {
            t = 0.0f;
        }

        // square value with time stamp
        public double SqrTime()
        {
            double tt = t * t;

            if (tt < 0.0D)
                return double.MaxValue;

            return tt;
        }

        public double CubTime()
        {
            double ttt = t * t * t;

            if (ttt < 0.0D)
                return double.MaxValue;

            return ttt;
        }

        // exponential value with time stamp
        public double ExpTime()
        {
            double e = Math.Exp(t);

            if (e < 0.0D)
                return double.MaxValue;

            return e;
        }
    }
}