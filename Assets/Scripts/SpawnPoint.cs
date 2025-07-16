using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int openingDirection;
    // 1 - Puerta bottom necesitada
    // 2 - Puerta Top necesitada
    // 3 - Puerta left necesitada
    // 4 - Puerta Right necesitada

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false) {
            if (openingDirection == 1)
            {
                rand = Random.Range(0, templates.bottomrooms.Length);
                Instantiate(templates.bottomrooms[rand], transform.position, templates.bottomrooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.toprooms.Length);
                Instantiate(templates.toprooms[rand], transform.position, templates.toprooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.leftrooms.Length);
                Instantiate(templates.leftrooms[rand], transform.position, templates.leftrooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, templates.rightrooms.Length);
                Instantiate(templates.rightrooms[rand], transform.position, templates.rightrooms[rand].transform.rotation);
            }
            spawned = true;
        }
        
    }

    void OnTriggerEntered(Collider other)
    {
        if(other.CompareTag("SpawnPoint") && other.GetComponent<SpawnPoint>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
