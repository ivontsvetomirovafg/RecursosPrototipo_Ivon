using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [Header("Crafteos")]
    public List<Receta> inventarioUsuario;
    [Header("Recursos")]
    public Recursos[] recursos;
}
