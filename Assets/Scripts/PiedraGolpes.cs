using UnityEngine;

public class PiedraGolpes : MonoBehaviour
{
    private int golpesParaRomper = 3;
    public Sprite piedraActual;
    [SerializeField]
    private Sprite piedraRomper1;
    [SerializeField]
    private Sprite piedraRomper2;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RomperRoca()
    {
        golpesParaRomper--;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (golpesParaRomper == 2)
        {
            sprite.sprite = piedraRomper1;
        }

        else if (golpesParaRomper == 1)
        {
            sprite.sprite = piedraRomper2;
        }

        else if (golpesParaRomper <= 0)
        {
            levelManager.AddItem("Stone", 1);
            Destroy(gameObject); 
        }
    }
}
