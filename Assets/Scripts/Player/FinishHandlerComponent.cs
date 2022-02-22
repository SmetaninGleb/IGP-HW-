using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class FinishHandlerComponent : MonoBehaviour
{
    [SerializeField] private LevelConfigSO _levelConfig;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FinishTrigger finish))
        {
            int levelIndex = PlayerPrefs.GetInt(LevelConfigSO.PlayerPrefsLevelName);
            levelIndex = (levelIndex + 1) % _levelConfig.Levels.Count;
            PlayerPrefs.SetInt(LevelConfigSO.PlayerPrefsLevelName, levelIndex);
            SceneManager.LoadScene(0);
        }
    }
}