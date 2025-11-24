using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnOperateAction;
    public event EventHandler OnPauseAction;

    private GameControl gameControl;

    private void Awake()
    {
        // 单例保护（防止重复）
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gameControl = new GameControl();
        gameControl.Player.Enable();

        gameControl.Player.Interact.performed += Interact_Performed;
        gameControl.Player.Operate.performed += Operate_Performed;
        gameControl.Player.Pause.performed += Pause_Performed;
    }

    private void OnDestroy()
    {
        if (gameControl != null)
        {
            gameControl.Player.Interact.performed -= Interact_Performed;
            gameControl.Player.Operate.performed -= Operate_Performed;
            gameControl.Player.Pause.performed -= Pause_Performed;
        }
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Operate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = gameControl.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(inputVector2.x, 0f, inputVector2.y);
        return direction.normalized;
    }
}