using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawnerController : MonoBehaviour
{
    public List<LaserSpawner> spawners;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomFire());
    }

    System.Collections.IEnumerator RandomFire(){
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        spawners[Random.Range(0, 2)].Warn();
        StartCoroutine(RandomFire());
    }
}
