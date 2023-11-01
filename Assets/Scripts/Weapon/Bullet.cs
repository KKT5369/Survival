using System;
using Data;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : WeaponBase
{
    protected override void SetWeaponInfo(WeaponInfo info)
    {
        // todo Json 파싱으로 추후 적용
        // 레벨에 따라 능력치 적용...
        
        Damage = info.damage;
        Per = info.per;
        Count = info.count;
        Speed = info.speed;
    }

    protected async override void WeaponAction()
    {
        for (int i = 0; i < Count; i++)
        {
            Transform bullet = null;
            
            string weaponName = Convert.ToString(WeaponType.Bullet);

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                await ResourceLoadManager.Instance.LoadAssetasync<GameObject>(weaponName, (result) =>
                {
                    bullet = Instantiate(result, parent).transform;
                });
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / Count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f , Space.World);
            
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.back * Speed * Time.deltaTime);
    }
}
