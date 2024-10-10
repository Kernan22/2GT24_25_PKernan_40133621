using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = false;
    public bool gameOver = false;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        // Adding rigidbody to allow for the jump action
        playerRb = GetComponent<Rigidbody>();
        
        // Adding variable to change gravity
        Physics.gravity *= gravityModifier;
        //Set up jump animation
        playerAnim = GetComponent<Animator>();
        
        // Adding audio to the players jumps and crashes
        playerAudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make the character jump when space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            explosionParticle.Play();
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
        if (!isOnGround || gameOver)
        {
            dirtParticle.Stop();
        }
    }
}
