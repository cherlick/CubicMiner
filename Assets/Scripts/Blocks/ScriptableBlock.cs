using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Scriptable Objects/ Block Data")]
public class ScriptableBlock : ScriptableObject
{
    [Header("Block ID")]
    public string blockName;
    public float health;
    public float coinValue;
    public float scoreValue;
    public Sprite displaySprites;
    public List<Sprite> decaySprites;
    public bool isUnlock;


}
