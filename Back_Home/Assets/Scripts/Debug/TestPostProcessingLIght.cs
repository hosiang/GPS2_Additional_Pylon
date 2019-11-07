using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TestPostProcessingLIght : MonoBehaviour
{
    PostProcessVolume postProcessVolume;
    Bloom bloom;

    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume = this.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings<Bloom>(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        bloom.intensity.value += Mathf.Sin(Time.time * 3.0f) * 0.1f;

        if (Input.GetKeyDown(KeyCode.G))
        {
            bloom.intensity.value += 1f;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            bloom.intensity.value -= 1f;
        }
        
    }
}
