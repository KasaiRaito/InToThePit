using UnityEngine;

public class ShineShaderManager : MonoBehaviour
{
    public void SetTexture(Sprite texture)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer.material.shader = Shader.Find("Sprite Unlit"))
        {
            renderer.material.SetTexture("_MainTexture", texture.texture);
            
            Debug.Log(renderer.material.shader.name);
        }
        else
        {
            Debug.LogError(renderer.material.shader.name);
        }
    }
}
