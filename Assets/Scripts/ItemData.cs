using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemData : MonoBehaviour
    {
        public ItemType itemType;
    }
    public enum ItemType
    {
        Madera,
        Metal,
        Alambre,
        Tornillo,
        Destornillador,
        Ganzua,     // Crafteado
        Llave,      // Crafteado
        Palanca     // Crafteado
    }
}
