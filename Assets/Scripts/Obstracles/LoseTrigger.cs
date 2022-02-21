using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LoseTrigger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}