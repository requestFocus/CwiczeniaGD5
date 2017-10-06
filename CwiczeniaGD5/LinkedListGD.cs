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

				if (position == 0) {                                    // DODAJ ELEMENT NA POCZATKU

					newNode.Previous = null;
					newNode.Next = Head;
					Head.Previous = newNode;
					newNode.Value = nodeValue;
					Head = newNode;

				}

				else if (position == Count) {                           // DODAJ ELEMENT NA KONCU

					newNode.Previous = Tail;
					newNode.Next = null;
					Tail.Next = newNode;
					newNode.Value = nodeValue;
					Tail = newNode;
				}

				else {                                                  // DODAJ ELEMENT W SRODKU

					NodeGD temp = Head;
					for (int i = 0; i < position; i++) {                // przesuwa na wskazana pozycje
						temp = temp.Next;
					}

					newNode.Value = nodeValue;
					newNode.Next = temp;
					newNode.Previous = temp.Previous;
					temp.Previous.Next = newNode;
					temp.Previous = newNode;
				}

			}
			else {
				//Console.WriteLine("Lista nie istnieje. Element " + nodeValue + " został dodany na początku listy.");

				newNode.Previous = null;
				newNode.Next = null;
				newNode.Value = nodeValue;

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

			NodeGD temp = Head;

			while (temp != null) {							// sprawdzaj dopoki aktualnie sprawdzany węzeł nie jest nullem

				if (temp.Value == nodeValue) {				// jesli wartosc sprawdzanego noda jest rowna szukanej wartosci

					if (temp.Previous == null) {            // jesli znaleziono wartość na początku listy
						
						//Console.WriteLine("Element " + nodeValue + " znajdował sie na początku listy i został usunięty.");
						temp.Next.Previous = null;
						Head = temp.Next;
					}

					else if (temp.Next == null) {           // jesli znaleziono wartość na koncu
						
						//Console.WriteLine("Element " + nodeValue + " znajdował sie na końcu listy i został usunięty.");
						temp.Previous.Next = null;
						Tail = temp.Previous;
					}

					else {                                // jeśli znaleziono wartość w środku listy
						
						//Console.WriteLine("Element " + nodeValue + " znajdował sie w środku listy i został usunięty.");
						temp.Previous.Next = temp.Next;
						temp.Next.Previous = temp.Previous;
						//CurrentNode = temp.Next;
					}

					nodeFound = true;						// element wyznaczony do usuniecia znajduje sie na liście
					Count--;
				}

				temp = temp.Next;							// przejdz na nastepny węzeł
			}

			if (!nodeFound)									// jesli elementu wyznaczonego do usuniecia nie ma na liscie
				Console.WriteLine("Element " + nodeValue + " nie jest elementem listy i nie może zostać usunięty");

            //PrintListGD(ref CurrentNode, Count);                    // aby umożliwić wypisanie zawartości listy po usunięciu elementu (jeśli odkomentowane - zakomentować wywołanie metody PrintListGD w metodzie ExecuteListGD)
        }

		//================PrintListGD - WYPISZ LISTĘ

		private void PrintListGD(ref NodeGD node) {

			Console.Write("Lista od HEAD: ");
			NodeGD temp1 = Head;
			while (temp1 != null) {                         // dopóki nie zostanie osiągnięty koniec listy
				Console.Write(temp1.Value + " ");           // wypisz element listy
				temp1 = temp1.Next;                     // i przejdź na następny
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
