using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Util;

namespace Menu {

	public class UILineRenderer : MonoBehaviour  {

		public RectTransform container;
		public GameObject line;
		public GameObject node;
		public List<Vector2> nodePositions;
		public Color color;
		public int lineSize;
		public int nodeSize;

		List<RectTransform> lines;
		List<RectTransform> nodes;

		/// <summary>
		/// Spawns all needed nodes and lines.
		/// </summary>
		public void ConfigureLine() {

			int nodeCount = nodePositions.Count;
			int lineCount = nodeCount - 1;
			lines = new List<RectTransform>();
			nodes = new List<RectTransform>();

			for (int i = 0; i < nodeCount; i++) {
				GameObject go = (GameObject)Instantiate(node);
				RectTransform rect = go.GetComponent<RectTransform>();
				Image img = go.GetComponent<Image>();
				img.color = color;
				rect.SetParent(container);
				rect.localPosition = Vector3.zero;
				rect.localScale = Vector3.one;
				Utility.SetRectWidth(ref rect, nodeSize);
				Utility.SetRectHeight(ref rect, nodeSize);
				nodes.Add(rect);
			}

			for (int i = 0; i < lineCount; i++) {
				GameObject go = (GameObject)Instantiate(line);
				RectTransform rect = go.GetComponent<RectTransform>();
				Image img = go.GetComponent<Image>();
				img.color = color;
				rect.SetParent(container);
				rect.localPosition = Vector3.zero;
				rect.localScale = Vector3.one;
				Utility.SetRectWidth(ref rect, lineSize);
				Utility.SetRectHeight(ref rect, lineSize);
				lines.Add(rect);
			}
		}

		/// <summary>
		/// Positions the nodes and orients the lines between nodes.
		/// </summary>
		public void Draw() {

			for (int i = 0; i < nodes.Count; i++) {

				//calculate with offset
				Vector2 resultPos1 = nodePositions[i];
				Vector2 resultPos2 = Vector2.zero; //will be set below if applicable

				//position nodes
				Vector3 pos = nodes[i].localPosition;
				pos.x = resultPos1.x;
				pos.y = resultPos1.y;
				nodes[i].localPosition = pos;

				//rotate lines
				if (i < nodes.Count - 1) {

					resultPos2 = nodePositions[i+1];

					RectTransform line = lines[i];
					float dX = resultPos2.x - resultPos1.x;
					float dY = resultPos2.y - resultPos1.y;
					float angle = Mathf.Atan2(dY, dX) * Mathf.Rad2Deg;
					float length = Mathf.Sqrt(dX*dX + dY*dY);
					line.localPosition = pos;
					line.rotation = Quaternion.Euler(0, 0, angle);
					Utility.SetRectWidth(ref line, length);
					lines[i] = line;
				}
			}
		}

		public void DestroyElements() {
			if (lines != null) {
				for (int i = lines.Count - 1; i >= 0; i--) {
					Destroy(lines[i].gameObject);
				}
			}
			if (nodes != null) {
				for (int i = nodes.Count - 1; i >= 0; i--) {
					Destroy(nodes[i].gameObject);
				}
			}
			nodePositions = new List<Vector2>();
		}

		/// <summary>
		/// Adds a node position to the line renderer.
		/// All calls for AddNode should be made before Configure() and Draw().
		/// </summary>
		public void AddNode(Vector2 pos) {
			nodePositions.Add(pos);
		}

		/// <summary>
		/// If the index exists, replaces nodePositions[index] with pos
		/// </summary>
		public void SetNode(int index, Vector2 pos) {
			if (index < 0 ||
				index > nodePositions.Count - 1)
				return;

			nodePositions[index] = pos;
		}

		/// <summary>
		/// Returns the node position at index if it exists
		/// </summary>
		public Vector2 GetNode(int index) {
			if (index < 0 ||
				index > nodePositions.Count - 1) 
				return Vector2.zero;

			return nodePositions[index];
		}
	}
}