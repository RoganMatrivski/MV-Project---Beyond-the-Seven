using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadConfiguration : MonoBehaviour
{
    string json;

    private MasterTick masterTick;
    // Use this for initialization
    void Start()
    {
        json = System.IO.File.ReadAllText(saveConfiguration.configurationPath);
        masterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();

        if (masterTick == null)
            throw new System.Exception("Can't find MasterTick to configure offset to.");

        config conf = Newtonsoft.Json.JsonConvert.DeserializeObject<config>(json);

        masterTick.offset = conf.offset;
    }
}