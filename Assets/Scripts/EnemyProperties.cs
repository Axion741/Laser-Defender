using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour {

    public int maxHits;
    public int timesHit;
    public float health = 200;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D (Collider2D hit)
    {
        timesHit++;
       
        Projectile laser = hit.gameObject.GetComponent<Projectile>();

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

}

