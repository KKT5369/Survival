using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer srAvatar;
    [SerializeField] private Transform weapon;
    public Scanner scanner;

    private Dictionary<string, GameObject> _weaponList = new();

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

    public void SetWeapon<T>(ItemData data) where T : WeaponBase
    {
        string strWeaponName = data.itemName;
        GameObject go;
        if (_weaponList.TryGetValue(strWeaponName,out go))
        {
            go.GetComponent<T>().LevelUp(data);
            return;
        }
        
        var weaponGo = new GameObject($"{strWeaponName}_Item");
        var script = weaponGo.AddComponent<T>();
        script.LevelUp(data);
        weaponGo.transform.parent = weapon;
        _weaponList.Add(strWeaponName,weaponGo);
    }
}
