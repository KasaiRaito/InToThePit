using System;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class DataLoad : MonoBehaviour
{
    DatabaseReference databaseReference;
    FirebaseAuth firebaseAuth;

    private void Start()
    {
        firebaseAuth = FirebaseAuth.DefaultInstance;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        if (firebaseAuth == null)
        {
            Debug.LogError("No firebase auth NOT found");
            return;
        }
        
        string uid = firebaseAuth.CurrentUser.UserId;
        LoadMyData(uid);
    }

    private void LoadMyData(string uid)
    {
        databaseReference.Child("users").Child(uid).GetValueAsync().ContinueWithOnMainThread
        (task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Load Failled:" + task.Exception);
                }

                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string json = snapshot.GetRawJsonValue();
                    Debug.Log(json);
                }
                else
                {
                    Debug.LogError($"{uid} data was not found");
                }
            }
        );
    }
}
