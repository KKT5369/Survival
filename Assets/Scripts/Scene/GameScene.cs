using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameScene : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;
    
    private void Start()
    {
        GameManager.Instance.camera = camera;
        GameManager.Instance.SetUp();
    }
    
    
}
