using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] private GameObject craftingTableDialogue;
    [SerializeField] private GameObject craftingTable;
    [SerializeField] private TextMeshProUGUI pageNoTxt;
    [SerializeField] private GameObject[] craftingList;
    private int pageNo;
    private bool canOpenCraftingTable;
    // Start is called before the first frame update
    void Start()
    {
        canOpenCraftingTable = false;
        pageNo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpenCraftingTable)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                craftingTable.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                craftingTable.SetActive(false);
            }
        }

        Crafting();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CraftingTable")
        {
            craftingTableDialogue.SetActive(true);
            canOpenCraftingTable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CraftingTable")
        {
            craftingTableDialogue.SetActive(false);
            canOpenCraftingTable = false;
        }
    }

    private void Crafting()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (craftingTable.activeSelf)
        {
            player.isCrafting = true;
            pageNoTxt.text = pageNo.ToString();

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (pageNo > 1) pageNo--;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (pageNo < 2) pageNo++;
            }

            int index = pageNo - 1;
            craftingList[index].SetActive(true);
            for(int i=0; i<craftingList.Length; i++)
            {
                if (i != index) craftingList[i].SetActive(false);
            }
         }
        else
        {
            player.isCrafting = false;
        }
    }
}
