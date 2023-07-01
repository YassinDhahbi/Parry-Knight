using UnityEngine;
using Tagging;
using UnityEngine.Events;

public class CollisionTriggerResponse : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnTriggerEnter;

    [SerializeField]
    private UnityEvent OnTriggerExit;

    [SerializeField]
    private UnityEvent<GameObject> OnCollisonEnter;

    [SerializeField]
    private Tagger targetTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckTag(other.gameObject, OnTriggerEnter);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CheckTag(other.gameObject, OnTriggerExit);
    }

    private void CheckTag(GameObject other, UnityEvent unityEvent)
    {
        if (other.TryGetComponent(out TagManager tagManager))
        {
            if (tagManager.tagList.Contains(targetTag))
            {
                unityEvent.Invoke();
                OnCollisonEnter?.Invoke(other.gameObject);
            }
        }
    }
}