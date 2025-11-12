using _1Core.Scripts.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1Core.Scripts.Map
{
  public class Platform : MonoBehaviour
  {
    [SerializeField] private Transform _finishPlatform;
    [SerializeField] private Vector2 _minMaxWidth;

    private Vector3 _startScale;

    protected void Awake()
    {
      _startScale = transform.localScale;
      Restart();
      EventBus.OnRestart += Restart;
    }

    private void OnDestroy()
    {
      EventBus.OnRestart -= Restart;
    }

    public void UpdateScale(float value)
    {
      transform.localScale = transform.localScale.WithX(value);
    }

    private void Restart()
    {
      var width = Random.Range(_minMaxWidth.x, _minMaxWidth.y);
      transform.localScale = new Vector3(_startScale.x, 1, width);
      transform.position = new Vector3(transform.position.x, transform.position.y, width / 2 - .5f);

      var finishPosition = width / 2 + _finishPlatform.localScale.z / 2 + transform.position.z;
      _finishPlatform.position = new Vector3(_finishPlatform.position.x, _finishPlatform.position.y, finishPosition);
    }
  }
}