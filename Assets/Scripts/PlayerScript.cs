using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;


    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text WinText;
    public Text livesText;
    private int lives = 3;
    private int scoreValue = 0;

    public AudioSource musicSource;
    public AudioClip backgroundClip;
    public AudioClip winClip;

    private bool facingRight = true;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        WinText.text = "";
        livesText.text = "Lives: " + lives.ToString();
        
        musicSource.clip = backgroundClip;
        musicSource.Play();
        musicSource.loop = true;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        

    }

    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (scoreValue >= 8)
        {
            WinText.text = "You Win! This game was made by Matt Reuter.";
        }
        
        if (lives == 0)
        {
            WinText.text = "You Lose! This game was made by Matt Reuter.";
            gameObject.SetActive (false);
        }

        livesText.text = "Lives: " + lives.ToString();
        
        if (hozMovement > 0)
        {
            anim.SetInteger("State", 1);  
        }
        if (hozMovement < 0)
        {
            anim.SetInteger("State", 1);  
        }
        if (hozMovement == 0)
        {
            anim.SetInteger("State", 0);  
        }
        if (verMovement > 0)
        {
            anim.SetInteger("State", 2);
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();

            if (scoreValue == 4)
            {
                transform.position = new Vector2(38.0f, 0.0f);
                lives = 3;
            }
            
            if (scoreValue == 8)
            {
                musicSource.clip = winClip;
                musicSource.Play();
                musicSource.loop = true;
            }

        }

        
        


        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            livesText.text = "Lives: " + lives.ToString();
            Destroy(collision.collider.gameObject);
        }



    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }        
        

    }

}
