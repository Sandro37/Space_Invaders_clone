using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    [Tooltip("Prefab do Tiro inimigo")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("Prefab dos inimigos")]
    [SerializeField] private GameObject motherShipPrefab;

    private Vector3 horizontalMoveDistance = new(0.05f, 0, 0);
    private Vector3 verticalMoveDistance = new(0, 0.15f, 0);
    private Vector3 motherShipSpawnPos = new(3.75f, 3.45f, 0);

    private const float MAX_LEFT = -2.5f;
    private const float START_Y = 1.15f;
    private const float MAX_RIGHT = 2.5f;
    private const float MAX_MOVE_SPEED = 0.2f;

    private float moveTimer = 0.01f;
    private const float moveTime = 0.005f;

    private float shootTimer = 3f;
    private const float shootTime = 3f;

    private float motherShipTimer = 1f;
    private const float MOTHERSHIP_MIN = 15f;
    private const float MOTHERSHIP_MAX = 60f;

    private bool movingRight;
    private bool entering = true;

    public static List<GameObject> allAliens = new();

    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Alien"))
            allAliens.Add(obj);
    }

    // Update is called once per frame
    void Update()
    {
        if (entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime  * 10);

            if(transform.position.y <= START_Y)
            {
                entering = false;
            }
        }
        else
        {
            if (moveTimer <= 0)
                this.MoveEnemies();

            if (shootTimer <= 0)
                this.Shoot();

            if (motherShipTimer <= 0)
                this.SpawnMotherShip();

            moveTimer -= Time.deltaTime;
            shootTimer -= Time.deltaTime;
            motherShipTimer -= Time.deltaTime;
        }      
    }

    private void MoveEnemies()
    {
        if(allAliens.Count > 0)
        {
            int hitMax = 0;
            for (int i = 0; i < allAliens.Count; i++)
            {
                if (movingRight)
                    allAliens[i].transform.position += horizontalMoveDistance;
                else
                    allAliens[i].transform.position -= horizontalMoveDistance;

                if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)
                    hitMax++;
            }

            if(hitMax > 0)
            {
                for (int i = 0; i < allAliens.Count; i++)
                    allAliens[i].transform.position -= verticalMoveDistance;

                movingRight = !movingRight;
                
            }

            moveTimer = GetMoveSpeed();
        }
    }
    private void Shoot()
    {
        if (allAliens.Count == 0)
            return;

        Vector2 pos = allAliens[Random.Range(0, allAliens.Count)].transform.position;   

        Instantiate(bulletPrefab, pos, Quaternion.identity);
        shootTimer = shootTime;
    }

    private void SpawnMotherShip()
    {
        Instantiate(motherShipPrefab, motherShipSpawnPos, Quaternion.identity);
        motherShipTimer = Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);
    }
    private float GetMoveSpeed() => allAliens.Count * moveTime < MAX_MOVE_SPEED ? MAX_MOVE_SPEED : allAliens.Count * moveTime;  
}
