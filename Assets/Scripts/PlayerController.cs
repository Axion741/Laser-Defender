using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 12.0f;
    float padding;
    public float xmin;
    public float xmax;
    public GameObject laser;
    public float projectileSpeed;
    public float fireRate = 0.4f;
    public float health = 300;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        //gets distance from centre to edge of sprite and uses to prevent overflow
        padding = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
       
    }
	
	// Update is called once per frame
	void Update () {
        //movement
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        //restrict player to gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        //call "Fire" on keydown and repeat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        
    }
    //fire laser
    void Fire()
    {
        GameObject playerLaser = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
        playerLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        EnemyProjectile laser = hit.gameObject.GetComponent<EnemyProjectile>();

        if (laser)
        {
            Debug.Log("Player hit");
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
