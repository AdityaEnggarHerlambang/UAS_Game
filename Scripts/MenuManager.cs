using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // <-- Tambahkan ini

public class MenuManager : MonoBehaviour
{
    public Button level2Button;
    public AudioSource buttonSound; // <- Tambahan: AudioSource untuk klik tombol

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        bool isLevel1Complete = PlayerPrefs.GetInt("Level1Complete", 0) == 1;

        if (level2Button != null)
        {
            level2Button.interactable = isLevel1Complete;

            if (!isLevel1Complete)
                level2Button.GetComponentInChildren<Text>().text = "Level 2 (Locked)";
        }
    }

    // Fungsi Load dengan suara dan delay:
    public void LoadLevel1()
    {
        PlayClickAndLoad("Level1");
    }

    public void LoadLevel2()
    {
        PlayClickAndLoad("Level2");
    }

    public void LoadAboutScene()
    {
        PlayClickAndLoad("AboutScene");
    }

    public void LoadTeamScene()
    {
        PlayClickAndLoad("TeamScene");
    }

    public void LoadMainMenu()
    {
        PlayClickAndLoad("MainMenu");
    }

    public void QuitGame()
    {
        buttonSound.Play(); // suara tetap diputar sebelum quit
        Application.Quit();
    }

    // Tambahan method: memutar suara lalu delay sebelum LoadScene
    private void PlayClickAndLoad(string sceneName)
    {
        buttonSound.Play();
        StartCoroutine(DelaySceneLoad(sceneName));
    }

    private IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(0.25f); // delay agar suara terdengar
        SceneManager.LoadScene(sceneName);
    }
}
