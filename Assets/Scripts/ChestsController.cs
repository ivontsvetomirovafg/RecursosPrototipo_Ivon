using UnityEngine;

public class ChestsController : MonoBehaviour
{
    [SerializeField] private GameObject iconUI;

    private bool inTrigger;
    private bool opened;
    private Animator animator;
    private LevelManager levelManager;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        levelManager =GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (inTrigger == true && opened == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenChest();
            }
        }
    }

private void OpenChest()
{
    animator.SetTrigger("Open");
    opened = true;
    iconUI.SetActive(false);
    GetComponent<Collider2D>().enabled = false;
    int amount = Random.Range(1, 6);
    int randomItem = Random.Range(0, 4);

    switch (randomItem)
    {
        case 0:
            levelManager.AddItem("Wood", amount);
            Debug.Log("Obtuviste " + amount + " Wood");
            break;

        case 1:
            levelManager.AddItem("Stone", amount);
            Debug.Log("Obtuviste " + amount + " Stone");
            break;

        case 2:
            levelManager.AddItem("Slime", amount);
            Debug.Log("Obtuviste " + amount + " Slime");
            break;

        case 3:
            levelManager.AddItem("Bones", amount);
            Debug.Log("Obtuviste " + amount + " Bones");
            break;
    }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (opened == false)
        {
            inTrigger = true;
            iconUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (opened == false)
        {
            inTrigger = false;
            iconUI.SetActive(false);
        }
    }
}
