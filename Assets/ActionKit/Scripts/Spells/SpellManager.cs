using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class SpellManager : MonoBehaviour {

		public delegate void OnSpellStart(BaseSpell.SpellID spellID);
		public static event OnSpellStart onSpellStart;
		public static void spellStart(BaseSpell.SpellID spellID)
		{
			if(onSpellStart!=null)
			{
				onSpellStart(spellID);	
			}
		}
		
		public delegate void OnSpellEnd(BaseSpell.SpellID spellID);
		public static event OnSpellEnd onSpellEnd;
		public static void spellEnd(BaseSpell.SpellID spellID)
		{
			if(onSpellEnd!=null)
			{
				onSpellEnd(spellID);	
			}
		}
	}
}