using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]private KitchenObjectSO cutkitchenObjectSO;

    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            if (IsHaveKitchenObject() == false)
            {
                TransferKitchenObject(player, this);
            }
            else
            {

            }
        }
        else
        {
            if (IsHaveKitchenObject() == false)
            {

            }
            else
            {
                TransferKitchenObject(this, player);
            }
        }
    }
    public override void InteractOperate(Player player)
    {
        if(IsHaveKitchenObject())
        {
            DestroyKitchenObject();
            CreateKitchenObject(cutkitchenObjectSO.prefab);
        }
    }
}
