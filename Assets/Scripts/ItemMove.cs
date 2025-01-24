using Unity.VisualScripting;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    GameObject holdItem = null;
    public int IgnoreItemLayer = 6, IgnoreOtherLayer = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, ~((1 << IgnoreItemLayer) | (1 << IgnoreOtherLayer))) && holdItem != null)
        {
            holdItem.transform.position = new Vector3(hit.point.x, 2.5f, hit.point.z);
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (holdItem != null)
            {
                Variables.Object(holdItem).Set("isHold", false);
                if (Variables.Object(holdItem).Get<bool>("isMathing") == false) holdItem.GetComponent<Rigidbody>().useGravity = true;
                holdItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                holdItem.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                holdItem = null;
            }
            else if (Physics.Raycast(ray, out hit, 100.0f, ~(1 << IgnoreOtherLayer)) && hit.transform.gameObject.tag == "Item")
            {
                holdItem = hit.transform.gameObject;
                Variables.Object(holdItem).Set("isHold", true);
                holdItem.GetComponent<Rigidbody>().useGravity = false;
                holdItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                holdItem.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            }
        }
    }
}
