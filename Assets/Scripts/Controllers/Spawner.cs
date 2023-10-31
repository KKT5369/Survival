using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private Transform[] _spawnPoint;

    private float _timer;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.2f)
        {
            Vector3 spawanPos = _spawnPoint[Random.Range(0, _spawnPoint.Length)].transform.position;
            PoolManager.Instance.MonsterSpawn(spawanPos);
            _timer = 0;
        }
    }
}
