using System;
using Data;
using Unity.Mathematics;
using UnityEngine;

public class Shovel : WeaponBase
{
    protected override void WeaponAction()
    {
        for (int i = 0; i < data.baseCount; i++)
        {
            Transform tfWeapon = null;

            if (i < transform.childCount)
            {
                tfWeapon = transform.GetChild(i);
            }
            else
            {
                tfWeapon = Instantiate(data.projectile, transform).transform;
            }

            tfWeapon.localPosition = Vector3.zero;
            tfWeapon.localRotation = quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / data.baseCount;
            tfWeapon.Rotate(rotVec);
            tfWeapon.Translate(tfWeapon.up * 1.5f , Space.World);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.back * data.baseSpeed * Time.deltaTime);
    }
}
