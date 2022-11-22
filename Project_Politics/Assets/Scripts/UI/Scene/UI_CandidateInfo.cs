using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CandidateInfo : UI_Scene
{
    enum Components
    {
        Number,
        Image,
        PoliticalParty,
        Name,
    }

    public Character Candidate;

    void Start()
    {
        Bind<GameObject>(typeof(Components));

        GetObject((int)Components.Image).GetComponent<SpriteRenderer>().sprite = Candidate.Image;
        GetObject((int)Components.PoliticalParty).GetComponent<Text>().text = Candidate.PoliticalParty;
        GetObject((int)Components.Name).GetComponent<Text>().text = Candidate.Name;
    }
}
