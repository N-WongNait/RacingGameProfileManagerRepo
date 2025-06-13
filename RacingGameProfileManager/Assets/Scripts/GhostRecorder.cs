using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    [SerializeField]private GameObject _car, _gameSpawner;
    [SerializeField] private GameObject _ghostCar;
    public SaveData MyProfileSave;
    public GhostData MyGhostData, MyProfileGhost;

    private Vector3 _movePos;

    void Start()
    {
        _car = GameObject.FindGameObjectWithTag("Car");

        MyGhostData = new GhostData();
        MyProfileSave = _gameSpawner.GetComponent<GameSpawner>().MySaveData;
        MyProfileGhost = _gameSpawner.GetComponent<GameSpawner>().MyGhostData;

        LoadGhostData();
    }

    void Update()
    {
        //Debug.Log(car.transform.position);
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    myGhostData.Reset();

        //    SaveGhostData();
        //}

        //if (myGhostData.GetFirstPosition(ref movePos))
        //{
        //    SaveGhostData();
        //    ghostCar.SetActive(true);
        //    ghostCar.transform.position = movePos;
        //}
    }
    private void FixedUpdate()
    {
        MyGhostData.AddPosition(_car.transform.position);
        if (MyProfileSave.GhostData[MyProfileSave.CurrentIndex].GetNextPosition(ref _movePos))
        {
            _ghostCar.SetActive(true);
            _ghostCar.transform.position = _movePos;
        }
    }

    public void SaveGhostData()
    {
        Stream stream = File.Open("SaveFiles/GhostData.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(GhostData));
        serializer.Serialize(stream, MyGhostData);
        stream.Close();
    }

    public void LoadGhostData()
    {
        if (File.Exists("SaveFiles/GhostData.xml"))
        {
            Stream stream = File.Open("SaveFiles/GhostData.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(GhostData));
            MyProfileGhost = serializer.Deserialize(stream) as GhostData;
            stream.Close();
        }
    }
}
