using UnityEngine;
using TMPro;
using Unity.Services.Multiplayer;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public TMP_Text sessioninfo_TMP;

    private void Start()
    {
        GetSessionInfo();
    }
    
    public void GetSessionInfo()
    {
        if (SessionMenagers.Instance == null)
        {
            Debug.LogError("SessionMenagers.Instance == NULL!");
            return;
        }

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
        Debug.Log("Leaving session...");

        if (SessionMenagers.Instance == null)
        {
            Debug.LogError("SessionMenagers.Instance == NULL!");
            SceneManager.LoadScene("MainMenu");
            return;
        }

        ISession session = SessionMenagers.Instance.CurrentSession;

        if (session != null)
        {
            try
            {
                await session.LeaveAsync();
                Debug.Log("Successfully left session.");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error while leaving session: {e}");
            }
            finally
            {
                SessionMenagers.Instance.ClearSession();
            }
        }

        SceneManager.LoadScene("MainMenu");
    }
}
