using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtrasUI : MonoBehaviour
{
    [SerializeField] private Button teleportTilesButton;
    [SerializeField] private Button returnButton;

    private void Awake()
    {
        teleportTilesButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.TeleportTilesScene);
        });

        returnButton.onClick.AddListener(() =>
        {
            Hide();
        });

    }

    private void Start()
    {
        Hide();
    }


    public void Show()
    {
        gameObject.SetActive(true);
        teleportTilesButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
