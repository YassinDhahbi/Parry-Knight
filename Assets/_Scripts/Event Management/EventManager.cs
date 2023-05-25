using UnityEngine;
using ScriptableObjectArchitecture;
[CreateAssetMenu(fileName = "Event Manager", menuName = "Managers/Event Manager")]
public class EventManager : ScriptableObjectSingleton<EventManager>
{
    // public static EventManager Instance;
    // private void OnEnable()
    // {
    //     Instance = Resources.FindObjectsOfTypeAll<EventManager>()[0];
    // }

    public GameEvent OnProjectileDamage;
    public GameEvent OnBulletPlayerCollision;

    public GameEvent OnJump;
    public GameEvent OnGameStart;
    public GameEvent OnOutOfTime;
    public GameEvent OnPlayerMove;
    public GameObjectGameEvent OnPlayerItemPickup;
    public GameObjectGameEvent OnItemSelectInInventory;
    public GameObjectGameEvent OnItemSelectInCraftingMenu;





    public GameEvent OnInventoryOpenClose;

    [ContextMenu("Start game")]
    public void StartGame()
    {
        Instance.OnGameStart.Raise();
    }
    [ContextMenu("Select Craft Item")]
    public void PressItemInCrafting()
    {
        Instance.OnItemSelectInCraftingMenu.Raise();
    }
}