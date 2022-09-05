using Kingmaker.PubSubSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOTR_BOAT_BOAT_BOAT.MechanicsChanges
{
    class InjectStuffOnLoad : IAreaActivationHandler
    {

        public static List<Action> Injections = new();
        public void OnAreaActivated()
        {
            foreach (var injection in Injections)
            {
                injection();
            }
        }
    }
}
