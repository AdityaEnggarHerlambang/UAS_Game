using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckpointButton : MonoBehaviour
{
    [Header("Checkpoint Type")]
    public bool isLiftCheckpoint = false;

    [Header("Barrier Settings")]
    public GameObject barrier;
    public GameObject barrier2;

    [Header("Checkpoint Settings")]
    public Transform checkpointLocation;

    [Header("UI Soal (Kosongkan jika lift checkpoint - Level 1 saja)")]
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public Button answerA, answerB, answerC;

    private PlayerRespawn playerRespawn;

    [Header("Lift Settings (Untuk Lift Checkpoint Saja - Level 2)")]
    public GameObject liftObject;
    private PlatformLift platformLift;
    private Vector3 originalLiftPosition;

    private void Start()
    {
        if (questionPanel != null)
            questionPanel.SetActive(false);

        // 🔄 Berlaku untuk semua checkpoint yang ada soal
        if (questionPanel != null)
        {
            answerA.onClick.AddListener(() => Answer("A"));
            answerB.onClick.AddListener(() => Answer("B"));
            answerC.onClick.AddListener(() => Answer("C"));
        }

        if (isLiftCheckpoint && liftObject != null)
        {
            originalLiftPosition = liftObject.transform.position;
            platformLift = liftObject.GetComponent<PlatformLift>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🟢 Checkpoint triggered by Player");

            playerRespawn = other.GetComponent<PlayerRespawn>();

            if (playerRespawn != null && checkpointLocation != null)
            {
                Debug.Log("📝 Checkpoint location: " + checkpointLocation.position);
                playerRespawn.SetCheckpoint(checkpointLocation.position);

                // Jika ada soal, tampilkan dulu → tunda aktivasi lift
                if (questionPanel != null)
                {
                    ShowQuestion();
                    return;
                }

                // Jika lift tanpa soal
                if (isLiftCheckpoint && platformLift != null)
                {
                    Debug.Log("🔗 Linking lift to PlayerRespawn");
                    playerRespawn.LinkLift(platformLift);
                }
            }
        }
    }

    void ShowQuestion()
    {
        if (questionPanel != null)
        {
            questionPanel.SetActive(true);

            // 🔸 Soal berbeda untuk Level 2 (Lift)
            if (isLiftCheckpoint)
            {
                questionText.text = "Teks yang bertujuan untuk menceritakan pengalaman pribadi disebut...";

                answerA.GetComponentInChildren<TextMeshProUGUI>().text = "A. Teks eksplanasi";
                answerB.GetComponentInChildren<TextMeshProUGUI>().text = "B. Teks narasi";
                answerC.GetComponentInChildren<TextMeshProUGUI>().text = "C. Teks prosedur";
            }
            else
            {
                questionText.text = "Bahasa pemrograman yang paling umum digunakan untuk membuat game di Unity adalah...";

                answerA.GetComponentInChildren<TextMeshProUGUI>().text = "A. Python";
                answerB.GetComponentInChildren<TextMeshProUGUI>().text = "B. Java";
                answerC.GetComponentInChildren<TextMeshProUGUI>().text = "C. C#";
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Answer(string selected)
    {
        // 🔎 Cek jawaban benar untuk masing-masing level
        bool isCorrect = false;

        if (isLiftCheckpoint)
            isCorrect = (selected == "B"); // ✅ B untuk soal Level 2
        else
            isCorrect = (selected == "C"); // ✅ C untuk soal Level 1

        if (isCorrect)
        {
            Debug.Log("✅ Jawaban benar! Barrier terbuka.");
            questionPanel.SetActive(false);

            if (barrier != null) barrier.SetActive(false);
            if (barrier2 != null) barrier2.SetActive(false);

            // 🔗 Aktifkan lift checkpoint setelah menjawab benar
            if (isLiftCheckpoint && playerRespawn != null && platformLift != null)
            {
                Debug.Log("🔗 Linking lift ke player setelah jawaban benar");
                playerRespawn.LinkLift(platformLift);
            }
        }
        else
        {
            Debug.Log("❌ Jawaban salah! Respawn ke checkpoint.");
            questionPanel.SetActive(false);

            if (playerRespawn != null)
            {
                playerRespawn.RespawnToCheckpointOnly();

                if (isLiftCheckpoint && liftObject != null)
                    ResetLift();
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ResetLift()
    {
        liftObject.transform.position = originalLiftPosition;

        if (platformLift != null)
            platformLift.ResetLift();
    }
}
