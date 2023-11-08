using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo",menuName = "Scriptble Object/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("# Player Default Info")]
    public WeaponType weaponType;
    public int health;
    public int maxHealth;
    public int level;
    public int kill;
    public int exp;
    public float[] nextExp;
    public GameObject playerPre;

}
