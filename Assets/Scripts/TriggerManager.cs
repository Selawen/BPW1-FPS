using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    protected GameObject eventManager;

    // Start is called before the first frame update
    void Start()
    {
        eventManager = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            GetComponent<ShootBullet>().maxAmmo += 2;
            Destroy(other.gameObject);
        }
        eventManager.GetComponent<UIManager>().UpdateText();
    }
}
