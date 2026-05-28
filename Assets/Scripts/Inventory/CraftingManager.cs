using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public LevelManager levelManager;
    public Receta[] recetas;
    public CharacterControler player;

    public void Craft(Receta receta) 
    {
        if (levelManager.wood >= receta.woodCost && levelManager.stone >= receta.stoneCost 
        && levelManager.slime >= receta.slimeCost && levelManager.bones >= receta.bonesCost) 
        {
            levelManager.wood -= receta.woodCost;
            levelManager.stone -= receta.stoneCost;
            levelManager.slime -= receta.slimeCost;
            levelManager.bones -= receta.bonesCost;

            player.damage += receta.damageBonus;
            player.maxLife += receta.lifeBonus;

            switch (receta.item)
            {
                case "picoNivel1": 
                    levelManager.picoNivel1 = true; 
                    break;
                case "picoNivel2":
                    levelManager.picoNivel2 = true; 
                    break;
                case "espadaNivel2": 
                    levelManager.espadaNivel2 = true; 
                    break;
                case "espadaNivel3": 
                    levelManager.espadaNivel3 = true; 
                    break;
                case "espadaNivel4": 
                    levelManager.espadaNivel4 = true; 
                    break;
                case "armaduraNivel1": 
                    levelManager.armaduraNivel1 = true; 
                    break;   
                case "armaduraNivel2": 
                    levelManager.armaduraNivel2 = true; 
                    break;   
                case "armaduraNivel3": 
                    levelManager.armaduraNivel3 = true; 
                    break;   
                case "pocionVida": 
                    levelManager.pocionVida = true; 
                    break;   
                case "pocionDaño": 
                    levelManager.pocionDaño = true; 
                    break;   
                case "antorcha": 
                    levelManager.antorcha = true; 
                    break;   
            }
        }
        else
        {
            FindObjectOfType<InventarioUI>().MostrarError();
        }
    }
}