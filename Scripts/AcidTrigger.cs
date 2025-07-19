using UnityEngine;

public class AcidTrigger : MonoBehaviour
{
    public GameObject restartButton;       // Tombol restart (jika diperlukan)
    public bool respawnToCheckpoint = false; // Centang ini di Inspector untuk lava mode

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (respawnToCheckpoint)
            {
                // Respawn langsung ke checkpoint (mode lava)
                PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
                if (respawn != null)
                {
                    respawn.RespawnToCheckpointOnly();
                }
            }
            else
            {
                // Tampilkan tombol restart (mode acid atau lainnya)
                if (restartButton != null)
                {
                    restartButton.SetActive(true);
                }

                // Bebaskan kursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Nonaktifkan player agar tidak bisa bergerak
                other.gameObject.SetActive(false);
            }
        }
    }
}
