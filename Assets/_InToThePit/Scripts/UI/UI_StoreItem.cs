using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_StoreItem : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameTag;
    public TMP_Text priceTag;
    
    public ShineShaderManager shineShaderManager;

    public void Bind(ShopItem item, Sprite sprite)
    {
        nameTag.text = item.Name;
        priceTag.text = "$" + item.Price;
        //icon.sprite = sprite;
        icon.color = Color.white;
        
        shineShaderManager.SetTexture(sprite);
    }
}
