using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SessionMenagers : MonoBehaviour
{
    public static SessionMenagers Instance { get; private set; }
    public string sessionID, sessionName, sessionCode;

    public ISession CurrentSession { get; private set; }
    public TMP_Text sessionError;
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
    
    public void JoinSession(ISession session)
    {
        if (session == null)
        {
            Debug.LogError("JoinSession NULL!");
            return;
        }

        CurrentSession = session;
        
        Debug.Log($"Joined Session!");
        Debug.Log($"Session ID: {CurrentSession.Id}");
        Debug.Log($"Session Name: {CurrentSession.Name}");

        sessionID = CurrentSession.Id;
        sessionName = CurrentSession.Name;
        sessionCode = CurrentSession.Code;
        
        SceneManager.LoadScene("Lobby");
    }
    
    public void WriteData(ISession session)
    {
        Debug.Log(session);
    }
    
    public void ErrorMessage(SessionException sExepction)
    {
        Debug.Log(sExepction.Error);
        sessionError.text = $"There was a problem logging into the session! /n Error: {sExepction.Error}";
    }
    
    public void ClearSession()
    {
        CurrentSession = null;
    }
}