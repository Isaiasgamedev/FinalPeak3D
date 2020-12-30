using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public MeshRenderer RoofReference;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO ALL BEHAVIOR
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            RoofReference.enabled = false;
            gameObject.SetActive(false);
        }
        
    }
}
