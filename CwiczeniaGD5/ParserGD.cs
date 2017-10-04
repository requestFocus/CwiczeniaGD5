using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace CwiczeniaGD5
{

	class ParserGD {

		public string Path { get; private set; }  = "data.txt";
		public ArrayList ListToParse { get; private set; }
		public string[,] SubData { get; private set; }
		public int EntryCount { get; private set; }

		public ParserGD() {
			this.ListToParse = OpenFileSaveRawData(Path);
			ParseData(ListToParse);
		}

		//================OpenFileSaveRawData(string Path)

		private ArrayList OpenFileSaveRawData(string Path) {            

			ArrayList RawData = new ArrayList();								// ArrayList do przechowywania surowych danych z pliku
			string fileEntry;													// pojedynczy wpis z pliku

			try {
				using (FileStream fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read)) {				// otwarcie FileStreama
					StreamReader streamReader = new StreamReader(fileStream);										// otwarcie StreamReadera
					while ((fileEntry = streamReader.ReadLine()) != "P") {											// dopóki nie osiągnie znaku końca pliku (P)
						RawData.Add(fileEntry);																		// dodaje do ArrayList kolejne linie pliku
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.Message + " Umieść plik we właściwym katalogu");
			}

			return RawData;
		}

		//================ParseData(ArrayList listToParse)

		private void ParseData(ArrayList listToParse) {										// ArrayList jest skończona i nie będzie powiększana po uruchomieniu ParseData()

			EntryCount = 0;                                                                 // licznik linii dla ArrayList powstałej po odczytaniu pliku 
			foreach (string lineEntry in listToParse) {										
				EntryCount++;																// liczy ilość rekordów w ArrayList
			}
			SubData = new string[EntryCount, 3];                                            // tablica dwuwymiarowa do przechowywania poszczególnych fragmentów rekordów z ArrayList
			EntryCount = 0;

			Regex regex_ins = new Regex(@"i_[0-9]+_[0-9]+", RegexOptions.IgnoreCase);		// regex dla insert
			Regex regex_del = new Regex(@"d_[0-9]+", RegexOptions.IgnoreCase);				// regex dla delete
			foreach (string LineEntry in listToParse) {										// dla każdego wpisu w ArrayList

				Match match_ins = regex_ins.Match(LineEntry);								// bool dla dopasowania stringa wzgledem match_ins
				Match match_del = regex_del.Match(LineEntry);								// bool dla dopasowania stringa wzgledem match_del

				if (match_ins.Success || match_del.Success) {								// jeśli jedno lub drugie dopasowanie jest prawdziwe
					string[] SubDataEntry = LineEntry.Split('_');							// do subFileEntry wpisywana jest tylko jedna linia na raz
					SubData[EntryCount, 0] = SubDataEntry[0];								// wstaw string z SubDataEntry[0] do Subdata[i][0]
					SubData[EntryCount, 1] = SubDataEntry[1];								// wstaw string z SubDataEntry[0] do Subdata[i][0]
					if (SubDataEntry[0] == "i") {											// tylko jeśli pierwszy element Entry[] jest rowny i
						SubData[EntryCount, 2] = SubDataEntry[2];							// wstaw string z SubDataEntry[2] do Subdata[i][2]
					}
					EntryCount++;															// przejście na kolejny indeks tablicy SubData[][]	
				}
				else
					Console.WriteLine("Wpis \"" + LineEntry + "\" posiada niewłaściwy format. Prawidłowy format to i_X_X lub d_X");
			}
		}

		public string[,] GetSubDataEntries() {
			return SubData;
		}
	}
}
