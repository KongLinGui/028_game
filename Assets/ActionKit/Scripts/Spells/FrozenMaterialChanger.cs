using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {


	public class FrozenMaterialChanger : MonoBehaviour {
		private Material m_orgmat;
		private Material m_redMat;
		private bool m_frozen=false;
		private Renderer m_renderer;
		private Damagable m_damagable;
		void Start () {
			m_redMat = Resources.Load("RED") as Material;
			m_renderer = GetComponent<Renderer>();
			m_orgmat = m_renderer.material;
			m_damagable = gameObject.GetComponentInParent<Damagable>();
			if(SlowDownTime.SLOW_DOWN_TIME_SCALAR==0)
			{
				m_frozen=true;
				m_renderer.material = m_redMat;
			}
		}
		
		// Update is called once per frame
		void Update () {
			if(SlowDownTime.SLOW_DOWN_TIME_SCALAR==0 && (m_damagable && m_damagable.isAlive() || m_damagable==null)	)
			{
				if(m_frozen==false)
				{
					m_frozen=true;
					m_renderer.material =m_redMat;
				}
			}else{
				if(m_frozen)
				{
					m_frozen=false;

					m_renderer.material = m_orgmat;
				}
			}
		}
	}
}