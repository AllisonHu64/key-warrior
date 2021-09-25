// not using this code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{
    public float tileSpeed = 2f;
    public bool isOnButton = false;
    public KeyCode key;

    void Update()
    {
        transform.position -= new Vector3(0f, tileSpeed * Time.deltaTime, 0f);

        if (Input.GetKeyDown(key))
        {
            if (isOnButton)
            {
                Destroy(this.gameObject);
            }
        }
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Button")
        {
            isOnButton = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Button")
        {
            isOnButton = false;

        }
    }
}

