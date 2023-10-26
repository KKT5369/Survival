using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : SingleTon<GameManager>
{
    public CinemachineVirtualCamera mainCamera;
    public PlayerController playerController;
    public GameObject mapGo;
    public AssetReference MapReference { private get; set; }
    private List<GameObject> _grounds = new();

    public async void SetUp()
    {
        var mapRef = Instance.MapReference;
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>(mapRef, (result) =>
        {
            mapGo = result;
            var groundGo = Instantiate(result);
            
            
        });
        
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>("Player", (result) =>
        {
            var go = result;
            var avatarGo = Instantiate(go);
            playerController = avatarGo.GetComponent<PlayerController>();
            mainCamera.LookAt = avatarGo.transform;
            mainCamera.Follow = avatarGo.transform;
        });
    }
    
    


}
