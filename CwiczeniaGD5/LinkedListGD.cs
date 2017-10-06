using System;

namespace CwiczeniaGD5 {

	class LinkedListGD {

		public NodeGD CurrentNode;						
		public NodeGD Head;						
		public NodeGD Tail;						
		public int Count { get; private set; }

		public LinkedListGD() {

			CurrentNode = null;
		}

		public class NodeGD {											// klasa zagnieżdżona NodeGD, bo węzeł nie ma sensu istnienia poza listą

			public NodeGD Previous { get; set; }
			public NodeGD Next { get; set; }
			public int Value { get; set; }
		}

		//================AddNodeGD - DODAJ WĘZEŁ

		private void AddNodeGD(int nodeValue, int position) {			

			NodeGD newNode = new NodeGD();

			if (Count != 0) {											// jeśli lista istnieje 

				if (position == 0) {									// DODAJ ELEMENT NA POCZATKU

					//Console.WriteLine("Element " + nodeValue + " został dodany na początku listy.");

					//NodeGD MiddleToHead = CurrentNode;
					//GoToStart(ref MiddleToHead);						// znajdź początek listy

					//newNode.Previous = null;
					//newNode.Next = MiddleToHead;
					//MiddleToHead.Previous = newNode;
					//newNode.Value = nodeValue;
					//CurrentNode = newNode;

					newNode.Value = nodeValue;
					newNode.Previous = null;
					newNode.Next = Head;
					Head.Previous = newNode;
					Head = newNode;
				}

				else if (position == Count) {							// DODAJ ELEMENT NA KONCU

					//Console.WriteLine("Element " + nodeValue + " został dodany na końcu listy.");

					//NodeGD MiddleToEnd = CurrentNode;
					//GoToStart(ref MiddleToEnd);							// znajdź początek listy

					//while (MiddleToEnd.Next != null) {                  // przesuwa na ostatni element listy
					//	MiddleToEnd = MiddleToEnd.Next;
					//}

					//newNode.Previous = MiddleToEnd;
					//newNode.Next = null;
					//newNode.Value = nodeValue;
					//CurrentNode = newNode;

					newNode.Value = nodeValue;
					newNode.Next = null;
					newNode.Previous = Tail;
					Tail.Next = newNode;
					Tail = newNode;
				}

				else {                                                  // DODAJ ELEMENT W SRODKU

					//Console.WriteLine("Element " + nodeValue + " został dodany w środku listy.");

					//NodeGD HeadToMiddle = CurrentNode;
					//GoToStart(ref HeadToMiddle);						// znajdź początek listy

					//for (int i = 0; i < position; i++) {                // przesuwa na wskazana pozycje
					//	HeadToMiddle = HeadToMiddle.Next;
					//}

					//newNode.Previous = HeadToMiddle.Previous;
					//newNode.Next = HeadToMiddle;
					//HeadToMiddle.Previous = newNode;
					//newNode.Value = nodeValue;
					//CurrentNode = newNode;

					NodeGD temp = Head;
					for (int i = 0; i < position; i++) {                // przesuwa na wskazana pozycje
						temp = temp.Next;
					}

					Console.WriteLine("head value : " + temp.Value + " // insert value " + nodeValue + " // insert position " + position);

					newNode.Value = nodeValue;
					newNode.Previous = temp.Previous;
					newNode.Next = temp;
					temp.Previous = newNode;
					temp.Previous.Next = newNode;

				}

			}
			else {
				//Console.WriteLine("Lista nie istnieje. Element " + nodeValue + " został dodany na początku listy.");

				newNode.Previous = null;
				newNode.Next = null;
				newNode.Value = nodeValue;
				CurrentNode = newNode;

				Head = newNode;
				Tail = newNode;
				Head.Previous = Head.Next = Tail.Previous = Tail.Next = null;
			}
			Count++;

			PrintListGD(ref CurrentNode);                // aby umożliwić wypisanie zawartości listy po dodaniu elementu (jeśli odkomentowane - zakomentować wywołanie metody PrintListGD w metodzie ExecuteListGD)
		}

		//================DeleteNodeGD - USUŃ WĘZEŁ

