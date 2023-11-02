using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
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
        var info = new WeaponInfo(WeaponType.Shovel,0, 50, -1, 5, 150f);
        playerController.SetWeapon<Shovel>(info);
        var info2 = new WeaponInfo(WeaponType.GunBullet,0, 5, 3, 1, 15f);
        playerController.SetWeapon<Bullet>(info2);
        var info3 = new WeaponInfo(WeaponType.FireBullet,0, 8, 0, 1, 10f);
        playerController.SetWeapon<Bullet>(info3);
        
    }
    
}
