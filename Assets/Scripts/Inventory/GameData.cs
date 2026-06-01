using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Recursos
{
    public int cantidad;
    public Sprite imageObj;
    public string nombreObj;
}

[Serializable]
public class GameData
{
    [Header("Crafteos")]
    public List<Receta> inventarioUsuario;
    [Header("Recursos")]
    public Recursos[] recursos;

    [Header("Niveles actuales")]
    public Receta espadaActual;
    public Receta armaduraActual;
    public Receta picoActual;
}
