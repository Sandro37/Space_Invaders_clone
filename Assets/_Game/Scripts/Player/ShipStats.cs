using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipStats 
{
    [Range(1,5)]
    [SerializeField] private int maxHelth;

    [HideInInspector]
    [SerializeField] private int currentHelth;

    [HideInInspector]
    [SerializeField] private int maxLives = 3;

    [HideInInspector]
    [SerializeField] private int currentLives = 3;

    [SerializeField] private float shipSpeed;

    [Tooltip("Tempo entre um tiro e outro")]
    [SerializeField] private float fireRate;

    public ShipStats(){}
    public ShipStats(int maxHelth, int maxLives, float shipSpeed, float fireRate)
    {
        this.maxHelth = maxHelth;
        this.currentHelth = maxHelth;
        this.maxLives = maxLives;
        this.currentLives = maxLives;
        this.shipSpeed = shipSpeed;
        this.fireRate = fireRate;
    }

    public int MaxHelth
    {
        get => this.maxHelth;
    }
    public int CurrentHelth
    {
        get => this.currentHelth;
        set => this.currentHelth = value;
    }

    public int MaxLives
    {
        get => this.maxLives;
    }

    public int CurrentLives
    {
        get => this.currentLives;
        set => this.currentLives = value;
    }

    public float ShipSpeed
    {
        get => this.shipSpeed;
    }

    public float FireRate
    {
        get => this.fireRate;
    }
}
