using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
        var avatarRef = GameManager.Instance.AcAvatar;
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

    public void SetWeapon(ItemData data)
    {
        string strWeaponName = data.itemName;
        GameObject go;
        if (_weaponList.TryGetValue(strWeaponName,out go))
        {
            go.GetComponent<WeaponBase>().LevelUp(data);
            return;
        }
        
        var weaponGo = new GameObject($"{strWeaponName}_Item");
        weaponGo.transform.parent = weapon;
        var script = WeaponManager.Instance.SetWeaponComponent(weaponGo, data.weaponType);
        script.LevelUp(data);
        _weaponList.Add(strWeaponName,weaponGo);
    }
}
