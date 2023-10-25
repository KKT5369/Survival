using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.PlayerLoop;

public class GameManager : SingleTon<GameManager>
{
    private PlayerController _playerController;
    private GameObject _mapGo;
    public AssetReference MapReference { private get; set; }
    
    public async void SetUp()
    {
        var mapRef = GameManager.Instance.MapReference;
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>(mapRef, (result) =>
        {
            _mapGo = result;
            Instantiate(result);
        });
        
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>("Player", (result) =>
        {
            var go = result;
            _playerController = Instantiate(go).GetComponent<PlayerController>();
        });
    }


}
