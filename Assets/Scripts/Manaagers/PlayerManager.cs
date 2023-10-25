using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerManager : SingleTon<PlayerManager>
{
    public AssetReference AcAvatar { get; set; }
    public PlayerInfo PlayerInfo;

}
