using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyUnit : MonoBehaviour
{
   public int damage;
   public int heal;
   public int currentHP;
   public int maxHP;
   public int protect;
   public ActionType intent;
   
   public bool takeDamage(int damage)
   {
      currentHP -= (damage - protect);

      if (currentHP <= 0)
      {
         return true;
      }
      else
      {
         return false;
      }
   }
   
   public void resetValues()
   {
      damage = 0;
      heal = 0;
   }
   
   public enum ActionType
   {
      DAMAGE,
      PROTECT,
      DAMAGEPROTECT,
      HEAL
   }

   public void getNewAction()
   {
      Array values = Enum.GetValues(typeof(ActionType));
      System.Random rnd = new System.Random();
      intent = (ActionType) values.GetValue(rnd.Next(values.Length));
      
      switch (intent)
      {
         case ActionType.DAMAGE :
            damage = 30;
            break;
         case ActionType.HEAL:
            heal = 25;
            break;
         case ActionType.PROTECT:
            protect = 20;
            break;
         case ActionType.DAMAGEPROTECT:
            damage = 15;
            protect = 15;
            break;
      }
   }

}
