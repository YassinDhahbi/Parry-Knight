using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "Event Manager", menuName = "Managers/Event Manager")]
public class EventManager : ScriptableObjectSingleton<EventManager>
{
    #region No Parameter GameEvents

    public GameEvent OnGameStart;
    public GameEvent OnShieldBlock;
    public GameEvent OnShieldRaise;
    public GameEvent OnPlayerDeath;
    public GameEvent OnPerfectBlock;
    public GameEvent OnPlayerLevelUP;
    public GameEvent OnGameLost;
    public GameEvent OnGameWon;
    public AudioClipGameEvent OnSfxPlay;

    #endregion No Parameter GameEvents

    #region Parametered GameEvents

    public IntGameEvent OnBlockingExpCollected;
    public FloatGameEvent OnProjectileDamageTaken;
    public GameObjectGameEvent OnPlayerItemPickup;

    #endregion Parametered GameEvents

    [ContextMenu("Start game")]
    public void StartGame()
    {
        Instance.OnGameStart.Raise();
    }
}