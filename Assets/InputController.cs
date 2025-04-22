using UnityEngine;using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, DefaultInputActions.IPlayerActions
{
    private Vector2 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        transform.Translate(  move * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            move = context.ReadValue<Vector2>();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }
}
