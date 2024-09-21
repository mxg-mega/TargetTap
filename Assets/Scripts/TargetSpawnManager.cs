using System.Collections;
using UnityEngine;

public class TargetSpawnManager : MonoBehaviour
{

    [SerializeField] public int initialSpawnCount = 10;
    public Vector2 instantiationBoundary = Vector2.zero;
    private TargetPoolerManager pooler;
    private Vector2 lastSpawnPosition = Vector2.zero;
    [SerializeField] private float waitDuration = 0.7f;
    [SerializeField] private float rateOfSpawnBomb = 0.05f;
    [SerializeField] private float rateOfSpawnMovingTarget = 0.3f;
    [SerializeField] private float movingTargetSpeed = 7.0f;
    [SerializeField] private float xBounds;
    [SerializeField] private float yUpperBound;

    private GameObject spawnedTarget;

    public const string normalTargets = "Normal Target";
    public const string movingTargets = "Moving Target";
    public const string bombTargets = "Bomb Target";
    public const string timeTargets = "Time Target";


    void Awake()
    {
        StartCoroutine(DelayedSpawning());
        StartCoroutine(SpawnTimeTarget());
    }

    IEnumerator DelayedSpawning()
    {
        
        while (TargetPoolerManager.Instance == null)
        {
            yield return null;
        }

        pooler = TargetPoolerManager.Instance;

        while (true)
        {
            lastSpawnPosition = RandomLocation();
            SpawnTarget();

            yield return new WaitForSeconds(waitDuration);
        }
    }
    private void SpawnTarget()
    {
        float chanceOfSpawn = Random.value;

        if (chanceOfSpawn < rateOfSpawnBomb)
        {

            spawnedTarget = pooler.GetPooledTarget(bombTargets, lastSpawnPosition);
        }
        else
        {
            if (chanceOfSpawn < rateOfSpawnMovingTarget)
            {
                spawnedTarget = pooler.GetPooledTarget(movingTargets, lastSpawnPosition);
                if (spawnedTarget != null)
                    spawnedTarget.GetComponent<MovingTarget>().SetSpeed(movingTargetSpeed);
            }
            else
            {
                spawnedTarget = pooler.GetPooledTarget(normalTargets, lastSpawnPosition);
            }
        }
    }

    private IEnumerator SpawnTimeTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 7));
            lastSpawnPosition = RandomLocation();
            spawnedTarget = pooler.GetPooledTarget(timeTargets, lastSpawnPosition);
        }
        /*while (true)
        {
            yield return new WaitForSeconds(5.0f);
            float currentWairduration = waitDuration;
            float minWairduration = 0.3f;
            waitDuration = waitDuration - 0.1f;
            if (waitDuration > minWairduration)
            {
                waitDuration -= 0.5f;
                Debug.Log("updated Wait duration: " + waitDuration.ToString());
                switch (spawnedTarget.tag)
                {
                    case normalTargets:
                        if (spawnedTarget.GetComponent<Target>().Duration > 2)
                        {
                            spawnedTarget.GetComponent<Target>().Duration -= 2;
                            Debug.Log("current Target Duration: " + spawnedTarget.GetComponent<Target>().Duration);
                        }
                        break;

                    case movingTargets:
                        if (spawnedTarget.GetComponent<MovingTarget>().Duration > 2)
                        {
                            spawnedTarget.GetComponent<MovingTarget>().Duration -= 2;
                            Debug.Log("current Target Duration: " + spawnedTarget.GetComponent<MovingTarget>().Duration);
                        }
                        break;

                    case bombTargets:
                        if (spawnedTarget.GetComponent<BombTarget>().Duration > 2)
                        {
                            spawnedTarget.GetComponent<BombTarget>().Duration -= 2;
                            Debug.Log("current Target Duration: " + spawnedTarget.GetComponent<BombTarget>().Duration);
                        }
                        break;
                }

            }

        }*/
    }

    bool IsPositionFree(Vector2 position, float radius)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, radius);
        return hit == null;
    }

    private Vector2 RandomLocation()
    {
        Camera cam = Camera.main;
        float worldHeight = 2f * cam.orthographicSize;
        float worldWidth = worldHeight * cam.aspect;
        Vector2 newPosition;
        float xPos, yPos, attempt = 0;
        do
        {
            xPos = Random.Range(-worldWidth / 2 + instantiationBoundary.x,
                worldWidth / 2 - instantiationBoundary.x);
            yPos = Random.Range(-worldHeight / 2 + (instantiationBoundary.y / 2),
                worldHeight / 2 - instantiationBoundary.y); 
            newPosition = new Vector2(xPos, yPos);
            attempt++;
        } while (!IsPositionFree(newPosition, 1.0f) && attempt < 10);
        
        return newPosition;
    }
}
