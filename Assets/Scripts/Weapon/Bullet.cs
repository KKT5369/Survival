using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : WeaponBase
{
    private Transform _targer;
    private List<GameObject> _pool = new();
    private float _timer;

    private void FixedUpdate()
    {
        WeaponAction();
    }

    protected override async void WeaponAction()
    {
        string strWeaponType = Convert.ToString(WeaponType);
        _timer += Time.deltaTime;
        
        if (_timer > 2f)
        {
            foreach (var go in _pool)
            {
                if (!go.activeSelf)
                {
                    go.transform.position = transform.position;
                    _timer = 0f;
                    return;
                }
            }
            await ResourceLoadManager.Instance.LoadAssetasync<GameObject>(strWeaponType, (result) =>
            {
                var go = Instantiate(result, transform);
                _pool.Add(go);
                _timer = 0f;
            });
        }
    }
}
