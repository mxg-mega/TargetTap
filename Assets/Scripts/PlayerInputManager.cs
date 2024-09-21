using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputManager Instance { get; private set; }

    private PlayerInputs playerInputs;
    private InputAction touchTap;
    private InputAction touchPosition;
    private InputAction mouseTap;
    private InputAction mousePosition;

    public Vector2 LastTappedPosition { get; private set; }
    public bool Tapped { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInputs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeInputs()
    {
        playerInputs = new PlayerInputs();
        touchTap = playerInputs.Player.Touch;
        touchPosition = playerInputs.Player.TouchPosition;
        mouseTap = playerInputs.Player.Mouse;
        mousePosition = playerInputs.Player.MousePosition;
        RegisterActions();
    }

    private void RegisterActions()
    {
        touchTap.started += context => {
            Tapped = true;
            LastTappedPosition = touchPosition.ReadValue<Vector2>();
        };
        touchTap.canceled += context => Tapped = false;

        mouseTap.started += context =>
        {
            Tapped = true;
            LastTappedPosition = mousePosition.ReadValue<Vector2>();
        };
        mouseTap.canceled += context => Tapped = false;
    }

    private void IsTappingObject(Vector2 position)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(position);
        RaycastHit2D hit = Physics2D.CircleCast(worldPosition, 1.0f, Vector2.zero);

        if (hit.collider != null)
        {
            switch (hit.collider.tag)
            {
                case "Normal Target":
                    hit.collider.GetComponent<Target>().OnTapped();
                    break;
                case "Moving Target":
                    hit.collider.GetComponent<MovingTarget>().OnTapped();
                    break;
                case "Bomb Target":
                    hit.collider.GetComponent<BombTarget>().OnTapped();
                    break;
                case "Time Target":
                    hit.collider.GetComponent<TimeTarget>().OnTapped();
                    break;

            }
            
        }
    }
    private void Update()
    {
        if (Tapped)
        {
            IsTappingObject(LastTappedPosition);
            Tapped = false;
        }
    }
    private void OnEnable()
    {
        playerInputs.Player.Enable();
    }

    private void OnDisable()
    {
        if (playerInputs != null)
        {
            playerInputs.Player.Disable();
        }
    }
}
