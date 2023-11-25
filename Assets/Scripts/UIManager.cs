using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject battleStartTransition;
    public GameObject battleEndTransition;

    public GameObject battleSystem;

    public void TransitionScreen()
    {
        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    {
        battleStartTransition.SetActive(true);

        yield return new WaitForSeconds(2f);

        battleStartTransition.SetActive(false);

        battleSystem.SetActive(true);

        battleEndTransition.SetActive(true);

        yield return new WaitForSeconds(2f);

        battleEndTransition.SetActive(false);
    }
}
