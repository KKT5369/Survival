using System.Collections;
using System.Collections.Generic;
using Data;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoolManager : SingleTon<PoolManager>
{
    private GameObject _monsterContents;
    private List<GameObject>[] _pools;

    private List<string> _monsterNames;
    void Awake()
    {
        _monsterContents = new GameObject("MONSTERCONTENTS");
    }

    public async void Init()
    {
        _monsterNames = await ResourceLoadManager.Instance.GetLabelToAddressName("Stage1_Monster");
    }
    
    public async void MonsterSpawn()
    {
        await ResourceLoadManager.Instance.LoadAssetasync<GameObject>("Enemy",async (result) =>
        {
            var enemyGo = Instantiate(result, new Vector3(3, 3, 0),quaternion.identity,_monsterContents.transform);
            var enemyAnim = enemyGo.GetComponent<Animator>();

            var enemy = _monsterNames[Random.Range(0, _monsterNames.Count)];
            await ResourceLoadManager.Instance.LoadAssetasync<AnimatorOverrideController>(enemy,(ac) =>
            {
                enemyAnim.runtimeAnimatorController = ac;
            });

            enemyGo.name = enemy;
            
        });
    }
}
