using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Image imgItem;
    [SerializeField] private TMP_Text txtDesc;

    private ItemData _itemData;
    
    private void OnEnable()
    {
        _itemData = WeaponManager.Instance.GetRandomItemData();
        imgItem.sprite = _itemData.itemIcon;
        txtDesc.text = _itemData.itemDesc;
    }

    private void Awake()
    {
        btn.onClick.AddListener((() =>
        {
            Time.timeScale = 1;
            UIManager.Instance.CloseUI<UISelectItem>();
        }));
    }
}
