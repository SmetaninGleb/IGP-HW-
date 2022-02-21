using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TapToStartButton : MonoBehaviour
{
    [SerializeField] private PlayerComponent _player;
    [SerializeField] private LevelConfigSO _levelConfig;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _player.GetComponent<HeadMoveComponent>().StartMoving();
        AudioSource asource = _player.GetComponent<AudioSource>();
        asource.clip = _levelConfig.Levels[PlayerPrefs.GetInt(LevelConfigSO.PlayerPrefsLevelName)].LevelClip;
        asource.Play();
        gameObject.SetActive(false);
    }
}