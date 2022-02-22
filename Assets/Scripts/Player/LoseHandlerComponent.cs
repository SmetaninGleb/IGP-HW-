using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class LoseHandlerComponent : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LoseTrigger loseTrigger))
        {
            SceneManager.LoadScene(0);
        }
    }
}