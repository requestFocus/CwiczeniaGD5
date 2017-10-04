﻿using System;
using System.Collections;
using System.IO;

namespace CwiczeniaGD5
{

	class ParserGD {

		public string Path { get; private set; }  = "data.txt";
		public ArrayList ListToParse { get; private set; }
		public string[,] SubData { get; private set; }

		public ParserGD() {
			this.ListToParse = OpenFileSaveRawData(Path);
			ParseData(ListToParse);
		}

		private ArrayList OpenFileSaveRawData(string Path) {            // SPRAWDZ CZY DANE SA POPRAWNE!

			ArrayList RawData = new ArrayList();
			string fileEntry;

			try {
				using (FileStream fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read)) {
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

		private string[,] ParseData(ArrayList listToParse) {             // ArrayList jest skończona i nie będzie powiększana po uruchomieniu ParseData()

			int EntryCount = 0;                                         // licznik linii dla ArrayList
			foreach (string lineEntry in listToParse) {                 // dla każdej linii
				EntryCount++;                                           // zwiększa entryCount
			}
			SubData = new string[EntryCount, 3];                            // deklaracja i zainicjalizowanie tablicy dwuwymiarowej SubData[,] wg entryCount
			EntryCount = 0;                                             // wyzerowanie entryCount na potrzeby indeksowania SubData[][]

			foreach (string LineEntry in listToParse) {                 // dla każdego wpisu w ArrayList
				//if (LineEntry.Substring(0, 1) != "i") {				// ZALATW TO WYRAZENIEM REGULARNYM i podkreslenie JAKASLICZBA podkreslenie JAKASLICZBA lub d podkreslenie JAKASLICZBA
				//	if (LineEntry.Substring(0, 1) != "d") {
				//		Console.WriteLine("Lista zawiera niedozwolony wpis. Wpis został usunięty.");
				//		continue;
				//	}
				//	continue;
				//}

				string[] SubDataEntry = LineEntry.Split('_');           // do subFileEntry wpisywana jest tylko jedna linia na raz
				SubData[EntryCount, 0] = SubDataEntry[0];               // wstaw string z SubDataEntry[0] do Subdata[i][0]
				SubData[EntryCount, 1] = SubDataEntry[1];               // wstaw string z SubDataEntry[0] do Subdata[i][0]
				if (SubDataEntry[0] == "i") {                           // tylko jeśli pierwszy element Entry[] jest rowny i
					SubData[EntryCount, 2] = SubDataEntry[2];           // wstaw string z SubDataEntry[2] do Subdata[i][2]
				}
				EntryCount++;                                           // przejście na kolejny indeks tablicy SubData[][]
			}
			return SubData;
		}

		public string[,] GetSubDataEntries() {
			return SubData;
		}
	}


}