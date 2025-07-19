using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 3f;
    private bool isClimbing = false;
    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb = other.GetComponent<Rigidbody>();
            rb.useGravity = false;
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.useGravity = true;
            isClimbing = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isClimbing && other.CompareTag("Player"))
        {
            float vertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(0, vertical * climbSpeed, 0);
        }
    }
}
