using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float _speed = 2;
    private Rigidbody2D _target;

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
}
