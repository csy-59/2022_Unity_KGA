using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 다음의 GameData는 시리얼 라이저블 하다.
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

    GameData gameDatas;
    public GameData GameData
    {
        get
        {
            if(gameDatas == null)
            {
                // 만약 아직 gameData가 없다면 생성한다.
                LoadGameData(); // 파일이 없어도 자동으로 만들어짐
                SaveGameData(); //
            }

            return gameDatas;
        }


    }

    void InitGameData()
    {
        gameDatas = new GameData(100, 300, 5f);

        gameDatas.monsterKillDatas.Add(new MonsterData(1, "전지윤", 2f, 1f, "오늘 안옴"));
        gameDatas.monsterKillDatas.Add(new MonsterData(2, "권희영", 2f, 1f, "오늘 안옴"));
    }

    public void SaveGameData()
    {

        if(gameDatas == null)
        {
            InitGameData();
        }

        // Json 타입으로 파일을 저장할 것!
        string toJsonData = JsonUtility.ToJson(gameDatas, true); // 이쁘게 gameDatas를 Json 타입으로 변환 (반환은 string)
        string filePath = Application.persistentDataPath + GameDataFileName; // 플렛폼에 따라 저장 위치를 지정해줌
        // c://user/USER/~~프로젝트 명으로
        //해당 경로에 Json 타입으로 변환된 gameDatas를 저장함
        File.WriteAllText(filePath, toJsonData);
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        // 예외 처리: 파일이 없는 경우. 파일이 있으면 true 반환
        if(File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            // 받아온 데이터를 다시 GameData 타입으로 변환
            gameDatas = JsonUtility.FromJson<GameData>(fromJsonData);

            // 데이터가 없는 파일이었을 경우
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


    // 데이터 파일 이름(확장자 붙여주기 위한 용도)
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