		private void DeleteNodeGD(int nodeValue) {

			bool nodeFound = false;									// true, jeśli szukany element znajduje sie na liscie

			NodeGD NodeToDelete = CurrentNode;
			GoToStart(ref NodeToDelete);							// znajdź początek listy

			while (NodeToDelete != null) {							// sprawdzaj dopoki aktualnie sprawdzany węzeł nie jest nullem

				if (NodeToDelete.Value == nodeValue) {				// jesli wartosc sprawdzanego noda jest rowna szukanej wartosci

					if (NodeToDelete.Previous == null) {			// jesli znaleziono wartość na początku listy
						//Console.WriteLine("Element " + nodeValue + " znajdował sie na początku listy i został usunięty.");

						NodeToDelete.Next.Previous = null;
						CurrentNode = NodeToDelete.Next;
					}

					else if (NodeToDelete.Next == null) {			// jesli znaleziono wartość na koncu
						//Console.WriteLine("Element " + nodeValue + " znajdował sie na końcu listy i został usunięty.");

						NodeToDelete.Previous.Next = null;
						CurrentNode = NodeToDelete.Previous;
					}

					else {											// jeśli znaleziono wartość w środku listy
						//Console.WriteLine("Element " + nodeValue + " znajdował sie w środku listy i został usunięty.");

						NodeToDelete.Previous.Next = NodeToDelete.Next;
						NodeToDelete.Next.Previous = NodeToDelete.Previous;
						CurrentNode = NodeToDelete.Next;
					}

					nodeFound = true;								// element wyznaczony do usuniecia znajduje sie na liście
					Count--;
				}

				NodeToDelete = NodeToDelete.Next;					// przejdz na nastepny węzeł
			}

			if (!nodeFound)											// jesli elementu wyznaczonego do usuniecia nie ma na liscie
				Console.WriteLine("Element " + nodeValue + " nie jest elementem listy i nie może zostać usunięty");

            //PrintListGD(ref CurrentNode, Count);                    // aby umożliwić wypisanie zawartości listy po usunięciu elementu (jeśli odkomentowane - zakomentować wywołanie metody PrintListGD w metodzie ExecuteListGD)
        }

		//================PrintListGD - WYPISZ LISTĘ

		private void PrintListGD(ref NodeGD node) {

			//NodeGD PrintNode = node;
			//GoToStart(ref PrintNode);							// przejdź na początek listy

			//Console.Write("Lista OLD WAY: ");
			//while (PrintNode != null) {							// dopóki nie zostanie osiągnięty koniec listy
			//	Console.Write(PrintNode.Value + " ");			// wypisz element listy
			//	PrintNode = PrintNode.Next;						// i przejdź na następny
			//}
			//Console.WriteLine();

			Console.Write("Lista od HEAD: ");
			NodeGD temp1 = Head;
			while (temp1 != null) {                         // dopóki nie zostanie osiągnięty koniec listy
				Console.Write(temp1.Value + " ");           // wypisz element listy
				temp1 = temp1.Next;                     // i przejdź na następny
			}
			Console.WriteLine();

			Console.Write("Lista od TAIL: ");
			NodeGD temp2 = Tail;
			while (temp2 != null) {                         // dopóki nie zostanie osiągnięty koniec listy
				Console.Write(temp2.Value + " ");           // wypisz element listy
				temp2 = temp2.Previous;                     // i przejdź na następny
			}
			Console.WriteLine();
			Console.WriteLine();

		}

		//================GoToStart - PRZEJDŹ DO POCZĄTKU LISTY

		private void GoToStart(ref NodeGD item) {

			NodeGD ItemPosition = item;
			NodeGD temp = ItemPosition;
			while (ItemPosition.Previous != null) {				// dopóki nie zostanie osiągnięty początek listy
				temp = ItemPosition;                            // do zmiennej tymczasowej leci aktualna pozycja HEAD
				ItemPosition = ItemPosition.Previous;           // do aktualnej pozycji HEAD leci poprzednia pozycja HEAD
				ItemPosition.Next = temp;
			}
			item = ItemPosition;
		}

		//================ExecuteListGD	- WYKONAJ OPERACJE NA DANYCH Z PAMIĘCI, STWORZ LISTĘ I WYPISZ JĄ NA EKRAN

		public void ExecuteListGD(string[,] SubDataToParse) {							// jako argument dostanie sparsowaną tablicę 2D elementów odczytanych z pliku

			for (int i = 0; i < SubDataToParse.GetLength(0); i++) {			
				if (SubDataToParse[i, 0] == "i")															// jeśli element ma zostać dodany do listy
					AddNodeGD(Int32.Parse(SubDataToParse[i, 1]), Int32.Parse(SubDataToParse[i, 2]));		// dodaj go do listy wg danych z tablicy dwuwymiarowej
				if (SubDataToParse[i, 0] == "d")															// jeśli element ma zostać z listy usunięty
					DeleteNodeGD(Int32.Parse(SubDataToParse[i, 1]));										// usuń go z listy wg danych z tablicy dwuwymiarowej
			}

            //PrintListGD(ref CurrentNode);                            // wypisz zawartość tablicy na ekran
            Console.ReadLine();
		}
	}
}
