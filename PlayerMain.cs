using UnityEngine;
using System.Collections;

public class PlayerMain : MonoBehaviour {
    public float JumpForce = 10.00f;
    Rigidbody2D myRigidbody;
    bool isGrounded = false;
    float posX = 0.0f;
    bool isGameOver = false;
    ChallengeGeneratingScript myChallengeController;
    GameController myGameController;
    public AudioClip jump;
    public AudioClip deadSound;
    public AudioClip collectionSound;
    AudioSource myAudioPlayer;

	// Use this for initialization
	void Start () {
        myRigidbody = transform.GetComponent<Rigidbody2D>();
        posX = transform.position.x;
        myChallengeController = GameObject.FindObjectOfType<ChallengeGeneratingScript>();
        myGameController = GameObject.FindObjectOfType<GameController>();   
        myAudioPlayer = GameObject.FindObjectOfType<AudioSource>();

    }
	
	
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.Space) && isGrounded && !isGameOver) {
            myRigidbody.AddForce(Vector3.up * (JumpForce * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
            myAudioPlayer.PlayOneShot(jump);
            isGrounded = false;
        }
        //Hit in the face 
        if (transform.position.x < posX  && !isGameOver) {
            GameOver();
        }
    }

    void Update()
    {
    
    }

    void GameOver() {
        isGameOver = true;
        myAudioPlayer.PlayOneShot(deadSound);
        myChallengeController.GameOver();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground") {
            isGrounded = true;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }

        if (other.collider.tag == "Bad") {
            GameOver();
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectable")
        {
            myGameController.IncrementScore();
            myAudioPlayer.PlayOneShot(collectionSound);
            Destroy(other.gameObject);
        }
    }
}
