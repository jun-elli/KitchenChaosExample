using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    [SerializeField] private Image iconSuccessImage;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;
    [SerializeField] private TextMeshProUGUI deliverySuccessText;
    private const string DELIVERY_SUCCESS_MESSAGE = "Delivery\nsuccessful!";
    private const string DELIVERY_FAILED_MESSAGE = "Wrong\ndelivery";

    private Animator animator;
    private const string POPUPU_TRIGGER_ANIM = "Popup";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRightRecipeDelivered += DeliveryManager_OnRightRecipeDelivered;
        DeliveryManager.Instance.OnWrongRecipeDelivered += DeliveryManager_OnWrongRecipeDelivered;
        Hide();
    }

    private void DeliveryManager_OnWrongRecipeDelivered()
    {
        Show();
        UpdateVisual(false);
    }

    private void DeliveryManager_OnRightRecipeDelivered()
    {
        Show();
        UpdateVisual(true);
    }

    private void UpdateVisual(bool isSuccessful)
    {
        if (isSuccessful)
        {
            animator.SetTrigger(POPUPU_TRIGGER_ANIM);
            backgroundImage.color = successColor;
            iconSuccessImage.sprite = successSprite;
            deliverySuccessText.text = DELIVERY_SUCCESS_MESSAGE;
        }
        else
        {
            animator.SetTrigger(POPUPU_TRIGGER_ANIM);
            backgroundImage.color = failureColor;
            iconSuccessImage.sprite = failureSprite;
            deliverySuccessText.text = DELIVERY_FAILED_MESSAGE;
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
