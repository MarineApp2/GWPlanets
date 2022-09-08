using System;
using UnityEngine;

namespace Utils
{
    public static class Numerics
    {
        public const float TAU = 2.0f * Mathf.PI;
        public const int million = 1000000;
        public const long billion = 1000000000;
        public const long trillion = 1000000000000;

        public static Vector3 RNGPosition(Vector2 xrange, Vector2 yrange, Vector2 zrange)
        {
            float x = UnityEngine.Random.Range(xrange.x, xrange.y);
            float y = UnityEngine.Random.Range(yrange.x, yrange.y);
            float z = UnityEngine.Random.Range(zrange.x, zrange.y);

            return new Vector3(x, y, z);
        }
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
        // returns if the timer reached the specified count
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

    public static class Raycaster
    {
        public static RaycastHit Pick()
        {
            Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

            Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
            Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

            RaycastHit hit;
            Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

            return hit;
        }
    }
}