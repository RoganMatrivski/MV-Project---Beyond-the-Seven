using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

using UnityEngine.SceneManagement;

public static class saveConfiguration {
    public static string configurationPath = Application.dataPath + "\\settings.json";

    private static MasterTick masterTick;

    public static void saveConf()
    {
        masterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();

        config conf = new config();

        conf.offset = masterTick.offset;

        using (System.IO.StreamWriter file = System.IO.File.CreateText(configurationPath))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, conf);
        }

        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
    }
}