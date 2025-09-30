using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health Health;
    [SerializeField] public Image HealthBarImage;
    public GameObject HealthBarUI;

    public bool showHealthBarWhenFull = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health.EntityDamaged += UpdateHealthBar;
        Health.EntityHealed += UpdateHealthBar;
    }

    void Update()
    {
        if (showHealthBarWhenFull)
        {
            HealthBarUI.gameObject.SetActive(Health.HealthRatio != 1);
        }
    }

    void UpdateHealthBar(object sender, HealthChangedEventArgs health)
    {
        HealthBarImage.fillAmount = Health.HealthRatio;
    }
}
