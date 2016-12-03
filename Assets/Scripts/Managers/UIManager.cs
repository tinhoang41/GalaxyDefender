using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class UIManager : MonoBehaviour {

    public List<Button> buttons;
    public List<UnityAction> actions;

    // Use this for initialization
    public virtual void Start ()
    {
        SetUpActions();
        AddListeners();
    }

    protected abstract void SetUpActions();

    void AddListeners()
    {
        for(int i = 0; i < buttons.Count; i++)
            buttons[i].onClick.AddListener(actions[i]);
    }

    void RemoveListeners()
    {
        for (int i = 0; i < buttons.Count; i++)
            if(!buttons[i].IsDestroyed())
                buttons[i].onClick.RemoveListener(actions[i]);
    }


    void Destroy()
    {
        RemoveListeners();
    }
}
