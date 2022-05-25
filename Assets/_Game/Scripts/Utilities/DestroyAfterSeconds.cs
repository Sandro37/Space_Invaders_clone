using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [Tooltip("Tempo para o objeto ser destruido, assim que criado em cena!")]
    [SerializeField] private float seconds = 1f;
    void Start()
    {
        Destroy(gameObject, seconds);
    }
}
