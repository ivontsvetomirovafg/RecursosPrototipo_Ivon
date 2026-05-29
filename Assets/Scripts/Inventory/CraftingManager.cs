using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public LevelManager levelManager;
    public Receta[] recetas;
    public CharacterControler player;
    public GameObject errorText;

    public void Craft(Receta receta) 
    {
        /*if (levelManager.wood >= receta.woodCost && levelManager.stone >= receta.stoneCost && levelManager.slime >= receta.slimeCost && levelManager.bones >= receta.bonesCost) 
        {
            levelManager.wood -= receta.woodCost;
            levelManager.stone -= receta.stoneCost;
            levelManager.slime -= receta.slimeCost;
            levelManager.bones -= receta.bonesCost;

            levelManager.AddItemInventario(receta.item);
        }
        else
        {
            errorText.SetActive(true);
        }*/
    }
}