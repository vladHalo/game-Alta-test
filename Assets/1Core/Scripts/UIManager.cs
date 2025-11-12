using UnityEngine;
using UnityEngine.UI;

namespace _1Core.Scripts
{
  public class UIManager : MonoBehaviour
  {
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private Button _loseRestartButton;
    [SerializeField] private Button _winRestartButton;

    private void Start()
    {
      EventBus.OnFinish += HandleFinish;
      _loseRestartButton.onClick.AddListener(Restart);
      _winRestartButton.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
      EventBus.OnFinish -= HandleFinish;
      _loseRestartButton.onClick.RemoveAllListeners();
      _winRestartButton.onClick.RemoveAllListeners();
    }

    private void HandleFinish(bool isWin)
    {
      if (isWin) _winMenu.SetActive(true);
      else _loseMenu.SetActive(true);
    }

    private void Restart()
    {
      _loseMenu.SetActive(false);
      _winMenu.SetActive(false);
      EventBus.OnRestart?.Invoke();
    }
  }
}