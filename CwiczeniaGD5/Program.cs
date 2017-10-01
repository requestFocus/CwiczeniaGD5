
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


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

class NodeGD {

	public NodeGD Previous;
	public NodeGD Next;
	public int Value;
}

class LinkedListGD {

	public NodeGD CurrentNode;
	public int Count;

	public LinkedListGD() {

		CurrentNode = null;
	}

	//=================================================================================

	public void AddNodeGD(int nodeValue, int position) {

		NodeGD newNode = new NodeGD();

		if (Count != 0) {               // lista istnieje 

			if (position == 0) {                    // ELEMENT NA POCZATKU

				NodeGD MiddleToHead = CurrentNode;
				GoToStart(ref MiddleToHead);

				newNode.Previous = null;
				newNode.Next = MiddleToHead;
				MiddleToHead.Previous = newNode;
				newNode.Value = nodeValue;
				CurrentNode = newNode;
			}

			else if (position == Count) {           // ELEMENT NA KONCU

				NodeGD MiddleToEnd = CurrentNode;
				GoToStart(ref MiddleToEnd);

				while (MiddleToEnd.Next != null) {                  // przesuwa na ostatni element listy
					MiddleToEnd = MiddleToEnd.Next;
				}

				newNode.Previous = MiddleToEnd;
				newNode.Next = null;
				newNode.Value = nodeValue;
				CurrentNode = newNode;
			}

			else {                                  // ELEMENT W SRODKU

				NodeGD HeadToMiddle = CurrentNode;
				GoToStart(ref HeadToMiddle);

				for (int i = 0; i < position; i++) {                // przesuwa na wskazana pozycje
					HeadToMiddle = HeadToMiddle.Next;
				}

				newNode.Previous = HeadToMiddle.Previous;
				newNode.Next = HeadToMiddle;
				HeadToMiddle.Previous = newNode;
				newNode.Value = nodeValue;
				CurrentNode = newNode;
			}

		}
		else {
			newNode.Previous = null;
			newNode.Next = null;
			newNode.Value = nodeValue;
			CurrentNode = newNode;
		}
		Count++;
	}

	//=========================================================

	public void GoToStart(ref NodeGD item) {

		NodeGD ItemPosition = item;
		NodeGD temp = ItemPosition;
		while (ItemPosition.Previous != null) {
			temp = ItemPosition;                            // do zmiennej tymczasowej leci aktualna pozycja HEAD
			ItemPosition = ItemPosition.Previous;           // do aktualnej pozycji HEAD leci poprzednia pozycja HEAD
			ItemPosition.Next = temp;
		}
		item = ItemPosition;
	}

	//=================================================================================

	public void DeleteNodeGD(int nodeValue) {

		//bool nodeFound = false;

		NodeGD NodeToDelete = CurrentNode;
		GoToStart(ref NodeToDelete);

		while (NodeToDelete != null) {                      // sprawdzaj dopoki aktualnie sprawdzany node nie jest nullem

			if (NodeToDelete.Value == nodeValue) {          // jesli wartosc sprawdzanego noda jest rowna szukanej wartosci

				if (NodeToDelete.Previous == null) {         // jesli znaleziono wartosc na poczatku listy
															 //Console.WriteLine("     Element do usuniecia znajduje sie na poczatku listy");
					NodeToDelete.Next.Previous = null;
					CurrentNode = NodeToDelete.Next;
				}

				else if (NodeToDelete.Next == null) {       // jesli znaleziono wartosc na koncu
															//Console.WriteLine("     Element do usuniecia znajduje sie na końcu listy");
					NodeToDelete.Previous.Next = null;
					CurrentNode = NodeToDelete.Previous;
				}

				else {
					NodeToDelete.Previous.Next = NodeToDelete.Next;
					NodeToDelete.Next.Previous = NodeToDelete.Previous;
					CurrentNode = NodeToDelete.Next;
				}

				//nodeFound = true;
				Count--;
			}

			NodeToDelete = NodeToDelete.Next;               // przejdz na nastepny nod
		}

		//if (!nodeFound)
		//	Console.WriteLine("     Elementu do usunięcia nie ma na liście");
	}

