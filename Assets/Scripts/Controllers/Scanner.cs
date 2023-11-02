using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private float _scanRange = 7;
    [SerializeField] private LayerMask targetLayer;
    private RaycastHit2D[] _targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        _targets = Physics2D.CircleCastAll(transform.position, _scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100f;

        foreach (var v in _targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = v.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = v.transform;
            }
        }
        
        return result;
    }
}
