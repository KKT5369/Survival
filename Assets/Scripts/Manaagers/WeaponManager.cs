using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponManager : SingleTon<WeaponManager>
{
    private Dictionary<string, ItemData> _itemDatas = new();

    public async void Init()
    {
        var names = await ResourceLoadManager.Instance.GetLabelToAddressName("Weapon");
        foreach (var v in names)
        {
            await ResourceLoadManager.Instance.LoadAssetasync<ItemData>(v, (result) =>
            {
                _itemDatas.Add(result.itemName,result);
            });
        }
        GameManager.Instance.TestCode();
    }
    public ItemData GetRandomItemData()
    {
        var count = _itemDatas.Count;
        int randomIndex = Random.Range(0, count);
        WeaponType weaponType = (WeaponType)randomIndex;
        
        
        ItemData data;
        if (_itemDatas.TryGetValue(weaponType.ToString(),out data))
        {
            return data;
        }
        
        Debug.Log("그런 무기는 없습니다.");
        return null;
        

    }

    public ItemData GetItemData(string itemName)
    {
        ItemData data;
        if (_itemDatas.TryGetValue(itemName,out data))
        {
            return data;
        }

        return null;
    }
    
    public WeaponBase SetWeaponComponent(GameObject go,WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Shovel:
                return go.AddComponent<Shovel>();
            case WeaponType.Gun:
                return go.AddComponent<Bullet>();
            case WeaponType.FireBall:
                return go.AddComponent<Bullet>();
            default:
                return null;
        }
    }
    
}
