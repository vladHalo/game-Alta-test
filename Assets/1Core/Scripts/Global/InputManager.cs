using System;
using UnityEngine;

namespace _1Core.Scripts.Global
{
  public class InputManager : MonoBehaviour
  {
    public Action<float> OnCharge;
    public Action OnShot;

    private void Awake()
    {
      EventBus.OnUpdate += InputData;
    }

    private void OnDestroy()
    {
      EventBus.OnUpdate -= InputData;
    }

    private void InputData()
    {
      if (Input.GetMouseButton(0))
      {
        var time = Time.deltaTime;
        OnCharge?.Invoke(time);
      }

      if (Input.GetMouseButtonUp(0)) OnShot?.Invoke();
    }
  }
}