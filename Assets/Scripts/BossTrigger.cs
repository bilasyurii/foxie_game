using UnityEngine;
using UnityEngine.Events;

public class BossTrigger : MonoBehaviour
{
    public UnityEvent OnFightEntered;

    private void Awake()
    {
        if (OnFightEntered == null)
            OnFightEntered = new UnityEvent();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            OnFightEntered.Invoke();
            Destroy(gameObject);
        }
    }
}
