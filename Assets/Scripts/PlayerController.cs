using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 move;
    private int score = 0;

    public Text scoreText;
    public GameObject prefab;

    public float speed = 1.0f;
    public float timer = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("SpawnCoin",3.0f,3.0f);
    }

    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        move = new Vector3(h,0.0f, v);


        // IF YOU DONT USE INVOKE YOU COULD 
        //SpawnCoinWithTimer();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = move*speed ;
        // IF YOU DONT USE INVISIBLE WALLS YOU COULD
        //LimitPlayerMovement();
       
    }

    // ALTERNATIVE TO INVOKE ON SPAWNCOIN
    void SpawnCoinWithTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            SpawnCoin();
            timer = 3.0f;
        }
    }
    // ALTERNATIVE TO INVISIBLEWALLS
    void LimitPlayerMovement()
    {
         if (transform.position.x > 25.0f)
        {
            transform.position = new Vector3 ( 25.0f, transform.position.y, transform.position.z);
        }
         if (transform.position.x < -25.0f)
        {
            transform.position = new Vector3 ( -25.0f, transform.position.y, transform.position.z);
        }
        if (transform.position.z > 25.0f)
        {
            transform.position = new Vector3 (transform.position.x, transform.position.y, 25.0f);
        }
         if (transform.position.z< -25.0f)
        {
            transform.position = new Vector3 ( transform.position.x, transform.position.y, -25.0f);
        }
    }

    // METHOD THAT SPAWNS THE COINS 
    void SpawnCoin()
    {
        Vector3 randomVector = new Vector3(Random.Range(-25.0f,25.0f),0.0f, Random.Range(-25.0f,25.0f)); 

        GameObject spawnObject = Instantiate(prefab,randomVector,Quaternion.identity);

        Destroy(spawnObject, 3.0f);
    }
    
    // WHEN WE COLLIDE WITH THE COINS 
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            score += 100;
            scoreText.text = ("Score  = " + score);
            Destroy(other.gameObject);
        }
        
    }
}
