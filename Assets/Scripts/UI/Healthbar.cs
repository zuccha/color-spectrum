using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private Image _healthBarSprite;

    [SerializeField]
    private Sprite _healthBarGreenSprite;

    [SerializeField]
    private Sprite _healthBarRedSprite;

    [SerializeField]
    private Sprite _healthBarYellowSprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        float healthProgress = currentHealth / maxHealth;
        _healthBarSprite.fillAmount = healthProgress;

        if (healthProgress > 0.5)
        {
            _healthBarSprite.sprite = _healthBarGreenSprite;
            _healthBarSprite.color = Color.green;
        }
        else if (healthProgress > 0.25)
        {
            _healthBarSprite.sprite = _healthBarYellowSprite;
            _healthBarSprite.color = Color.yellow;
        }
        else
        {
            _healthBarSprite.sprite = _healthBarRedSprite;
            _healthBarSprite.color = Color.red;
        }
    }
}
