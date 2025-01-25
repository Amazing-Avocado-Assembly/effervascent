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
}
