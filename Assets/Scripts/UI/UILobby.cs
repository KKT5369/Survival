using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    [SerializeField] private Button btnSelectAvatar;
    [SerializeField] private Button btnStartGame;

    void Start()
    {
        SetAddListener();
    }

    void SetAddListener()
    {
        btnSelectAvatar.onClick.AddListener((() =>
        {
            print("미구현");
        }));
        
        btnStartGame.onClick.AddListener((() =>
        {
            SceneManager.LoadScene("GameScene");
        }));
    }
    
}
