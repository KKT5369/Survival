using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public InfoType type;

    [SerializeField] private TMP_Text myText;
    [SerializeField] private Slider mySlider;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = _gameManager.exp;
                float maxExp = _gameManager.nextExp[_gameManager.level];
                mySlider.value = curExp / maxExp;
                
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", _gameManager.level); // F >> 소수점 지정
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", _gameManager.kill);
                break;
            case InfoType.Time:
                float remainTiem = _gameManager.maxGameTime - _gameManager.gameTime;
                int min = Mathf.FloorToInt(remainTiem / 60);
                int sec = Mathf.FloorToInt(remainTiem % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min,sec); // D >> 자리수 고정
                break;
            case InfoType.Health:
                float curHealth = _gameManager.health;
                float maxHealth = _gameManager.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
