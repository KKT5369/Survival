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

    protected override void WeaponAction()
    {
        _timer += Time.deltaTime;
        target = GameManager.Instance.playerController.scanner.nearestTarget;
        if (!target)
            return;
        Transform playerTf = GameManager.Instance.playerController.transform;
        Vector3 playerPos = playerTf.position + playerTf.up * 1.5f;
        Quaternion playerRot = playerTf.rotation;
        
        if (_timer > Count)
        {
            foreach (var go in _pool)
            {
                if (!go.activeSelf)
                {
                    go.transform.position = playerPos;
                    go.transform.rotation = playerRot;
                    go.SetActive(true);
                    var item = go.GetComponent<BulletItemBase>();
                    item.Fire(target.position);
                    _timer = 0f;
                    return;
                }
            }
            var bulletItemgo = Instantiate(asset, playerPos, playerRot, transform);
            var bulletItem = bulletItemgo.GetComponent<BulletItemBase>();
            bulletItem.Bullet = this;
            bulletItem.Fire(target.position);
            _timer = 0f;
            _pool.Add(bulletItemgo);
        }
    }
}
