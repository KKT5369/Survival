using System;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    public ItemData data;

    public void Init(ItemData data)
    {
        SetWeaponInfo(data);
        WeaponAction();
    }
    
    void SetWeaponInfo(ItemData data)
    {
        this.data = data;
    }
    
    public void LevelUp()
    {
        var level = data.level++;

        data.baseDamage += data.damages[level];
        data.baseSpeed += data.speeds[level];
        data.baseCount += data.counts[level];
        data.basePer += data.pers[level];
        
        WeaponAction();
    }
    

    
    protected abstract void WeaponAction();
}
