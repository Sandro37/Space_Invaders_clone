using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [SerializeField] private ShipStats shipStats;

    [Tooltip("Prefab do tiro do jogador")]
    [SerializeField] private GameObject bulletPrefab;

    private const float MAX_LEFT = -2.5f;
    private const float MAX_RIGHT = 2.5f;

    [Tooltip("Posição de onde irá sair o tiro do jogador")]
    [SerializeField] private Transform BulletPosition;

    [Tooltip("Sons de efeito / sound effect")]
    [SerializeField] private AudioClip shootSFX;

    private bool isShooting;
    private Vector2 horizontal;

    private Vector2 offScreenPosition = new(0, 20f);
    private Vector2 startPosition = new(0, -4.5f);

    public ShipStats ShipStats
    {
        get => this.shipStats;
        set => this.shipStats = value;
    }

    private void Start()
    {
        shipStats.CurrentHelth = shipStats.MaxHelth;
        shipStats.CurrentLives = shipStats.MaxLives;
        transform.position = startPosition;

        UIManager.UpdateHealthBar(shipStats.CurrentHelth);
        UIManager.UpdateLives(shipStats.CurrentLives);
    }

    void Update()
    { 
        this.Move();
        this.Shoot();
    }

    void Move()
    {
        horizontal.x = Input.GetAxisRaw("Horizontal");
        transform.Translate(shipStats.ShipSpeed * Time.deltaTime * horizontal);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, MAX_LEFT, MAX_RIGHT), transform.position.y);
    }
    void Shoot()
    {
        if (Input.GetButton("Fire1") && !isShooting)
        {
            StartCoroutine(Shooting());
        }
    }
    public void TakeDamage()
    {
        shipStats.CurrentHelth--;
        UIManager.UpdateHealthBar(shipStats.CurrentHelth);
        if (shipStats.CurrentHelth <= 0)
        {
            shipStats.CurrentLives--;
            UIManager.UpdateLives(shipStats.CurrentLives);
            if (shipStats.CurrentLives <= 0)
            {
                SaveManager.SaveProgress();
                this.Kill();
            }
            else
            {
                StartCoroutine(Respawn());
            }
        } 
    }

    public void AddHelth()
    {
        if(shipStats.CurrentHelth == shipStats.MaxHelth)
        {
            UIManager.UpdateScore(250);
        }
        else
        {
            shipStats.CurrentHelth++;   
            UIManager.UpdateHealthBar(shipStats.CurrentHelth);
        }
    }

    public void AddLife()
    {
        if (shipStats.CurrentLives == shipStats.MaxLives)
        {
            UIManager.UpdateScore(1000);
        }
        else
        {
            shipStats.CurrentLives++;
            UIManager.UpdateLives(shipStats.CurrentLives);
        }
    }
    private void Kill()
    {
        Destroy(gameObject);
    }
    private IEnumerator Shooting()
    {
        isShooting = true;
        Instantiate(bulletPrefab, BulletPosition.position, Quaternion.identity);
        AudioManager.PlaySoundEffect(shootSFX);
        yield return new WaitForSeconds(shipStats.FireRate);
        isShooting = false;
    }

    private IEnumerator Respawn()
    {
        transform.position = offScreenPosition;
        yield return new WaitForSeconds(2);

        shipStats.CurrentHelth = shipStats.MaxHelth;
        UIManager.UpdateHealthBar(shipStats.CurrentHelth);
        transform.position = startPosition;
    }
}
