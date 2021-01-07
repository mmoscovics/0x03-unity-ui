using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    float moveH;
    float moveV;
    public float speed = 10;
    public int health = 5;
    public Text scoreText;
    public Text healthText;    
    public Text winloseText;
    public Image winloseBG;

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
            winloseText.text = "Game Over!";
            winloseText.color = Color.white;
            winloseBG.color = Color.red;
            winloseBG.gameObject.SetActive(true);
            health = 5;
            score = 0;
            StartCoroutine(LoadScene(3));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
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
            SetScoreText();
        }
        /// decrements health on trap collision
        if (other.gameObject.CompareTag("Trap"))
        {
            health -= 1;
            SetHealthText();  
        }
        /// win on goal collision
        if (other.gameObject.CompareTag("Goal"))
        {
            winloseText.text = "You Win!";
            winloseText.color = Color.black;
            winloseBG.color = Color.green;
            winloseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }

    /// Update ScoreText object with players score
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    /// Update HealthText object with players health
    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();        
    }

    /// Reload scene after win or lose
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("maze");
    }
}
