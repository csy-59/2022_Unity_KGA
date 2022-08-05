using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // ������ ���� �̸�
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