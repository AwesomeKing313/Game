using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    
    private Animator anime;

    private Rigidbody2D rb;

    [Header("Audio")]
    [SerializeField] private AudioSource deathSoundEffect;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            deathSoundEffect.Play();
            Die();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anime.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

