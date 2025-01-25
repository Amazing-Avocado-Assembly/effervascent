using UnityEngine;


public class Cog : MonoBehaviour
{
    public enum State {
        FALLING,
        START,
        BUBBLE,
        FINISH
    }

    public State state = State.FALLING;

    private float rotationSpeed = 0;

    public void Update() {
        if (state == State.START) {
            rotationSpeed+= 1f;
            rotationSpeed = Mathf.Min(rotationSpeed, 10 * 360f);
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        } else if (state == State.BUBBLE) {
            rotationSpeed *= 0.99f;
            if (rotationSpeed < 1f) {
                rotationSpeed = 0;
            }
            transform.Rotate(0, 0, rotationSpeed);
        }
    }
}
