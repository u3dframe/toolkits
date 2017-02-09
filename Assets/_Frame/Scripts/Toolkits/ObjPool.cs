using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Toolkits;
using System.IO;

/// ///////////////////////////////////////////////////////////////////////////////////////////////////
public abstract class AbstractObjectPool<T>
{
	public Queue<T> queue = new Queue<T> ();
	
	public abstract T createObject (string key = null);
	
	public abstract T resetObject (T t);
	
	protected int max;
	
	public T borrowObject (string key = null)
	{
		if (queue.Count > 0) {
			T en = queue.Dequeue ();
			return resetObject(en);
		}
		
		return createObject (key);
	}
	
	public void returnObject (T obj)
	{
		if (max > 0 && queue.Count > max)
			return;
		
		if (obj is T) {
			obj = resetObject (obj);
			queue.Enqueue (obj);
		}
	}
	
	public bool typeTrue (object obj)
	{
		return obj is T;
	}
	
	public void Clear ()
	{
		queue.Clear ();
	}
	
	public int Count ()
	{
		return queue.Count;
	}
}
/// ///////////////////////////////////////////////////////////////////////////////////////////////////

public class StringPool : AbstractObjectPool<StringBuilder>
{
	public override StringBuilder createObject (string name = null)
	{
		return new StringBuilder ();
	}

	public override StringBuilder resetObject (StringBuilder t)
	{
		t.Remove (0, t.Length);
		return t;
	}
}

/// ///////////////////////////////////////////////////////////////////////////////////////////////////

public class MemoryStreamPool : AbstractObjectPool<MemoryStream>
{
	public override MemoryStream createObject (string name = null)
	{
		MemoryStream ret = new MemoryStream ();
		return ret;
	}

	public override MemoryStream resetObject (MemoryStream t)
	{
		t.Position = 0;
		return t;
	}
}


/// ///////////////////////////////////////////////////////////////////////////////////////////////////
public static class ObjPool
{
	public static StringPool strs = new StringPool ();
	//public static MemoryStreamPool mems = new MemoryStreamPool ();
}