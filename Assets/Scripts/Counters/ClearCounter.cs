using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO objectSO;

    public override void Interact(Player player)
    {
        // If there's no object in counter
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        // If there's object on counter
        else
        {
            if (player.HasKitchenObject())
            {
                // If player has Plate, put ingredient in player's plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateOnPlayer))
                {
                    if (plateOnPlayer.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                // If player holds object but not a plate
                else
                {
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateOnCounter))
                    {
                        if (plateOnCounter.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
