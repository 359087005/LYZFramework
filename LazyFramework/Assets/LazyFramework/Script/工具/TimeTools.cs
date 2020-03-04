using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTools 
{
    public static string SToHMS(int s)
    {
        string temp = "";
        int hour = 0;
        int min = 0;
        int sce = 0;
        hour = s / 3600;
        min = (s-(hour*3600)) / 60;
        sce = s % 60;
        temp = hour.ToString("00") + ":" + min.ToString("00") + ":" + sce.ToString("00");
        return temp;
    }
}
