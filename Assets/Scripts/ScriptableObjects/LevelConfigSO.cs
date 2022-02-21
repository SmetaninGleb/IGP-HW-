using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/Level Config")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] private List<OneLevelConfig> _levels;

    public List<OneLevelConfig> Levels => _levels;
    public const string PlayerPrefsLevelName = "LevelNumber";
}

[Serializable]
public class OneLevelConfig
{
    [SerializeField] public AudioClip LevelClip;
    [SerializeField] public bool UseClipTime = true;
    [SerializeField] public float LevelTimeInSeconds;
}