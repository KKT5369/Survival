using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Random = UnityEngine.Random;

public class PoolManager : SingleTon<PoolManager>
{
    private GameObject _enemyPrefab;
    private GameObject _monsterContents;
    private Dictionary<string, List<GameObject>> _pools;
    public List<string> _acMonster = new();
    private List<AnimatorOverrideController> _animControllers = new();
    
    List<GameObject> enemyList;
    
    void Awake()
    {
        _monsterContents = new GameObject("MONSTERCONTENTS");
    }

    public async void Init()
    {
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>("Enemy", (result) =>
        {
            _enemyPrefab = result;
        });

        _acMonster = await ResourceLoadManager.Instance.GetLabelToAddressName(Const.Stage1Monster);
        _pools = new Dictionary<string, List<GameObject>>();
        for (int i = 0; i < _acMonster.Count; i++)
        {
            _pools[_acMonster[i]] = new List<GameObject>();
            await ResourceLoadManager.Instance.LoadAssetasync<AnimatorOverrideController>(_acMonster[i], (result) =>
            {
                _animControllers.Add(result);
            });
        }
    }
    
    public void MonsterSpawn(SpawnData spawnData,Vector3 spawnPos)
    {
        var level = spawnData.spriteType;

        var enemyName = _acMonster[level];

        if (_pools.TryGetValue(_acMonster[level], out enemyList))
        {
            foreach (var v in enemyList)
            {
                if (!v.activeSelf)
                {
                    v.transform.position = spawnPos;
                    v.SetActive(true);
                    return;
                }
            }
            
            var enemyGo = Instantiate(_enemyPrefab, spawnPos,quaternion.identity,_monsterContents.transform);
            var enemyAnim = enemyGo.GetComponent<Animator>();
            enemyAnim.runtimeAnimatorController = _animControllers[level];
            enemyGo.name = enemyName;
            enemyGo.GetComponent<EnemyController>().SpawnData = spawnData;
            enemyList.Add(enemyGo);
        }
    }
}
