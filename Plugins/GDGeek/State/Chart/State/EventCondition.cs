using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDGeek.StateChart
{
    [System.Serializable]
    public class EventCondition
    {
        [SerializeField]
        public string evt;
        [SerializeField]
        public MessageHandler handler;
       

        public void building(GDGeek.State state)
        {
            state.addAction(this.evt, delegate (FSMEvent evt)
            {

               

                if (this.handler != null)
                {
                    LeafState next = handler.invoke(evt);
                    if(next != null) {
                        return next.stateName;
                    }
                }
              
                return "";
            });

           
        }
    }
}