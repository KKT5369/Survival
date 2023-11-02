using UnityEngine;


public abstract class BulletItemBase : MonoBehaviour
{
    protected int _per;
    public Bullet Bullet { get; set; }
    public Rigidbody2D rigid;

    public abstract void Fire(Vector3 targerPos);
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy"))
            return;

        _per--;

        if (_per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        rigid.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
