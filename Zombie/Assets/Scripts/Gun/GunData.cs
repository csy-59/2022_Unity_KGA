using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C# 기능[어트리뷰트(특성)]
// 어떤 클래스, 메서드, 변수 등에 대한 추가 정보를 제공하는 문법
// 메타 데이터를 코드로 표현
[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class GunData: ScriptableObject
{
    public AudioClip ShotClip;
    public AudioClip ReloadClip;

    public float Damage = 25f;

    public int InitialAmmoCount = 100;
    public int MagazineCapacity = 25;

    public float FireCooltime = 0.12f;
    public float ReloadTime = 1.8f;
}
