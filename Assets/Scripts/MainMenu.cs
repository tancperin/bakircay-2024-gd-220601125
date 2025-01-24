using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string GameSceneName = "GameArea";
    
    public void onPair10ButtonClicked()
    {
        GameMode.gameMode = "Pair10";
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameSceneName);
    }

    public void onPair25ButtonClicked()
    {
        GameMode.gameMode = "Pair25";
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameSceneName);
    }

    public void onPair40ButtonClicked()
    {
        GameMode.gameMode = "Pair40";
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameSceneName);
    }

    public void onUnlimitedButtonClicked()
    {
        GameMode.gameMode = "Unlimited";
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameSceneName);
    }
}