	//=================================================================================

	public void PrintListGD(ref NodeGD node, int count) {

		NodeGD PrintNode = node;
		NodeGD temp = PrintNode;
		while (PrintNode.Previous != null) {
			temp = PrintNode;                   // do zmiennej tymczasowej leci aktualna pozycja HEAD
			PrintNode = PrintNode.Previous;        // do aktualnej pozycji HEAD leci poprzednia pozycja HEAD
			PrintNode.Next = temp;
		}

		Console.Write("List: ");
		while (PrintNode != null) {
			Console.Write(PrintNode.Value + " ");
			PrintNode = PrintNode.Next;
		}
		Console.WriteLine();
	}

	public void ExecuteListGD(ParserGD parser) {

		string[,] SubDataToParse = parser.GetSubDataEntries();
		for (int i = 0; i < SubDataToParse.GetLength(0); i++) {
			if (SubDataToParse[i,0] == "i") {
				AddNodeGD(Int32.Parse(SubDataToParse[i,1]), Int32.Parse(SubDataToParse[i,2]));
			}
			if (SubDataToParse[i,0] == "d") {
				DeleteNodeGD(Int32.Parse(SubDataToParse[i, 1]));
			}
		}

		PrintListGD(ref CurrentNode, Count);
	}
}

class ParserGD {

	string path = "data.txt";
	ArrayList ListToParse;
	string[,] SubData;

	public ParserGD() {
		//new ParserGD(path);
		this.ListToParse = OpenFileSaveRawData(path);
		ParseData(ListToParse);
	}

	//public ParserGD(string path) {
	//	this.path = path;
	//	new ParserGD();
	//}

	private ArrayList OpenFileSaveRawData(string path) {            // SPRAWDZ CZY DANE SA POPRAWNE!

		ArrayList RawData = new ArrayList();
		string fileEntry;

		try {
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
				StreamReader streamReader = new StreamReader(fileStream);
				while ((fileEntry = streamReader.ReadLine()) != "P") {
					RawData.Add(fileEntry);
				}
			}
		}
		catch (Exception ex) {
			Console.WriteLine("Wyjątek : " + ex.Message);
			Console.WriteLine(@"Plik musi mieć nazwę data.txt i musi znajdować się w katalogu X:\Visual Studio\source\repos\ArrayListGD\ArrayListGD\bin\Debug\.");
		}

		return RawData;
	}

	public string[,] ParseData(ArrayList listToParse) {				// ArrayList jest skończona i nie będzie powiększana po uruchomieniu ParseData()

		int entryCount = 0;											// licznik linii dla ArrayList
		foreach (string lineEntry in listToParse) {					// dla każdej linii
			entryCount++;											// zwiększa entryCount
		}
		SubData = new string[entryCount,3];							// deklaracja i zainicjalizowanie tablicy dwuwymiarowej SubData[,] wg entryCount
		entryCount = 0;												// wyzerowanie entryCount na potrzeby indeksowania SubData[][]

		foreach (string lineEntry in listToParse) {					// dla każdego wpisu w ArrayList
			string[] SubDataEntry = lineEntry.Split('_');			// do subFileEntry wpisywana jest tylko jedna linia na raz
			SubData[entryCount, 0] = SubDataEntry[0];				// wstaw string z SubDataEntry[0] do Subdata[i][0]
			SubData[entryCount, 1] = SubDataEntry[1];				// wstaw string z SubDataEntry[0] do Subdata[i][0]
			if (SubDataEntry[0] == "i") {							// tylko jeśli pierwszy element Entry[] jest rowny i
				SubData[entryCount, 2] = SubDataEntry[2];			// wstaw string z SubDataEntry[2] do Subdata[i][2]
			}
			entryCount++;											// przejście na kolejny indeks tablicy SubData[][]
		}
		return SubData;
	}

	public string[,] GetSubDataEntries() {
		return SubData;
	}
}



