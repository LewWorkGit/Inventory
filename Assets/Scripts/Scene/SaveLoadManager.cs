using UnityEngine;
using System.Collections;
using System;

public class SaveLoadManager : MonoBehaviour
{
    [Header("Настройки")]
    private bool loadOnStart = true;
    private bool saveOnExit = true;
    private float saveInterval = 60f; // Автосохранение каждые N секунд

    private float lastSaveTime;

    public event Action OnLoadOver;
    private void Start()
    {
        if (loadOnStart)
        {
            StartCoroutine(LoadAllDelayed());
        }

        if (saveInterval > 0)
        {
            StartCoroutine(AutoSaveRoutine());
        }
    }

    private IEnumerator LoadAllDelayed()
    {
        // Ждем один кадр, чтобы все объекты успели инициализироваться
        yield return null;

        LoadAll();
    }

    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(saveInterval);
            SaveAll();
        }
    }

    private void OnApplicationQuit()
    {
        if (saveOnExit)
        {
            SaveAll();
        }
    }

    public void SaveAll()
    {
        SavableObject[] savables = FindObjectsOfType<SavableObject>(true);
        foreach (var savable in savables)
        {
            savable.SaveObject();
        }
      
        lastSaveTime = Time.time;
    }

    public void LoadAll()
    {
        SavableObject[] savables = FindObjectsOfType<SavableObject>(true);
        foreach (var savable in savables)
        {
            savable.LoadObject();
        }

        OnLoadOver?.Invoke();
    }

    public void SetSaveExit(bool isSave)
    {
        saveOnExit = isSave;
    }
}