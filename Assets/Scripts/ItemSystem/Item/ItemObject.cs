using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public float rotateSpeed = 0.5f;
    public Item item;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RandomShot();
    }
    void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        transform.Rotate(0, rotateSpeed, 0);        
    }
    void RandomShot()
    {
        Vector3 dir = new Vector3(Random.Range(-1.0f, 1.0f), 1, Random.Range(-1.0f, 1.0f));
        float force = Random.Range(0.5f,2f);
        rb.AddForce(dir * force, ForceMode.Impulse);
    }
}
