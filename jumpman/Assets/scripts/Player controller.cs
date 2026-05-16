using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{
    [Header("Movimentacao")]
    private Rigidbody rb;//fisica
    public float jumpForce = 10f;
    private bool isGrounded = true;

    [Header("animações")]
    private Animator playerAnim;

    [Header("Physics")]
    public float gravityModifier = 1f;

    [Header("particulas")]
    public ParticleSystem explosionParticles;
    public ParticleSystem particleleft;

    [Header("audio")]
    public AudioSource _audioSource;
    public List<AudioClip> _sounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * (gravityModifier - 1) * Physics.gravity.magnitude, ForceMode.Acceleration);
    }
    private void OnJump(InputValue value)
    {
        if (isGrounded && value.isPressed && !SpawnManager.Instance.IsGameOver)
        {
            
             playerAnim.SetTrigger("Jump_trig");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            particleleft.Stop();
            _audioSource.PlayOneShot(_sounds[0]);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")&&
            !SpawnManager.Instance.IsGameOver)
        {
            isGrounded = true;
            particleleft.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            
            SpawnManager.Instance.IsGameOver = true;
            Destroy(collision.gameObject);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            explosionParticles.Play();
            particleleft.Stop();
            _audioSource.PlayOneShot(_sounds[1]);
        }

    }



}