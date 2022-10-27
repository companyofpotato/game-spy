using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private static EquipmentManager currentInstance;

    public static List<Equipment> equipmentList {get; private set;}
    public static int count {get; private set;}

    // 싱글톤 접근용 프로퍼티
    public static EquipmentManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (currentInstance == null)
            {
                // 씬에서 EquipmentManager 오브젝트를 찾아 할당
                currentInstance = FindObjectOfType<EquipmentManager>();
            }

            // 싱글톤 오브젝트를 반환
            return currentInstance;
        }
    }

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 EquipmentManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        equipmentList = new List<Equipment>();

        equipmentList.Add(new Equipment(0, "Fake ID", 10, "Hiding your true self is always useful.", -10, +10, -10, +10));
        equipmentList.Add(new Equipment(1, "Pistol", 30, "Easy to hide, hard to kill.", +5, +90, +20, +10));
        equipmentList.Add(new Equipment(2, "Sniper Rifle", 50, "If you are a skilled shooter, use this.", +10, +100, +10, -5));
        equipmentList.Add(new Equipment(3, "Small Bomb", 20, "Be careful when you handle this.", +5, +80, +0, +0));
        equipmentList.Add(new Equipment(4, "Big Bomb", 50, "If you don't need a body, this works best.", +15, +120, +0, +0));
        equipmentList.Add(new Equipment(5, "Vehicle", 20, "There are times when speed is necessary, even if it is noticeable.", +5, 0, +10, +80));
        equipmentList.Add(new Equipment(6, "Camera", 15, "With this, you don't have to remember what you saw.", +0, +10, +0, +0));
        equipmentList.Add(new Equipment(7, "Eavesdropper", 25, "The walls have ears. Literally.", +0, +30, +10, +0));
        equipmentList.Add(new Equipment(8, "Micro Camera", 30, "A useful tool in a private place.", +0, +40, +5, +0));
        count = equipmentList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
