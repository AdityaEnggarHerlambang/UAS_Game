
using UnityEngine;
public class LavaScroll : MonoBehaviour {
    public float scrollX = 0.1f;
    public float scrollY = 0.1f;

    void Update() {
        Renderer rend = GetComponent<Renderer>();
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
