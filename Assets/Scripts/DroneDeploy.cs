using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioSource = UnityEngine.AudioSource;

public class DroneDeploy : MonoBehaviour
{
    public PlayerMove player;
    public GameObject dronePrefab;

    float respawnTime;
    public float startRT;
    public float nextRT;
    public float hardRT;
    public float finalRT;
    public float audioDroneSpawnVol;
    public AudioSource audioSource;

    private Vector2 screenBounds;
    public float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        respawnTime = startRT;
        timeCounter = 0;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(droneWave());
    }

    private void spawnDrone()
    {
        GameObject newDrone = Instantiate(dronePrefab) as GameObject;
        newDrone.transform.position = new Vector2(screenBounds.x - 25, Random.Range(-4, 5));

        // Play drone spawn sound
        audioSource.PlayOneShot(audioSource.clip, audioDroneSpawnVol);
    }

    IEnumerator droneWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnDrone();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            timeCounter += Time.deltaTime;
        }
        if ((timeCounter >= 30) && (timeCounter < 60))
        {
            respawnTime = nextRT;
        }
        if ((timeCounter >= 60) && (timeCounter < 120))
        {
            respawnTime = hardRT;
        }
        if (timeCounter >= 120)
        {
            respawnTime = finalRT;
        }
    }
}
