using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private Transform[] _spawnPoint;
    
    private int _level;
    private float _timer;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _level = Mathf.FloorToInt(GameManager.Instance.gameTime / 10f);
        
        if (_timer > (_level == 0 ? 0.5f : 0.2f))
        {
            Vector3 spawanPos = _spawnPoint[Random.Range(0, _spawnPoint.Length)].transform.position;
            PoolManager.Instance.MonsterSpawn(spawanPos);
            _timer = 0;
        }
    }
}

[Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}