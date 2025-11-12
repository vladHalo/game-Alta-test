using System;
using UnityEngine;

public static class EventBus
{
  public static Action OnUpdate;
  public static Action OnFixedUpdate;
  public static Action<bool> OnFinish;
  public static Action OnRestart;
  
  public static Action<Transform> OnHitBlock;
}