using UnityEngine;
using UnityEngine.UI;

public class PicoController : MonoBehaviour
{
    public CharacterControler player;
    private bool equipado = false;
    private Animator animator;
    private LevelManager levelManager;
    public GameObject espada;
    [SerializeField]
    private Sprite espadaUI;
    private PiedraGolpes piedras;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    // Update is called once per frame
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stone"))
        {
            piedras = collision.GetComponent<PiedraGolpes>(); 
            if (piedras != null)
            {
                piedras.RomperRoca();
            }            
        }
    }
    public void Seleccionar()
    {
        if (equipado == false)
        {
            equipado = true;
            espada.SetActive(false);
        }
        else
        {
            equipado = false;
            espada.SetActive(true);
        }
        player.pico.SetActive(equipado);
    }
}
