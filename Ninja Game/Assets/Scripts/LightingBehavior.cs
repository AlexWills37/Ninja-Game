using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomly generate a bright flash of lightning
/// </summary>
public class LightingBehavior : MonoBehaviour
{

    public Light lightSource;
    public AudioSource thunderSound;
    public float minLightningTime = .5f;
    public float maxLightningTime = 10f;
    public float lightningIntensity = 5f;

    private float nextLightningTime;


    // Start is called before the first frame update
    void Start()
    {
        thunderSound = this.GetComponent<AudioSource>();
        lightSource = this.GetComponent<Light>();
        lightSource.intensity = 0;

        nextLightningTime = Time.time + Random.Range(minLightningTime, maxLightningTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextLightningTime)
        {
            StartLightning();
            nextLightningTime = Time.time + Random.Range(minLightningTime, maxLightningTime);
            // Play lighting and thunder sound effect at a random pitch
            thunderSound.PlayDelayed(0.1f);
            thunderSound.pitch = Random.Range(0.5f, 2f);
        } else
        {
            LowerLightning();
        }
    }

    /// <summary>
    /// Create a flash of light with the lighting source
    /// </summary>
    private void StartLightning()
    {
        lightSource.intensity = lightningIntensity;
    }

    /// <summary>
    /// After a lightning effect is started, quickly fade the light off
    /// </summary>
    private void LowerLightning()
    {
        // End lightning when it fades enough
        if(lightSource.intensity > 0.1)
        {
            lightSource.intensity -= lightningIntensity * Time.deltaTime;
        } else
        {
            lightSource.intensity = 0;
        }
    }
}
