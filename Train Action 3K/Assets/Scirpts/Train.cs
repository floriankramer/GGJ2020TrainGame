using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public static float trainSpeed = 4f;

    private Dictionary<PartType, List<Part>> parts = new Dictionary<PartType, List<Part>>();

    public GameObject GameOverScreen;


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
        
    }

    void Defeat() {
        trainSpeed = 0;
        GameObject o = Instantiate(GameOverScreen, new Vector3(0, 0, 0), new Quaternion());
        o.transform.SetParent(GameObject.Find("Canvas").transform);
        o.transform.position = new Vector3(0, 0, 10);
        o.transform.localScale = new Vector3(1, 1, 1);
        o.transform.rotation = new Quaternion(0, 0, 0, 1);

    }
}
