using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Menu {

	public class UIGraph : MonoBehaviour {

		public UILineRenderer uiLine;
		public RectTransform scrollRect;
		public RectTransform[] nodes;

		List<UILineRenderer> lines;

		void Start() {
			ConfigureGraph();
		}

		void ConfigureGraph() {

			lines = new List<UILineRenderer>();

			for (int i = 0; i < nodes.Length; i++) {
				Vector2 nodePos = new Vector2(nodes[i].localPosition.x, nodes[i].localPosition.y);
				uiLine.AddNode(nodePos);
			}

			uiLine.container = scrollRect;
			uiLine.ConfigureLine();
			uiLine.Draw();
			lines.Add(uiLine);
		}
	}
}
