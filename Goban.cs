using System;
using System.Collections.Generic;

namespace SuperGo
{
	public class Goban
	{
		private uint[,] _board;
		public readonly int Taille;

		public uint[,] Board { get { return _board; } }
		
		public Goban (int taille = 19)
		{
			Taille = taille;
			_board = new uint[taille, taille];
			for (int i = 0; i < Taille; i++) {
				for (int j = 0; j < Taille; j++) {
					_board [i, j] = 0;
				}
			}
		}

		public void setCase(int x, int y, uint value) {
			_board [x, y] = value;
		}

		public void printGoban() {
			Dictionary<uint, char> symbols = new Dictionary<uint, char> ();
			Dictionary<uint, ConsoleColor> colors = new Dictionary<uint, ConsoleColor> ();


			symbols [0] = '·';
			symbols [1] = '⬤';
			symbols [2] = '⬤';

			colors [0] = ConsoleColor.Gray;
			colors [1] = ConsoleColor.Black;
			colors [2] = ConsoleColor.White;

			Console.BackgroundColor = ConsoleColor.DarkRed;
			Console.ForegroundColor = ConsoleColor.Black;

			Console.Clear ();
			Console.Write ("\n");

			Console.Write ("  ");
			for (int i = 0; i < Taille; i++) {
				Console.Write (" " + ((char)('a' + i)).ToString());
			}
			Console.Write ("\n");

			for (int i = 0; i < Taille; i++) {
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write (" " + ((char)('a' + i)).ToString());
				for (int j = 0; j < Taille; j++) {
					Console.ForegroundColor = colors [_board [i, j]];
					Console.Write (" " + symbols [_board [i, j]].ToString() );
				}
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write (" " + ((char)('a' + i)).ToString());
				Console.Write ("\n");
			}

			Console.Write ("  ");
			for (int i = 0; i < Taille; i++) {
				Console.Write (" " + ((char)('a' + i)).ToString());
			}

			Console.Write ("\n");
		}
	}
}

