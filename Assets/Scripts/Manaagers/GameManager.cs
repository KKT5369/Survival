using System.Collections.Generic;
using Cinemachine;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : SingleTon<GameManager>
{
    public CinemachineVirtualCamera mainCamera;
    public PlayerController playerController;
    public AssetReference MapReference { private get; set; }
    private Dictionary<string, ItemData> _itemDatas = new();

    public float gameTime;
    public float maxGameTime = 2 * 60;

    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public float[] nextExp = { 3, 5, 10, 100, 123, 140, 200, 250 };
    
    public async void Init()
    {
        health = maxHealth;
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
        
        var names = await ResourceLoadManager.Instance.GetLabelToAddressName("Weapon");
        foreach (var v in names)
        {
            await ResourceLoadManager.Instance.LoadAssetasync<ItemData>(v, (result) =>
            {
                _itemDatas.Add(result.itemName,result);
            });
        }
        
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
        ItemData data;
        if (_itemDatas.TryGetValue(WeaponType.Shovel.ToString(), out data))
        {
            playerController.SetWeapon<Shovel>(data);
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
    
}
