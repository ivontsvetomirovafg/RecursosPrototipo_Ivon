using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Inventory")]
    public int wood;
    public int stone;
    public int slime;
    public int bones;
    

public void AddItem(string itemName, int amount)
{
    switch (itemName)
    {
        case "Wood":
            wood += amount;
            break;

        case "Stone":
            stone += amount;
            break;

        case "Slime":
            slime += amount;
            break;

        case "Bones":
            bones += amount;
            break;
    }
}
}
