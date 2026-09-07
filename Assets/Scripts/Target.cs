using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IPooledTarget
{
    public float elapsedTime = 0.0f;
    public float Duration { get; set; } = 1.0f;
    public int ScoreValue { get; set; } = 1;

    [SerializeField] protected Animator animator;
    
    // Cache animation hashes for high performance (Zero string lookup overhead)
    protected static readonly int ShrinkTrigger = Animator.StringToHash("Shrink");
    protected static readonly int IdleTrigger = Animator.StringToHash("Idle");

    private bool isDying = false;

    protected virtual void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    public virtual void OnSpawnTarget(int scoreValue = 1)
    {
        ScoreValue = scoreValue;
        elapsedTime = 0.0f;
        isDying = false;
        
        if (animator != null) animator.SetTrigger(IdleTrigger);
    }

    protected virtual void Update()
    {
        if (isDying) return;

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= Duration)
        {
            DespawnTarget();
        }
    }

    public virtual void OnTapped()
    {
        if (isDying) return; // Prevent double taps
        
        ScoreManager.Instance.AddScore(ScoreValue);
        DespawnTarget();
    }

    protected void DespawnTarget()
    {
        isDying = true;
        
        if (animator != null)
        {
            animator.SetTrigger(ShrinkTrigger);
            // Let the animation finish before disabling. 
            // Better alternative: Handle the return to pool using an Animation Event at the end of the clip!
            StartCoroutine(WaitAndPool());
        }
        else
        {
            TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
        }
    }

    private IEnumerator WaitAndPool()
    {
        // Simple unallocated wait based on time, avoiding creating fresh garbage objects
        yield return new WaitForSeconds(0.15f); 
        TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
    }
}