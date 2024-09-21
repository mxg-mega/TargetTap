using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTarget : Target
{
    private void Awake()
    {
        ScoreValue = 0;
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
