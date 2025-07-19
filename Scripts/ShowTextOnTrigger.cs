using UnityEngine;

public class ShowTextOnTrigger : MonoBehaviour
{
    public GameObject textObject;

    private void Start()
    {
        if (textObject != null)
            textObject.SetActive(false); // Nonaktifkan saat start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textObject != null)
            textObject.SetActive(true); // Munculkan teks saat masuk trigger
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && textObject != null)
            textObject.SetActive(false); // Sembunyikan teks saat keluar trigger
    }
}
