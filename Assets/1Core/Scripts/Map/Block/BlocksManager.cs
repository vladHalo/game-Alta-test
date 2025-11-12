using System.Collections.Generic;
using _1Core.Scripts.Global;
using Lean.Pool;
using UnityEngine;

namespace _1Core.Scripts.Map.Block
{
  public class BlocksManager : MonoBehaviour
  {
    [SerializeField] private Transform _platform;
    [SerializeField] private Factory _factory;
    [SerializeField] private Vector2Int _rangeBlocks = new(15, 30);

    [SerializeField] private Color _changeColor;
    [SerializeField] private float _torqueForce, _upForce;

    private readonly Dictionary<GameObject, Block> _blocks = new();

    private void Start()
    {
      Spawn();
      EventBus.OnRestart += Restart;
    }

    private void OnDestroy()
    {
      EventBus.OnRestart -= Restart;
    }

    public void BreakBlock(GameObject blockObj)
    {
      if (_blocks.TryGetValue(blockObj, out var block)) block.Break();
    }

    private void Spawn()
    {
      var count = Random.Range(_rangeBlocks.x, _rangeBlocks.y);
      for (var i = 0; i < count; i++)
      {
        var scale = Random.Range(1f, 3f);
        var pos = GetPosition(scale);
        var block = _factory.Spawn<Transform>(PrefabType.Block, pos);
        block.localScale = new Vector3(scale, scale, scale);
        AddBlock(block.gameObject);
      }
    }

    private void AddBlock(GameObject blockObj)
    {
      if (_blocks.ContainsKey(blockObj)) return;
      var block = new Block(blockObj, _changeColor, _torqueForce, _upForce);
      _blocks.Add(blockObj, block);
    }

    private void Restart()
    {
      foreach (var block in _blocks.Values)
        if (block.IsBroken)
          block.Restore();

      LeanPool.DespawnAll();
      Spawn();
    }

    private Vector3 GetPosition(float scale)
    {
      var positionY = scale + scale / 2;
      var positionX = Random.Range(-8, 8);
      var positionZ = Random.Range(40, _platform.localScale.z - 30);

      return new Vector3(positionX, positionY, positionZ);
    }
  }
}