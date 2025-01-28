using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BottomBar : MonoBehaviour
{
    [SerializeField] private Matcher matcher;
    [SerializeField] private Component[] scriptsToPause;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject allSameSkillButton, doublePointsSkillButton, hintSkillButton, shakeSkillButton;
    [SerializeField] private GameObject allSameSkillEffect, doublePointsSkillEffect, hintSkillEffect, shakeSkillEffect;
    [SerializeField] private Spawn spawn;
    [SerializeField] private string mainMenu;
    [SerializeField] private string gameArea;
    [SerializeField] private float allSameSkillButtonCooldown = 5f, doublePointsSkillButtonCooldown = 5f, hintSkillButtonCooldown = 5f, shakeSkillButtonCooldown = 5f;
    private float allSameSkillButtonCooldownTimer = 0, doublePointsSkillButtonCooldownTimer = 0, hintSkillButtonCooldownTimer = 0, shakeSkillButtonCooldownTimer = 0;
    private bool isPaused = false, isDoublePoints = false;

    private void Update()
    {
        // if (BasicSkillCooldownTimer > 0)
        // {
        //     BasicSkillCooldownTimer -= Time.deltaTime;
        // }
        // if (BasicSkillCooldownTimer < 0)
        // {
        //     BasicSkillCooldownTimer = 0;
        // }
        // if (BasicSkillCooldownTimer == 0)
        // {
        //     basicSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        // }
        // else
        // {
        //     basicSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        // }

        if (allSameSkillButtonCooldownTimer > 0)
        {
            allSameSkillButtonCooldownTimer -= Time.deltaTime;
        }
        if (allSameSkillButtonCooldownTimer < 0)
        {
            allSameSkillButtonCooldownTimer = 0;
        }
        if (allSameSkillButtonCooldownTimer == 0)
        {
            allSameSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            allSameSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (doublePointsSkillButtonCooldownTimer > 0)
        {
            doublePointsSkillButtonCooldownTimer -= Time.deltaTime;
            if (isDoublePoints && (doublePointsSkillButtonCooldownTimer % 0.5f < 0.01f))
            {
                DoublePointsAction();
            }
        }
        if (doublePointsSkillButtonCooldownTimer < 0)
        {
            doublePointsSkillButtonCooldownTimer = 0;
        }
        if (doublePointsSkillButtonCooldownTimer == 0)
        {
            doublePointsSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
            isDoublePoints = false;
        }
        else
        {
            doublePointsSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        // if (isDoublePoints)
        // {
        //     if (doublePointsActionTimer < 0.5f)
        //     {
        //         doublePointsActionTimer += Time.deltaTime;
        //     }
        //     else
        //     {
        //         DoublePointsAction();
        //         doublePointsActionTimer = 0;
        //     }
        // }

        if (hintSkillButtonCooldownTimer > 0)
        {
            hintSkillButtonCooldownTimer -= Time.deltaTime;
        }
        if (hintSkillButtonCooldownTimer < 0)
        {
            hintSkillButtonCooldownTimer = 0;
        }
        if (hintSkillButtonCooldownTimer == 0)
        {
            hintSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            hintSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (shakeSkillButtonCooldownTimer > 0)
        {
            shakeSkillButtonCooldownTimer -= Time.deltaTime;
        }
        if (shakeSkillButtonCooldownTimer < 0)
        {
            shakeSkillButtonCooldownTimer = 0;
        }
        if (shakeSkillButtonCooldownTimer == 0)
        {
            shakeSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            shakeSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
    public void OnMenuButtonClicked()
    {
        if (isPaused) return;
        isPaused = true;
        Time.timeScale = 0;
        foreach (var script in scriptsToPause)
        {
            if (script is MonoBehaviour)
            {
                ((MonoBehaviour)script).enabled = false;
            }
        }
        pauseMenu.SetActive(true);
    }

    public void OnResumeButtonClicked()
    {
        isPaused = false;
        Time.timeScale = 1;
        foreach (var script in scriptsToPause)
        {
            if (script is MonoBehaviour)
            {
                ((MonoBehaviour)script).enabled = true;
            }
        }
        pauseMenu.SetActive(false);
    }

    public void OnRetryButtonClicked()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameArea);
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu);
    }

    // public void OnBasicSkillButtonClicked()
    // {
    //     if (isPaused) return;
    //     Instantiate(basicSkillEffect, new Vector3(0, 2, 0), Quaternion.identity);
    //     GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
    //     foreach (GameObject item in items)
    //     {
    //         Rigidbody rb = item.GetComponent<Rigidbody>();
    //         Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
    //         rb.AddForce(direction * 500);
    //     }
    //     BasicSkillCooldownTimer = BasicSkillCooldown;
    // }
    

    // public void RefreshItemsSkill()
    // {
    //     Instantiate(refreshEffect, new Vector3(0, 2, 0), Quaternion.identity);
    //     GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
    //     int count = items.Length;
    //     foreach (GameObject item in items) Destroy(item);
    //     spawn.RespwanItems(count);
    // }

    // public void HintSkill()
    // {
    //     GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
    //     int selected = Random.Range(0, items.Length);
    //     Instantiate(hintSkillEffect, items[selected].transform.position, Quaternion.identity, items[selected].transform);
    //     foreach (GameObject item in items)
    //     {
    //         if (item == items[selected]) continue;
    //         if (item.name == items[selected].name)
    //         {
    //             Instantiate(hintSkillEffect, item.transform.position, Quaternion.identity, item.transform);
    //             break;
    //         }
    //     }
    // }

    // public void MakeAllSameSkill()
    // {
    //     GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
    //     GameObject selected = items[Random.Range(0, items.Length)];
    //     foreach (GameObject item in items)
    //     {
    //         Vector3 position = item.transform.position;
    //         Quaternion rotation = item.transform.rotation;
    //         Instantiate(allSameSkillEffect, position, rotation, item.transform);
    //         Destroy(item);
    //         GameObject newItem = Instantiate(selected, position, rotation);
    //         newItem.GetComponent<Rigidbody>().isKinematic = false;
    //     }
    // }

    // public void SelectAdvancedSkill()
    // {
    //     int posibility = Random.Range(0, 100);
    //     if (posibility < 4)
    //     {
    //         skill = 0;
    //         advancedSkillText.text = "All Same";
    //     }
    //     else if (posibility > 48)
    //     {
    //         skill = 1;
    //         advancedSkillText.text = "Refresh";
    //     }
    //     else
    //     {
    //         skill = 2;
    //         advancedSkillText.text = "Hint";
    //     }
    //     advancedSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    // }

    // public void OnAdvancedSkillButtonClicked()
    // {
    //     if (isPaused) return;
    //     if (skill == 0)
    //     {
    //         MakeAllSameSkill();
    //     }
    //     else if (skill == 1)
    //     {
    //         RefreshItemsSkill();
    //     }
    //     else
    //     {
    //         HintSkill();
    //     }
    //     advancedSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    //     advancedSkillText.text = "";
    // }

    public void OnAllSameSkillButtonClicked()
    {
        if (isPaused) return;
        Instantiate(allSameSkillEffect, new Vector3(0, 2, 0), Quaternion.identity);
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject selected = items[Random.Range(0, items.Length)];
        foreach (GameObject item in items)
        {
            Vector3 position = item.transform.position;
            Quaternion rotation = item.transform.rotation;
            Instantiate(allSameSkillEffect, position, rotation, item.transform);
            Destroy(item);
            GameObject newItem = Instantiate(selected, position, rotation);
            newItem.GetComponent<Rigidbody>().isKinematic = false;
        }
        allSameSkillButtonCooldownTimer = allSameSkillButtonCooldown;
    }

    public void OnDoublePointsSkillButtonClicked()
    {
        if (isPaused) return;
        isDoublePoints = true;
        doublePointsSkillButtonCooldownTimer = doublePointsSkillButtonCooldown;
        matcher.DoublePoints(5f);
    }

    public void DoublePointsAction()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            if (Variables.Object(item).Get<bool>("isMathing") || Variables.Object(item).Get<bool>("isHold")) continue;
            Rigidbody rb = item.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
            Transform tr = new GameObject().transform;
            tr.position = item.transform.position;
            tr.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            // Instantiate(doublePointsSkillEffect, item.transform.position, Quaternion.identity, tr);
            rb.AddForce(direction * 100);
        }
    }

    public void OnHintSkillButtonClicked()
    {
        if (isPaused) return;
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        int selected = Random.Range(0, items.Length);
        Instantiate(hintSkillEffect, items[selected].transform.position, Quaternion.identity, items[selected].transform);
        foreach (GameObject item in items)
        {
            if (item == items[selected]) continue;
            if (item.name == items[selected].name)
            {
                Instantiate(hintSkillEffect, item.transform.position, Quaternion.identity, item.transform);
                break;
            }
        }
        hintSkillButtonCooldownTimer = hintSkillButtonCooldown;
    }

    public void OnShakeSkillButtonClicked()
    {
        if (isPaused) return;
        // Instantiate(shakeSkillEffect, new Vector3(0, 2, 0), Quaternion.identity);
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            if (Variables.Object(item).Get<bool>("isMathing") || Variables.Object(item).Get<bool>("isHold")) continue;
            Rigidbody rb = item.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
            rb.AddForce(direction * 500);
        }
        shakeSkillButtonCooldownTimer = shakeSkillButtonCooldown;
    }
}
