using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public int level;
    public int Damage { get; protected set; }
    public int Per { get; protected set; }
    public int Count { get; protected set; }
    public float Speed { get; protected set; }

    public Transform parent;

    public void LevelUp()
    {
        level++;
        var info = new WeaponInfo(0, 10, -1, 2, 150f);
        SetWeaponInfo(info);
        WeaponAction();
    }

    protected abstract void SetWeaponInfo(WeaponInfo info);
    protected abstract void WeaponAction();
}

[Serializable]
public class WeaponInfo
{
    public int level;
    public int damage;
    public int per;
    public int count;
    public float speed;

    public WeaponInfo(int level,int damage, int per, int count, float speed)
    {
        this.level = level;
        this.damage = damage;
        this.per = per;
        this.count = count;
        this.speed = speed;
    }
}
