
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
				//Console.WriteLine("Dodane CurrentNode.Value " + CurrentNode.Value + " na koncu listy");
				//Console.WriteLine("Poprzednia CurrentNode.Previous.Value " + CurrentNode.Previous.Value);
				//Console.WriteLine("Head.Value " + Head.Value);

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
				//Console.WriteLine("Dodane CurrentNode.Value " + CurrentNode.Value + " na koncu listy");
				//Console.WriteLine("Poprzednia CurrentNode.Previous.Value " + CurrentNode.Previous.Value);
				//Console.WriteLine("Head.Value " + Head.Value);
			}

			else {                                  // ELEMENT W SRODKU

				NodeGD HeadToMiddle = CurrentNode;
				GoToStart(ref HeadToMiddle);

				for (int i = 0; i < position; i++) {                // przesuwa na wskazana pozycje
					HeadToMiddle = HeadToMiddle.Next;
				}
				//Console.WriteLine("Szukana pozycja HeadToMiddle.Value ma wartosc: " + HeadToMiddle.Value);
				//Console.WriteLine("Przed przypisaniami element CurrentNode.Value ma wartość: " + CurrentNode.Value);

				newNode.Previous = HeadToMiddle.Previous;
				newNode.Next = HeadToMiddle;
				HeadToMiddle.Previous = newNode;
				newNode.Value = nodeValue;
				CurrentNode = newNode;
				//Console.WriteLine("Po dodaniu elementu CurrentNode.Value ma wartość: " + CurrentNode.Value);
			}

		}
		else {
			//Console.WriteLine("Lista w tym momencie nie istnieje");
			// stworz pierwszy element
			//Console.WriteLine(Count + ") Probuje stworzyc pierwszy element");
			newNode.Previous = null;
			newNode.Next = null;
			newNode.Value = nodeValue;
			CurrentNode = newNode;
			//Console.WriteLine("Dodane CurrentNode.Value " + CurrentNode.Value + " na poczatku listy // Ustawienie Head");
			//Head = CurrentNode;													// USTAWIENIE HEAD
		}
		Count++;

		//PrintListGD(ref CurrentNode, Count);
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

		//Console.WriteLine("     Probuje usunąć element " + nodeValue);     // USTAWIENIE HEAD

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
					//Console.WriteLine("     Element do usuniecia znajduje sie w środku listy");
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

		//PrintListGD(ref CurrentNode, Count);
	}

	//=================================================================================

	public void PrintListGD(ref NodeGD node, int count) {

		NodeGD PrintNode = node;
		NodeGD temp = PrintNode;
		while (PrintNode.Previous != null) {
			//Console.WriteLine("PrintNode.Value w PrindListGD podczas wyzerowania: " + PrintNode.Value);
			temp = PrintNode;                   // do zmiennej tymczasowej leci aktualna pozycja HEAD
			PrintNode = PrintNode.Previous;        // do aktualnej pozycji HEAD leci poprzednia pozycja HEAD
			PrintNode.Next = temp;
			//Console.WriteLine("PrintNode.Value w PrindListGD po wyzerowaniu: " + PrintNode.Value);
		}

		//Console.WriteLine("PrintNode.Value w PrindListGD po wyzerowaniu: " + PrintNode.Value);

		Console.Write("List: ");
		while (PrintNode != null) {
			Console.Write(PrintNode.Value + " ");
			PrintNode = PrintNode.Next;
			//Console.Write(" // CurrentNode.Value w PrindListGD w petli wypisującej: " + PrintNode.Value);
		}
		Console.WriteLine();
	}

	public void ExecuteListGD(ParserGD parser) {

		//AddNodeGD(3, 0);
		//AddNodeGD(5, 1);
		//AddNodeGD(7, 2);
		//AddNodeGD(1, 3);
		//AddNodeGD(111, 0);
		//AddNodeGD(222, 0);
		//AddNodeGD(2, 4);
		//AddNodeGD(22, 2);
		//DeleteNodeGD(3);
		//AddNodeGD(4444, 1);
		//AddNodeGD(666, 3);
		//AddNodeGD(99, 2);
		//DeleteNodeGD(1);
		//AddNodeGD(77, 4);
		//DeleteNodeGD(7);
		//AddNodeGD(44, 0);
		//AddNodeGD(33, 3);
		//AddNodeGD(6, 7);
		//DeleteNodeGD(44);
		//AddNodeGD(7, 7);
		//DeleteNodeGD(7);

		string[,] SubDataToParse = parser.GetSubDataEntries();
		for (int i = 0; i < SubDataToParse.GetLength(0); i++) {
			//Console.WriteLine(subDataToParse[i]);
			if (SubDataToParse[i,0] == "i") {
				//Console.WriteLine("dodajemy");
				AddNodeGD(Int32.Parse(SubDataToParse[i,1]), Int32.Parse(SubDataToParse[i,2]));
			}
			if (SubDataToParse[i,0] == "d") {
				//Console.WriteLine("odejmujemy");
				//Console.Write("usun: " + subDataToParse[i].Substring(2, 1));
				DeleteNodeGD(Int32.Parse(SubDataToParse[i, 1]));
			}
			//Console.WriteLine();
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
			//Console.WriteLine("wpis oryginalny: " + lineEntry);
			string[] SubDataEntry = lineEntry.Split('_');			// do subFileEntry wpisywana jest tylko jedna linia na raz
			//Console.Write("wpis pocięty: ");
			//Console.Write(SubDataEntry[0]);
			SubData[entryCount, 0] = SubDataEntry[0];				// wstaw string z SubDataEntry[0] do Subdata[i][0]
			//Console.Write(SubDataEntry[1]);
			SubData[entryCount, 1] = SubDataEntry[1];				// wstaw string z SubDataEntry[0] do Subdata[i][0]
			if (SubDataEntry[0] == "i") {							// tylko jeśli pierwszy element Entry[] jest rowny i
				//Console.Write(SubDataEntry[2]);	
				SubData[entryCount, 2] = SubDataEntry[2];			// wstaw string z SubDataEntry[2] do Subdata[i][2]
			}
			entryCount++;											// przejście na kolejny indeks tablicy SubData[][]
			//Console.WriteLine();
		}
		return SubData;
	}

	public string[,] GetSubDataEntries() {
		return SubData;
	}
}



