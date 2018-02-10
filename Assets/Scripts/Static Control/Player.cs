using System;
using UnityEngine;
using System.Collections;
using System.IO;

public static class Player
{
    public static int Level = 0;
    public static int lastLevel = 0;
    public static int LevelBuffer = 0;
    public static float timeBuffer;
    public static float timeLast;
    public static double Score;
    public static bool IsBoy = true;
    public static int _Rows;

    public static string FormatTime(float time)
    {
        if (time <= 60)
        {
            return "00 : " + (int)time;
        }
        else
        {
            return (int) (time/60) + " : " + (int) (time%60);
        }
    }

    public static string getLastTime()
    {
        return FormatTime(timeLast);
    }
}
