using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameController gameController;
    public HealthBar healthBar;
    public int hp = 5;
    private bool isDead = false;
    private Rigidbody2D rb;
    private float numIFrames = 1f;
    private float blinkInterval = 0.15f;
    private Renderer rend;
    private bool inIFrames = false;



    void Start() {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.GainHearts(hp);
    }
    public void takeDamage(){
        if (!inIFrames){
            healthBar.LoseHearts(Math.Min(1, hp));
            hp -= 1;
            if (hp <= 0){
                isDead = true;
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.simulated = false;
                StartCoroutine(Die());
            }

            if (!isDead) StartCoroutine(IFrames());
        }
    }

    public bool getDeathStatus(){
        return isDead;
    }

    System.Collections.IEnumerator Die() {
		yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        PlayerPrefs.SetInt("Score", gameController.numCoins);
        SceneManager.LoadScene(2);
    }
    System.Collections.IEnumerator IFrames() {
        inIFrames = true;
        int newLayer = LayerMask.NameToLayer("IFrames");
        gameObject.layer = newLayer;

        float elapsedTime = 0f;
		while (elapsedTime < numIFrames){
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        rend.enabled = true;
        newLayer = LayerMask.NameToLayer("Player");
        gameObject.layer = newLayer;
        inIFrames = false;
    }
}
