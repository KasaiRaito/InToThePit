using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_StoreItem : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameTag;
    public TMP_Text priceTag;

    private UI_Store UI_Store;
    private GameControllerCore GameControllerCore;

    public void Start()
    {
        UI_Store = FindAnyObjectByType<UI_Store>();
        GameControllerCore = FindAnyObjectByType<GameControllerCore>();
    }

    public void Bind(ShopItem item, Sprite sprite)
    {
        nameTag.text = item.Name;
        //icon.sprite = sprite;
        icon.color = Color.white;

        GameControllerCore = FindAnyObjectByType<GameControllerCore>();

        Button button = this.gameObject.GetComponent<Button>();

        if (GameControllerCore.CheckIfOwned(item.Name))
        {
            priceTag.text = "Owned";
            button.interactable = false;
        }
        else
        {
            priceTag.text = "$" + item.Price;
            button.interactable = true;
            button.onClick.AddListener(BuyItem);
        }

        //shineShaderManager.SetTexture(sprite);
    }

    public void BuyItem()
    {
        if(GameControllerCore == null) return;

        GameControllerCore.PurchaseItemFromStore(int.Parse(priceTag.text.Replace("$", "")), nameTag.text);

        Button button = this.gameObject.GetComponent<Button>();
        priceTag.text = "Owned";
        button.interactable = false;
        button.onClick.RemoveAllListeners();
    }
}
