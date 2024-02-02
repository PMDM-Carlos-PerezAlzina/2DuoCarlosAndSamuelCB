using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFire : MonoBehaviour
{
    public GameObject player;
    private KnightController knightController;
    public 

    void Start()
    {
        knightController = player.GetComponent<KnightController>();
        GenerarItem();
        float tiempoParaDestruccion = 0.68333f;
        Destroy(gameObject, tiempoParaDestruccion);
    }

    public void GenerarItem()
    {
        int randomValue = Random.Range(0, 10);
        Debug.Log("Random value: " + randomValue);

        if (randomValue > 1)
        {
            int randomValue2 = Random.Range(1, 4);
            Debug.Log("Random value: " + randomValue2);
            if (player != null)
            {
                Debug.Log("Torch: "+ player.GetComponent<KnightController>().torchQuantity);
                if (knightController != null)
                {
                    switch (randomValue2)
                    {
                        case 1:
                            knightController.IncreaseTorch();
                            break;

                        case 2:
                            knightController.IncreasePotionLife();
                            break;

                        case 3:
                            knightController.IncreasePotionSanity();
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
