using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    float moveH;
    float moveV;
    public float speed = 10;
    public int health = 5;

    private int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is once per frame before render
    void Update()
    {
        if (health == 0)
        {
            Debug.Log($"Game Over!");
            health = 5;
            score = 0;
            SceneManager.LoadScene("maze");
        }
    }

    // FixedUpdate is called once per frame before physics
    void FixedUpdate()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveH, 0.0f, moveV);

        rb.AddForce(movement * speed);
    }

    /// Causes trigger events with player
    void OnTriggerEnter(Collider other)
    {
        /// increments score on pickup collision
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            Debug.Log($"Score: {score}");
        }
        /// decrements health on trap collision
        if (other.gameObject.CompareTag("Trap"))
        {
            health -= 1;
            Debug.Log($"Health: {health}");
        }
        /// win on goal collision
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log($"You win!");
        }
    }
}
