using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public float initLife = 100;
    public GameObject lifeBar;

    void Start()
    {
        SetLifeBar(initLife);
    }

    void Update()
    {
        
    }

    public void SetLifeBar(float life)
    {
        var completLifeWidth = lifeBar.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        var currentLifeWidth = completLifeWidth * life / initLife;
        if (currentLifeWidth <= 0) currentLifeWidth = 0;
        lifeBar.transform.GetChild(0).GetChild(0).transform.GetComponent<RectTransform>().offsetMin = new Vector2(currentLifeWidth, lifeBar.transform.GetChild(0).GetChild(0).transform.GetComponent<RectTransform>().offsetMin.y);
    }
}
