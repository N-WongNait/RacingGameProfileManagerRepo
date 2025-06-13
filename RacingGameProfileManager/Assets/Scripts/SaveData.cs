using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<PlayerData> Players;
    public TopScore[] Leaders;
    public List<GhostData> GhostData;

    public int CurrentIndex;
    public SaveData()
    {
        Players = new List<PlayerData>();
        Leaders = new TopScore[5];
        GhostData = new List<GhostData>();

        for (int i = 0; i < Leaders.Length; ++i)
        {
            Leaders[i] = new TopScore();
        }
    }

    public void AddProfile()
    {
        Players.Add(new PlayerData("Profile" + (Players.Count + 1), 0, 0, 0, 100.00f));
        GhostData.Add(new GhostData());
    }
    public void RemoveProfile(int index)
    {
        Players.RemoveAt(index);
        GhostData.RemoveAt(index);
    }
}
