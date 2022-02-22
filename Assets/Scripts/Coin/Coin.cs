using UnityEngine;

[RequireComponent(typeof(CoinAnimation))]
[RequireComponent(typeof(BoxCollider))]
public class Coin : MonoBehaviour
{

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}