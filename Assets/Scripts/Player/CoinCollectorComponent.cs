using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CoinCollectorComponent : MonoBehaviour
{
    public const string CoinPrefsName = "Coins";

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            if (!PlayerPrefs.HasKey(CoinPrefsName))
            {
                PlayerPrefs.SetInt(CoinPrefsName, 1);
            } else
            {
                int coinNum = PlayerPrefs.GetInt(CoinPrefsName);
                PlayerPrefs.SetInt(CoinPrefsName, coinNum + 1);
            }
            coin.gameObject.SetActive(false);
        }
    }
}