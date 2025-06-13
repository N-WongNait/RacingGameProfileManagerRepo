using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int VehicleTypeIndex;
    public int ColourIndex;
    public int CarIndex;
    public float BestTime;

    public PlayerData()
    {
        Name = "New Profile";
        VehicleTypeIndex = 0;
        ColourIndex = 0;
        CarIndex = 0;
        BestTime = 100.00f;
    }

    public PlayerData(string changeName, int changeType, int changeColour, int changeCar, float changeTime)
    {
        Name = changeName;
        VehicleTypeIndex = changeType;
        ColourIndex = changeColour;
        CarIndex = changeCar;
        BestTime = changeTime;
    }

    public string GetName()
    {
        return Name;
    }
    public void SetName(string changeName)
    {
        Name = changeName;
    }
    public int GetVehicleType()
    {
        return VehicleTypeIndex;
    }
    public void SetVehicleType(int changeType)
    {
        VehicleTypeIndex = changeType;
    }
    public int GetColour()
    {
        return ColourIndex;
    }
    public void SetColour(int changeColor)
    {
        ColourIndex = changeColor;
    }
    public int GetCar()
    {
        return CarIndex;
    }
    public void SetCar(int changeCar)
    {
        CarIndex = changeCar;
    }
    public float GetTime()
    {
        return BestTime;
    }
    public void SetTime(float changeTime)
    {
        BestTime = changeTime;
    }
}
