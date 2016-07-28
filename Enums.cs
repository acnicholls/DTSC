using System;

namespace DBTableMover
{
	public enum currentConnectionType
	{

		MSSQL,
		XML
	}

	public enum ScriptType
	{
		TableStructure,
		ValuesOnly,
		TableAndValues,
		DatabaseTableStructure,
		StructureAndValues,
		AllStoredProcedures,
		StructureProcedure,
		StructProcValues
	}
}
