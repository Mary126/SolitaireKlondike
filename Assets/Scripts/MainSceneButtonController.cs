using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainSceneButtonController : MonoBehaviour
{
    public Button buttn;
    void TaskOnClick()
    {
        switch (buttn.tag)
        {
            case "MenuButton": SceneManager.LoadScene("MenuScene"); break;
            case "RestartButton": SceneManager.LoadScene("MainScene"); break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        buttn = GetComponent<Button>();
        buttn.onClick.AddListener(TaskOnClick);
    }
}
