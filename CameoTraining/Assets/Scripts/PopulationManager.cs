using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] private GameObject _personPrefab;
    [SerializeField] private int _populationSize = 10;

    public static float Elapsed { get; set; } = 0;

    private List<GameObject> _population = new List<GameObject>();

    private int _generation = 1;
    private int _trialTime = 10;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _populationSize; i++)
        {
            Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);

            GameObject firstPeople = Instantiate(_personPrefab, position, Quaternion.identity);

            firstPeople.GetComponent<DNA>().R = Random.Range(0.0f, 1.0f);
            firstPeople.GetComponent<DNA>().G = Random.Range(0.0f, 1.0f);
            firstPeople.GetComponent<DNA>().B = Random.Range(0.0f, 1.0f);

            _population.Add(firstPeople);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Elapsed += Time.deltaTime;

        if (Elapsed > _trialTime)
        {
            BreedNewPopulation();
            Elapsed = 0;
        }
    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();

        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + _generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)Elapsed, guiStyle);
    }

    private void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedPopulation = _population.OrderByDescending(p => p.GetComponent<DNA>().TimeToDie).ToList();

        _population.Clear();

        // Breed upper half of sorted population
        for (int i = (int)(sortedPopulation.Count / 2.0f) - 1; i < sortedPopulation.Count - 1; i++)
        {
            _population.Add(Breed(sortedPopulation[i], sortedPopulation[i + 1]));
            _population.Add(Breed(sortedPopulation[i + 1], sortedPopulation[i]));
        }

        // Destroy all parents and previous population
        for (int i = 0; i < sortedPopulation.Count; i++)
        {
            Destroy(sortedPopulation[i]);
        }

        _generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);

        GameObject offSpring = Instantiate(_personPrefab, position, Quaternion.identity);

        DNA dnaParent1 = parent1.GetComponent<DNA>();
        DNA dnaParent2 = parent2.GetComponent<DNA>();

        // Swap parent DNA
        if (Random.Range(0, 1000) > 5)
        {
            offSpring.GetComponent<DNA>().R = Random.Range(0, 10) < 5 ? dnaParent1.R : dnaParent2.R;
            offSpring.GetComponent<DNA>().G = Random.Range(0, 10) < 5 ? dnaParent1.G : dnaParent2.G;
            offSpring.GetComponent<DNA>().B = Random.Range(0, 10) < 5 ? dnaParent1.B : dnaParent2.B;
        }
        else
        {
            offSpring.GetComponent<DNA>().R = Random.Range(0.0f, 1.0f);
            offSpring.GetComponent<DNA>().G = Random.Range(0.0f, 1.0f);
            offSpring.GetComponent<DNA>().B = Random.Range(0.0f, 1.0f);
        }

        return offSpring;
    }
}
