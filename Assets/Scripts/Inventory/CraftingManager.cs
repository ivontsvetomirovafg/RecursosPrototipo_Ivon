using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public LevelManager levelManager;
    public Receta[] recetas;
    public CharacterControler player;
    public GameObject errorText;
    public PicoController picoController;

    [SerializeField]
    private AudioClip succes;
    [SerializeField]
    private AudioClip error;

    public bool Craft(Receta receta)
    {
        if (TieneRecursos(receta))
        {
            AudioManager.Instance.PlaySFX(succes);
            RestarRecursos(receta);
            if (receta.item == 0)
            {
                levelManager.slotPico.SetActive(true);
                levelManager.slotPico.GetComponent<Image>().sprite = receta.objectImage;
                levelManager.picoActual = receta;
                picoController.botonPico.interactable = true;
            }    
            else if (receta.item == 1)
            {
                levelManager.espadaActual = receta;
            }     
            else if (receta.item == 2)
            {
                levelManager.armaduraActual = receta;
            }         
            else if (receta.PocionVida)
            {
                player.HealLife(receta.lifeBonus);
            }
            else if (receta.PocionDaño)
            {
                player.PocionDaño(receta.damageBonus, 30f);
            }

            if (receta.PocionVida == false && receta.PocionDaño == false)
            {
                player.baseDamage += receta.damageBonus;
                player.damage = player.baseDamage;       
                player.maxLife += receta.lifeBonus;
                player.UpdateLife();
                player.UpdateDamage();
            }                
            errorText.SetActive(false);
            return true; 
        }
        else
        {
            AudioManager.Instance.PlaySFX(error);
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