using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;

public class UIManager : MonoBehaviour {

    public Button restartButton;
    public Button quitButton;

	public List<Button> buttons;
	public List<UnityAction> actions;

    // Use this for initialization
    void Start () {
        restartButton.onClick.AddListener(() => RestartLevel());
        quitButton.onClick.AddListener(() => Quit());

        gameObject.SetActive(false);
    }
    
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
    }

    void Destroy()
    {
        restartButton.onClick.RemoveListener(() => RestartLevel());
        quitButton.onClick.RemoveListener(() => Quit());

    }

    void Quit()
    {
        Application.Quit();
    }
}
