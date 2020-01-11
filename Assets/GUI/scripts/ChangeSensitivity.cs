using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivity : MonoBehaviour
{
    public Slider horizontal, vertical;
    public PlayerStats stats;

    private void Awake()
    {
        horizontal.value = stats.horizSensitivity;
        vertical.value = stats.vertSensitivity;
    }

    public void OnChangeSlider()
    {
        stats.horizSensitivity = horizontal.value;
        stats.vertSensitivity = vertical.value;
    }
}
