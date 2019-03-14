using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;

    public AudioClip mainEngineSound;
    public AudioClip crashSound;
    public AudioClip finishSound;

    public ParticleSystem[] rocketParticles = new ParticleSystem[3];


    private bool isWon = false;
    private bool isCrashed = false;

    public float rotateSpeed = 100f;
    public float boostSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWon && !isCrashed)
            PCMovement();
        if (isWon)
        {
            if (!rocketParticles[1].GetComponent<ParticleSystem>().isPlaying)
            {
                audioSource.PlayOneShot(finishSound);
                rocketParticles[1].GetComponent<ParticleSystem>().Play();
                Invoke("LoadNextLevel", 1f);
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Deadly")
        {
            rocketParticles[0].GetComponent<ParticleSystem>().Play();
            isCrashed = true;
            audioSource.PlayOneShot(crashSound);
            Invoke("ReloadLevel", 1f);
        }
        else if (collision.gameObject.tag == "Finish")
        {            
            isWon = true;
        }
    }

    public void ReloadLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }



    public void PCMovement()
    {
        float rotation = rotateSpeed * Time.deltaTime;

        rb.freezeRotation = true;

        Thrust();
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotation);
        }
        rb.freezeRotation = false;
    }
    public void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            boostSpeed = 2500f;
            float boost = boostSpeed * Time.deltaTime;
            rb.AddRelativeForce(Vector3.up * boost);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSound);
            }
            if (!rocketParticles[2].GetComponent<ParticleSystem>().isPlaying)
                rocketParticles[2].GetComponent<ParticleSystem>().Play();
        }
        else if (GameObject.Find("Rocket").GetComponent<MobileControls>().isBoostToggled == true)
        {
            boostSpeed = 1500f;
            float boost = boostSpeed * Time.deltaTime;
            rb.AddRelativeForce(Vector3.up * boost);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSound);
            }
            if (!rocketParticles[2].GetComponent<ParticleSystem>().isPlaying)
                rocketParticles[2].GetComponent<ParticleSystem>().Play();
        }
        else
        {
            audioSource.Stop();
            rocketParticles[2].GetComponent<ParticleSystem>().Stop();

        }
    }
}
