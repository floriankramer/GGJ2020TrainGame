using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public static float trainSpeed = 4f;
    public static float EngineLength = 7;
    public static float CarLength = 3;

    private Dictionary<PartType, List<Part>> parts = new Dictionary<PartType, List<Part>>();

    public GameObject GameOverScreen;
    public GameObject TrainCarObj;

    public int numCars = 1;


    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> toProcess = new List<GameObject>();
        toProcess.Add(gameObject);

        while (toProcess.Count > 0) {
            GameObject g = toProcess[toProcess.Count - 1];
            toProcess.RemoveAt(toProcess.Count - 1);

            foreach (Transform t in g.transform) {
                toProcess.Add(t.gameObject);
            }

            Part p = g.GetComponent<Part>();
            if (p != null) {
                Debug.LogWarning("Found a part");
                if (!parts.ContainsKey(p.partType)) {
                    parts.Add(p.partType, new List<Part>());
                }
                parts[p.partType].Add(p);
                p.onDeathEvent.AddListener(delegate() {
                    Debug.LogWarning("A Part Died");
                    parts[p.partType].Remove(p);
                    if (parts[p.partType].Count == 0) {
                        Defeat();
                    }
                });
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Move with the Speed of the Train
        float xPosition = transform.position.x + Train.trainSpeed * Time.deltaTime;
        transform.position = new Vector3(xPosition, transform.position.y);
    }

    void Defeat() {
        trainSpeed = 0;
        this.GetComponent<AudioSource>().Pause();
        GameObject o = Instantiate(GameOverScreen, new Vector3(0, 0, 0), new Quaternion());
        o.transform.SetParent(GameObject.Find("Canvas").transform);
        o.transform.position = new Vector3(0, 0, 10);
        o.transform.localScale = new Vector3(1, 1, 1);
        o.transform.rotation = new Quaternion(0, 0, 0, 1);

    }

    public void AddCar() {
        GameObject newCar = GameObject.Instantiate(TrainCarObj, new Vector3(0, 0, 0), new Quaternion());
        numCars += 1;

        newCar.transform.SetParent(gameObject.transform);
        newCar.transform.localPosition = new Vector3(-5.1f - (numCars - 1) * CarLength, -0.32f, 0);
        newCar.transform.rotation = new Quaternion();
        newCar.transform.localScale = new Vector3(1, 1, 1);

    }
}
