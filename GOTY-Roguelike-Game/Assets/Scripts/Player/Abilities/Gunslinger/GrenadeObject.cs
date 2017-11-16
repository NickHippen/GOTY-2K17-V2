using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeObject : MonoBehaviour
{
    public GameObject Parent;
    public Rigidbody RBody;
    public float Force;

    private bool isThrown;

    // Use this for initialization
    void Start()
    {
        transform.position = Parent.transform.position;
        RBody.useGravity = false;
        isThrown = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isThrown)
        {
            transform.position = Parent.transform.position;
        }
    }

    public void Release()
    {
        transform.parent = null;
        RBody.useGravity = true;
        transform.rotation = Parent.transform.rotation;
        RBody.AddForce(transform.forward * Force);
        isThrown = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
