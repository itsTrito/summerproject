using Player;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public MyPlayerController player;
    public Transform backGroundTransform;
    private Transform _transform;
    private Vector3 _camPos;
    private Vector3 _playerPos;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        var position = player.transform.position;
        _transform.position = position;
        backGroundTransform.position = position;
        _transform.Translate(0f,0f,-10f);
    }
}