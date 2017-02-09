using System;
using System.Collections;

namespace Toolkits
{
	public class Ref<T>
	{
		public T val;
	
		public Ref (T v)
		{
			this.val = v;
		}

		public override string ToString(){
			return Convert.ToString(this.val);
		}
	}
}