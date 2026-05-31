using UnityEngine;

[CreateAssetMenu(fileName = "Receta", menuName = "Crafting/Receta")]
public class Receta : ScriptableObject
{
    [Header("Stats")]
    public float damageBonus;
    public float lifeBonus;

    [Header("Description")]
    public Sprite objectImage;
    public string itemName;
    public string description;

    [Header("Costs")]
    public int woodCost;
    public int stoneCost;
    public int slimeCost;
    public int bonesCost;
    
    [Header("Siguiente nivel")]
    public Receta siguienteNivel;

    public int item;
}
