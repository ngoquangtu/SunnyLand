using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PermagnentUI : MonoBehaviour
{   public int cherries=0;
    public int healthAmount=5;
    public TextMeshProUGUI CherriesPoint;
    public TextMeshProUGUI Healthy;
    public static PermagnentUI perm;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(!perm)
        {
            perm=this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void ResetPoint()
    {
        cherries=0;
        CherriesPoint.text=cherries.ToString();
        healthAmount=5;
        Healthy.text=healthAmount.ToString();
    }

}
