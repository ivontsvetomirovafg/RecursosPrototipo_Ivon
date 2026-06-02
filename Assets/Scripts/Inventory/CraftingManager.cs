using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public LevelManager levelManager;
    public Receta[] recetas;
    public CharacterControler player;
    public GameObject errorText;

    public bool Craft(Receta receta)
    {
        if (TieneRecursos(receta))
        {
            RestarRecursos(receta);
            if (receta.item == 0)
            {
                levelManager.slotPico.SetActive(true);
                levelManager.slotPico.GetComponent<Image>().sprite = receta.objectImage;
                levelManager.picoActual = receta;
            }    
            else if (receta.item == 1)
            {
                levelManager.espadaActual = receta;
            }     
            else if (receta.item == 2)
            {
                levelManager.armaduraActual = receta;
            }         
        
            player.damage += receta.damageBonus;
            player.maxLife += receta.lifeBonus;
            errorText.SetActive(false);
            return true; 
        }
        else
        {
            errorText.SetActive(true);
            return false; 
        }
    }

    private bool TieneRecursos(Receta receta)
    {
        int wood = 0;
        int stone = 0;
        int slime = 0;
        int bones = 0;

        for (int i = 0; i < levelManager.gameData.recursos.Length; i++)
        {
            Recursos recurs = levelManager.gameData.recursos[i];
            if (recurs.nombreObj == "Wood") 
            {
                wood = recurs.cantidad;
            }
            if (recurs.nombreObj == "Stone") 
            {
                stone = recurs.cantidad;
            }
            if (recurs.nombreObj == "Slime") 
            {
                slime = recurs.cantidad;
            }
            if (recurs.nombreObj == "Bones") 
            {
                bones = recurs.cantidad;
            }
        }
        if (wood >= receta.woodCost && stone >= receta.stoneCost && slime >= receta.slimeCost && bones >= receta.bonesCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RestarRecursos(Receta receta)
    {
        for (int i = 0; i < levelManager.gameData.recursos.Length; i++)
        {
            Recursos recurs = levelManager.gameData.recursos[i];
            if (recurs.nombreObj == "Wood") 
            {
                recurs.cantidad -= receta.woodCost;
            }
            if (recurs.nombreObj == "Stone") 
            {
                recurs.cantidad -= receta.stoneCost;
            }
            if (recurs.nombreObj == "Slime") 
            {
                recurs.cantidad -= receta.slimeCost;
            }
            if (recurs.nombreObj == "Bones") 
            {
                recurs.cantidad -= receta.bonesCost;
            }
        }
        levelManager.AddItem("", 0); // esto refresca la UI
    }
}