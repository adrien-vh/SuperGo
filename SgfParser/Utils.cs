using System;

namespace SuperGo.SgfParser
{
	public static class Utils
	{
		public static int[] translateCoordinates (string lettersCoordinates) {
			int[] coordinates = new int[2];

			lettersCoordinates = lettersCoordinates.ToLower ();

			coordinates [0] = (int)lettersCoordinates.ToCharArray () [0] - (int)'a';
			coordinates [1] = (int)lettersCoordinates.ToCharArray () [1] - (int)'a';

			return coordinates;
		}
	}
}

