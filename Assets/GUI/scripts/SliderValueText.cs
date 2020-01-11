using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    private Text _text;
    private Slider _slider;

    private void OnEnable()
    {
        _text = GetComponent<Text>();
        _slider = GetComponentInParent<Slider>();
    }

    private void Update()
    {
        _text.text = _slider.value.ToString();
    }
}
