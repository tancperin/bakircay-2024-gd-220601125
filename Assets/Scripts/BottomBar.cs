using UnityEngine;
using TMPro;

public class BottomBar : MonoBehaviour
{
    [SerializeField] private Component[] scriptsToPause;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject basicSkillButton;
    [SerializeField] private GameObject advancedSkillButton;
    [SerializeField] private GameObject hintEffect;
    [SerializeField] private GameObject basicSkillEffect;
    [SerializeField] private GameObject makeAllSameEffect;
    [SerializeField] private GameObject refreshEffect;
    [SerializeField] private TextMeshProUGUI advancedSkillText;
    [SerializeField] private Spawn spawn;
    [SerializeField] private string mainMenu;
    [SerializeField] private string gameArea;
    [SerializeField] private float BasicSkillCooldown = 5f;
    private float BasicSkillCooldownTimer = 0f;
    private bool isPaused = false;
    private int skill = 0;

    private void Update()
    {
        if (BasicSkillCooldownTimer > 0)
        {
            BasicSkillCooldownTimer -= Time.deltaTime;
        }
        if (BasicSkillCooldownTimer < 0)
        {
            BasicSkillCooldownTimer = 0;
        }
        if (BasicSkillCooldownTimer == 0)
        {
            basicSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            basicSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
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

    public void OnBasicSkillButtonClicked()
    {
        if (isPaused) return;
        Instantiate(basicSkillEffect, new Vector3(0, 2, 0), Quaternion.identity);
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
            rb.AddForce(direction * 500);
        }
        BasicSkillCooldownTimer = BasicSkillCooldown;
    }

    public void RefreshItemsSkill()
    {
        Instantiate(refreshEffect, new Vector3(0, 2, 0), Quaternion.identity);
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        int count = items.Length;
        foreach (GameObject item in items) Destroy(item);
        spawn.RespwanItems(count);
    }

    public void HintSkill()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        int selected = Random.Range(0, items.Length);
        Instantiate(hintEffect, items[selected].transform.position, Quaternion.identity, items[selected].transform);
        foreach (GameObject item in items)
        {
            if (item == items[selected]) continue;
            if (item.name == items[selected].name)
            {
                Instantiate(hintEffect, item.transform.position, Quaternion.identity, item.transform);
                break;
            }
        }
    }

    public void MakeAllSameSkill()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject selected = items[Random.Range(0, items.Length)];
        foreach (GameObject item in items)
        {
            Vector3 position = item.transform.position;
            Quaternion rotation = item.transform.rotation;
            Instantiate(makeAllSameEffect, position, rotation, item.transform);
            Destroy(item);
            GameObject newItem = Instantiate(selected, position, rotation);
            newItem.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void SelectAdvancedSkill()
    {
        int posibility = Random.Range(0, 100);
        if (posibility < 4)
        {
            skill = 0;
            advancedSkillText.text = "All Same";
        }
        else if (posibility > 48)
        {
            skill = 1;
            advancedSkillText.text = "Refresh";
        }
        else
        {
            skill = 2;
            advancedSkillText.text = "Hint";
        }
        advancedSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public void OnAdvancedSkillButtonClicked()
    {
        if (isPaused) return;
        if (skill == 0)
        {
            MakeAllSameSkill();
        }
        else if (skill == 1)
        {
            RefreshItemsSkill();
        }
        else
        {
            HintSkill();
        }
        advancedSkillButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        advancedSkillText.text = "";
    }
}
