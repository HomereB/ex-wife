using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public float TextBlinkTime = 1f;

    private Color _textColorON;
    private Color _textColorOFF;
    private bool _fadingIn;
    private float _timer;
    private float _tmpDeltaTime;

    void Start()
    {
        var textColor = this.GetComponent<TextMeshProUGUI>().color;
        _textColorON = new Color(textColor.r, textColor.g, textColor.b, 1);
        _textColorOFF = new Color(textColor.r, textColor.g, textColor.b, 0);

        _fadingIn = false;
        _timer = Time.time;
    }

    void Update()
    {
        _tmpDeltaTime = (Time.time - _timer) / TextBlinkTime;

        if (_fadingIn)
        {
            this.GetComponent<TextMeshProUGUI>().color = Color.Lerp(_textColorOFF, _textColorON, _tmpDeltaTime);
        }
        else
        {
            this.GetComponent<TextMeshProUGUI>().color = Color.Lerp(_textColorON, _textColorOFF, _tmpDeltaTime);
        }
        if (_tmpDeltaTime > 1)
        {
            _fadingIn = !_fadingIn;
            _timer = Time.time;
        }
    }
}
