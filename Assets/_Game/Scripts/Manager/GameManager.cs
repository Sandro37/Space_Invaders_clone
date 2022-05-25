using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allAliensSets;

    private GameObject currentSet;
    private Vector2 spawnPosition = new(0, 10);
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public static void SpawnNewWave()
    {
        instance.StartCoroutine(instance.SpawnWave());
    }

    public static void CancelGame()
    {
        instance.StopAllCoroutines();
        AlienMaster.allAliens.Clear();

        if (instance.currentSet != null)
            Destroy(instance.currentSet);

        UIManager.ResetUI();
        AudioManager.StopBattleMusic();
    }
    private IEnumerator SpawnWave()
    {
        AudioManager.UpdateBattleMusicDelay(1);
        AudioManager.StopBattleMusic(); 
        AlienMaster.allAliens.Clear();

        if (currentSet != null)
            Destroy(currentSet);

        yield return new WaitForSeconds(3);

        currentSet = Instantiate(allAliensSets[Random.Range(0, allAliensSets.Length)], spawnPosition, Quaternion.identity);

        UIManager.UpdateWave();
        AudioManager.PlayBattleMusic();
    }
}
