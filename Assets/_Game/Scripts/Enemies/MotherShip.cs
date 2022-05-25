using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    [SerializeField] private int scoreValue;

    private const float MAX_LEFT = -5f;
    private float speed = 5f;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);
       
        if(transform.position.x <= MAX_LEFT)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamage obj = collision.gameObject.GetComponent<IDamage>();
        if (obj != null)
        {
            UIManager.UpdateScore(scoreValue);
            obj?.TakeDamage();
            Destroy(gameObject);
        }
    }
}
