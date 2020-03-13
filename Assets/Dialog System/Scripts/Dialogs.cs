using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// переменные, хранящие имя говорящего и его фразы
[System.Serializable]
public class Dialogs {

    public string npc_name;

    [TextArea(1, 20)]
    public string phrases;
}
