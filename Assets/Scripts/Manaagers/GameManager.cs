using System;
using System.Collections.Generic;
using Cinemachine;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

public class GameManager : SingleTon<GameManager>
{
    public PlayerInfo playerInfo;
    public AssetReference AcAvatar { get; set; }
    public CinemachineVirtualCamera mainCamera;
    public PlayerController playerController;
    public AssetReference MapReference { private get; set; }
    

    public float gameTime;
    public float maxGameTime = 2 * 60;

    // public int health;
    // public int maxHealth = 100;
    // public int level;
    // public int kill;
    // public int exp;
    // public float[] nextExp = { 3, 5, 10, 20, 20, 20, 200, 250 };
    
    public async void Init()
    {
        var mapRef = Instance.MapReference;
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>(mapRef, (result) =>
        {
            Instantiate(result);
        });
        
        await ResourceLoadManager.Instance.LoadAssetasync<PlayerInfo>("Player", (result) =>
        {
            playerInfo = result;
            var go = result.playerPre;
            var avatarGo = Instantiate(go);
            playerController = avatarGo.GetComponent<PlayerController>();
            mainCamera.LookAt = avatarGo.transform;
            mainCamera.Follow = avatarGo.transform;
        });
        UIManager.Instance.OpenUI<UIMain>();
        TestCode();
    }
    
    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > 0.2f)
        {
            gameTime += 0;
        }
    }
    // ++ Test Code ++ // 
    public void TestCode()
    {
    }

    public void GetExp()
    {
        playerInfo.exp++;

        if (playerInfo.exp == playerInfo.nextExp[playerInfo.level])
        {
            playerInfo.level++;
            playerInfo.exp = 0;
            UIManager.Instance.OpenUI<UISelectItem>();
            Time.timeScale = 0;
        }
    }
    
}
