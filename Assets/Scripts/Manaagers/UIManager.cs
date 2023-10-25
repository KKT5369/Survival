using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
    private Dictionary<string, GameObject> UI = new();
    GameObject _uiGo;
    private Transform _uiContents;
    
    // UI를 찾아서 있으면 활성화 없으면 생성후 저장 합니다.
    public T OpenUI<T>()
    {
        if (UI.TryGetValue(typeof(T).Name, out _uiGo))
        {
            _uiGo.SetActive(true);
            return _uiGo.GetComponent<T>();
        }
        else
        {
            if (!_uiContents)
            {
                _uiContents = new GameObject(">>> UICONTENTS <<<").transform;
            }
            var go = Resources.Load<GameObject>($"UI/{typeof(T).Name}");
            var uiGo = Instantiate(go,_uiContents.transform);
            UI.Add(typeof(T).Name,uiGo);
            return uiGo.GetComponent<T>();
        }
    }

    // UI를 찾아서 있으면 비활성화 합니다.
    public void CloseUI<T>()
    {
        if (UI.TryGetValue(typeof(T).Name, out _uiGo))
        {
            _uiGo.SetActive(false);
        }
    }
    
    // UI GameObject를 반환 해줍니다.
    public GameObject GetUI<T>()
    {
        if (UI.TryGetValue(typeof(T).Name, out _uiGo))
        {
            return _uiGo;
        }

        return null;
    }
    
    // 모든 UI를 삭제 합니다.
    public void ClearUI()
    {
        if (UI == null) return;
        
        foreach (var pair in UI)
        {
            Destroy(pair.Value);
        }
        UI.Clear();
    }
}
