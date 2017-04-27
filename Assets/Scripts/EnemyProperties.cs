using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour {

    public int maxHits;
    public int timesHit;
    public float health = 200;
    public float projectileSpeed;
    private float fireRate = 0.5f;
    public GameObject elaser;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Enemy fire
        float probability = Time.deltaTime * fireRate;
        if(Random.value < probability) { Fire(); }
    }

    private void OnTriggerEnter2D (Collider2D hit)
    {
        timesHit++;
       
        PlayerProjectile laser = hit.gameObject.GetComponent<PlayerProjectile>();
        
        //detects firetype and acts appropriately
        if (laser)
        {
            Debug.Log("Hit by laser");
            //call hit function from projectile
            laser.Hit();
            health -= laser.GetDamage();
            //Enemy death
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        } 
    }

    void Fire()
    {
        GameObject enemyLaser = Instantiate(elaser, transform.position, Quaternion.identity) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }
}

