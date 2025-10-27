using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] RecipeDatabase database;
    private Dictionary<CounterType, List<ProcessRule>> listProcess = new();


    public static RecipeManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);    
        }

        SetUpData();
    }

    private void SetUpData()
    {
        if (database == null) return;

        listProcess.Clear();
        foreach (var process in database.processGroups)
        {
            listProcess.Add(process.counterType, process.rules);
        }
    }

    public ProcessRule GetOutputKitchen(CounterType counterType, KitchenType inputType)
    {
        if(listProcess.TryGetValue(counterType, out List<ProcessRule> rules))
        {
            foreach (ProcessRule rule in rules)
            {
                if(rule.inputType == inputType) return rule;
            }    
        }
        return null;
    }
    

}
