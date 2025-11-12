using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1Core.Scripts.Map.Block
{
  public class Block
  {
    private readonly Transform _parent;
    private readonly Color _changeColor;
    private readonly float _torqueForce;
    private readonly float _upForce;
    private readonly Collider _collider;
    private readonly BlockPart[] _parts;

    public bool IsBroken => !_collider.enabled;

    public Block(GameObject parent, Color changeColor, float torqueForce, float upForce)
    {
      _parent = parent.transform;
      _changeColor = changeColor;
      _torqueForce = torqueForce;
      _upForce = upForce;
      _collider = _parent.GetComponent<Collider>();

      _parts = new BlockPart[_parent.childCount];

      for (var i = 0; i < _parent.childCount; i++)
      {
        var child = _parent.GetChild(i);
        _parts[i] = new BlockPart
        {
          mesh = child.GetComponent<MeshRenderer>(),
          rb = child.GetComponent<Rigidbody>(),
          startPos = child.localPosition,
          startRot = child.localEulerAngles
        };
      }
    }

    public void Break()
    {
      foreach (var part in _parts)
      {
        var rb = part.rb;
        rb.isKinematic = false;

        var randTorque = Random.Range(-_torqueForce, _torqueForce);
        rb.AddForce(Vector3.up * _upForce, ForceMode.Impulse);
        rb.AddTorque(Vector3.right * randTorque + Vector3.forward * randTorque, ForceMode.Impulse);
        rb.linearVelocity = Random.onUnitSphere * randTorque;

        part.mesh.material.color = _changeColor;
      }

      _collider.enabled = false;
      LeanPool.Despawn(_parent, 10);
    }

    public void Restore()
    {
      _collider.enabled = true;
      foreach (var part in _parts)
      {
        var rb = part.rb;
        rb.isKinematic = true;
        rb.transform.localPosition = part.startPos;
        rb.transform.localEulerAngles = part.startRot;
        part.mesh.material.color = Color.white;
      }
    }
  }
}