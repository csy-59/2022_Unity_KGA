using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] Characters;

    private int SelectedID { get; set; }

    void Start()
    {
        UIMgrChangeCharacter(0);
    }

    void Update()
    {
        
    }

    public void UIMgrChangeCharacter(int index)
    {
        if (index >= Characters.Length)
            return;

        foreach(GameObject character in Characters)
        {
            character.SetActive(false);
        }

        SelectedID = index;
        Characters[SelectedID].SetActive(true);
    }

    public void UIMgrSelectCharacter()
    {
        Characters[SelectedID].GetComponent<CharacterRotate>().IsSelected = true;
    }
}
