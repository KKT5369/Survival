using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class Bullet : WeaponBase
{
    public Transform  target;
    private List<GameObject> _pool = new();
    private float _timer;
    private void FixedUpdate()
    {
        WeaponAction();
    }

    protected override async void WeaponAction()
    {
        target = GameManager.Instance.playerController.scanner.nearestTarget;
        if (!target)
            return;
        _timer += Time.deltaTime;
        Transform playerTf = GameManager.Instance.playerController.transform;
        Vector3 playerPos = playerTf.position + playerTf.up * 1.5f;
        Quaternion playerRot = playerTf.rotation;
        
        string strWeaponType = Convert.ToString(WeaponType);
        if (_timer > Count)
        {
            foreach (var go in _pool)
            {
                if (!go.activeSelf)
                {
                    go.transform.position = playerPos;
                    go.transform.rotation = playerRot;
                    go.SetActive(true);
                    var item = BulletItemComponent(go,WeaponType);
                    item.Fire(target.position);
                    _timer = 0f;
                    return;
                }
            }
            await ResourceLoadManager.Instance.LoadAssetasync<GameObject>(strWeaponType, (result) =>
            {
                var go = Instantiate(result, playerPos, playerRot, transform);
                var item = BulletItemComponent(go,WeaponType);
                item.Bullet = this;
                item.Fire(target.position);
                _timer = 0f;
                _pool.Add(go);
            });
        }
    }

    BulletItemBase BulletItemComponent(GameObject go,WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.GunBullet: 
                return go.GetComponent<GunBullet>();
                break;
            case WeaponType.FireBullet:
                return go.GetComponent<FireBullet>();
            default:
                return null;
        }
    }
    
}
