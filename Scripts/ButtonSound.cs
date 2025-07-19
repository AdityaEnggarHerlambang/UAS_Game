using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource sharedAudioSource;
    public AudioClip buttonClip;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (sharedAudioSource != null && buttonClip != null)
        {
            sharedAudioSource.PlayOneShot(buttonClip);
        }
        else
        {
            Debug.LogWarning("AudioSource atau Clip belum diatur!");
        }
    }
}
