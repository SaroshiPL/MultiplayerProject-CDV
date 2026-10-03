using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionMenagers : MonoBehaviour
{
    public static SessionMenagers Instance { get; private set; }

    public ISession CurrentSession { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    public void JoinSession(ISession session)
    {
        if (session == null)
        {
            Debug.LogError("JoinSession otrzymało NULL!");
            return;
        }

        CurrentSession = session;

        Debug.Log($"Joined Session!");
        Debug.Log($"Session ID: {CurrentSession.Id}");
        Debug.Log($"Session Name: {CurrentSession.Name}");

        SceneManager.LoadScene("Lobby");
    }

    public async void LeaveSession()
    {
        if (CurrentSession != null)
        {
            await CurrentSession.LeaveAsync();
            CurrentSession = null;
        }

        SceneManager.LoadScene("MainMenu");
    }
}