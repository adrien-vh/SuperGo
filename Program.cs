using System;
using System.Collections.Generic;
using SuperGo.SgfParser;
using WebServer;


namespace SuperGo
{
	class Program
	{
		public static void Main(string[] args)
		{
			var parser = new Parser ();
			int[] coordonnees;
			Goban g = new Goban ();

			List<Game> games = parser.ReadSgfFile ("test1.sgf");

			var s = new Server("http://localhost:8000/");
			
			s.start().Join();
			
			Node n = games [0].RootNode;
			while (n != null) {
				if (n.Properties != null) {
					if (n.Properties.ContainsKey ("B")) {
						coordonnees = Utils.translateCoordinates (n.Properties ["B"]);
						//Console.Write ("Noir : " + n.Properties ["B"] + " x : " + coordonnees[0] + " y : " + coordonnees[1] + "\n");
						g.setCase(coordonnees[0], coordonnees[1], 1);

					} 
					if (n.Properties.ContainsKey ("W")) {
						coordonnees = Utils.translateCoordinates (n.Properties ["W"]);
						//Console.Write ("Blanc : " + n.Properties ["W"] + " x : " + coordonnees[0] + " y : " + coordonnees[1] + "\n");
						g.setCase(coordonnees[0], coordonnees[1], 2);
					}
				}

				if (n.Children != null) {
					n = n.Children [0];
				} else {
					n = null;
				}

				g.printGoban ();
				Console.ReadKey ();
			}
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}