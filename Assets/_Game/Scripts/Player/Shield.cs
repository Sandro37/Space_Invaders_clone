using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Tooltip("Layers que podem dar dano no escudo")]
    [SerializeField] private LayerMask layerDamage;
    [SerializeField] private Sprite[] states;
    private int helth;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        helth = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerDamage) != 0)
        {
            IDamage obj = collision.gameObject.GetComponent<IDamage>();
            if (obj != null)
            {
                obj?.TakeDamage();
            }

            helth--;
            if (helth <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                spriteRenderer.sprite = states[helth - 1];
            }
        }
    }
}
