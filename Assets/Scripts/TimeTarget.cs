using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTarget : Target
{
    private Camera cachedCamera;
    private float screenBottomY;

    protected override void Awake()
    {
        ScoreValue = 0;
        base.Awake();
        cachedCamera = Camera.main;
        
        // Cache the world coordinate equivalent of the bottom of the screen once
        if (cachedCamera != null)
        {
            screenBottomY = cachedCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        }
    }

    public override void OnTapped()
    {
        float TimeIncreaseAmount = Random.Range(3.0f, 5.0f);  // Amount of time to add
        // Increase game time
        GameManager.Instance.IncreaseGameTime(TimeIncreaseAmount);

        // Call base method to handle the rest (like adding score and returning to the pool)
        base.OnTapped();
    }
}
