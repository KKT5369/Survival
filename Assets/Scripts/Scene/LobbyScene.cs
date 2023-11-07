using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] private AssetReference acAvatarRef;
    [SerializeField] private AssetReference mapRef;
    
    void Start()
    {
        TestCode();
        PlayerManager.Instance.AcAvatar = acAvatarRef;
        GameManager.Instance.MapReference = mapRef;
        UIManager.Instance.OpenUI<UILobby>();
    }
    // *** 테스트 코드 ***
    void TestCode()
    {

    }
}
