using _1Core.Scripts.Extensions;
using _1Core.Scripts.Map;
using UnityEngine;

namespace _1Core.Scripts.Balls
{
  public class BallPlayer : BaseBall
  {
    private Vector3 _startPos, _startScale;

    protected override void Start()
    {
      base.Start();
      _startPos = transform.position;
      _startScale = transform.localScale;

      EventBus.OnFixedUpdate += Move;
      EventBus.OnRestart += Restart;
    }

    private void OnDestroy()
    {
      EventBus.OnFixedUpdate -= Move;
      EventBus.OnRestart -= Restart;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!other.TryGetComponent(out FinishDoor finish)) return;
      EventBus.OnFinish.Invoke(true);
      gameObject.SetActive(false);
      base.Die();
    }

    private void Move()
    {
      _rb.linearVelocity = _rb.linearVelocity.WithZ(Stats.speed);
    }

    public void CheckScale()
    {
      if (!(transform.localScale.x < 0)) return;
      ScaleIn();
    }

    protected override void Die()
    {
      base.Die();
      gameObject.SetActive(false);
      EventBus.OnFinish?.Invoke(false);
    }

    private void Restart()
    {
      transform.position = _startPos;
      transform.localScale = _startScale;
      gameObject.SetActive(true);
    }
  }
}