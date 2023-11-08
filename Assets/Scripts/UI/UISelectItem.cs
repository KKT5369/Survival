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
            Instantiate(selectItem, selectArea).SetActive(true);
        }
        
    }
}
