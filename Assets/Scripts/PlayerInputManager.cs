using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputManager Instance { get; private set; }

    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {

        // Explicitly Enabling the Enhanced Touch Support
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        // playerInputs?.Player.Disable();
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                TriggerRayCast(touch.screenPosition);
            }
        }

        void TriggerRayCast(Vector2 screenPos)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);


            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent<Target>(out var target))
                {
                    target.OnTapped();
                }
                //         switch (hit.collider.tag)
                //         {
                //             case "Normal Target":
                //                 hit.collider.GetComponent<Target>().OnTapped();
                //                 break;
                //             case "Moving Target":
                //                 hit.collider.GetComponent<MovingTarget>().OnTapped();
                //                 break;
                //             case "Bomb Target":
                //                 hit.collider.GetComponent<BombTarget>().OnTapped();
                //                 break;
                //             case "Time Target":
                //                 hit.collider.GetComponent<TimeTarget>().OnTapped();
                //                 break;

                //         }
            }
        }
    }
}
