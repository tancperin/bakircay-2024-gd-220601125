using UnityEngine;
using TMPro;

public class TopBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI PointText;
    private float time = 0f;
    private int points = 0;
    public int Points
    {
        get => points;
        set
        {
            points = value;
            PointText.text = "Points\n" + points;
        }
    }
    public float Timer
    {
        get => time;
    }
    void Update()
    {
        time += Time.deltaTime;
        timerText.text = "Time\n" + time.ToString("F0");
    }
}
