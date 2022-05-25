using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    private Renderer render;
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        render.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
