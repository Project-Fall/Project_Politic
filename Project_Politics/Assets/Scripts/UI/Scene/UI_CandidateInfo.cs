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
        Awareness,
    }

    public Character Candidate;

    void Start()
    {
        Bind<GameObject>(typeof(Components));

        if (GetObject((int)Components.Image) != null)
            GetObject((int)Components.Image).GetComponent<Image>().sprite = Candidate.Image;

        if(GetObject((int)Components.PoliticalParty) != null)
            GetObject((int)Components.PoliticalParty).GetComponent<Text>().text = Candidate.PoliticalParty;

        if(GetObject((int)Components.Name) != null)
            GetObject((int)Components.Name).GetComponent<Text>().text = Candidate.Name;

        if(GetObject((int)Components.Awareness) != null)
            GetObject((int)Components.Awareness).GetComponent<Text>().text = Candidate.Awareness.ToString();
    }
}
