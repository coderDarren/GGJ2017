using UnityEngine;
using System.Collections;

namespace PoolingServices {

	public class Pool : MonoBehaviour {

		GameObject[] objects;

		public void ConfigurePool(Transform container, GameObject prefab, int size)
		{
			objects = new GameObject[size];
			for (int i = 0; i < objects.Length; i++)
			{
				GameObject go = (GameObject)Instantiate(prefab);
				go.transform.parent = container;
				go.SetActive(false);
				objects[i] = go;
			}
		}

		public int GetObject()
		{
			for (int i = 0; i < objects.Length; i++) {
				if (!objects[i].activeSelf) {
					objects[i].SetActive(true);
					return i;
				}
			}
			return -1;
		}

		public void DiscardObject(int id)
		{
			if (id < 0 || id >= objects.Length)
				return;

			objects[id].SetActive(false);
		}

		public Transform ObjectTransform(int id)
		{
			if (id < 0 || id >= objects.Length)
				return null;

			return objects[id].transform;
		}
	}
}