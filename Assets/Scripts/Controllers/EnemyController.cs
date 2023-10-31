using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SpawnData SpawnData { private get; set; }
    private Rigidbody2D _target;
    private float _speed;
    private int _health;
    
    
    private bool _isLive; 
    
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer spriter;

    private void Start()
    {
        _target = GameManager.Instance.playerController.GetComponent<Rigidbody2D>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
            return;

        if (_health > 0 )
        {
            _health -= other.GetComponent<Bullet>().damage;
        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        _isLive = false;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _isLive = true;
        _speed = SpawnData.speed;
        _health = SpawnData.health;
    }
}
