using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;                                                    //Bullet speed
    public float lifeDuration = 2f;                                             //Bullet life time

    private float lifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;      //Make the bullet move

        lifeTimer -= Time.deltaTime;                                           //Check if the bullet should be destroyed
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
