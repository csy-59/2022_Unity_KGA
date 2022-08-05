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
    // 싱글톤 적용
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

    // 데이터 파일 이름
    public string GameDataFileName = ".json";

    [Header("몬스터 관련 DB")]
    [SerializeField] TextAsset monsterDB; // 이 형식으로 데이터 파일을 받아옴(시리얼 라이즈)
    public Dictionary<int, MonsterData> MonsterDataDict { get; set; } // <인덱트, 몬스터 데이터>


    private void SetMonsterDataFromCSV()
    {
        // 외부 파일을 프로그램 실행 전에 불러오는 것이 아닌, 실행 중에 해당 함수가 호출되면 파일을 불러옴

        monsterDB = Resources.Load<TextAsset>("CSV/GameData - Monster");

        if(monsterDB == null)
        {
            Debug.LogError("SetMonsterDataFromCSV: 파일이 없으셈!");
        }

        if(MonsterDataDict == null)
        {
            MonsterDataDict = new Dictionary<int, MonsterData>();
        }

        // 문서의 처음부터 끝까지 줄바꿈이 있는 것을 기준으로 데이터를 나눠서 string 배열을 반환함
        string[] lines = monsterDB.text.Substring(0, monsterDB.text.Length).Split('\n');

        // 받아온 데이터를 나눠서 저장함
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

        Debug.LogWarning(index + ", 해당 인덱스 데이터 없음");
        return null;
    }
}
