using UnityEngine;
using TMPro;
using Unity.Services.Multiplayer;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public TMP_Text sessioninfo_TMP;
    public ISession CurrentSession { get; private set; }

    private void Start()
    {
        var session = SessionMenagers.Instance.CurrentSession;
        CurrentSession = session;
        GetSessionInfo();
    }
    
    public void GetSessionInfo()
    {
        if (SessionMenagers.Instance.CurrentSession == null)
        {
            Debug.LogError("CurrentSession == NULL!");
            return;
        }

        var session = SessionMenagers.Instance.CurrentSession;
        
        string sessionName = session.Name;
        string sessionCode = session.Code;

        if (sessioninfo_TMP != null)
        {
            sessioninfo_TMP.text = $"SESSION NAME: {sessionName}\nSESSION CODE: {sessionCode}";
        }
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
