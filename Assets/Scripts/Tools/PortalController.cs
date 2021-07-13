using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private PortalController destination;
    private bool Transporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Transporting)
        {
            destination.Transporting = true;

            if (other.transform.CompareTag("Player"))
            {
                //other.transform.position = destination.transform.position;
                StartCoroutine(Teleport(other));
            }
        }
    }

    /* private void OnTriggerExit2D(Collider2D other)
    {
        Transporting = false;
    } */

    public IEnumerator Teleport(Collider2D other)
    {
        other.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        other.transform.position = destination.transform.position;
        yield return new WaitForSeconds(0.1f);
        other.gameObject.SetActive(true);
    }
}
