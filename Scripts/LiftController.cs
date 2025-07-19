using UnityEngine;

public class LiftController : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 topPos;
    public float speed = 2f;

    private bool goingUp = false;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (goingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, topPos, speed * Time.deltaTime);
        }
    }

    public void StartLift()
    {
        goingUp = true;
    }

    public void ResetLift()
    {
        transform.position = startPos;
        goingUp = false;
        Debug.Log("?? Lift reset ke posisi bawah");
    }
}
