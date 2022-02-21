using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingConfig", menuName = "Config/Moving Config")]
public class MovingConfigSO : ScriptableObject
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private List<Vector3> _directionList = new List<Vector3>() { Vector3.forward, Vector3.left };

    public float PlayerSpeed => _playerSpeed;
    public List<Vector3> DirectionList => _directionList;
}