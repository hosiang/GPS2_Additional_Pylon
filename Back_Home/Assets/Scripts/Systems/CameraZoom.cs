using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    [SerializeField] float startSize = 2f;
    [SerializeField] float endSize = 10f;
    [SerializeField] float sizeRate = 0.1f;
    private bool isZooming = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.orthographicSize = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.orthographicSize >= 10f)
        {
            isZooming = false;
            mainCamera.orthographicSize = endSize;
            StopCoroutine(Zoom());
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Zoom());
        }
    }

    private IEnumerator Zoom()
    {
        while(isZooming)
        {
            mainCamera.orthographicSize += sizeRate;
            yield return new WaitForSeconds(0.0001f);
        }
    }
}
