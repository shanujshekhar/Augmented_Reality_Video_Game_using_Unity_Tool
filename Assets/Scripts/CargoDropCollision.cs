using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoDropCollision : MonoBehaviour
{
    private Vector3 pos;
    private bool touched;
    private GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.Find("ImageTarget").transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Plane"))
        {
            transform.GetComponent<Rigidbody>().useGravity = false;

            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            transform.SetParent(other.transform);
            transform.position = new Vector3(transform.position.x, plane.transform.position.y + 0.5f, transform.position.z);
            transform.rotation = plane.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
