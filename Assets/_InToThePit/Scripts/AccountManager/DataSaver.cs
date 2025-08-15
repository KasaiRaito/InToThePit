using System.Collections;
using UnityEngine;
using System;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;   // for ContinueWithOnMainThread

[Serializable]
public class dataToSave
{
    public string userName;
    public int totalCoins;
    public int crrLevel;
    public int highScore;
}

public class DataSaver : MonoBehaviour
{
    public dataToSave dts;

    private DatabaseReference dbRef;
    private FirebaseAuth auth;
    private string userId;

    // Initialize Firebase, ensure a user exists, then set up DB
    private async void Awake()
    {
        var deps = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (deps != DependencyStatus.Available)
        {
            Debug.LogError("Firebase dependencies not available: " + deps);
            return;
        }

        auth = FirebaseAuth.DefaultInstance;

        // Auto sign in anonymously if no cached user
        if (auth.CurrentUser == null)
        {
            Debug.Log("No user found. Signing in anonymously�");
            try
            {
                await auth.SignInAnonymouslyAsync();
            }
            catch (Exception e)
            {
                Debug.LogError("Anonymous sign-in failed: " + e);
                return;
            }
        }

        userId = auth.CurrentUser.UserId;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log("Firebase ready. Current UID = " + userId);

        // Ensure dts exists so Save doesn't serialize null
        if (dts == null) dts = new dataToSave();
    }

    public void SaveDataFn()
    {
        if (!IsReady()) return;

        string json = JsonUtility.ToJson(dts);
        dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled) { Debug.LogError("Save canceled."); return; }
                if (task.IsFaulted) { Debug.LogError("Save failed: " + task.Exception); return; }
                Debug.Log("Save successful for UID: " + userId);
            });
    }

    public void LoadDataFn()
    {
        if (!IsReady()) return;
        StartCoroutine(LoadDataEnum());
    }

    private IEnumerator LoadDataEnum()
    {
        var op = dbRef.Child("users").Child(userId).GetValueAsync();
        yield return new WaitUntil(() => op.IsCompleted);

        if (op.IsFaulted)
        {
            Debug.LogError("Load failed: " + op.Exception);
            yield break;
        }

        var snapshot = op.Result;
        var jsonData = snapshot.GetRawJsonValue();

        if (!string.IsNullOrEmpty(jsonData))
        {
            dts = JsonUtility.FromJson<dataToSave>(jsonData);
            Debug.Log("Server data loaded for UID: " + userId);
        }
        else
        {
            Debug.Log("No data found for UID: " + userId);
        }
    }

    private bool IsReady()
    {
        if (auth == null || dbRef == null)
        {
            Debug.LogError("Firebase not initialized yet.");
            return false;
        }
        if (auth.CurrentUser == null)
        {
            Debug.LogError("No authenticated user. (Did sign-in fail?)");
            return false;
        }
        if (string.IsNullOrEmpty(userId))
        {
            userId = auth.CurrentUser.UserId; // refresh if needed
        }
        return true;
    }
}