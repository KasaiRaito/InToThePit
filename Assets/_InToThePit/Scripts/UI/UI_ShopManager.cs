using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Collections;
using Firebase.Extensions;

[SerializeField] public class ShopItem
{
    public string ID;
    public string Name;
    public string Price;
    public string Description;
    public string IconPath;
    
    private DatabaseReference dbRef;
    private bool firebaseIsReady;
}
public class UI_ShopManager : MonoBehaviour
{
   public Transform container;
   public GameObject itemPrefab;
   private string path = "Store/Items/";

   private DatabaseReference databaseReference;
   private bool firebaseIsReady;

   private async void Awake()
   {
       var dependency = await FirebaseApp.CheckDependenciesAsync();

       if (dependency == DependencyStatus.Available)
       {
           databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
           firebaseIsReady = true;
       }

       else
       {
           Debug.Log("Firebase not available");
       }
   }

   public void LoadShop()
   {
       if (!firebaseIsReady)
       {
           return;
       }

       StartCoroutine(FillStore());
   } 
   
   private IEnumerator FillStore()
   {
       var task = databaseReference.Child(path).OrderByChild("Active").EqualTo(true).GetValueAsync();
       
       while (!task.IsCompleted)
           yield return null;

       if (task.IsFaulted)
       {
           Debug.LogError("Error loading store: " + task.Exception);
           yield break;
       }
       
       var snap = task.Result;

       if (!snap.Exists)
       {
           Debug.LogError("No store found");
           yield break;
       }

       Debug.Log(snap.ChildrenCount);

        foreach (var child in snap.Children)
       {
           var item = JsonUtility.FromJson<ShopItem>(child.GetRawJsonValue());
           item.ID = child.Key;
           
           var itemInstance = Instantiate(itemPrefab, container);
           UI_StoreItem storeItem = itemInstance.GetComponent<UI_StoreItem>();

           var icon = LoadLocalSprite(item.IconPath);
           storeItem.Bind(item,icon);
       }
   }

   Sprite LoadLocalSprite(string iconPath)
   {
       if (string.IsNullOrEmpty(iconPath))
       {
           return null;
       }

       return Resources.Load<Sprite>(iconPath);
   }
   
}
