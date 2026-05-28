using UnityEngine;

[CreateAssetMenu(fileName = "Receta", menuName = "Crafting/Receta")]
public class Receta : ScriptableObject
{
    [Header("Stats")]
    public float damageBonus;
    public float lifeBonus;
    
    public Sprite objectImage;
    public string itemName;
    public string description;

    public int woodCost;
    public int stoneCost;
    public int slimeCost;
    public int bonesCost;

    public string item;
}
