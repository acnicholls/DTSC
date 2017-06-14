namespace DBTableMover
{
    /// <summary>
    /// type of data connection currently being used by the program
    /// </summary>
	public enum currentConnectionType
	{

		MSSQL,
		XML
	}

    /// <summary>
    /// types of scripts that can be created
    /// </summary>
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
