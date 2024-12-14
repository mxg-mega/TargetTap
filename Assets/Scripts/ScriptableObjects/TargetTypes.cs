using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    Normal,
    Moving,
    TimeFreeze,
    Enemy,
}

[CreateAssetMenu(menuName ="TargetType")]
public class TargetTypes : ScriptableObject
{
    public TargetType type;
    public float duration = 0f;
    public int scoreValue;
    public bool addsTime;
    public int addedTime;

    public void OnSpawn()
    {

    }

    public void OnTapped()
    {

    }
}
