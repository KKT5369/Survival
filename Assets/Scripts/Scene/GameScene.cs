using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameScene : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    
    private void Start()
    {
        GameManager.Instance.mainCamera = mainCamera;
        GameManager.Instance.SetUp();
    }
    
    
}
