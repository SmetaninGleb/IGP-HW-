using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinishTrigger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}