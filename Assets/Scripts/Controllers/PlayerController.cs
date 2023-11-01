using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer srAvatar;
    [SerializeField] private Transform weapon;
    
    public Vector3 inputVec;
    
    private void Start()
    {
        gameObject.SetActive(false);
        SetAnim();
    }

    private void Update()
    {
        transform.position += inputVec * 5f * Time.deltaTime;
        anim.SetFloat("Speed", inputVec.magnitude);
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
        inputVec = value.ReadValue<Vector3>();

        if(inputVec.magnitude == 0) return;
        
        srAvatar.flipX = inputVec.x < 0;
    }

    public void SetWeapon<T>() where T : WeaponBase
    {
        var go = weapon.GetComponentInChildren<T>(gameObject);
        if (!go)
        {
            var weaponGo = new GameObject($"{typeof(T).Name}_Item");
            var script = weaponGo.AddComponent<T>();
            script.parent = weaponGo.transform;
            script.LevelUp();
            weaponGo.transform.parent = weapon;
        }
        else
        {
            print($"생성된 아이텐  >> {go.name}"); 
        }
    }
    
}
