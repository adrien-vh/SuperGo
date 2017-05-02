using System;
using System.Collections.Generic;

namespace SuperGo.SgfParser
{
	public class Node
	{
		public readonly Node Parent;


		private List<Node> _children = null;
		private Dictionary<string, string> _properties = null;

		public bool HasChildren { get { return _children != null; } }
		public List<Node> Children { get { return _children; } }
		public Dictionary<string, string> Properties { get {return _properties; } }

		public Node (Node parent = null) {
			Parent = parent;
		}

		public Node AddChild(Node child) {
			if (_children == null) {
				_children = new List<Node> ();
			}
			_children.Add (child);

			return child;
		}

		public void AddProperty (string name, string value) {
			if (_properties == null) {
				_properties = new Dictionary<string, string> ();
			}
			_properties.Add (name, value);
		}

		public void PrintPretty(string indent = "  ", bool last = true)
		{
			Console.Write(indent);
			if (last) {
				Console.Write("|-");
				indent += "  ";
			} else {
				Console.Write("|-");
				indent += "| ";
			}

			if (_properties == null) {
				Console.WriteLine ("xx");
			} else {
				if (_properties.ContainsKey ("C")) {
					Console.WriteLine (_properties ["C"]);
				} else {
					Console.WriteLine ("xx");
				}
			}

			if (Children != null) {
				for (int i = 0; i < Children.Count; i++)
					Children [i].PrintPretty (indent, i == Children.Count - 1);
			}
		}
	}
}

