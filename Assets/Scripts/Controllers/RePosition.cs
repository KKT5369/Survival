using System;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        print("트리거 체크");
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
                print("체크");
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
                
                break;
            
        }


    }
}
