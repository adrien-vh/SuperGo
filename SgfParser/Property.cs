using System;

namespace SuperGo.SgfParser
{
	public class Property
	{
		public readonly string Name;
		private string _value;
		public string Value {get { return _value; }}

		public Property (string name) {
			Name = name;
			_value = "";
		}

		public void concatToValue (char c) {
			_value += c;
		}
	}
}

