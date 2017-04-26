using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 0.5f;
    private bool movingRight = true;
    private float xmax;
    private float xmin;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Use this for initialization
    void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightBoundary.x;
        xmin = leftBoundary.x;

        foreach (Transform child in transform)
        {
        GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
        //sets EnemyFormation as parent of spawed enemy.
        enemy.transform.parent = child;
        }
	}
	
	// Update is called once per frame
	void Update () {
      if (movingRight)
      {
        transform.position += Vector3.right *speed * Time.deltaTime;
      }
        else
        {
        transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (width/2);
        float leftEdgeOfFormation = transform.position.x - (width/2);
        if(leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
          else if(rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
        Debug.Log(movingRight);
    }
}
