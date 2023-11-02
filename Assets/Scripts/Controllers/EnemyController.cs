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
    private int _maxHealth;
    private bool _isLive; 
    
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer spriter;
    [SerializeField] private Animator anim;
    private WaitForFixedUpdate _wait;

    private void Start()
    {
        _target = GameManager.Instance.playerController.GetComponent<Rigidbody2D>();
        _speed = SpawnData.speed;
        _health = SpawnData.health;
        _maxHealth = _health;
        _wait = new WaitForFixedUpdate();
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Weapon"))
            return;
        
        _health -= col.GetComponentInParent<WeaponBase>().Damage;
        anim.SetTrigger("Hit");
        if (_isLive)
        {
            StartCoroutine(nameof(KnockBack));
        }
        
        if (_health > 0 )
        {
        }
        else
        {
            Dead();
        }
    }

    IEnumerator KnockBack()
    {
        yield return _wait;
        Vector3 playerPos = GameManager.Instance.playerController.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        _isLive = false;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _isLive = true;
        _health = _maxHealth;
    }
}
