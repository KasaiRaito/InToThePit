using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private MyJoyStick joystick;
    [SerializeField] private float speed;
    [SerializeField] private float size;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        this.gameObject.transform.position += new Vector3(joystick.GetMovementAmount().x * (speed * Time.deltaTime), 0f ,joystick.GetMovementAmount().y * (speed * Time.deltaTime));
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.gameObject.GetComponent<Consumable>();
        if(consumable)
        {
            consumable.OnSwallowed();
        }
    }
}
