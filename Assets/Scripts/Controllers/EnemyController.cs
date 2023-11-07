using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SpawnData SpawnData { private get; set; }
    private Rigidbody2D _target;
    private float _speed;
    private float _health;
    private float _maxHealth;
    private bool _isLive;

    [SerializeField] private CapsuleCollider2D coll;
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
        if (!_isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        
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
        if (!col.CompareTag("Weapon") || !_isLive)
            return;
        
        _health -= col.GetComponentInParent<WeaponBase>().Damage;
        StartCoroutine(nameof(KnockBack));
        
        if (_health > 0 )
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            _isLive = false;
            spriter.sortingOrder = 1;
            coll.enabled = false;
            rigid.simulated = false;
            anim.SetBool("Dead",true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
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
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _isLive = true;
        spriter.sortingOrder = 2;
        coll.enabled = true;
        rigid.simulated = true;
        anim.SetBool("Dead",false);
        _health = _maxHealth;
    }
}
