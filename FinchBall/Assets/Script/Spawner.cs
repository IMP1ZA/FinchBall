using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    public void SpawnBall() 
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay() 
    {
        yield return new WaitForSeconds(5);
        Instantiate(ball, transform.position, Quaternion.identity);

    }
}
