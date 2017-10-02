
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwiczeniaGD5 {

	class Program {

		static void Main(string[] args) {

			ParserGD parser = new ParserGD();

			LinkedListGD linkedListGD = new LinkedListGD();

			linkedListGD.ExecuteListGD(parser);

			Console.ReadLine();
		}
	}
}





