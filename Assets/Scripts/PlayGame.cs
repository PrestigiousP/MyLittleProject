using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//permet d'accepter la classe IPointerClickHandler
using UnityEngine.SceneManagement;//permet de gerer les scenes (duh)

public class PlayGame : MonoBehaviour, IPointerClickHandler 
{
   public void OnPointerClick(PointerEventData pointerEvent)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
