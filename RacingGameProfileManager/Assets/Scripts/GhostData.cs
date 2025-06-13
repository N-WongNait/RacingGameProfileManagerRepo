using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GhostData
{
    public List<float> PosX, PosZ;

    private int index;

    public GhostData() 
    {
        PosX = new List<float>();
        PosZ = new List<float>();
    }

    public void Reset()
    {
        PosX.Clear();
        PosZ.Clear();
    }

    public void AddPosition(Vector3 position)
    {
        PosX.Add(position.x);
        PosZ.Add(position.z);
    }
    public bool GetNextPosition(ref Vector3 vec)
    {
        index++;

        if (index >= 0 && index < PosX.Count)
        {
            vec = new Vector3(PosX[index], 0.5f, PosZ[index]);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetFirstPosition(ref Vector3 vec)
    {
        if (PosX.Count > 0)
        {
            index = 0;

            vec = new Vector3(PosX[index], 0.5f, PosZ[index]);
            return true;
        }
        else
        {
            return false;
        }
    }
}
