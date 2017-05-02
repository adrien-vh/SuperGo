using System;
using System.IO;
using System.Collections.Generic;

namespace SuperGo.SgfParser
{
	public class Parser
	{
		private List<string> warnings = new List<string> ();
		private List<string> errors = new List<string> ();

		public List<string> Warnings {get { return warnings; }}
		public List<string> Errors {get { return errors; }}

		public Parser() {
		}

		public List<Game> ReadSgfFile(string sgfFile) {

			List<Game> games = new List<Game> ();

			using (StreamReader r = new StreamReader(sgfFile))
			{
				uint currentLineNo = 1;
				uint currentCharNo = 1;

				Game currentGame = null;
				Node currentNode = null;
				Property currentProperty = null;
				string currentPropertyName = "";
				bool isEscaping = false;
				bool escapeIfLineReturn = false;
				uint startedBranches = 0;
				Stack<Node> parents = new Stack<Node> ();

				char c;
				char[] buffer = new char[1024];
				int read;
				while ((read = r.ReadBlock(buffer, 0, buffer.Length)) > 0)
				{
					for (int i = 0; i < read; i++) {
						c = buffer [i];
						if (currentGame == null) {
							if (!Char.IsWhiteSpace (c)) {
								if (c == SgfConstants.CHAR_BRANCH_START) {
									currentGame = new Game ();
									currentNode = currentGame.RootNode;
								} else {
									addWarning (SgfConstants.CHAR_BRANCH_START + " attendu...");
								}
							}
						} else if (currentProperty == null) {
							if (!Char.IsWhiteSpace (c)) {
								if (c == SgfConstants.CHAR_BRANCH_START) {
									parents.Push (currentNode);
									startedBranches++;
								} else if (c == SgfConstants.CHAR_NODE_START) {
									currentNode = currentNode.AddChild (new Node (currentNode));
								} else if (c == SgfConstants.CHAR_PROP_START) {
									currentProperty = new Property (currentPropertyName);
									currentPropertyName = "";
								} else if (c == SgfConstants.CHAR_BRANCH_END) {
									if (startedBranches > 0) {
										startedBranches--;
										currentNode = parents.Pop();
									} else {
										games.Add (currentGame);
										currentGame = null;
									}
								} else {
									currentPropertyName += c;
								}
							}
						} else {
							if (isEscaping) {
								if (isLineReturn (c)) {
									escapeIfLineReturn = true;
								} else if (c == SgfConstants.CHAR_PROP_END) {
									currentProperty.concatToValue (SgfConstants.CHAR_PROP_END);
								}
								isEscaping = false;
							} else if (c == '\\') {
								isEscaping = true;
							} else if (c == SgfConstants.CHAR_PROP_END) {
								currentNode.AddProperty (currentProperty.Name, currentProperty.Value);
								currentProperty = null;
							} else {
								if (escapeIfLineReturn) {
									if (!isLineReturn (c)) {
										currentProperty.concatToValue (c);
									} else {
										currentProperty.concatToValue (c);
									}
								} else {
									currentProperty.concatToValue (c);
								}
							}
						}

						//Console.WriteLine (c);
						if (isLineReturn(c)) {
							currentLineNo++;
							currentCharNo = 1;
						} else {
							currentCharNo++;
						}
					}
				}
			}

			return games;
		}

		private void addWarning(string message) {
			warnings.Add (message);
			Console.Write (message);
		}

		private static bool isLineReturn(char c) {
			return (c == '\r' || c == '\n');
		}
			
	}
}

