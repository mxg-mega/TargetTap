using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledTarget 
{
    void OnSpawnTarget(int scoreValue = 1);
}
