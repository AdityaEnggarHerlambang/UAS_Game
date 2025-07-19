using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🏁 Level selesai! Kembali ke Main Menu.");

            // ✅ Simpan bahwa Level 1 sudah selesai
            PlayerPrefs.SetInt("Level1Complete", 1);
            PlayerPrefs.Save();

            // ✅ Kembali ke Main Menu
            SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene Main Menu kamu
        }
    }
}
