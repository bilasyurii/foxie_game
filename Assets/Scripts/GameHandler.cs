using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Transform leftBound = null;
    [SerializeField] private Transform rightBound = null;

    public Transform LeftBound { get { return leftBound; } }
    public Transform RightBound { get { return rightBound; } }

    public static GameHandler instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
