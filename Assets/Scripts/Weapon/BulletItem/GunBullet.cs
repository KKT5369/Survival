using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : BulletItemBase
{
    

    public override void Fire(Vector3 targetPos)
    {
        _per = Bullet.Per;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        if (_per > -1)
        {
            rigid.velocity = dir * Bullet.Speed;
        }
    }

    
}
