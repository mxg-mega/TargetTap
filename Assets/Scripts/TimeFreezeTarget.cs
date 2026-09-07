using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeTarget : Target
{
    private Camera cachedCamera;

    private float screenBottomY;

    protected override void Awake()
    {
        base.Awake();
        cachedCamera = Camera.main;
        
        // Cache the world coordinate equivalent of the bottom of the screen once
        if (cachedCamera != null)
        {
            screenBottomY = cachedCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        }
        ScoreValue = 0;
    }
}
