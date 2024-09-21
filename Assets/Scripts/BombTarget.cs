using UnityEngine;

public class BombTarget : Target
{
    private void Awake()
    {
        ScoreValue = -1;
    }
    public override void OnSpawnTarget(int scoreValue)
    {
        base.OnSpawnTarget(ScoreValue);
    }
}
