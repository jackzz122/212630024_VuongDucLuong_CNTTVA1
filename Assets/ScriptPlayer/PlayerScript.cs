using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isGameOver = false;
    public float x, y;
    private Animator player;
    int score = 0;
    public Text txtScore;
    private Collider2D playerCollider;
    private Rigidbody2D rb;
    public Text txtHighScore;
    private int highScore = 0;
    private bool isJumping = false;

    void Start()
    {
        txtScore = GameObject.Find("Score").GetComponent<Text>();
        txtHighScore = GameObject.Find("HighScore").GetComponent <Text>();
        player = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        isGameOver = false;
        player.SetBool("isRunning", false);
        player.SetBool("isStay", true);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        txtHighScore.text = "High score: " + highScore.ToString();
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GoldCoin")
        {
            score++;
            Destroy(collision.gameObject);
            txtScore.text = "Score: " + score.ToString();
            if(score > highScore)
            {
                highScore = score;
                txtHighScore.text = "High Score: " + highScore.ToString();
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }
    }
    void Update()
    {
        if (!isGameOver)
        {
            Debug.Log("isRunning");
            if(Input.GetKey(KeyCode.LeftArrow)) 
            {
                player.SetBool("isRunning", true);
                player.SetBool("isStay", false);
                gameObject.transform.Translate(Vector2.left * x * Time.deltaTime);
                if (gameObject.transform.localScale.x > 0)
                {
                    gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                player.SetBool("isRunning", true);
                player.SetBool("isStay", false);
                gameObject.transform.Translate(Vector2.right * x * Time.deltaTime);
                if (gameObject.transform.localScale.x < 0)
                {
                    gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                }
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                playerCollider.isTrigger = true;
                player.SetBool("isRunning", true);
                player.SetBool("isStay", false);
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, y);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, y);
            }
            else
            {
                if (!isJumping && rb.velocity.y <= 0)
                {
                    playerCollider.isTrigger = false;
                }
                player.SetBool("isRunning", false);
                player.SetBool("isStay", true);
            }


            if (rb.velocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }
}
