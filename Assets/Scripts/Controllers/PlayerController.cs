using System;
using Data;
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

    public void SetWeapon<T>(WeaponType weaponType) where T : WeaponBase
    {
        var go = weapon.GetComponentInChildren<T>(gameObject);
        
        // 무기 정보를 어디서 가져 오는게 좋을까? 
        var info = new WeaponInfo(weaponType,0, 10, -1, 2, 150f);
        if (!go)
        {
            info.weaponType = weaponType;
            var weaponGo = new GameObject($"{weaponType.ToString()}_Item");
            var script = weaponGo.AddComponent<T>();
            script.LevelUp(info);
            weaponGo.transform.parent = weapon;
        }
        else
        {
            go.GetComponent<T>().LevelUp(info);
        }
    }
    
}
