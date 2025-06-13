using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class TopScore
{
    public string Name;
    public float Time;

    public TopScore()
    {
        Name = "Empty";
        Time = 100.00f;
    }

    public TopScore(string changeName, float changeTime)
    {
        Name = changeName;
        Time = changeTime;
    }
    public string GetName()
    {
        return Name;
    }
    public void SetName(string changeName)
    {
        Name = changeName;
    }
    public float GetTime()
    {
        return Time;
    }
    public void SetTime(float changeTime)
    {
        Time = changeTime;
    }
}
