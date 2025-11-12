using UnityEngine;

namespace _1Core.Scripts.Map
{
  [RequireComponent(typeof(BoxCollider))]
  public class FinishDoor : MonoBehaviour
  {
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private Transform _player;

    private Quaternion _closedRotOne, _closedRotTwo;
    private Transform _doorOne;
    private Transform _doorTwo;
    private Quaternion _openRotOne, _openRotTwo;

    private void Start()
    {
      _doorOne = transform.GetChild(0);
      _doorTwo = transform.GetChild(1);
      _closedRotOne = _doorOne.localRotation;
      _closedRotTwo = _doorTwo.localRotation;

      _openRotOne = _closedRotOne * Quaternion.Euler(0, _openAngle, 0);
      _openRotTwo = _closedRotTwo * Quaternion.Euler(0, -_openAngle, 0);

      EventBus.OnRestart += ResetDoors;
      EventBus.OnUpdate += UpdateDoors;
    }

    private void OnDestroy()
    {
      EventBus.OnRestart -= ResetDoors;
      EventBus.OnUpdate -= UpdateDoors;
    }

    private void ResetDoors()
    {
      _doorOne.localRotation = _closedRotOne;
      _doorTwo.localRotation = _closedRotTwo;
    }

    private void UpdateDoors()
    {
      var isNear = Mathf.Abs(transform.position.z - _player.position.z) <= _distance;
      var rotationSpeed = _speed * 90f * Time.deltaTime;

      _doorOne.localRotation = Quaternion.RotateTowards(
        _doorOne.localRotation,
        isNear ? _openRotOne : _closedRotOne,
        rotationSpeed
      );

      _doorTwo.localRotation = Quaternion.RotateTowards(
        _doorTwo.localRotation,
        isNear ? _openRotTwo : _closedRotTwo,
        rotationSpeed
      );
    }
  }
}