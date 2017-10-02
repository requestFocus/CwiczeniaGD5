using System;

namespace CwiczeniaGD5 {

	class LinkedListGD {

		public NodeGD CurrentNode;
		public int Count;

		public LinkedListGD() {

			CurrentNode = null;
		}

		public class NodeGD {

			public NodeGD Previous;
			public NodeGD Next;
			public int Value;
		}

		//================AddNodeGD

		private void AddNodeGD(int nodeValue, int position) {

			NodeGD newNode = new NodeGD();

			if (Count != 0) {											// lista istnieje 

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

			//PrintListGD(ref CurrentNode, Count);				// aby umożliwić wypisanie wszystkich kroków
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

		//================DeleteNodeGD

		private void DeleteNodeGD(int nodeValue) {

			//bool nodeFound = false;							// jeśli istotne jest sprawdzenie czy element do usunięcia występował na liście

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

					//nodeFound = true;							// jeśli istotne jest sprawdzenie czy element do usunięcia występował na liście
					Count--;
				}

				NodeToDelete = NodeToDelete.Next;               // przejdz na nastepny nod
			}

			//if (!nodeFound)									// jeśli istotne jest sprawdzenie czy element do usunięcia występował na liście
			//	Console.WriteLine("     Elementu do usunięcia nie ma na liście");

			//PrintListGD(ref CurrentNode, Count);				// aby umożliwić wypisanie wszystkich kroków
		}

		//================PrintListGD

		private void PrintListGD(ref NodeGD node, int count) {

			NodeGD PrintNode = node;
			NodeGD temp = PrintNode;
			while (PrintNode.Previous != null) {
				temp = PrintNode;                             // do zmiennej tymczasowej leci aktualna pozycja PrintNode
				PrintNode = PrintNode.Previous;               // do aktualnej pozycji PrintNode leci poprzednia pozycja PrintNode
				PrintNode.Next = temp;                        // do następnej pozycji PrintNode leci zmienna tymczasowa
			}

			Console.Write("List: ");
			while (PrintNode != null) {
				Console.Write(PrintNode.Value + " ");
				PrintNode = PrintNode.Next;
			}
			Console.WriteLine();
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
