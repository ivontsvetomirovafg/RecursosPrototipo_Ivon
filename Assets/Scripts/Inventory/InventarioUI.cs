using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panelCrafteo;

    [Header("Info del item seleccionado")]
    public Text nombreText;
    public Text descripcionText;
    public Text costeText;
    public Button craftButton;
    public Text nombreBoton;
    public Text errorText;

    private Receta receta;
    private CraftingManager craftingManager;
    [SerializeField]
    private Animator animator;

    void Start()
    {
        craftingManager = FindObjectOfType<CraftingManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (animator.GetBool("Closed") == false)
            {
                animator.SetBool("Closed", true);
            }
            else
            {
                animator.SetBool("Closed", false);
            }
        }
    }

    public void MostrarInfo(Receta _receta)
    {
        receta = _receta;
        panelCrafteo.SetActive(true);
        nombreText.text = receta.itemName;
        descripcionText.text = receta.description;
        string coste = "";
        
        if (receta.woodCost > 0) 
        {
            coste += "x" + receta.woodCost + " Madera  ";
        }
        if (receta.stoneCost > 0) 
        {
            coste += "x" + receta.stoneCost + " Piedra  ";
        }
        if (receta.slimeCost > 0) 
        {
            coste += "x" + receta.slimeCost + " Slime  ";
        }
        if (receta.bonesCost > 0) 
        {
            coste += "x" + receta.bonesCost + " Huesos  ";
        }
        costeText.text = coste;
    }

    public void Craftear()
    {
        craftingManager.Craft(receta);

        if (nombreBoton != null)
        {
            nombreBoton.text = receta.itemName;
        }    
    }
    public void MostrarError()
    {
        errorText.text = "";
    }
}


