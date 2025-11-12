using System.Collections;
using UnityEngine;

namespace _1Core.Scripts.Balls
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(SphereCollider))]
  public abstract class BaseBall : MonoBehaviour
  {
    private const string TagBlock = "Block";

    [field: SerializeField] protected Stats Stats { get; private set; }
    protected Rigidbody _rb;
    private Collider _сol;

    protected virtual void Start()
    {
      _rb = GetComponent<Rigidbody>();
      _сol = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag(TagBlock))
      {
        EventBus.OnHitBlock?.Invoke(collision.transform);
        HitAction();
        ScaleIn();
        _сol.enabled = false;
        return;
      }

      Jump();
    }

    protected virtual void Die()
    {
      _сol.enabled = true;
      _rb.linearVelocity = Vector3.zero;
      _rb.angularVelocity = Vector3.zero;
    }

    private void Jump()
    {
      var up = Vector3.up * Stats.height;
      _rb.AddForce(up, ForceMode.VelocityChange);
    }

    protected virtual void HitAction()
    {
    }

    protected void ScaleIn()
    {
      if (gameObject.activeSelf) StartCoroutine(ScaleInCoroutine());
    }

    private IEnumerator ScaleInCoroutine()
    {
      var value = Stats.downScale;
      while (transform.localScale.x > 0)
      {
        transform.localScale -= new Vector3(value, value, value);
        yield return new WaitForEndOfFrame();
      }

      Die();
    }
  }
}