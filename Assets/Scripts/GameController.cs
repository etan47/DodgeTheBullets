using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numCoins;
    public List<BulletSpawner> spawners = new List<BulletSpawner>();
    private int difficulty;
    private List<Vector3> coinPositions = new List<Vector3>{new Vector3(-9.41f, -2.13f, 0f), 
        new Vector3(4.45f, -1.07f, 0f), new Vector3(10.07f, -3.19f, 0f), new Vector3(0.03f, 1.87f, 0f), new Vector3(-6f, 2.98f, 0f)};
    
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
        numCoins = 0;
        StartCoroutine(CoinSpawner());
    }

    public void CollectCoin(){
        numCoins += 1;
        IncreaseDifficulty();
        StartCoroutine(CoinSpawner());
    }

    private void IncreaseDifficulty(){
        difficulty += 1;
        if (difficulty % 2 == 1){
            foreach (BulletSpawner s in spawners){
                s.firePeriod -= 0.1f;
            }
        }
        else{
            foreach(BulletSpawner s in spawners){
                s.bulletSpeedIncrease += 1f;
            }
        }
        if (difficulty % 5 == 0){
            foreach(BulletSpawner s in spawners){
                s.spray += 1;
            }
        }
    }

    System.Collections.IEnumerator CoinSpawner(){
        yield return new WaitForSeconds(5f);
        Vector3 position = coinPositions[Random.Range(0, coinPositions.Count)];
        GameObject curCoin = Instantiate(coin, position, Quaternion.identity);
        curCoin.GetComponent<Coin>().gameController = this;
    }
}
