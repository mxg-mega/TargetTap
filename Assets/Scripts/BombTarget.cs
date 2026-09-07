using UnityEngine;

public class BombTarget : Target
{
    private Camera cachedCamera;
    private float screenBottomY;

    protected override void Awake()
    {
        ScoreValue = -1;
        
        base.Awake();
        cachedCamera = Camera.main;
        
        // Cache the world coordinate equivalent of the bottom of the screen once
        if (cachedCamera != null)
        {
            screenBottomY = cachedCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        }
    }
    public override void OnSpawnTarget(int scoreValue)
    {
        base.OnSpawnTarget(ScoreValue);
    }

}
