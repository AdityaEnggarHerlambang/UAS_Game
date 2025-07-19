using UnityEngine;

public class PlatformLift : MonoBehaviour
{
    public float targetHeight = 5f;
    public float speed = 2f;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isMoving = false;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.up * targetHeight;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isMoving = true;
        }

        if (isMoving && transform.position.y < endPos.y)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }

    // Agar player ikut terbawa naik
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.SetParent(transform);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.SetParent(null);
    }

    // ✅ Dipanggil oleh checkpoint saat respawn (Level 2)
    public void ResetLift()
    {
        transform.position = startPos;
        isMoving = false;
    }

}
