using System;
using Cinemachine;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : SingleTon<GameManager>
{
    public CinemachineVirtualCamera mainCamera;
    public PlayerController playerController;
    public AssetReference MapReference { private get; set; }

    public float gameTime;
    public float maxGameTime = 2 * 60;

    public async void Init()
    {
        var mapRef = Instance.MapReference;
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>(mapRef, (result) =>
        {
            Instantiate(result);
        });
        
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>("Player", (result) =>
        {
            var go = result;
            var avatarGo = Instantiate(go);
            playerController = avatarGo.GetComponent<PlayerController>();
            mainCamera.LookAt = avatarGo.transform;
            mainCamera.Follow = avatarGo.transform;
        });
        
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
        playerController.SetWeapon<Bullet>();
    }
    
}
