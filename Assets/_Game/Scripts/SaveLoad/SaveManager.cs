using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadProgress();
    }

    public static void SaveProgress()
    {
        SaveObject saveObject = new();

        saveObject.Coins = Inventory.CurrentCoins;
        saveObject.HighScore = UIManager.GetHighScore();
        saveObject.ShipStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ShipStats;

        SaveLoad.SaveState(saveObject);
    }

    public static void LoadProgress()
    {
        SaveObject saveObject = SaveLoad.LoadState();

        Inventory.CurrentCoins = saveObject.Coins;
        UIManager.UpdateHighScore(saveObject.HighScore);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ShipStats = saveObject.ShipStats;
    }
}
