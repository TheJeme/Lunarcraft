using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Obstacle obstacle;
    private Vector3 startingPos;

    public float period = 2f;
    public Vector3 movementVector = new Vector3(10f,10f,10f);

    [Range(0,1)] [SerializeField] float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        obstacle = GetComponent<Obstacle>();   
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
