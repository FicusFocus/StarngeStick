using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Finish _finish;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _finishCanvas;

    private void OnEnable()
    {
        _finish.FinishPassed += OnFinishPassed;
    }

    private void OnDisable()
    {
        _finish.FinishPassed -= OnFinishPassed;
    }

    private void OnFinishPassed()
    {
        _finishCanvas.SetActive(true);
        _player.SetForvardSpeedValue(0);
    }
}
