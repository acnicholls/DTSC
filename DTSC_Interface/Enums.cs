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
        /// <summary>
        /// outputs only the table structure 
        /// </summary>
		TableStructure,
        /// <summary>
        /// outputs only the values, or rows of the datatable
        /// </summary>
		ValuesOnly,
        /// <summary>
        /// outputs the table structure and all data rows
        /// </summary>
		TableAndValues,
        /// <summary>
        /// outputs structure for all database tables
        /// </summary>
		DatabaseTableStructure,
        /// <summary>
        /// outputs structure for all tables and all data for those tables
        /// </summary>
		StructureAndValues,
        /// <summary>
        /// NOT IMPLEMENTED (v1.5) - outputs only stored procedures
        /// </summary>
		AllStoredProcedures,
        /// <summary>
        /// NOT IMPLEMENTED (v1.5) - outputs all table structure and all stored procedures
        /// </summary>
		StructureProcedure,
        /// <summary>
        /// NOT IMPLEMENTED (v1.5) - outputs all table structure, all stored procedures and all data
        /// </summary>
		StructProcValues
    }

    /// <summary>
    /// this enum defines the output style of the script
    /// later versions will include PL/SQL output for Oracle
    /// </summary>
    public enum ScriptOutputType
    {
        /// <summary>
        /// outputs the script MSSQL style with square brackets
        /// </summary>
        MsSQL,
        /// <summary>
        /// outputs the script for MySQL with backticks
        /// </summary>
        MySQL
    }
}
