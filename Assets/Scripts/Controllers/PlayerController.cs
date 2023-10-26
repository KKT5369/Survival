using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer srAvatar;
    private Vector3 _pos;
    
    private void Start()
    {
        gameObject.SetActive(false);
        SetAnim();
    }

    private void Update()
    {
        transform.position += _pos * 5f * Time.deltaTime;
        anim.SetFloat("Speed", _pos.magnitude);
    }

    async void SetAnim()
    {
        var avatarRef = PlayerManager.Instance.AcAvatar;
        await ResourceLoadManager.Instance.LoadAssetasync<AnimatorOverrideController>(avatarRef, (result) =>
        {
            anim.runtimeAnimatorController = result;
        });
        gameObject.SetActive(true);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        _pos = value.ReadValue<Vector3>();

        if(_pos.magnitude == 0) return;
        
        srAvatar.flipX = _pos.x < 0;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
