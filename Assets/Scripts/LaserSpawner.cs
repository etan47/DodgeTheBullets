using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public Laser laser;
    public Warning warning;
    public PlayerHealth playerHealth;
    public void Warn(){
        Warning w = Instantiate(warning, transform.position, Quaternion.identity);
        w.spawner = this;
    }
    public void Fire(){
        Laser l = Instantiate(laser, transform.position, Quaternion.identity);
        l.playerHealth = playerHealth;
    }
}
