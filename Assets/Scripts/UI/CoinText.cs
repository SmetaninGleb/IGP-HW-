using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CoinText : MonoBehaviour
{
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _text.text = PlayerPrefs.GetInt(CoinCollectorComponent.CoinPrefsName).ToString();
    }
}