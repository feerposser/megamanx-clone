using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : MonoBehaviour
{
    public static FireSystem instance;

    [SerializeField] private Transform shootSpawn;
    [SerializeField] private BaseShoot[] shoots;

    void Start()
    {
        instance = this;    
    }

    public void Shoot(int shootIndex, string sideState)
    {
        Instantiate(shoots[shootIndex], 
            shootSpawn.position, shootSpawn.rotation)
            .SetDirection(sideState);
    }
}
