using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    private Button buttn;
    public GameObject MenuCanvas;
    public GameObject RulesCanvas;
    void TaskOnClick()
    {
        switch (buttn.tag)
        {
            case "StartButton": SceneManager.LoadScene("MainScene"); break;
            case "RulesButton": RulesCanvas.SetActive(true); MenuCanvas.SetActive(false); break;
            case "BackButton": MenuCanvas.SetActive(true); RulesCanvas.SetActive(false); break;
            case "ExitButton": Application.Quit(); break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        buttn = GetComponent<Button>();
        buttn.onClick.AddListener(TaskOnClick);
    }
}
