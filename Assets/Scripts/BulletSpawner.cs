using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class BulletSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerHealth playerHealth;
    public Transform player;
    public List<String> states = new List<String>();
    public List<GameObject> bullets = new List<GameObject>();
    public List<bool> canSpray = new List<bool>();
    public int spray;
    public float firePeriod;
    public float angularVelo;
    public float spinUpperLimit;
    public float spinLowerLimit;
    public float resetRotation;
    public float bulletSpeedIncrease;
    public float sprayAngle;
    private float curTime = 0f;
    private GameObject curBullet;
    private GameObject bulletType;
    private int stateID;
    private float timeBetweenSwitches = 5f;
    private int curSpray;
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, resetRotation);
        bulletType = bullets[0];
        stateID = 0;
        curSpray = spray;
        StartCoroutine(StatusFlipper());
    }

    // Update is called once per frame
    void Update()
    {
        if (states[stateID] == "spin") {
            Spin();
        }
        else if (states[stateID] == "aim") {
            Aim();
        }

        if (curTime >= firePeriod){
            Fire(bulletType, curSpray);
            curTime = 0f;
        }
        curTime += Time.deltaTime;
    }

    void ChangeState(int state){
        transform.eulerAngles = new Vector3(0f, 0f, resetRotation);
        bulletType = bullets[state];
        stateID = state;
        if (canSpray[state]) curSpray = spray;
        else curSpray = 1;
    }

    void Spin() {
        if (spinLowerLimit > spinUpperLimit){
            if (spinUpperLimit <= transform.eulerAngles.z && transform.eulerAngles.z <= spinLowerLimit){
                angularVelo *= -1;
            }
        }
        else{
            if (spinUpperLimit <= transform.eulerAngles.z || spinLowerLimit >= transform.eulerAngles.z){
                angularVelo *= -1;
            }
        }
        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + angularVelo);
    }

    void Aim() {
        if (player) {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void Fire(GameObject bullet, int num)
    {   
        if (num % 2 == 1){
            Shoot(bullet, transform.eulerAngles.z);
            Spray(bullet, (num - 1) / 2, false);
        }
        else{
            Spray(bullet, num / 2, true);
        }
    }

    void Spray(GameObject bullet, int num, bool isEven){
        float gap = sprayAngle / (num + 1);
        float curGap = 0;
        if (isEven) curGap -= gap / 2;
        for (int i = 0; i < num; i++){
            curGap += gap;
            Shoot(bullet, transform.eulerAngles.z + curGap);
            Shoot(bullet, transform.eulerAngles.z - curGap);
        }
    }

    void Shoot(GameObject bullet, float angle){
            curBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            curBullet.GetComponent<Bullet>().speed += bulletSpeedIncrease;
            curBullet.GetComponent<Bullet>().initialAngle = angle;
            curBullet.GetComponent<Bullet>().playerHealth = playerHealth;
    }

    System.Collections.IEnumerator StatusFlipper(){
		yield return new WaitForSeconds(timeBetweenSwitches);
        if (stateID == 0) ChangeState(1);
        else ChangeState(0);
        StartCoroutine(StatusFlipper());
	}

}
