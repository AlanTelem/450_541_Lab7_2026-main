using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthHUD : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject hpIcon;
    // [SerializeField] private Sprite fullHeartSprite; //not needed for now because prefab has sprite set to full by default
    [SerializeField] private Sprite emptyHeartSprite;

    // [SerializeField] private Transform hpContainer; //commented out if script is going to be placed on the container
    private List<Image> iconSprites = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hpIcon != null && playerHealth != null)
        {
            for (int i = 0; i < playerHealth.MaxHealth; i++)
            {
                GameObject heart = Instantiate(hpIcon, this.gameObject.transform);
                iconSprites.Add(heart.GetComponent<Image>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (emptyHeartSprite == null || iconSprites.Count == 0)
        {
            Debug.Log("You need to set playerHealth and emptyHeartSprite fields.");
        }
        else
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
}
