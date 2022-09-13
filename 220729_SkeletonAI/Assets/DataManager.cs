using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ������ GameData�� �ø��� �������� �ϴ�.
[Serializable]
public class GameData
{
    public int BGM_Volume = 0;
    public int Effect_Volume = 0;

    public int gold = 0;
    public int hp = 10;
    public float moveSpeed = 5f;

    public List<MonsterData> monsterKillDatas;

    public GameData(int _gold, int _hp, float _moveSpeed)
    {
        gold = _gold;
        hp = _hp;
        moveSpeed = _moveSpeed;

        monsterKillDatas = new List<MonsterData>();
    }
}

[Serializable]
public class MonsterData
{
    public int index;
    public string name;
    public float moveSpeed;
    public float rotationSpeed;
    public string description;

    public MonsterData(int index, string name, float moveSpeed, float rotationSpeed, string description)
    {
        this.index = index;
        this.name = name;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
        this.description = description;
    }
}

public class DataManager : MonoBehaviour
{
    // �̱��� ����
    static GameObject container;
    static GameObject Container { get => container; }

    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if(instance == null)
            {
                container = new GameObject();
                container.name = "DataMagr";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;

                instance.SetMonsterDataFromCSV();
                DontDestroyOnLoad(container);
            }

            return instance;
        }
    }

    GameData gameDatas;
    public GameData GameData
    {
        get
        {
            if(gameDatas == null)
            {
                // ���� ���� gameData�� ���ٸ� �����Ѵ�.
                LoadGameData(); // ������ ��� �ڵ����� �������
                SaveGameData(); //
            }

            return gameDatas;
        }


    }

    void InitGameData()
    {
        gameDatas = new GameData(100, 300, 5f);

        gameDatas.monsterKillDatas.Add(new MonsterData(1, "������", 2f, 1f, "���� �ȿ�"));
        gameDatas.monsterKillDatas.Add(new MonsterData(2, "����", 2f, 1f, "���� �ȿ�"));
    }

    public void SaveGameData()
    {

        if(gameDatas == null)
        {
            InitGameData();
        }

        // Json Ÿ������ ������ ������ ��!
        string toJsonData = JsonUtility.ToJson(gameDatas, true); // �̻ڰ� gameDatas�� Json Ÿ������ ��ȯ (��ȯ�� string)
        string filePath = Application.persistentDataPath + GameDataFileName; // �÷����� ���� ���� ��ġ�� ��������
        // c://user/USER/~~������Ʈ ������
        //�ش� ��ο� Json Ÿ������ ��ȯ�� gameDatas�� ������
        File.WriteAllText(filePath, toJsonData);
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        // ���� ó��: ������ ���� ���. ������ ������ true ��ȯ
        if(File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            // �޾ƿ� �����͸� �ٽ� GameData Ÿ������ ��ȯ
            gameDatas = JsonUtility.FromJson<GameData>(fromJsonData);

            // �����Ͱ� ���� �����̾��� ���
            if(gameDatas == null)
            {
                InitGameData();
            }
        }
        else
        {
            InitGameData();
        }

        
    }


    // ������ ���� �̸�(Ȯ���� �ٿ��ֱ� ���� �뵵)
    public string GameDataFileName = ".json";

    [Header("���� ���� DB")]
    [SerializeField] TextAsset monsterDB; // �� �������� ������ ������ �޾ƿ�(�ø��� ������)
    public Dictionary<int, MonsterData> MonsterDataDict { get; set; } // <�ε�Ʈ, ���� ������>


    private void SetMonsterDataFromCSV()
    {
        // �ܺ� ������ ���α׷� ���� ���� �ҷ����� ���� �ƴ�, ���� �߿� �ش� �Լ��� ȣ��Ǹ� ������ �ҷ���

        monsterDB = Resources.Load<TextAsset>("CSV/GameData - Monster");

        if(monsterDB == null)
        {
            Debug.LogError("SetMonsterDataFromCSV: ������ ������!");
        }

        if(MonsterDataDict == null)
        {
            MonsterDataDict = new Dictionary<int, MonsterData>();
        }

        // ������ ó������ ������ �ٹٲ��� �ִ� ���� �������� �����͸� ������ string �迭�� ��ȯ��
        string[] lines = monsterDB.text.Substring(0, monsterDB.text.Length).Split('\n');

        // �޾ƿ� �����͸� ������ ������
        for(int i = 1; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(',');

            MonsterDataDict.Add(int.Parse(row[0]), new MonsterData(
                int.Parse(row[0]),      // index
                row[1],                 // name
                float.Parse(row[2]),    // moveSpeed
                float.Parse(row[3]),    // rotationSpeed
                row[4]                  // description
                ));
        }
    }

    public MonsterData GetMonsterData(int index)
    {
        if(MonsterDataDict.ContainsKey(index))
        {
            return MonsterDataDict[index];
        }

        Debug.LogWarning(index + ", �ش� �ε��� ������ ����");
        return null;
    }
}
