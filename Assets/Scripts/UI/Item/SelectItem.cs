using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Image imgItem;
    [SerializeField] private TMP_Text txtDesc;
    
    public void Init(ItemData itemData,Action callback)
    {
        imgItem.sprite = itemData.itemIcon;
        txtDesc.text = itemData.itemDesc;
        btn.onClick.AddListener((() =>
        {
            callback?.Invoke();
            Time.timeScale = 1;
            UIManager.Instance.CloseUI<UISelectItem>();
        }));
    }
}
