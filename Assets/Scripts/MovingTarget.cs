using System.Collections;
using UnityEngine;

public class MovingTarget : Target
{
    [SerializeField] private float speed = 7;

    private void Awake()
    {
        ScoreValue = 2;
        Duration = 50.0f;
    }

    /*private void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x < (Screen.width / 0.1))
        {
            transform.Translate(speed * Time.deltaTime * Vector2.left);
        }
        else if (screenPosition.y < (Screen.height / 0.1))
        {
            transform.Translate(speed * Time.deltaTime * Vector2.up);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector2.up);
        }

        if (CheckOutOfBound())
        {
            TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
        }
    }*/

    public override void OnSpawnTarget(int scoreValue)
    {
        base.OnSpawnTarget(2);
        StartCoroutine(MoveTarget());
    }
    public void SetSpeed(float modifiedSpeed)
    {
        speed = modifiedSpeed;
    }
    IEnumerator MoveTarget()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.down);

            if (CheckOutOfBound())
            {
                TargetPoolerManager.Instance.ReturnPooledTarget(gameObject);
            }
            yield return null;
        }
    }
    
    private bool CheckOutOfBound()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return screenPosition.x > Screen.width || screenPosition.y > Screen.height || screenPosition.x < 0 || screenPosition.y < 0;
    }

}
