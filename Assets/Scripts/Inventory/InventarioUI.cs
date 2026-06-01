using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InventarioUI : MonoBehaviour
{
    public GameObject panelCrafteo;

    [Header("Info del item seleccionado")]
    public Text nombreText;
    public Text descripcionText;
    public Text costeText;
    public Button craftButton;
    public Text nombreBoton;
    [SerializeField]
    private GameObject infoPanel;
    private Receta receta;
    private CraftingManager craftingManager;
    private LevelManager levelManager;
    [SerializeField]
    private Animator animator;

    [Header("Recetas iniciales")]
    public Receta recetaEspadaInicial;
    public Receta recetaArmaduraInicial;
    public Receta recetaPicoInicial;
    

    void Start()
    {
        craftingManager = FindObjectOfType<CraftingManager>();
        levelManager = FindObjectOfType<LevelManager>();
        animator.SetBool("Closed", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (animator.GetBool("Closed") == false)
            {
                animator.SetBool("Closed", true);
                StartCoroutine(TiempoClose()); 
            }
            else
            {
                animator.SetBool("Closed", false);
            }
        }
    }
    public IEnumerator TiempoClose()
    {        
        yield return new WaitForSeconds(2f);
        infoPanel.SetActive(false);
    }

    public void MostrarInfo(Receta _receta)
    {
        craftingManager.errorText.SetActive(false);
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
    /*public void MostrarEspada()
    {
    if (levelManager.espadaActual != null)
        MostrarInfo(levelManager.espadaActual);
    else
        MostrarInfo(recetaEspadaInicial);
}

public void MostrarPico()
{
    if (levelManager.picoActual != null)
        MostrarInfo(levelManager.picoActual);
    else
        MostrarInfo(recetaPicoInicial);
}

public void MostrarArmadura()
{
    if (levelManager.armaduraActual != null)
        MostrarInfo(levelManager.armaduraActual);
    else
        MostrarInfo(recetaArmaduraInicial);
}*/

    public void Craftear()
    {
        if (receta == null) 
        {
            return;
        }
        
        bool exito = craftingManager.Craft(receta);

        if (exito == true && receta.siguienteNivel != null)
        {
            MostrarInfo(receta.siguienteNivel);
        }
    }
}


