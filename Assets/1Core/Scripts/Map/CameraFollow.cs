using _1Core.Scripts.Extensions;
using UnityEngine;

namespace _1Core.Scripts.Map
{
  public class CameraFollow : MonoBehaviour
  {
    [SerializeField] private float _speed = 2, _offsetZ = 5;
    [SerializeField] private Transform _player;
    private Camera _camera;

    private Vector3 _startPos;

    private void Start()
    {
      _camera = Camera.main;
      _startPos = transform.position;
      EventBus.OnRestart += Restart;
    }

    private void LateUpdate()
    {
      var pos = _camera.transform.position.WithZ(_player.position.z - _offsetZ);
      _camera.transform.position = Vector3.Lerp(_camera.transform.position, pos, _speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
      EventBus.OnRestart -= Restart;
    }

    private void Restart()
    {
      transform.position = _startPos;
    }
  }
}