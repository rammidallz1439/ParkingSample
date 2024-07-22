using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

namespace Game
{
    public class VechileController : VechileManager,IController
    {
        public VechileController(VechileHandler vechileHandler)
        {
            handler = vechileHandler;
        }

        public void OnInitialized()
        {
            EventManager.Instance.TriggerEvent(new MakeLevelEvent());
        }

        public void OnRegisterListeners()
        {
            EventManager.Instance.AddListener<MakeLevelEvent>(MakeLevelEventHandler);
            EventManager.Instance.AddListener<FindParkingSpotEvent>(FindParkingSpotEventHandler);
        }

        public void OnRelease()
        {
        }

        public void OnRemoveListeners()
        {
            EventManager.Instance.RemoveListener<MakeLevelEvent>(MakeLevelEventHandler);
            EventManager.Instance.RemoveListener<FindParkingSpotEvent>(FindParkingSpotEventHandler);

        }

        public void OnStarted()
        {
        }

        public void OnVisible()
        {
        }
    }
}

