using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class VechileManager
    {
        protected VechileHandler handler;

        #region Handler
        protected void MakeLevelEventHandler(MakeLevelEvent e)
        {

            FillSlotId();
        }

        protected void FindParkingSpotEventHandler(FindParkingSpotEvent e)
        {
            ParkingSlot slot = handler.ParkingSlots.Find(x => x.Id == e.Id);
            e.ParkingSlot = slot.transform;
        }


        #endregion

        #region Methods

        void FillSlotId()
        {
            for (int i = 1; i <= handler.ParkingSlots.Count; i++)
            {
                handler.Ids.Add(i);
            }

            ShuffleList(handler.Ids);
            for (int i = 0; i < handler.ParkingSlots.Count; i++)
            {
                handler.ParkingSlots[i].Id = handler.Ids[i];
                handler.ParkingSlots[i].TextId.text = handler.ParkingSlots[i].Id.ToString();
            }

            List<ParkingSlot> tempSlots = handler.ParkingSlots.FindAll(X => X.VechileType == VechileType.Car);
            ShuffleList(tempSlots);
            List<CarSlot> carSlots = handler.CarSlots.FindAll(car => car.VechileType == VechileType.Car);
            for (int j = 0; j < carSlots.Count; j++)
            {
                carSlots[j].Id = tempSlots[j].Id;
                carSlots[j].TextId.text = carSlots[j].Id.ToString();
                carSlots[j].ParkingSlot = tempSlots[j];
            }
            ParkingSlot slot = handler.ParkingSlots.Find(x => x.VechileType == VechileType.Bus);
            CarSlot Bus = handler.CarSlots.Find(bus => bus.VechileType == VechileType.Bus);
            Bus.Id = slot.Id;
            Bus.TextId.text = Bus.Id.ToString();
            Bus.ParkingSlot = slot;


        }

        void ShuffleList<T>(List<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        #endregion

    }
}

