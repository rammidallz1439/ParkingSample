using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vault
{
    public class ContextController : Registerer
    {
        public override void Enable()
        {
        }

        public override void OnAwake()
        {
            AddController(EventManager.Instance);
            AddController(ObjectPoolManager.Instance);
            AddController(DataManager.Instance);
        }

        public override void OnStart()
        {
        }
    }
}

