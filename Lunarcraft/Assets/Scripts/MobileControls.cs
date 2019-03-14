using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    private bool isLeftToggled = false;
    private bool isRightToggled = false;
    public bool isBoostToggled = false;

    public float rotateSpeed = 100f;

    public AudioClip mainEngineSound;

    private AudioSource audioSource;
    public ParticleSystem[] rocketParticles = new ParticleSystem[1];

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MobileMovement();
    }

    public void MobileMovement()
    {
        rb.freezeRotation = true;

        if (isBoostToggled)
            Boost();
        if (isLeftToggled)
            TurnLeft();
        else if (isRightToggled)
            TurnRight();

        rb.freezeRotation = false;
    }

    public void Boost()
    {
        GameObject.Find("Rocket").GetComponent<Spaceship>().Thrust();
    }

    public void TurnLeft()
    {
        float rotation = rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotation);
    }

    public void TurnRight()
    {
        float rotation = rotateSpeed * Time.deltaTime;
        transform.Rotate(-Vector3.forward * rotation);
    }
       
    public void ToggleButtonLeft()
    {
        isLeftToggled = !isLeftToggled;
    }
    public void ToggleButtonRight()
    {
        isRightToggled = !isRightToggled;
    }
    public void ToggleButtonBoost()
    {
        isBoostToggled = !isBoostToggled;
    }
}
