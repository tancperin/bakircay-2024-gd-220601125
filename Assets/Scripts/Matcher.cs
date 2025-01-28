using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class Matcher : MonoBehaviour
{
    [SerializeField] TopBar topBar;
    [SerializeField] BottomBar bottomBar;
    [SerializeField] GameObject MatchEffect;
    [SerializeField] GameObject finishedPopup;
    [SerializeField] TextMeshProUGUI scoreText;
    GameObject item;
    public float rotateSpeed = 60f;
    public float sizeChangeSpeed = 1f;
    private Vector3 originalScale;
    private Vector3 maxScale, minScale;
    private float way = 1f, doublePointsTime = 0f;
    private int pointsForMatch, originalPointsForMatch = 10;

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            item.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        if (item != null)
        {
            item.transform.localScale = Vector3.Lerp(item.transform.localScale, item.transform.localScale + Vector3.one * way, sizeChangeSpeed * Time.deltaTime);
            if (item.transform.localScale.x >= maxScale.x)
            {
                way = -1f;
            }
            if (item.transform.localScale.x <= minScale.x)
            {
                way = 1f;
            }
        }

        if (doublePointsTime > 0)
        {
            doublePointsTime -= Time.deltaTime;
        }
        if (doublePointsTime <= 0)
        {
            pointsForMatch = originalPointsForMatch;
        }

    }

    //OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (item == null && other.gameObject.CompareTag("Item") && Variables.Object(other.gameObject).Get<bool>("isHold") == true)
        {
            item = other.gameObject;
            Variables.Object(item).Set("isMathing", true);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.GetComponent<Collider>().isTrigger = true;
            originalScale = item.transform.localScale;
            maxScale = originalScale * 1.1f;
            minScale = originalScale * 0.5f;
        }
        else if (item != null && other.gameObject.CompareTag("Item") && other.gameObject.name == item.name && other.gameObject != item)
        {
            Instantiate(MatchEffect, item.transform.position, Quaternion.identity);
            Destroy(item);
            Destroy(other.gameObject);
            item = null;
            topBar.Points += pointsForMatch;
            if (GameObject.FindGameObjectsWithTag("Item").Length == 2)
            {
                scoreText.text = "Score\n" + topBar.Points + "\nTime\n" + topBar.Timer.ToString("F0");
                finishedPopup.SetActive(true);
            }
        }
        else if (item != null && other.gameObject.CompareTag("Item") && other.gameObject.name != item.name && Variables.Object(other.gameObject).Get<bool>("isHold") == true)
        {
            Variables.Object(item).Set("isMathing", false);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Collider>().isTrigger = false;
            item.transform.localScale = originalScale;
            item.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)) * 10f, ForceMode.Impulse);
            item = other.gameObject;
            Variables.Object(item).Set("isMathing", true);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.GetComponent<Collider>().isTrigger = true;
            originalScale = item.transform.localScale;
            maxScale = originalScale * 1.1f;
            minScale = originalScale * 0.5f;
        }
    }

    //OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        // if (item != null && other.gameObject.CompareTag("Item") && other.gameObject == item)
        // {
        //     Variables.Object(item).Set("isMathing", false);
        //     item.GetComponent<Rigidbody>().useGravity = true;
        //     item.GetComponent<Collider>().isTrigger = false;
        //     item.transform.localScale = originalScale;
        //     item = null;
        // }

        if (item != null && other.gameObject.CompareTag("Item") && other.gameObject == item)
        {
            Variables.Object(item).Set("isMathing", false);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Collider>().isTrigger = false;
            item.transform.localScale = originalScale;
            item = null;
        }
    }

    public void DoublePoints(float time)
    {
        doublePointsTime = time;
        pointsForMatch *= 2;
    }
}
