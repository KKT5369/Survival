using System;
using Data;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    public WeaponType WeaponType { get; protected set; }
    public int Level { get; private set; }
    public int Damage { get; protected set; }
    public int Per { get; protected set; }
    public int Count { get; protected set; }
    public float Speed { get; protected set; }

    public void LevelUp(WeaponInfo info)
    {
        SetWeaponInfo(info);
        WeaponAction();
    }
    

    void SetWeaponInfo(WeaponInfo info)
    {
        // todo Json 파싱으로 추후 적용
        // 레벨에 따라 능력치 적용...
        WeaponType = info.weaponType;
        Damage = info.damage;
        Per = info.per;
        Count = info.count;
        Speed = info.speed;
    }
    protected abstract void WeaponAction();
}

[Serializable]
public class WeaponInfo
{
    public WeaponType weaponType;
    public int level;
    public int damage;
    public int per;
    public int count;
    public float speed;

    public WeaponInfo(WeaponType weaponType,int level,int damage, int per, int count, float speed)
    {
        this.weaponType = weaponType; 
        this.level = level;
        this.damage = damage;
        this.per = per;
        this.count = count;
        this.speed = speed;
    }
}
