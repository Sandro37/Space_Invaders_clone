using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien :  MonoBehaviour, IDamage
{
    [SerializeField] private float scoreValue;
    [SerializeField] private GameObject explosionPrefab;

    [Header("Pickups")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private GameObject healthPrefab;

    private const int LIFE_CHANCE = 1;
    private const int HEALTH_CHANCE = 10;
    private const int COIN_CHANCE = 50;
    public void TakeDamage()
    {
        this.Kill();
    }

    private void Kill()
    {
        UIManager.UpdateScore((int)scoreValue);
        AlienMaster.allAliens.Remove(gameObject);

        int random = Random.Range(0, 1000);

        if (random == LIFE_CHANCE)
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        else if (random <= HEALTH_CHANCE)
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        else if (random <= COIN_CHANCE)
            Instantiate(coinPrefab, transform.position, Quaternion.identity);

        AudioManager.UpdateBattleMusicDelay(AlienMaster.allAliens.Count);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (AlienMaster.allAliens.Count == 0)
            GameManager.SpawnNewWave();

        Destroy(gameObject);
    }
}
