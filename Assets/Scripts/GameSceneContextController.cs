using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;
public class GameSceneContextController : Registerer
{
    [SerializeField] private VechileHandler _vechileHandler;
    public override void Enable()
    {
    }

    public override void OnAwake()
    {
        AddController(new VechileController(_vechileHandler));
    }

    public override void OnStart()
    {
    }
}
