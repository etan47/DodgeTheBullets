using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
	[SerializeField] private LayerMask playerLayer;
    public PlayerHealth playerHealth;

    public float speed = 4f;
    public float life = 0f;
    public float initialAngle = 0f;

    private float timer = 0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float angle = initialAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)); 
        rb.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= life) {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }

    
    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void OnCollisionEnter2D(Collision2D collision){
        Destroy(this.gameObject);
        playerHealth.takeDamage();
    }
}
