using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    public void CreateNewSave()
    {
        this.gameData = new GameData();
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataHandler.Save(gameData);

        // update stats handler to match new save data
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetMoney(gameData.money);
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetDay(gameData.day);
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetCurrentXP(gameData.currentXP);
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetRequiredXP(gameData.requiredXP);
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetReputationLevel(gameData.reputationLevel);
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetTuition(gameData.tuition);

        // update item handler
        GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().SetItems(gameData.items);
        GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().SetUpgrades(gameData.upgrades);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    public void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
