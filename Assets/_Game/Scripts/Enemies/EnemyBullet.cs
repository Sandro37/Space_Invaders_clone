using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IDamage

{
    [SerializeField] private float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamage obj = collision.gameObject.GetComponent<IDamage>();
        if (obj != null)
        {
            obj?.TakeDamage();
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
