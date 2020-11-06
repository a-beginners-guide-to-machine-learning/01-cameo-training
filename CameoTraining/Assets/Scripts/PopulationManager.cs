using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] private GameObject _personPrefab;
    [SerializeField] private int _populationSize = 10;
    [SerializeField] private static float _elapsed = 0;

    private List<GameObject> _population = new List<GameObject>();

    private int _generation = 1;
    private int _trialTime = 10;

    private GUIStyle _guiStyle = new GUIStyle();

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _populationSize; i++)
        {
            Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);

            GameObject gameObject = Instantiate(_personPrefab, position, Quaternion.identity);

            gameObject.GetComponent<DNA>().R = Random.Range(0.0f, 1.0f);
            gameObject.GetComponent<DNA>().G = Random.Range(0.0f, 1.0f);
            gameObject.GetComponent<DNA>().B = Random.Range(0.0f, 1.0f);

            _population.Add(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        _elapsed += Time.deltaTime;

        if (_elapsed < _trialTime)
        {
            // BreedNewPopulation();
            _elapsed = 0;
        }
    }

    private void OnGUI()
    {
        _guiStyle.fontSize = 50;
        _guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + _generation, _guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)_trialTime, _guiStyle);
    }
}
