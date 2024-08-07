using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isCollected = false;
    public GameController gameController;

    void Start(){
        StartCoroutine(CoinIFrames());
    }

    System.Collections.IEnumerator CoinIFrames(){
        yield return new WaitForSeconds(0.5f);
        int newLayer = LayerMask.NameToLayer("Coins");
        gameObject.layer = newLayer;
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        if (!isCollected){
            isCollected = true;
            Destroy(this.gameObject);
            gameController.CollectCoin();
        }
    }
}
