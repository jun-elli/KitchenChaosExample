using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGameObject;

    private IHasProgress hasProgressI;

    private void Start()
    {
        hasProgressI = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgressI == null)
        {
            Debug.LogError("Game Object " + hasProgressGameObject.name + " doesn't have IHasProgress interface");
        }
        hasProgressI.OnProgressChanged += IHasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void IHasProgress_OnProgressChanged(float normalizedProgress)
    {
        barImage.fillAmount = normalizedProgress;

        if (normalizedProgress <= 0 || normalizedProgress >= 1)
        {
            Hide();
        }
        else
        {
            Show();
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
