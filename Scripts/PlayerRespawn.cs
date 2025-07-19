using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastCheckpoint;

    private PlatformLift linkedLift;       // ✅ Lift terkait (jika checkpoint lift)
    private bool checkpointIsLift = false; // ✅ Menandai apakah checkpoint-nya lift

    private void Start()
    {
        lastCheckpoint = transform.position;
        Debug.Log("▶ Start position set as initial checkpoint: " + lastCheckpoint);
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
        checkpointIsLift = false; // ✅ Asumsikan checkpoint biasa
        linkedLift = null;        // ✅ Hapus link lift sebelumnya

        Debug.Log("✅ Checkpoint updated to: " + newCheckpoint);
    }

    // ✅ Dipanggil hanya jika checkpoint adalah lift
    public void LinkLift(PlatformLift lift)
    {
        linkedLift = lift;
        checkpointIsLift = true;
    }

    public void Respawn()
    {
        transform.position = lastCheckpoint;
        gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RespawnToCheckpointOnly()
    {
        Debug.Log("🔁 Respawning to last checkpoint at: " + lastCheckpoint);
        transform.position = lastCheckpoint;

        // ✅ Reset lift hanya jika checkpoint memang checkpoint lift
        if (checkpointIsLift && linkedLift != null)
        {
            Debug.Log("🔄 Resetting lift to start position.");
            linkedLift.ResetLift();
        }
    }

    public void RespawnFromUI()
    {
        Respawn();

        GameObject restartButton = GameObject.Find("RestartButton");
        if (restartButton != null)
            restartButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("🔘 [R] Key pressed. Respawning to checkpoint...");
            RespawnToCheckpointOnly();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("📌 [T] Manual checkpoint saved at: " + transform.position);
            SetCheckpoint(transform.position);
        }
    }
}
