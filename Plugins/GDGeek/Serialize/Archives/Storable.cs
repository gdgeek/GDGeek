
using UnityEngine;
namespace GDGeek.Archives {

  public abstract class Storable : MonoBehaviour,IStorable {
      //public abstract string save();
      public abstract void load(string serialize);

  }
}