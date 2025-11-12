using UnityEngine;

namespace _1Core.Scripts.Extensions
{
  public static class VectorExtensions
  {
    #region Component Modification

    public static Vector3 WithX(this Vector3 v, float x)
    {
      return new Vector3(x, v.y, v.z);
    }

    public static Vector3 WithZ(this Vector3 v, float z)
    {
      return new Vector3(v.x, v.y, z);
    }

    #endregion
  }
}