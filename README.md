# ZebMed

1) Use App.config To determine the data path:
	- Under key "dataPath"
2) External packeges should be installed automatically, if NOT:
	- Use NuGet packeges manager to install:
		- MongoDB.Bson
		- Newtonsoft.Json
4) For using this service as a DLL:
	- Compile the project
	- Add it as a refernce
	- Create Object of API class
		- e.g. API api = new API("b2", "a", "WEB"); (studyId, dataSource, method)
3) Assumptations regarding the Exc. are attached in "Assumptations.txt"
