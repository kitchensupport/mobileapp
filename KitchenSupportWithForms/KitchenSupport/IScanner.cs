using System;
using System.Threading.Tasks;

namespace KitchenSupport
{
	public interface IScanner
	{
		Task<string> Scan();
	}
}