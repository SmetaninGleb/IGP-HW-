using System.Collections;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private float _angleSpeed = 60f;
    [SerializeField] private float _verticalMovingRange = 0.2f;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        FrameRotation();
        FrameMove();
    }

    private void FrameRotation()
    {
        Vector3 curRot = transform.rotation.eulerAngles;
        Vector3 newRotation = new Vector3(curRot.x, curRot.y + _angleSpeed * Time.deltaTime, curRot.z);
        transform.rotation = Quaternion.Euler(newRotation);
    }

    private void FrameMove()
    {
        Vector3 newPos = _startPos + Vector3.up * (Mathf.Sin(Time.timeSinceLevelLoad) * _verticalMovingRange);
        transform.position = newPos;
    }
}