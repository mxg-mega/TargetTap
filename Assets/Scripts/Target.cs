using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IPooledTarget
{

    public float elaspedTime = 0.0f;
    public float Duration { get; set; }
    public int ScoreValue { get; set; }

    private void Awake()
    {
        ScoreValue = 1;
    }
    public virtual void OnSpawnTarget(int scoreValue = 1)
    {
        ScoreValue = scoreValue;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // Reset elapsed time for new target
        elaspedTime = 0.0f;
        Duration = 2.0f;

        // Wait for the target to be active for 'Duration' seconds
        yield return new WaitForSeconds(Duration);

        // Return the target to the object pool
        TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
    }

    public virtual void OnTapped()
    {
        TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
        ScoreManager.Instance.AddScore(ScoreValue);
    }
}
