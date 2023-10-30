using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RePosition : MonoBehaviour
{
    private Collider2D _col;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area"))
            return;
        var playerController = GameManager.Instance.playerController;
        Vector3 playerPos = playerController.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = playerController.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (_col.enabled)
                {
                    transform.Translate(playerDir * 40 + new Vector3(Random.Range(-3f,3f),Random.Range(-3f,3f),0));
                }
                break;
            
        }


    }
}
