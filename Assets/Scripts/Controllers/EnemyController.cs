using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SpawnData SpawnData { private get; set; }
    private Rigidbody2D _target;
    private float _speed;

    private bool _isLive; 
    
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer spriter;

    private void Start()
    {
        _target = GameManager.Instance.playerController.GetComponent<Rigidbody2D>();
        _speed = SpawnData.speed;
        print($"{gameObject.name} HP { SpawnData.health } 생성!!");
    }

    private void FixedUpdate()
    {
        Vector2 dirVec = _target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * _speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spriter.flipX = _target.position.x < rigid.position.x;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
