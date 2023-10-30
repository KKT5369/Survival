using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : SingleTon<GameManager>
{
    public CinemachineVirtualCamera mainCamera;
    public PlayerController playerController;
    public AssetReference MapReference { private get; set; }

    public async void SetUp()
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

        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>("Enemy", (result) =>
        {
            Instantiate(result, new Vector3(3, 3, 0),quaternion.identity);
        });
    }
}
