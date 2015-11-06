using System;
using System.Threading.Tasks;

namespace MCCForms
{
	public interface IGPSCoordinates
	{
		Task<GPSCoordinatesResponse> GetLocation ();

		bool IsLocationServiceEnabled ();
	}
}