using UnityEngine;
using UnityEngine.UI;

public class TuScript : MonoBehaviour
{
    public Sprite[] lifeImages;
    public Sprite[] sanityImages;
    public Text inventoryText;
    public Image lifeImage;
    public Image sanityImage;
    public GameObject player;
    private KnightController knight;

    // ... (otras variables y métodos)

    void Start()
    {
        knight = player.GetComponent<KnightController>();
    }

    void Update() {
        UpdateLifeUI();
        UpdateSanityUI();
        UpdateInventoryUI();
    }

    // Método para actualizar la UI de la vida
    void UpdateLifeUI()
    {
        // Acceder a la vida actual del jugador a través de KnightController
        float currentLife = knight.life;

        // Definir los rangos y asignar el índice de la imagen correspondiente
        int currentIndex = 0;
        if (currentLife >= 75)
        {
            currentIndex = 0;
        }
        else if (currentLife >= 50)
        {
            currentIndex = 1;
        }
        else if (currentLife >= 25)
        {
            currentIndex = 2;
        }
        else
        {
            currentIndex = 3; // Puedes ajustar esto según tus necesidades
        }

        // Mostrar la imagen correspondiente
        lifeImage.sprite = lifeImages[currentIndex];
    }

    // Método para actualizar la UI de la cordura
    void UpdateSanityUI()
    {
        // Acceder a la cordura actual del jugador a través de KnightController
        float currentSanity = knight.sanity;

        // Definir los rangos y asignar el índice de la imagen correspondiente
        int currentIndex = 0;
        if (currentSanity >= 75)
        {
            currentIndex = 0;
        }
        else if (currentSanity >= 50)
        {
            currentIndex = 1;
        }
        else if (currentSanity >= 25) {
            currentIndex = 2;
        } else
        {
            currentIndex = 3; // Puedes ajustar esto según tus necesidades
        }

        // Mostrar la imagen correspondiente
        sanityImage.sprite = sanityImages[currentIndex];
    }

    // Método para actualizar la UI del inventario
    void UpdateInventoryUI()
    {
        // Construir una cadena que representa el inventario
        string inventoryString = "Inventory: ";

        // Obtener la cantidad de cada elemento del inventario desde GameManager
        int torchQuantityLocal = knight.torchQuantity;
        int lifePotionQuantityLocal = knight.lifePotionQuantity;
        int sanityPotionQuantityLocal = knight.sanityPotionQuantity;

        // Agregar la cantidad de cada elemento al texto del inventario
        inventoryString += "Torches: " + torchQuantityLocal + " | Life Potions: " + lifePotionQuantityLocal + " | Sanity Potions: " + sanityPotionQuantityLocal;

        // Actualizar el texto del inventario en la UI
        inventoryText.text = inventoryString;
    }

}
