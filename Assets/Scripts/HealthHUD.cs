using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPHUD : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject hpIcon;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    // [SerializeField] private Transform hpContainer; //commented out if script is going to be placed on the container
    private List<Image> iconSprites = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < playerHealth.MaxHealth; i++)
        {
            //this will create as many full hp icons as the current max health of the player at the start of this scripts lifetime
            GameObject heart = Instantiate(hpIcon, this.gameObject.transform);
            iconSprites.Add(heart.GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < iconSprites.Count; i++)
        {
            if (i >= playerHealth.CurrentHealth)
            {
                iconSprites[i].sprite = emptyHeartSprite;
            }
        }
    }
}
