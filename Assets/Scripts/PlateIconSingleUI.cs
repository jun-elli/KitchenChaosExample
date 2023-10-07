
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetIcon(KitchenObjectSO objectSO)
    {
        image.sprite = objectSO.sprite;
    }
}
