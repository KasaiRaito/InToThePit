using DG.Tweening;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private MyJoyStick joystick;
    [SerializeField] private float speed;
    private float _size;
    private Transform _myTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _myTransform = this.gameObject.transform;
        _size = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += new Vector3(joystick.GetMovementAmount().x *
                                                          (speed * Time.deltaTime), 0f, joystick.GetMovementAmount().y *
            (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.gameObject.GetComponent<Consumable>();

        if (consumable)
        {
            consumable.OnSwallowed();
        }
    }

    public void Grow(int amount)
    {
        _size += amount;
        _myTransform.DOScale(new Vector3(_size, _size, _size), 0.5f);
    }

}
