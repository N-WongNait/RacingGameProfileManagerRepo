using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSpawner : MonoBehaviour
{
    public SaveData MySaveData;

    public GameObject[] CarList;

    private GameObject _currentCar; 

    [SerializeField]private GameObject _ghostRecorder;

    public GhostData MyGhostData;

    [SerializeField]private GameObject _overwriteGhostPanel;

    private bool _overwriteCheck = false;

    private void Awake()
    {
        LoadData();

        if (MySaveData.Players[MySaveData.CurrentIndex].GetCar() == 0)
        {
            CarList[0].gameObject.SetActive(true);
            _currentCar = CarList[0].gameObject;
        }
        else if (MySaveData.Players[MySaveData.CurrentIndex].GetCar() == 1)
        {
            CarList[1].gameObject.SetActive(true);
            _currentCar = CarList[1].gameObject;
        }
        else if (MySaveData.Players[MySaveData.CurrentIndex].GetCar() == 2)
        {
            CarList[2].gameObject.SetActive(true);
            _currentCar = CarList[2].gameObject;
        }
        else if (MySaveData.Players[MySaveData.CurrentIndex].GetCar() == 3)
        {
            CarList[3].gameObject.SetActive(true);
            _currentCar = CarList[3].gameObject;
        }
        else if (MySaveData.Players[MySaveData.CurrentIndex].GetCar() == 4)
        {
            CarList[4].gameObject.SetActive(true);
            _currentCar = CarList[4].gameObject;
        }
        else if (MySaveData.Players[MySaveData.CurrentIndex].GetCar() == 5)
        {
            CarList[5].gameObject.SetActive(true);
            _currentCar = CarList[5].gameObject;
        }

        //Debug.Log(currentCar.name);

        MyGhostData = _ghostRecorder.GetComponent<GhostRecorder>().MyGhostData;
    }

    void Update()
    {
        if (_overwriteCheck)
        {
            _overwriteGhostPanel.SetActive(true);
        }
    }

    public void LoadData()
    {
        if (File.Exists("SaveFiles/Profiles.xml"))
        {
            Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            MySaveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
    }

    public void SaveScore(float changeTime)
    {
        if (changeTime < MySaveData.Players[MySaveData.CurrentIndex].GetTime())
        {
            MySaveData.Players[MySaveData.CurrentIndex].SetTime(changeTime);
        }

        CheckTopScores(changeTime, MySaveData.Players[MySaveData.CurrentIndex].GetName());

        Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        serializer.Serialize(stream, MySaveData);
        stream.Close();

        if (MySaveData.GhostData[MySaveData.CurrentIndex].PosX.Count == 0)
        {
            SaveProfileGhost();
            SceneManager.LoadScene(0);
        }
        else
        {
            _overwriteCheck = true;
        }
    }

    void CheckTopScores(float checkTime, string checkName)
    {
        float tempTime;
        string tempName;

        for (int i = 0; i < MySaveData.Leaders.Length; i++)
        {
            if (MySaveData.Leaders[i].GetTime() > checkTime)
            {
                tempTime = MySaveData.Leaders[i].GetTime();
                tempName = MySaveData.Leaders[i].GetName();

                MySaveData.Leaders[i].SetTime(checkTime);
                MySaveData.Leaders[i].SetName(checkName);

                checkTime = tempTime;
                checkName = tempName;
            }
        }
    }

    public void SaveProfileGhost()
    {
        MySaveData.GhostData[MySaveData.CurrentIndex].Reset();
        //Debug.Log(mySaveData.ghostData.posX.Count);
        MySaveData.GhostData[MySaveData.CurrentIndex].PosX.AddRange(_ghostRecorder.GetComponent<GhostRecorder>().MyGhostData.PosX);
        MySaveData.GhostData[MySaveData.CurrentIndex].PosZ.AddRange(_ghostRecorder.GetComponent<GhostRecorder>().MyGhostData.PosZ);

        Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        serializer.Serialize(stream, MySaveData);
        stream.Close();
    }

    public void ResetGhost()
    {
        //myGhostData.Reset();

        Stream stream = File.Open("SaveFiles/GhostData.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(GhostData));
        serializer.Serialize(stream, MyGhostData);
        stream.Close();
    }

    public void SwitchPanel()
    {
        if (_overwriteGhostPanel.activeInHierarchy)
        {
            _overwriteGhostPanel.SetActive(false);
        }
        else
        {
            _overwriteGhostPanel.SetActive(true);
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
