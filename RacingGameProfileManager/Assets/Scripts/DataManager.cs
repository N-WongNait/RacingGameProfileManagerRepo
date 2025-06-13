using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public SaveData MySaveData;

    public Button[] ProfileButtons;
    public Button StartGameButton;
    public Button DeleteButton;

    public TextMeshProUGUI[] LeaderText;

    public TMP_InputField NameField;
    public TMP_Dropdown TypeDropdown;
    public TMP_Dropdown ColourDropdown;
    public TextMeshProUGUI ProfileBestTimeText;

    public int Index;

    void Start()
    {
        MySaveData = new SaveData();
        LoadData();
        Index = -1;

        NameField.interactable = false;
        TypeDropdown.interactable = false;
        ColourDropdown.interactable = false;

        StartGameButton.interactable = false;
        DeleteButton.interactable = false;
    }

    public void LoadData()
    {
        //making a new profiles.xml everytime
        // If the XML file exists then load the data.
        if (File.Exists("SaveFiles/Profiles.xml"))
        {
            Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            MySaveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            for (int i = 0; i < MySaveData.Leaders.Length; i++)
            {
                LeaderText[i].text = (i + 1) + ": " + MySaveData.Leaders[i].GetName() + " - " + MySaveData.Leaders[i].GetTime();
            }
        }
        else
        {
            ClearLeaderBoard();
        }

        UpdateProfileButtons();
    }

    public void SaveData()
    {
        if (!Directory.Exists("SaveFiles"))
        {
            Debug.Log("Created Folder");
            Directory.CreateDirectory("SaveFiles");
        }

        Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        serializer.Serialize(stream, MySaveData);
        stream.Close();
    }

    public void SelectProfile(int buttonIndex)
    {
        // If the profile button pressed does not yet have a profile associated with it then add a new profile.
        if (buttonIndex > MySaveData.Players.Count - 1)
        {
            // Add a profile and set the index to that profile.
            MySaveData.AddProfile();
            Index = MySaveData.Players.Count - 1;
            MySaveData.CurrentIndex = Index;

            UpdateProfileButtons();
        }
        // Otherwise just select the profile
        else
        {
            // Set the index to the profile selected and update the profile info.
            Index = buttonIndex;
            MySaveData.CurrentIndex = Index;
        }
        NameField.text = MySaveData.Players[Index].GetName();
        TypeDropdown.value = MySaveData.Players[Index].GetVehicleType();
        ColourDropdown.value = MySaveData.Players[Index].GetColour();
        ProfileBestTimeText.text = "Best Time: " + MySaveData.Players[Index].GetTime();

        StartGameButton.interactable = true;
        DeleteButton.interactable = true;

        UpdateProfileButtons();
    }

    public void DeleteProfile()
    {
        if (Index < MySaveData.Players.Count)
        {
            // Remove the selected profile.
            MySaveData.Players.RemoveAt(Index);
            MySaveData.GhostData.RemoveAt(Index);
            UpdateProfileButtons();
        }
    }

    void UpdateProfileButtons()
    {
        // Set all of the profile buttons active state to false.
        for (int i = 0; i < ProfileButtons.Length; i++)
        {
            ProfileButtons[i].gameObject.SetActive(false);
        }

        // For each loaded profile activate the profile button and change the text to the name of the profile.
        for (int i = 0; i < MySaveData.Players.Count; i++)
        {
            ProfileButtons[i].gameObject.SetActive(true);
            ProfileButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = MySaveData.Players[i].GetName();
        }

        if (MySaveData.Players.Count < 3)
        {
            ProfileButtons[MySaveData.Players.Count].gameObject.SetActive(true);
            ProfileButtons[MySaveData.Players.Count].GetComponentInChildren<TextMeshProUGUI>().text = "Add Profile";
        }

        if (MySaveData.Players.Count == 0)
        {
            NameField.interactable = false;
            TypeDropdown.interactable = false;
            ColourDropdown.interactable = false;

            StartGameButton.interactable = false;
            DeleteButton.interactable = false;
        }
        else
        {
            NameField.interactable = true;
            TypeDropdown.interactable = true;
            ColourDropdown.interactable = true;
        }

        SaveData();
    }

    public void GetValueFromTypeDropDown()
    {
        int playerChoice = TypeDropdown.value;
        ChangeType(playerChoice);
    }

    public void GetValueFromColourDropDown()
    {
        int playerChoice = ColourDropdown.value;
        ChangeColour(playerChoice);
    }

    public void ChangeName(string changeName)
    {
        MySaveData.Players[Index].SetName(changeName);
        UpdateProfileButtons();
    }
    public void ChangeType(int changeType)
    {
        MySaveData.Players[Index].SetVehicleType(changeType);

        if (MySaveData.Players[Index].GetVehicleType() == 0 && MySaveData.Players[Index].GetColour() == 0)
        {
            MySaveData.Players[Index].SetCar(0);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 0 && MySaveData.Players[Index].GetColour() == 1)
        {
            MySaveData.Players[Index].SetCar(1);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 0 && MySaveData.Players[Index].GetColour() == 2)
        {
            MySaveData.Players[Index].SetCar(2);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 1 && MySaveData.Players[Index].GetColour() == 0)
        {
            MySaveData.Players[Index].SetCar(3);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 1 && MySaveData.Players[Index].GetColour() == 1)
        {
            MySaveData.Players[Index].SetCar(4);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 1 && MySaveData.Players[Index].GetColour() == 2)
        {
            MySaveData.Players[Index].SetCar(5);
        }

        UpdateProfileButtons();
    }

    public void ChangeColour(int changeColour)
    {
        MySaveData.Players[Index].SetColour(changeColour);

        if (MySaveData.Players[Index].GetVehicleType() == 0 && MySaveData.Players[Index].GetColour() == 0)
        {
            MySaveData.Players[Index].SetCar(0);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 0 && MySaveData.Players[Index].GetColour() == 1)
        {
            MySaveData.Players[Index].SetCar(1);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 0 && MySaveData.Players[Index].GetColour() == 2)
        {
            MySaveData.Players[Index].SetCar(2);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 1 && MySaveData.Players[Index].GetColour() == 0)
        {
            MySaveData.Players[Index].SetCar(3);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 1 && MySaveData.Players[Index].GetColour() == 1)
        {
            MySaveData.Players[Index].SetCar(4);
        }
        else if (MySaveData.Players[Index].GetVehicleType() == 1 && MySaveData.Players[Index].GetColour() == 2)
        {
            MySaveData.Players[Index].SetCar(5);
        }

        UpdateProfileButtons();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ClearLeaderBoard()
    {
        for (int i = 0; i < MySaveData.Leaders.Length; i++)
        {
            MySaveData.Leaders[i].SetName("Empty");
            MySaveData.Leaders[i].SetTime(100.00f);
            LeaderText[i].text = (i + 1) + ": " + MySaveData.Leaders[i].GetName() + " - " + MySaveData.Leaders[i].GetTime();

            SaveData();
        }
    }
}
