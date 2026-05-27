using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameData gameData;
    [Header("Inventory")]
    public int wood;
    public int stone;
    public int slime;
    public int bones;

    [Header("Crafteos")]
    public bool picoNivel1;
    public bool picoNivel2;

    public bool espadaNivel2;
    public bool espadaNivel3;
    public bool espadaNivel4;

    public bool armaduraNivel1;
    public bool armaduraNivel2;
    public bool armaduraNivel3;

    public bool pocionVida;
    public bool pocionDaño;
    public bool antorcha;

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
