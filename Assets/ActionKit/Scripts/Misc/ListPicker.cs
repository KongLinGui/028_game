using UnityEngine;
using System.Collections;

namespace InaneGames {

	/// <summary>
	/// List picker.
	/// </summary>
	public class ListPicker  {
		private int m_listCount;
		private bool[] m_inUse;
		private int m_count=0;
		
		public bool resetWhenDone = true;
		public bool sequentially = false;

		private int m_index=0;
		public ListPicker(int count,bool s=false)
		{
			init (count,s);
		}
		public void init (int count, bool s)
		{
			m_count=count;
			sequentially = s;
			m_inUse = new bool[count];
			reset();
		}

		public void reset()
		{
			if(resetWhenDone)
			{
				m_listCount = m_count-1;
				for(int i=0; i<m_count; i++)
				{
					m_inUse[i] = false;
				}		
			}
		}
		public int pickRandomIndex()
		{
			int rc = -1;
			if(m_count>0 && m_listCount>-1)
			{
				int randomIndex = Random.Range(0,m_listCount);

				if(sequentially)
				{
					randomIndex = m_index++;
					if(m_index>=m_count)
					{
						m_index=0;
					}
				}
				rc = selectIndex( randomIndex);
				m_listCount--;
			

			}
			if(rc==-1)
			{
				reset();
				rc=pickRandomIndex();

			}

			return rc;
		}

		public int selectIndex(int index)
		{
			if(index==-1)
			{
				return -1;
			}
			int count = 0;
			int rc = -1;
			if(m_inUse==null)
			{
				return -1;
			}
			for(int i=0; i<m_inUse.Length && rc == -1; i++)
			{
				if(m_inUse[i]==false)
				{
					if(count == index)
					{
						m_inUse[i] = true;
						rc = i;
					}

					
					count++;
				}
				
			}
			return rc;
		}
	}
}
