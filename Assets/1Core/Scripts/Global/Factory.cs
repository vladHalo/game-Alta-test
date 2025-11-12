using System;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace _1Core.Scripts.Global
{
  [Serializable]
  public struct PrefabData
  {
    public Component prefab;
    public Transform parent;
  }

  public enum PrefabType
  {
    Ball,
    Block
  }

  public class Factory : MonoBehaviour
  {
    [SerializeField] private List<PrefabData> _prefabs = new();

    public T Spawn<T>(PrefabType type, Vector3 position, Quaternion rotation = default) where T : Component
    {
      var prefab = _prefabs[(int)type];
      var typedPrefab = prefab.prefab as T;
      return !typedPrefab ? null : LeanPool.Spawn(typedPrefab, position, rotation, prefab.parent);
    }
  }
}