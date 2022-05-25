using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject pauseMenu;

    public static MenuManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    private void Start()
    {
        ReturnToMainMenu();
    }

    public void OpenMainMenu()
    {
        instance.mainMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    public static void OpenGameOver()
    {
        instance.gameOverMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    public void OpenShop()
    {
        instance.mainMenu.SetActive(false);
        instance.shopMenu.SetActive(true);
    }
    public void CloseShop()
    {
        instance.shopMenu.SetActive(false);
        instance.mainMenu.SetActive(true);
    }

    public void OpenInGameMenu()
    {
        Time.timeScale = 1;

        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.ShipStats.CurrentHelth = player.ShipStats.MaxHelth;

        UIManager.UpdateHealthBar(player.ShipStats.CurrentHelth);

        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);

        GameManager.SpawnNewWave();
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0; 
        instance.inGameMenu.SetActive(false);
        instance.pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        instance.pauseMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        instance.gameOverMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.inGameMenu.SetActive(false);

        instance.mainMenu.SetActive(true);
        GameManager.CancelGame();
    }

    public static void CloseWindown(GameObject gameObj)
    {
        gameObj.SetActive(false);
    }
}
