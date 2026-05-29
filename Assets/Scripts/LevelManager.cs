using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameData gameData;
    [Header("Inventory")]

    [SerializeField]
    private Text woodText;

    [SerializeField]
    private Text stoneText;

    [SerializeField]
    private Text slimeText;

    [SerializeField]
    private Text bonesText;  

    [SerializeField]
    private GameObject[] imageInventory;

    [Header("Crafteos")]
    public List<Receta> todosObjetos;

    public void AddItem(string itemName, int amount)
    { //un for que repase la lista que hay en el gamedata de recursos y si la variable "nombreObj" es == a "itemName", que le sume el amount.
        switch (itemName)
        {
            case "Wood":
            //wood += amount;
                break;

            case "Stone":
            //stone += amount;
                break;

            case "Slime":
            //slime += amount;
                break;

            case "Bones":
            //bones += amount;
                break;
        }
        //UpdateUI();
    }
    public void AddItemInventario(int itemIndex)
    {
        gameData.inventarioUsuario.Add(todosObjetos[itemIndex]);
    }
    /*private void UpdateUI()
    {

    //otro for que repase la lista y si el recurso i.cantidad == 0 no haces nada

        if (wood == 0)
        {
            woodText.gameObject.SetActive(false);
        }
        else
        {
            woodText.gameObject.SetActive(true);
            woodText.text = wood.ToString();
            imageWood.sprite = spriteWood;
        }
        if (stone == 0)
        {
            imageStone.gameObject.SetActive(false);
            stoneText.gameObject.SetActive(false);
        }
        else
        {
            imageStone.gameObject.SetActive(true);
            stoneText.gameObject.SetActive(true);
            stoneText.text = stone.ToString();
            imageStone.sprite = spriteWood;
        }
        if (slime == 0)
        {
            imageSlime.gameObject.SetActive(false);
            slimeText.gameObject.SetActive(false);
        }
        else
        {
            imageSlime.gameObject.SetActive(true);
            slimeText.gameObject.SetActive(true);
            slimeText.text = slime.ToString();
            imageSlime.sprite = spriteWood;
        }
        if (bones == 0)
        {
            imageBones.gameObject.SetActive(false);
            bonesText.gameObject.SetActive(false);
        }
        else
        {
            imageBones.gameObject.SetActive(true);
            bonesText.gameObject.SetActive(true);
            bonesText.text = bones.ToString();
            imageBones.sprite = spriteWood;
        }
    }*/
}
[Serializable]
public class Recursos 
{
    public int cantidad;
    public Sprite imageObj;
    public string nombreObj;
}

