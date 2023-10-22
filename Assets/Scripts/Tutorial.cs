using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
     [SerializeField] public GameObject TutorialPanel;
    // Start is called before the first frame update
    void Start()
    {
        TutorialPanel.SetActive(false);
    }
    public void tutorial()
    {
        TutorialPanel.SetActive(true);
    }
    public void QuitTutorial()
    {
        TutorialPanel.SetActive(false);
    }
}
