using TMPro;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PointText;
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
}
