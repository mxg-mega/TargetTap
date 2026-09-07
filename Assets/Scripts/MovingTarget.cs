using UnityEngine;

public class MovingTarget : Target
{
    [SerializeField] private float speed = 7f;
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
    }

    public override void OnSpawnTarget(int scoreValue = 2)
    {
        // Bypass the base timer rule entirely if moving targets only despawn when falling off-screen
        base.OnSpawnTarget(scoreValue);
        Duration = 999f; 
    }

    public void SetSpeed(float modifiedSpeed)
    {
        speed = modifiedSpeed;
    }

    protected override void Update()
    {
        base.Update(); // Keeps the safety timer running if you want it

        // Direct hardware transform step - highly performant
        transform.Translate(speed * Time.deltaTime * Vector2.down);

        // Quick, inexpensive float comparison instead of full WorldToScreen projection loops
        if (transform.position.y < screenBottomY - 1f)
        {
            TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
        }
    }
}