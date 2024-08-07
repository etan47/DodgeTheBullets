using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer rend;
    public float blinkInterval = 1f;
    public int numBlinks = 3;
    public LaserSpawner spawner;
    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(Blink());
    }

    System.Collections.IEnumerator Blink(){
        int curBlink = 0;
		while (curBlink < numBlinks){
            yield return new WaitForSeconds(blinkInterval);
            rend.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            rend.enabled = true;
            curBlink += 1;
        }
        spawner.Fire();
        Destroy(this.gameObject);
    }
}
