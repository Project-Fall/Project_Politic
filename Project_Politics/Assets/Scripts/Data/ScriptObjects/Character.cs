using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/Character")]
public class Character : ScriptableObject
{
    // (후보자) 이름
    [SerializeField] private string _candidateName;
    public string Name { get { return _candidateName; } }
    
    // 열정 카리스마 공감 리더십 말빨
    [SerializeField] private int[] _stat = new int[5];
    public int[] Stat { get { return _stat; } }

    // 인지도
    public int Awareness
    { get 
        {
            int awareness = 0;

            foreach (int i in Stat)
                awareness += i;
            return awareness;
        }
    }

    // 악명
    [SerializeField] private int _badReputation;
    public int BadReputation { get { return _badReputation; } }

    // 이미지
    public Sprite Image;

    // 정당
    [SerializeField] private string _politicalParty;
    public string PoliticalParty { get { return _politicalParty; } }
}