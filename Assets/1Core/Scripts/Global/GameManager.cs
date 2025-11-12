using _1Core.Scripts.Map.Block;
using UnityEngine;

namespace _1Core.Scripts.Global
{
  public class GameManager : MonoBehaviour
  {
    [SerializeField] private BlocksManager _blocksManager;

    private void Start()
    {
      EventBus.OnHitBlock += HandleBlockHit;
    }
    
    private void OnDestroy()
    {
      EventBus.OnHitBlock -= HandleBlockHit;
    }
    
    private void Update()
    {
      EventBus.OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
      EventBus.OnFixedUpdate?.Invoke();
    }

    private void HandleBlockHit(Transform block)
    {
      _blocksManager.BreakBlock(block.gameObject);
    }
  }
}