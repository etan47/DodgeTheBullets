using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public float life = 0f;
    private float timer = 0f;
    private SpriteRenderer spriteRenderer;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (timer >= life) {
            StartCoroutine(FadeOut());
        }
        timer += Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision){
        playerHealth.takeDamage();
    }

    void SetAlpha(float alpha){
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    System.Collections.IEnumerator FadeIn(){
        float duration = 0.1f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, time / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(1);
    }

    System.Collections.IEnumerator FadeOut(){
        float duration = 0.3f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, time / duration);
            SetAlpha(alpha);
            yield return null;
        }

        Destroy(this.gameObject);
}


}
