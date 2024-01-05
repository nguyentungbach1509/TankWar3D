using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINavBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI energyText;

    private void Update()
    {
        goldText.text = PlayerPrefs.GetInt("PlayerGold").ToString();
        energyText.text = PlayerPrefs.GetInt("PlayerEnergy").ToString() + "/20";
    }
}
