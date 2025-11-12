using Lean.Pool;
using UnityEngine;

namespace _1Core.Scripts.Balls
{
  public class BallShot : BaseBall
  {
    private readonly Collider[] _results = new Collider[15];

    private void OnEnable()
    {
      _rb ??= GetComponent<Rigidbody>();
      _rb.isKinematic = true;
    }

    public void Force()
    {
      _rb.isKinematic = false;
      _rb.AddForce(Vector3.forward * Stats.speed, ForceMode.VelocityChange);
    }

    protected override void Die()
    {
      base.Die();
      LeanPool.Despawn(gameObject);
    }

    protected override void HitAction()
    {
      if (Physics.OverlapSphereNonAlloc(transform.position, transform.localScale.x * 3, _results) == 0) return;
      foreach (var col in _results)
      {
        if (!col) return;
        EventBus.OnHitBlock?.Invoke(col.transform);
      }
    }
  }
}