using UnityEngine;

public class Matcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Item"))
        {
            otherGameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    //OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Item"))
        {
            otherGameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
