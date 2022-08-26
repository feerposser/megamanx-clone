using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthStatusBar : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthStatus;

    void Start()
    {
        healthStatus = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateHealthStatusBar(int value)
    {
        healthStatus.text = value.ToString();
    }
}
