using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        // ✅ Untuk PC / Editor:
        // Kamera boleh diatur oleh script lain (misal MouseLook) atau manual
        // Jadi cukup jaga posisi ke player, rotasi tetap dikendalikan mouse
        transform.position = player.position;

#elif UNITY_ANDROID
        // ✅ Untuk Android:
        // Kamera otomatis menghadap ke depan player
        transform.position = player.position;
        transform.rotation = Quaternion.LookRotation(player.forward, Vector3.up);
#endif
    }
}
