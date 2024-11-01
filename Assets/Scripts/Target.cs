using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour, IPooledTarget
{

    public float elaspedTime = 0.0f;
    public float Duration { get; set; }
    public int ScoreValue { get; set; }
    private Animator animator;
    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip shrink;


    private void Awake()
    {
        ScoreValue = 1;
        
        if (gameObject.GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
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

        animator.Play(idle.name);
        // Wait for the target to be active for 'Duration' seconds
        yield return new WaitForSeconds(Duration);

        // Play shrinking animation
        StartCoroutine(PlayAnim(shrink));
        // Return the target to the object pool
        TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
    }

    public virtual void OnTapped()
    {
        ScoreManager.Instance.AddScore(ScoreValue);
        StartCoroutine(PlayAnim(shrink));
        TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
    }

    private IEnumerator PlayAnim(AnimationClip clip)
    {
        animator.Play(clip.name);
        yield return new WaitForSeconds(clip.length);
    }
}
