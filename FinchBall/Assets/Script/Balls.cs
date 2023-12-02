using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float targetRotation;
    [SerializeField] private float returnRotation;
    [SerializeField] private float launchForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dragafterlaunch;
    [SerializeField] private float maxLaunchTime;
    [SerializeField] private float maxLaunchForce;
    [SerializeField] private float speedToMaxForce;
    [SerializeField] private GameObject debugger;

    private Rigidbody rb;
    private Spawner spawner;
    private bool isRotating = false;
    private bool readyToShoot = false;


    void Start()
    {
        spawner = FindAnyObjectByType<Spawner>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RotateObject(targetRotation));
    }

    void Update()
    {

        Move();

        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StopAllCoroutines();
            isRotating = false;
            readyToShoot = true;   
        }
        if (readyToShoot) 
        {
            debugger.transform.localScale = new Vector3(debugger.transform.localScale.x, debugger.transform.localScale.y, launchForce / 25f);
        }
        

        if (readyToShoot) 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                LaunchBall();
            }
        }
        launchForce = Mathf.PingPong(Time.time * speedToMaxForce,maxLaunchForce);
       

    }
    void LaunchBall()
    {
        rb.isKinematic = false;
        rb.drag = dragafterlaunch;
        debugger.SetActive(false);

        Vector3 launchDirection = transform.forward;
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse); 
        spawner.SpawnBall();
        this.enabled = false;
        
    }
    void Move() 
    {
        if (!readyToShoot) 
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
            rb.AddForce(movement.normalized * moveSpeed, ForceMode.Force);
        }

    }
    IEnumerator RotateObject(float targetAngle)
    {
        isRotating = true;

        float currentAngle = transform.rotation.eulerAngles.y;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, t);
            transform.rotation = Quaternion.Euler(0, newAngle, 0);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, targetAngle, 0);

        // Swap the target and return rotations
        float temp = targetRotation;
        targetRotation = returnRotation;
        returnRotation = temp;

        // Start rotating to the new target rotation
        StartCoroutine(RotateObject(targetRotation));

        isRotating = false;
    }
}
