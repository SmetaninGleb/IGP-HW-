using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private float _peakClip= 0.7f;
    [SerializeField] private float audioTimeStep = 0.1f;
    [SerializeField] private LevelConfigSO _levelConfig;
    [SerializeField] private MovingConfigSO _movingConfig;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private Platform _finishPlatform;
    [SerializeField] private float _coinDistance = 1f;
    [SerializeField] private float _coinHeight = 1f;
    [SerializeField] private Coin _coinPrefab;

    private void Start()
    {
        int levelIndex;
        if (!PlayerPrefs.HasKey(LevelConfigSO.PlayerPrefsLevelName))
        {
            PlayerPrefs.SetInt(LevelConfigSO.PlayerPrefsLevelName, 0);
            levelIndex = 0;
        } else
        {
            levelIndex = PlayerPrefs.GetInt(LevelConfigSO.PlayerPrefsLevelName);
        }
        if (_levelConfig == null || _levelConfig.Levels.Count == 0)
        {
            throw new System.Exception("Levels config is empty! Please add levels config");
        }
        AudioClip currentClip = _levelConfig.Levels[levelIndex].LevelClip;
        float levelTime = _levelConfig.Levels[levelIndex].LevelTimeInSeconds;
        if (_levelConfig.Levels[levelIndex].UseClipTime)
        {
            levelTime = currentClip.length;
        }
        //currentClip = PrepareClip(currentClip);
        List<float> peaksTimeList = ParseAudioClip(currentClip, levelTime);
        BuildMap(peaksTimeList);
    }

    private AudioClip PrepareClip(AudioClip clip)
    {
        float[] data = new float[clip.samples * clip.channels];
        clip.GetData(data, (int)(clip.frequency * 0f));
        float maxValue = 0;
        for (int i = 0; i < data.Length; i++)
        {
            maxValue = Mathf.Max(maxValue, data[i]);
        }
        for (int i = 0; i < data.Length; i++)
        {
            data[i] *= 1f / maxValue;
            data[i] = Mathf.Clamp(data[i], -1, 1);
        }
        clip.SetData(data, 0);
        return clip;
    }

    private List<float> ParseAudioClip(AudioClip clip, float endTime)
    {
        List<float> peaksTimeList = new List<float>();
        int hzStep = (int)(clip.frequency * audioTimeStep);
        float[] data = new float[clip.samples * clip.channels];
        clip.GetData(data, (int)(clip.frequency * 0f));
        float sampleTime = clip.length / clip.samples;
        int lastSample = (int)(endTime / sampleTime) - (int)(clip.frequency * 0f);
        if (clip.channels == 1)
        {
            for (int i = hzStep; i < lastSample - 1; i += hzStep)
            {
                if (data[i] - data[i - hzStep] > _peakClip)
                {
                    peaksTimeList.Add(i * sampleTime);
                }
            }
        }
        else if (clip.channels == 2)
        {
            for (int i = 2; i < lastSample - 2; i += 2)
            {
                if (data[i] > _peakClip && data[i] > data[i - 2] && data[i] > data[i + 2] && data[i + 1] > data[i + 3] && data[i + 1] > data[i - 1])
                {
                    peaksTimeList.Add(i * sampleTime);
                }
            }
        }
        return peaksTimeList;
    }

    private void BuildMap(List<float> peaksTimeList)
    {
        int nextDirectionIndex = 0;
        Vector3 nextDirection = _movingConfig.DirectionList[nextDirectionIndex];
        Vector3 nextSpawnPoint = Vector3.zero;
        for (int i = 0; i < peaksTimeList.Count; i++)
        {
            float time;
            if (i == 0)
            {
                time = peaksTimeList[i];
            } else
            {
                time = peaksTimeList[i] - peaksTimeList[i - 1];
            }
            float distance = time * _movingConfig.PlayerSpeed;
            Platform currentPlatform = Instantiate(_platformPrefab);
            SpawnCoinOnDistance(nextSpawnPoint, nextDirection, distance);
            currentPlatform.transform.rotation = Quaternion.LookRotation(nextDirection);
            Vector3 newScale = new Vector3(currentPlatform.transform.localScale.x, currentPlatform.transform.localScale.y, distance);
            currentPlatform.transform.localScale = newScale;
            currentPlatform.transform.position = nextSpawnPoint + nextDirection * (distance / 2f);
            nextSpawnPoint = currentPlatform.transform.position + nextDirection * (distance / 2f) - nextDirection * (_platformPrefab.transform.localScale.x / 2);
            nextDirectionIndex++;
            nextDirectionIndex %= _movingConfig.DirectionList.Count;
            nextDirection = _movingConfig.DirectionList[nextDirectionIndex];
            nextSpawnPoint += nextDirection * (_platformPrefab.transform.localScale.x / 2);
        }
        Platform finish = Instantiate(_finishPlatform);
        finish.transform.position = nextSpawnPoint + nextDirection * finish.transform.localScale.z / 2;
    }

    private void SpawnCoinOnDistance(Vector3 spawnPoint, Vector3 direction, float distance)
    {
        for (int i = 0; i < distance / _coinDistance - 1; i++)
        {
            Vector3 coinSpawnPoint = spawnPoint + direction * _coinDistance * i + Vector3.up * _coinHeight;
            Coin coin = Instantiate(_coinPrefab, coinSpawnPoint, _coinPrefab.transform.rotation);
        }
    }
}