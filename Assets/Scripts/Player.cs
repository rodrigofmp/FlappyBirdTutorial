using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Force = 100f;
    public bool IsAlive = true;
    public bool IsGameStarted = false;
    public bool IsGrounded = false;
    public int Score;
    public Text ScoreText;
    public Button ButtonStart;

    Rigidbody2D rb;
    AudioSource wingSound;
    AudioSource hitSound;
    AudioSource coinSound;
    Vector3 initialPosition;
    bool firstTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wingSound = GetComponents<AudioSource>()[0];
        hitSound = GetComponents<AudioSource>()[1];
        coinSound = GetComponents<AudioSource>()[2];
        initialPosition = this.transform.position;
        firstTime = true;
    }

    void Update()
    {
        if (IsGameStarted)
        { 
            if (Input.GetMouseButtonDown(0) && IsAlive)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0, Force));
                wingSound.Play();
            }

            if (!IsGrounded)
            { 
                if (rb.velocity.y > 0)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 45);
                }
                else if (rb.velocity.y < 0)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, -45);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        else
        {
            if (firstTime)
            { 
                rb.velocity = Vector2.zero;
                this.transform.position = initialPosition;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if ((coll.transform.CompareTag("Pipe") || coll.gameObject.name == "ground") && IsAlive)
        {
            IsAlive = false;
            hitSound.Play();
            ButtonStart.gameObject.SetActive(true);
            IsGameStarted = false;
            IsGrounded = true;
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pipe") && IsAlive)
        {
            Score++;
            ScoreText.text = Score.ToString();
            coinSound.Play();
        }
    }

    public void OnGameStart()
    {
        foreach(var item in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            GameObject.Destroy(item);
        }
        Score = 0;
        ScoreText.text = Score.ToString();
        this.transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        IsAlive = true;
        IsGameStarted = true;
        ButtonStart.gameObject.SetActive(false);
        IsGrounded = false;
        firstTime = false;
    }
}
