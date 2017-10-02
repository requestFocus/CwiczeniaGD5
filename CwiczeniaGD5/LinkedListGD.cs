using System;

namespace CwiczeniaGD5 {

	class LinkedListGD {

		public NodeGD CurrentNode;						// czy jeśli dam tutaj { get; private set; } to pokaże się błąd, bo miejsce na definiowanie NodeGD jest w konstruktorze NodeGD?
		public int Count { get; private set; }

		public LinkedListGD() {

			CurrentNode = null;
		}

		public class NodeGD {

			public NodeGD Previous { get; set; }
			public NodeGD Next { get; set; }
			public int Value { get; set; }
		}

		//================AddNodeGD

		private void AddNodeGD(int nodeValue, int position) {

			NodeGD newNode = new NodeGD();

			if (Count != 0) {											// lista istnieje 

				if (position == 0) {                    // ELEMENT NA POCZATKU

					//Console.WriteLine("Element " + nodeValue + " został dodany na początku listy.");

					NodeGD MiddleToHead = CurrentNode;
					GoToStart(ref MiddleToHead);

					newNode.Previous = null;
					newNode.Next = MiddleToHead;
					MiddleToHead.Previous = newNode;
					newNode.Value = nodeValue;
					CurrentNode = newNode;
				}

				else if (position == Count) {           // ELEMENT NA KONCU

					//Console.WriteLine("Element " + nodeValue + " został dodany na końcu listy.");

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

					//Console.WriteLine("Element " + nodeValue + " został dodany w środku listy.");

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
				//Console.WriteLine("Lista nie istnieje. Element " + nodeValue + " został dodany na początku listy.");

				newNode.Previous = null;
				newNode.Next = null;
				newNode.Value = nodeValue;
				CurrentNode = newNode;
			}
			Count++;

			//PrintListGD(ref CurrentNode, Count);				// aby umożliwić wypisanie zawartości listy po dodaniu elementu
		}

		//================DeleteNodeGD

		private void DeleteNodeGD(int nodeValue) {

			//bool nodeFound = false;								// true, jeśli szukany element znajduje sie na liscie

			NodeGD NodeToDelete = CurrentNode;

			GoToStart(ref NodeToDelete);						// ustawia sie na pierwszym elemencie listy

			while (NodeToDelete != null) {                      // sprawdzaj dopoki aktualnie sprawdzany node nie jest nullem

				if (NodeToDelete.Value == nodeValue) {          // jesli wartosc sprawdzanego noda jest rowna szukanej wartosci

					if (NodeToDelete.Previous == null) {         // jesli znaleziono wartosc na poczatku listy
						//Console.WriteLine("Element " + nodeValue + " znajdował sie na początku listy i został usunięty.");

						NodeToDelete.Next.Previous = null;
						CurrentNode = NodeToDelete.Next;
					}

					else if (NodeToDelete.Next == null) {       // jesli znaleziono wartosc na koncu
						//Console.WriteLine("Element " + nodeValue + " znajdował sie na końcu listy i został usunięty.");

						NodeToDelete.Previous.Next = null;
						CurrentNode = NodeToDelete.Previous;
					}

					else {
						//Console.WriteLine("Element " + nodeValue + " znajdował sie w środku listy i został usunięty.");

						NodeToDelete.Previous.Next = NodeToDelete.Next;
						NodeToDelete.Next.Previous = NodeToDelete.Previous;
						CurrentNode = NodeToDelete.Next;
					}

					//nodeFound = true;                           // element wyznaczony do usuniecia znajduje sie na liście
					Count--;
				}

				NodeToDelete = NodeToDelete.Next;               // przejdz na nastepny nod
			}

			//if (!nodeFound)										// jesli elementu wyznaczonego do usuniecia nie ma na liscie
				//Console.WriteLine("Element " + nodeValue + " nie jest elementem listy i nie może zostać usunięty");

			//PrintListGD(ref CurrentNode, Count);				// aby umożliwić wypisanie zawartości listy po usunięciu elementu
		}

		//================PrintListGD

		private void PrintListGD(ref NodeGD node, int count) {

			NodeGD PrintNode = node;

			GoToStart(ref PrintNode);

			Console.Write("List: ");
			while (PrintNode != null) {
				Console.Write(PrintNode.Value + " ");
				PrintNode = PrintNode.Next;
			}
			Console.WriteLine();
		}

		//================GoToStart

		private void GoToStart(ref NodeGD item) {

			NodeGD ItemPosition = item;
			NodeGD temp = ItemPosition;
			while (ItemPosition.Previous != null) {
				temp = ItemPosition;                            // do zmiennej tymczasowej leci aktualna pozycja HEAD
				ItemPosition = ItemPosition.Previous;           // do aktualnej pozycji HEAD leci poprzednia pozycja HEAD
				ItemPosition.Next = temp;
			}
			item = ItemPosition;
		}

		//================ExecuteListGD

		public void ExecuteListGD(ParserGD parser) {

			string[,] SubDataToParse = parser.GetSubDataEntries();
			for (int i = 0; i < SubDataToParse.GetLength(0); i++) {
				if (SubDataToParse[i, 0] == "i")
					AddNodeGD(Int32.Parse(SubDataToParse[i, 1]), Int32.Parse(SubDataToParse[i, 2]));
				if (SubDataToParse[i, 0] == "d")
					DeleteNodeGD(Int32.Parse(SubDataToParse[i, 1]));
			}

			PrintListGD(ref CurrentNode, Count);
		}
	}
}
