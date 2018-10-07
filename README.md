# ZebMed

1) Use App.config To determine the data path:
	- Under key "dataPath"
2) External packeges should be installed automatically, if NOT:
	- Use NuGet packeges manager to install:
		- MongoDB.Bson
		- Newtonsoft.Json
3) For using this service as a DLL:
	- Compile the project
	- Add it as a refernce
	- Create Object of API class
		- e.g. API api = new API("b2", "a", "WEB"); (studyId, dataSource, method)
4) Assumptations regarding the Exc. are attached in "Assumptations.txt"
5) Use code example can be found in Program.cs - Main