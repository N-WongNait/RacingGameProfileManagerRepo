using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System;
using UnityEngine.UI;

public class RaceGame : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _instructionText, _timeText;
    [SerializeField]private Button _doneButton;

    private float _time;

    private float _timeStamp;

    [SerializeField]private bool _gameStart, _gameFinish, _finishOnce = false;
    void Start()
    {
        _time = 0.00f;
        _gameStart = false;
        _gameFinish = false;

        GameEvents.GameFinish.AddListener(Finished);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void FixedUpdate()
    {
        _instructionText.text = "GO!!";
        _gameStart = true;

        if (_gameStart && !_gameFinish)
        {
            _time += Time.deltaTime;
            _timeText.text = Math.Round(_time, 2).ToString();
        }

        if (_gameFinish && !_finishOnce)
        {
            _time = (float)Math.Round(_time, 2);
            _instructionText.text = "FINISHED";
            _doneButton.gameObject.SetActive(true); _doneButton.gameObject.SetActive(false);
            FinishGame();
            _finishOnce = true;
        }
    }

    void Finished()
    {
        _gameFinish = true;
    }

    public void FinishGame()
    {
        GetComponent<GameSpawner>().SaveScore(_time);
    }
}
