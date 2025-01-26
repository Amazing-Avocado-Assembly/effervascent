using UnityEngine;
using UnityEngine.InputSystem;

public class MouseToGamepadInputProcessor : InputProcessor<Vector2>
{
    Vector2 lastPosition = Vector2.zero;

    public override Vector2 Process(Vector2 value, InputControl control)
    {
        if (Game.Instance == null || Game.Instance.Player == null) return Vector2.zero;

        value = Input.mousePosition;

        Vector2 position = Camera.main.ScreenToWorldPoint(value) - Game.Instance.Player.transform.position;
        position.Normalize();

        if ((position - lastPosition).magnitude < 0.01f) return Vector2.zero;
        
        lastPosition = position;
        return position;
    }
}