using System.Collections.Generic;
using UnityEngine;

public class HPHUD : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject hpIcon;
    [SerializeField] private GameObject hpLostIcon;
    // [SerializeField] private Transform hpContainer; //commented out if script is going to be placed on the container
    private List<GameObject> allHPIcons = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < playerHealth.MaxHealth; i++)
        {
            //this will create as many full hp icons as the current max health of the player at the start of this scripts lifetime
            allHPIcons.Add(Instantiate(hpIcon, this.gameObject.transform));
            //todo: spawn inactive lost hpIcons?
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
