namespace DBTableMover
{
    /// <summary>
    /// type of data connection currently being used by the program
    /// </summary>
	public enum currentConnectionType
	{
        /// <summary>
        /// used to denote that the interface is connected to a MSSQL database
        /// </summary>
		MSSQL,
        /// <summary>
        /// used to denote that the interface is connected to a MySQL database
        /// </summary>
        MySQL,
        /// <summary>
        /// used to denote that the interface is connected to an XML file
        /// </summary>
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
