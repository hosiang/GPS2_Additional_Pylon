using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkieTakieDissapear : MonoBehaviour
{
    public GameObject tutorialDialogue;
    public GameObject walkieTalkie;

    private void Update()
    {
        if(tutorialDialogue == null)
        {
            walkieTalkie.SetActive(false);
        }
    }
}
