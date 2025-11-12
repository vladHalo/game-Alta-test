using _1Core.Scripts.Extensions;
using _1Core.Scripts.Global;
using _1Core.Scripts.Map;
using Lean.Pool;
using UnityEngine;

namespace _1Core.Scripts.Balls
{
  public class BallShootingController : MonoBehaviour
  {
    [SerializeField] private float _minPower = .2f;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private BallPlayer _ballPlayer;
    [SerializeField] private Platform _platform;
    [SerializeField] private Factory _factory;

    private BallShot _ballShot;

    private void Start()
    {
      _inputManager.OnCharge += Charge;
      _inputManager.OnShot += Shot;
    }

    private void OnDestroy()
    {
      _inputManager.OnCharge -= Charge;
      _inputManager.OnShot -= Shot;
    }

    private void Charge(float time)
    {
      if (!_ballPlayer.gameObject.activeSelf) return;
      _ballPlayer.CheckScale();
      _platform.UpdateScale(_ballPlayer.transform.localScale.x);
      var scaleValue = new Vector3(time, time, time);
      var playerTr = _ballPlayer.transform;

      if (!_ballShot)
      {
        var pos = BallShotPosition(playerTr);
        _ballShot = _factory.Spawn<BallShot>(PrefabType.Ball, pos);
        var minScaleVector = new Vector3(_minPower, _minPower, _minPower);
        _ballShot.transform.localScale = minScaleVector;
        playerTr.localScale -= minScaleVector / 2;
      }

      var ballTr = _ballShot.transform;

      playerTr.localScale -= scaleValue;
      ballTr.localScale += scaleValue;
      ballTr.position = BallShotPosition(playerTr);
    }

    private void Shot()
    {
      if (!_ballPlayer.gameObject.activeSelf || !_ballShot) return;
      _ballShot.Force();
      LeanPool.Despawn(_ballShot.gameObject, 10f);
      _ballShot = null;
    }

    private Vector3 BallShotPosition(Transform ball)
    {
      var startPosition = ball.localScale.x / 2 + .5f + ball.position.z;
      return ball.position.WithZ(startPosition);
    }
  }
}