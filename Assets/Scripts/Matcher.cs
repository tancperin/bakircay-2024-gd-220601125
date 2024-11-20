using UnityEngine;

public class Matcher : MonoBehaviour
{
    GameObject item;
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
        if (item == null && other.gameObject.CompareTag("Item"))
        {
            item = other.gameObject;
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    //OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        if (item != null && other.gameObject.CompareTag("Item"))
        {
            item.GetComponent<Rigidbody>().isKinematic = false;
            item = null;
        }
    }
}
