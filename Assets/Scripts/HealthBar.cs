using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject heartPrefab;
    private float blinkTime = 0.75f;
    private float blinkInterval = 0.05f;
    private Stack<GameObject> hearts = new Stack<GameObject>();

    public void GainHearts(int i) {
        for (int j = 0; j < i; j++) {
            GameObject heart = Instantiate(heartPrefab, transform);
            hearts.Push(heart);
        }
    }

    public void LoseHearts(int i){
        for (int j = 0; j < i; j++) {
            GameObject heart = hearts.Pop();
            StartCoroutine(BlinkHeartAndDelete(heart));
        }
    }

    System.Collections.IEnumerator BlinkHeartAndDelete(GameObject heart){
        float elapsedTime = 0f;
		while (elapsedTime < blinkTime){
            heart.SetActive(!heart.activeSelf);
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        Destroy(heart);
    }
}
