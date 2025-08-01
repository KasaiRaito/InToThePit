using UnityEngine;
using System.Collections;
public class Consumable : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Rigidbody _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void OnSwallowed()
    {
        StartCoroutine(nameof(Swallow));
    }

    IEnumerator Swallow()
    {
        _rb.useGravity = true;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);

        FindAnyObjectByType<Hole>().Grow(1);
    }
}
