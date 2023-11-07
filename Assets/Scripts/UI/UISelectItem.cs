using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UISelectItem : MonoBehaviour
{
    [SerializeField] private GameObject selectItem;
    [SerializeField] private Transform selectArea;

    private List<SelectItem> _selectItems = new();

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            var selectGo = Instantiate(selectItem, selectArea);
            selectGo.SetActive(true);
            var data = GameManager.Instance.GetItemData(WeaponType.Shovel.ToString());
            var script = selectGo.GetComponent<SelectItem>();
            script.Init(data,(() =>
            {
                GameManager.Instance.TestCode();
            }));
        }
    }

    private void OnEnable()
    {
        if (_selectItems.Count < 0)
            return;

        var list = (WeaponType[])Enum.GetValues(typeof(WeaponType));

        for (int i = 0; i < _selectItems.Count; i++)
        {
            var data = GameManager.Instance.GetItemData(list[i].ToString());
            _selectItems[i].Init(data,(() =>
            {
                GameManager.Instance.TestCode();
            }));

        }
        
    }
}
