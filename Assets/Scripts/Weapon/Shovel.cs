using System;
using Data;
using Unity.Mathematics;
using UnityEngine;

public class Shovel : WeaponBase
{
    protected async override void WeaponAction()
    {
        for (int i = 0; i < Count; i++)
        {
            Transform tfWeapon = null;
            
            string weaponName = Convert.ToString(WeaponType);

            if (i < transform.childCount)
            {
                tfWeapon = transform.GetChild(i);
            }
            else
            {
                await ResourceLoadManager.Instance.LoadAssetasync<GameObject>(weaponName, (result) =>
                {
                    tfWeapon = Instantiate(result, transform).transform;
                });
            }

            tfWeapon.localPosition = Vector3.zero;
            tfWeapon.localRotation = quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / Count;
            tfWeapon.Rotate(rotVec);
            tfWeapon.Translate(tfWeapon.up * 1.5f , Space.World);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.back * Speed * Time.deltaTime);
    }
}
